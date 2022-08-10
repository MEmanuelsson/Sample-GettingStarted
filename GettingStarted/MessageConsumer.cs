using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GettingStarted
{
    public class Message
    {
        public string Text { get; set; }
        public List<KeyValuePair<string, string>> Filters { get; set; }
    }

    public class MessageConsumer :
        IConsumer<Message>
    {
        readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Message> context)
        {
            _logger.LogInformation("Received Text: {Text}", context.Message.Text);
            if (context.Message.Filters is null)
            {
                _logger.LogInformation("List of filters is null");
            }
            else
            {
                _logger.LogInformation("List of filters contain {count} items", context.Message.Filters.Count);
            }

            return Task.CompletedTask;
        }
    }
}