using System.Configuration;
using System.Data;
using System.Windows;
using BussinessLogic.Interface;
using BussinessLogic.Service;
using DataAccess.DAOs;
using Microsoft.Extensions.DependencyInjection;
using MyWPF.ViewModel;
using Repository;
using Repository.Interface;

namespace MyWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ServiceCollection(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ServiceCollection(ServiceCollection services)
        {
            //Đăng ký các service, repository, DAO mà bạn đã có sẵn
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ProductDAO>();
            services.AddTransient<ProductViewModel>();
            services.AddTransient<ClassViewModel>();


            //Đăng ký MainWindow
            services.AddSingleton<MainWindow>();
        }

    }

}
