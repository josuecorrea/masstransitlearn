using MassTransit;
using Newtonsoft.Json;

namespace ProducerService.Consumer
{
    public class MessageConsumer : IConsumer<Message>
    {
        private readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Message> context)
        {
            var msgJson = JsonConvert.SerializeObject(context.Message);

            await Console.Out.WriteLineAsync(msgJson);

            _logger.LogInformation($"Message -> {msgJson}");
        }
    }
}
