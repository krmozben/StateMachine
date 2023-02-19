namespace ReturnOrderService.Data.Interfaces
{
    public interface IHttpClientDataContext
    {
        public Task<HttpResponseMessage> GksOrderReturnAsync(string source, dynamic request);
    }
}
