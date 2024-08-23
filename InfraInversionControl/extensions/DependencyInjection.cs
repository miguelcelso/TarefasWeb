using Domain.interfaces.messagebrokers;
using Domain.interfaces.repositories;
using Domain.interfaces.services;
using HealthChecks.UI.Client;
using Infra.configurations.rabbitmq;
using Infra.contexts;
using Infra.queue;
using Infra.repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.mappings;
using Service.tarefas;

namespace InfraInversionControl.extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var stringConexao = configuration.GetConnectionString("TarefaConnection");
            if (stringConexao == null) {
                throw new NullReferenceException("GetConnectionString retornou a nullo");
            }
            services.AddDbContext<ContextoTarefa>(options => {
                options.UseSqlServer(stringConexao, mig => mig.MigrationsAssembly(typeof(ContextoTarefa).Assembly.FullName));
                }
            );
            services.Configure<RabbitMqConfiguracao>(a => configuration.GetSection(nameof(RabbitMqConfiguracao)).Bind(a));

            var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqConfiguracao)).Get<RabbitMqConfiguracao>();
            if (rabbitMqConfiguration == null)
            {
                throw new NullReferenceException("RabbitMqConfiguracao retornou a nullo");
            }
            var rabbitConnectionString = $"amqp://{rabbitMqConfiguration?.Username}:{rabbitMqConfiguration?.Password}@{rabbitMqConfiguration?.HostName}";
            if (rabbitConnectionString == null)
            {
                throw new NullReferenceException("rabbitConnectionString retornou a nullo");
            }
            services.AddSingleton<IMensagemService, RabbitMqService>();
            services.AddScoped<ITarefaRepository, TarefaRepository>();
            services.AddScoped<ITarefaService, TarefaService>();  
            services.AddAutoMapper(typeof(TarefaMppingProfile).Assembly);
            services 
                .AddHealthChecks()
                .AddRabbitMQ(rabbitConnectionString, name: "rabbitmq-check", tags: new string[] { "rabbitmq" })
                .AddSqlServer(stringConexao, name: "sqlserver-check", tags: new string[] { "sqlserver" });

            return services;
        }
        public static IApplicationBuilder ConfigureHealthCheck(this IApplicationBuilder builder)
        {
            builder.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = p => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            return builder;
        }
    }
}
