using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
            services.AddTransient<TourViewModel>();
            services.AddTransient<CreateAndEditTourViewModel>();
            services.AddTransient<TourListViewModel>();

            //services.AddTransient<TourListView>();
            //services.AddTransient<TourView>();
            //services.AddTransient<CreateAndEditTourView>();

            // Register ViewModelFactory
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            // Register NavigationService with ViewModelFactory
            services.AddSingleton<INavigationService>(provider =>
            {
                return new NavigationService(provider.GetRequiredService<IViewModelFactory>());
            });

            services.AddSingleton<MainWindow>(provider => new MainWindow() {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e) {
            _serviceProvider.GetRequiredService<MainWindow>();
            
            MainWindow.Show();
            base.OnStartup(e);
            
        }
    }
}
