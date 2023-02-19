using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using MongoDB.Driver;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using ReturnOrderService.Configuration;
using ReturnOrderService.Consumers;
using ReturnOrderService.Consumers.Boyner;
using ReturnOrderService.Consumers.MarketPlace;
using ReturnOrderService.Data;
using ReturnOrderService.Data.Interfaces;
using ReturnOrderService.Services.Interfaces;
using ReturnOrderService.StateMachine.Boyner;
using ReturnOrderService.StateMachine.Boyner.Status;
using ReturnOrderService.StateMachine.Mp;
using ReturnOrderService.StateMachine.Mp.Status;
using System.Reflection;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var config = new ConfigurationBuilder()
        .SetBasePath(System.IO.Directory
            .GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", true, true)
        .Build();

    CreateHostBuilder(args, config).Build().Run();
}
catch (Exception ex)
{
    // NLog: catch any exception and log it.
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}


static IHostBuilder CreateHostBuilder(string[] args, IConfiguration config) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureLogging(logging =>
               {
                   logging.ClearProviders();
                   logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
               }).UseNLog()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddLogging(loggingBuilder =>
                   {
                       // configure Logging with NLog
                       loggingBuilder.ClearProviders();
                       loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                       loggingBuilder.AddNLog(config);
                   });

                   // Retry policy
                   AsyncRetryPolicy asyncRetryPolicy = Policy.Handle<Exception>()
                      .WaitAndRetryAsync(3, duration => TimeSpan.FromSeconds(10));
                   services.AddSingleton(asyncRetryPolicy);

                   // HttpClient Retry Policy for HttpRequestException, 5XX (server errors) and 408 (request timeout)
                   var httpRetryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                      .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(10));

                   // Mongo
                   var mongoClientSettings = MongoClientSettings.FromConnectionString(config.GetConnectionString("MongoDB"));
                   mongoClientSettings.ReadConcern = ReadConcern.Majority;
                   mongoClientSettings.ReadPreference = ReadPreference.SecondaryPreferred;
                   mongoClientSettings.WriteConcern = WriteConcern.WMajority;
                   services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));
                   services.AddSingleton<IMongoDbDataContext, MongoDbDataContext>();

                   // HttpClientFactory
                   services.AddHttpClient<IHttpClientDataContext, HttpClientDataContext>(c =>
                  {
                      c.DefaultRequestHeaders.Add("Accept", "application/json");
                      c.DefaultRequestHeaders.Connection.Add("Keep-Alive");
                  }).AddPolicyHandler(httpRetryPolicy);

                   // Services
                   services.AddScoped<IReturnOrderService, ReturnOrderService.Services.ReturnOrderService>();
                   services.AddScoped<IChannelService, ReturnOrderService.Services.ChannelService>();

                   // State Machine
                   ///Boyner
                   services.AddScoped<BoynerReturnOrderStateMachine>();
                   services.AddScoped<BoynerInitial>();
                   services.AddScoped<BoynerGksSuccess>();
                   services.AddScoped<BoynerGksFail>();
                   services.AddScoped<BoynerGksBadRequest>();
                   services.AddScoped<BoynerEkolNotificationSuccess>();
                   services.AddScoped<BoynerEkolNotificationFail>();
                   services.AddScoped<BoynerFinalized>();
                   services.AddScoped<BoynerEkolNotificationSentQueueSuccess>();
                   ///Mp   
                   services.AddScoped<MpReturnOrderStateMachine>();
                   services.AddScoped<MpEkolNotificationFail>();
                   services.AddScoped<MpEkolNotificationSentQueueSuccess>();
                   services.AddScoped<MpEkolNotificationSuccess>();
                   services.AddScoped<MpFinalized>();
                   services.AddScoped<MpInitial>();
                   services.AddScoped<MpPoFail>();
                   services.AddScoped<MpPoSuccess>();
                   services.AddScoped<MpSendComengFail>();
                   services.AddScoped<MpSendComengSuccess>();
                   services.AddScoped<MpSendEntegratorFail>();
                   services.AddScoped<MpSendEntegratorSuccess>();

                   // RabbitMq Configuration & Consumer - Queue Mapping
                   var rabbitMqConfiguration = config.GetSection("RabbitMq").Get<RabbitMqConfiguration>();
                   services.AddMassTransit(x =>
                   {
                       x.AddConsumers(Assembly.GetExecutingAssembly());
                       x.AddDelayedMessageScheduler();

                       x.UsingRabbitMq((context, cfg) =>
                       {
                           cfg.Host(rabbitMqConfiguration.Uri, h =>
                           {
                               h.Username(rabbitMqConfiguration.Username);
                               h.Password(rabbitMqConfiguration.Password);
                           });

                           cfg.PrefetchCount = rabbitMqConfiguration.PrefetchCount;
                           cfg.UseRateLimit(rabbitMqConfiguration.RateLimiter.Limit);
                           cfg.UseConcurrencyLimit(rabbitMqConfiguration.ConcurrentConsumerLimit);
                           cfg.UseCircuitBreaker(cb =>
                           {
                               cb.TripThreshold = rabbitMqConfiguration.CircuitBreaker.TripThreshold;
                               cb.ActiveThreshold = rabbitMqConfiguration.CircuitBreaker.TripThreshold;
                               cb.ResetInterval = TimeSpan.FromSeconds(rabbitMqConfiguration.CircuitBreaker.ResetInterval);
                           });
                           cfg.UseRetry(retryConfig =>
                           {
                               retryConfig.Incremental(
                                   rabbitMqConfiguration.IncrementalRetryPolicy.RetryLimit,
                                   TimeSpan.FromSeconds(rabbitMqConfiguration.IncrementalRetryPolicy.InitialIntervalTime),
                                   TimeSpan.FromSeconds(rabbitMqConfiguration.IncrementalRetryPolicy.IntervalIncrementTime));
                           });
                           cfg.UseDelayedMessageScheduler();

                           cfg.ReceiveEndpoint(rabbitMqConfiguration.Queues.ReturnOrderMpQueue, e =>
                           {
                               e.ConfigureConsumer<MpReturnOrderConsumer>(context);
                           });

                           cfg.ReceiveEndpoint(rabbitMqConfiguration.Queues.ReturnOrderBoynerQueue, e =>
                           {
                               e.ConfigureConsumer<BoynerReturnOrderConsumer>(context);
                           });

                           cfg.ReceiveEndpoint(rabbitMqConfiguration.Queues.ReceiveSuccessEkolQueue, e =>
                           {
                               e.ConfigureConsumer<ReceiveSuccessConsumer>(context);
                           });
                       });
                   });

                   services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
               });