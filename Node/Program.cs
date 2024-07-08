using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Node;

class Program {
    static void Main(string[] args) {
        var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) => {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) => {
                    var rabbitMqSettings = context.Configuration.GetSection("RabbitMQ");
                    var factory = new ConnectionFactory() {
                        HostName = rabbitMqSettings["HostName"],
                        UserName = rabbitMqSettings["UserName"],
                        Password = rabbitMqSettings["Password"]
                    };

                    services.AddSingleton<IConnection>(sp => factory.CreateConnection());
                    services.AddSingleton<IModel>(sp => sp.GetRequiredService<IConnection>().CreateModel());
                    services.AddHostedService<NodeService>();
                })
                .Build();

        host.Run();
    }
}