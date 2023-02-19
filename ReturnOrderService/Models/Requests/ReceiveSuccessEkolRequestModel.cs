using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MediatR;

namespace ReturnOrderService.Models.Requests
{
    public class ReceiveSuccessEkolRequestModel : IRequest<bool>
    {
        public ReceiveSuccessEkolRequestEvent RequestEvent { get; set; }
    }
}
