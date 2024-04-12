using RapidApi.Authentication;
using RapidApi.Model;

namespace RapidApi.ServiceDependencies
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services) 
        {
            services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
            services.AddTransient<GenerateKey>();
            services.AddTransient<Actions>();
            services.AddTransient<IMailSender,MailActions>();

            return services;
        }
    }
}
