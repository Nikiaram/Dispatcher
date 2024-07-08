using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Node;
public class NodeService : BackgroundService {
    private readonly IModel _channel;

    public NodeService(IModel channel) {
        _channel = channel;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        _channel.QueueDeclare(queue: "job_queue",
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);

            var result = await ExecuteCommandAsync(message);

            Console.WriteLine(" [x] Done {0}", result);
        };

        _channel.BasicConsume(queue: "job_queue",
                              autoAck: true,
                              consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();

        return Task.CompletedTask;
    }

    private Task<string> ExecuteCommandAsync(string command) {
        return Task.FromResult($"Executed: {command}");
    }

    public override void Dispose() {
        _channel.Close();
        base.Dispose();
    }
}