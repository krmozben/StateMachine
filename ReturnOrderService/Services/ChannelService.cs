using Boyner.Invoice;
using Boyner.ReturnSave;
using Polly.Retry;
using ReturnOrderService.Data.Interfaces;
using ReturnOrderService.Helper;
using ReturnOrderService.Models.Requests;
using ReturnOrderService.Models.Responses;
using ReturnOrderService.Services.Interfaces;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.Json;

namespace ReturnOrderService.Services
{
    public class ChannelService : IChannelService
    {
        private readonly ILogger<ChannelService> _logger;
        private readonly IConfiguration _configuration;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly IHttpClientDataContext _httpClientDataContext;

        public ChannelService(ILogger<ChannelService> logger, IConfiguration configuration, AsyncRetryPolicy retryPolicy, IHttpClientDataContext httpClientDataContext)
        {
            _logger = logger;
            _configuration = configuration;
            _retryPolicy = retryPolicy;
            _httpClientDataContext = httpClientDataContext;
        }

        public async Task<bool> ReceiveSuccessEkolRequestHandler(ReceiveSuccessEkolRequestModel request)
        {
            BasicHttpBinding binding = new BasicHttpBinding() { };
            EndpointAddress address =
         new EndpointAddress($"{_configuration.GetSection("POClient")["BaseAddress"]}XISOAPAdapter/MessageServlet?senderParty=BBM&senderService=BC_3RD_BBM_ECOM&receiverParty=&receiverService=&interface=SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYN&interfaceNamespace=http%3A%2F%2Fboynerortak.com%2FMASTERDATA%2FEKOL&responsecode202=true");
            var client = new SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNClient(binding, address);
            try
            {
                using (var scope = new OperationContextScope(client.InnerChannel))
                {
                    HttpRequestMessageProperty property = new HttpRequestMessageProperty();
                    property.Headers[HttpRequestHeader.Authorization] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ _configuration.GetSection("POClient")["Username"]}:{ _configuration.GetSection("POClient")["Password"]}"));
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = property;
                    var getReturnOrderReviced = client.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNAsync(
                        new DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ()
                        {
                            WSC_GOODS_IN_PHY_RECEIVEDInput = new DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInput()
                            {
                                I_HEADER_WSC_STATUS_W_CIN = new DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CIN()
                                {
                                    WSC_STATUS_W = new DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CINWSC_STATUS_W()
                                    {
                                        RECORDS = new DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CINWSC_STATUS_WWSC_STATUS_TYPE[] {
                                            new DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CINWSC_STATUS_WWSC_STATUS_TYPE() {
                                                ORG_CODE = request.RequestEvent.OrgCode, RECORD_NO = request.RequestEvent.RecordNo}
                                        }
                                    }
                                },
                                O_RESULT_WSC_RESULT_W_COUT = ""
                            }
                        }).GetAwaiter().GetResult();
                }
                await client.CloseAsync();

                _logger.LogWithCorrelationId(LogLevel.Information, request.RequestEvent.CorrelationId, $"Return order Ekol is accepted:\r\n { JsonSerializer.Serialize(new { request.RequestEvent.OrgCode, request.RequestEvent.RecordNo }, LogHelper.JsonSerializerOptions)}");

            }
            catch (Exception ex)
            {
                _logger.LogWithCorrelationId(LogLevel.Error, request.RequestEvent.CorrelationId, ex, $"Return order Ekol is failed.", new { request.RequestEvent.OrgCode, request.RequestEvent.RecordNo, ex });

                client.Abort();
                return false;
            }
            return true;
        }

        public async Task<BaseResponse<HttpResponseMessage>> ReturnOrderSendGksRequestHandler(BoynerReturnOrderRequestModel request)
        {
            BaseResponse<HttpResponseMessage> response = new BaseResponse<HttpResponseMessage>();

            response.Data = await _httpClientDataContext.GksOrderReturnAsync("GksHttpClient", request.RequestEvent);

            return response;
        }

        public async Task<BaseResponse<bool>> SendReturnOrderInvoiceRequestHandler(MpReturnOrderRequestModel request)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            List<InvoiceDO> invoices = new List<InvoiceDO>() { (InvoiceDO)(request.RequestEvent.InvoiceDO) };
            BasicHttpBinding binding = new BasicHttpBinding() { };

            EndpointAddress address =
                new EndpointAddress(
                    $"{_configuration.GetSection("POClient")["BaseAddress"]}XISOAPAdapter/MessageServlet?senderParty=&senderService=BC_3RD_MARKETPLACE&receiverParty=&receiverService=&interface=SI_BPM_ORDER_GENIUS_CREATE_INVOICE_ABS_ASYN&interfaceNamespace=http://boynerortak.com/MASTERDATA/NGI/ORDERS&responsecode202=true");
            var client = new SI_BPM_ORDER_GENIUS_CREATE_INVOICE_ABS_ASYNClient(binding, address);
            try
            {
                _logger.LogWithCorrelationId(LogLevel.Information, request.RequestEvent.CorrelationId, $"CreateReturnInvoice SI_BPM_ORDER_GENIUS_CREATE_INVOICE_ABS_ASYNAsync Request : {JsonSerializer.Serialize(invoices, LogHelper.JsonSerializerOptions)}");

                using (var scope = new OperationContextScope(client.InnerChannel))
                {
                    HttpRequestMessageProperty property = new HttpRequestMessageProperty();
                    property.Headers[HttpRequestHeader.Authorization] = "Basic " +
                                                                        Convert.ToBase64String(
                                                                            Encoding.ASCII.GetBytes(
                                                                                $"{_configuration.GetSection("POClient")["Username"]}:{_configuration.GetSection("POClient")["Password"]}"));
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = property;
                    client.SI_BPM_ORDER_GENIUS_CREATE_INVOICE_ABS_ASYNAsync(invoices).GetAwaiter().GetResult();
                }

                client.CloseAsync();

                _logger.LogWithCorrelationId(LogLevel.Information, request.RequestEvent.CorrelationId, $"invoice could be created:\r\n {JsonSerializer.Serialize(invoices, LogHelper.JsonSerializerOptions)}");
            }
            catch (Exception ex)
            {
                _logger.LogWithCorrelationId(LogLevel.Error, request.RequestEvent.CorrelationId, ex, $"İade Faturası Oluşturulamadı", new { invoices });

                client.Abort();

                response.Errors.Add(ex.Message);
                response.Data = false;
                return response;
            }
            response.Data = true;
            return response;
        }
    }
}
