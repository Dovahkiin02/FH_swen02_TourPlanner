using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TourPlannerUi.Models;
using TourPlannerUi.Services;
using TourPlannerUi.ViewModels;
using TourPlannerUi.Views;

namespace TourPlannerUi {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ITourModel, TourModel>();
            services.AddSingleton<ITourLogModel, TourLogModel>();
            services.AddSingleton<IMapQuestModel, MapQuestModel>();
            services.AddSingleton<IGeneratePdfService, GeneratePdfService>();
            services.AddSingleton<IDataIOService, DataIOService>();
            services.AddSingleton<TourListViewModel>();

            services.AddTransient<TourViewModel>();
            services.AddTransient<CreateAndEditTourViewModel>();
            services.AddTransient<EditTourLogView>();
            

            // Register ViewModelFactory
            services.AddSingleton<IViewModelFactory, ViewModelFactory>(provider =>
            {
                return new ViewModelFactory(provider);
            });

            // Register NavigationService with ViewModelFactory
            services.AddSingleton<INavigationService>(provider =>
            {
                return new NavigationService(() => provider.GetRequiredService<IViewModelFactory>());
            });

            services.AddSingleton<MainWindow>(provider => new MainWindow() {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e) {
            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            
            MainWindow.Show();
            base.OnStartup(e);
            
        }
    }
}
