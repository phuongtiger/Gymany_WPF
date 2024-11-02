using System.Configuration;
using System.Data;
using System.Windows;
using BussinessLogic.Interface;
using BussinessLogic.Service;
using DataAccess.DAOs;
using Microsoft.Extensions.DependencyInjection;
using MyWPF.ViewModel;
using MyWPF.Views;
using Repository;
using Repository.Interface;

namespace MyWPF
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ServiceCollection(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<LoginView>();
            mainWindow.Show();
        }

        private static void ServiceCollection(ServiceCollection services)
        {
            //Đăng ký các service, repository, DAO mà bạn đã có sẵn
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ProductDAO>();
            services.AddTransient<ProductViewModel>();


            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<AdminDAO>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<ProfileViewModel>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<CategoryDAO>();
            services.AddTransient<CategoryViewModel>();

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<OrderDAO>();
            services.AddTransient<OrderViewModel>();

            services.AddTransient<IPersonalTrainerService, PersonalTrainerService>();
            services.AddTransient<IPersonalTrainerRepository, PersonalTrainerRepository>();
            services.AddTransient<PersonalTrainerDAO>();
            services.AddTransient<PersonalTrainerViewModel>();

            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<INotificationsRepository, NotificationRepository>();
            services.AddTransient<NotificationDAO>();
            services.AddTransient<NotificationViewModel>();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<CustomerDAO>();
            services.AddTransient<CustomerViewModel>();

            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<CartDAO>();
            services.AddTransient<CartViewModel>();

            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<PaymentDAO>();
            services.AddTransient<PaymentViewModel>();

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<PostDAO>();
            services.AddTransient<PostViewModel>();

            //Đăng ký MainWindow
            services.AddSingleton<LoginView>();
        }

    }

}
