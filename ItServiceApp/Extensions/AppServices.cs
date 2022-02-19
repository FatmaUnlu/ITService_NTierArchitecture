using System;
using ItServiceApp.Business.Services.Email;
using ItServiceApp.Business.Services.Payment;
using ItServiceApp.Core.MapperProfiles;
using ItServiceApp.InjectOrnek;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItServiceApp.Extensions
{
    public static class AppServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAutoMapper(options =>
            {
                options.AddProfile(typeof(AccountProfile));
                options.AddProfile(typeof(PaymentProfile));
                options.AddProfile<SubscriptionProfile>();
            });

            services.AddTransient<IEmailSender, EmailSender>(); //her mail kişiye özel ve farklı oldugu için her ihtiyaç duyulduğunda tekrar üretilimesi gerekir.
            services.AddScoped<IPaymentService, IyzikoPaymentService>(); //IPaymentService kullanacagm yerde IyzikoPayment servis kullanılsın
            services.AddScoped<IMyDependency, newMyDependency>(); 
            //loose coupling
            //services.AddTransient<EmailSender>();
            return services;
        }
    }
}
