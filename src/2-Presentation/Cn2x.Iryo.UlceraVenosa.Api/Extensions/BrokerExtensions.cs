
using Cn2x.Iryo.UlceraVenosa.Api.Configuration;
using Cn2x.Iryo.UlceraVenosa.Application.Consumers;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;


namespace Cn2x.Iryo.UlceraVenosa.Api.Extensions {
    public static class BrokerExtensions {
        public static IServiceCollection AddCustomMassTransit(this IServiceCollection services, IConfiguration configuration) {
            var rabbitMqConfiguration = configuration
                .GetSection(nameof(RabbitMQConfiguration))
                .Get<RabbitMQConfiguration>()!;

            services.AddMassTransit(busConfig =>
            {
                //busConfig.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
                //{
                //    o.QueryDelay = TimeSpan.FromSeconds(30);
                //    o.UsePostgres()
                //    .UseBusOutbox();
                //});

                busConfig.SetKebabCaseEndpointNameFormatter();

                // Configuração importante para usar Outbox nos endpoints
                //busConfig.AddConfigureEndpointsCallback((context, name, cfg) =>
                //{
                //    cfg.UseEntityFrameworkOutbox<ApplicationDbContext>(context);
                //});

                busConfig.AddConsumers(typeof(QuandoPacienteForAtualizado).Assembly);

                busConfig.UsingRabbitMq((context, cfg) =>
                {
                    // Constrói a URI baseada na configuração
                    var scheme = rabbitMqConfiguration.UseSsl ? "amqps" : "amqp";
                    var uri = new Uri($"{scheme}://{rabbitMqConfiguration.Username}:{rabbitMqConfiguration.Password}@{rabbitMqConfiguration.Host}:{rabbitMqConfiguration.Port}/{rabbitMqConfiguration.VirtualHost}");
                    
                    cfg.Host(uri, h =>
                    {
                        h.Username(rabbitMqConfiguration.Username);
                        h.Password(rabbitMqConfiguration.Password);
                    });

                    cfg.UseMessageRetry(r => r.Exponential(
                        rabbitMqConfiguration.RetryCount, 
                        TimeSpan.FromSeconds(rabbitMqConfiguration.RetryDelaySeconds), 
                        TimeSpan.FromSeconds(rabbitMqConfiguration.RetryDelaySeconds * 2), 
                        TimeSpan.FromSeconds(rabbitMqConfiguration.RetryDelaySeconds * 3)));

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;

        }
    }
} 
