using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<TourModel>();
            services.AddSingleton<TourLogModel>();
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
