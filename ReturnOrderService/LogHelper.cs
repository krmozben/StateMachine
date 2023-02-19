using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReturnOrderService
{
    public static class LogHelper
    {
        public static JsonSerializerOptions JsonSerializerOptions { get; set; }

        static LogHelper()
        {
            JsonSerializerOptions = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
