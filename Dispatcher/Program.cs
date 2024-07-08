using Microsoft.EntityFrameworkCore;
using Dispatcher.Data;
using Dispatcher.Repositories;
using Dispatcher.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace Dispatcher {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IJobRepository, JobRepository>();
            builder.Services.AddScoped<IJobService, JobService>();

            var rabbitConfig = builder.Configuration.GetSection("Rabbit");
            var factory = new ConnectionFactory() {
                Uri = new Uri(rabbitConfig["Uri"]),
                UserName = rabbitConfig["UserName"],
                Password = rabbitConfig["Password"]
            };

            try {
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                builder.Services.AddSingleton<IModel>(channel);
            }
            catch (BrokerUnreachableException ex) {
                Console.WriteLine($"RabbitMQ Broker Unreachable: {ex.Message}");
            }

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}