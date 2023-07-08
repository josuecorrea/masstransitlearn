using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ProducerService.Controllers
{
    [ApiController]
    [Route("client/v1/message")]
    public class ClientController : ControllerBase
    {
        private readonly IBus _bus;

        public ClientController(IBus bus)
        {
            _bus = bus;    
        }

        [HttpPost]
        [Route("publish-message")]
        public async Task<IActionResult> PublisMessage([FromBody] Message message) 
        {
            if (!message.Id.HasValue)
            {
                message.Id = Guid.NewGuid();
            }

            Uri uri = new Uri("rabbitmq://localhost/clientMessageQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(message);

            return Ok();
        }
    }
}
