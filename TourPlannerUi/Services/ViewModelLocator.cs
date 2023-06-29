using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerUi.ViewModels;

namespace TourPlannerUi.Services {
    public class ViewModelLocator {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelLocator(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public MainViewModel MainViewModel => _serviceProvider.GetRequiredService<MainViewModel>();
        public TourListViewModel TourListViewModel => _serviceProvider.GetRequiredService<TourListViewModel>();
        public TourViewModel TourViewModel => _serviceProvider.GetRequiredService<TourViewModel>();
        public CreateAndEditTourViewModel CreateAndEditTourViewModel => _serviceProvider.GetRequiredService<CreateAndEditTourViewModel>();

    }
}
