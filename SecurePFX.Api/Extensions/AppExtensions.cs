using MassTransit;

namespace SecurePFX.Api.Extensions
{
    public static class AppExtensions
    {
        public static void AddRabbitMqServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["RabbitMQ:Host"]!), host =>
                    {
                        host.Username(configuration["RabbitMQ:Username"]!);
                        host.Password(configuration["RabbitMQ:Password"]!);
                    });

                    cfg.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(5)));
                });
            });
        }
    }
}
