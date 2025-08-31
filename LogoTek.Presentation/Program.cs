using LogoTek.Application.Infrastructure.DatabaseManager;
using LogoTek.Application.Infrastructure.Services;
using LogoTek.Infrastructure.Services;
using LogoTek.Persistance.Database;
using Microsoft.Extensions.DependencyInjection;

namespace LogoTek.Presentation
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var form1 = serviceProvider.GetService<Form1>();
            System.Windows.Forms.Application.Run(form1!);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDatabaseManager, DatabaseManager>();
            services.AddSingleton<ITelegramService, TelegramService>();
            services.AddSingleton<Form1>();
        }
    }
}