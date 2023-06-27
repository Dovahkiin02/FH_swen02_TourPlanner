using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerUi.ViewModels {
    public class MainViewModel {
        private ToursViewModel _toursViewModel;
        private CreateAndEditTourViewModel _createAndEditTourViewModel;
        private MapViewModel _mapViewModel;

        public MainViewModel() {
            _toursViewModel = new ToursViewModel();
            _createAndEditTourViewModel = new CreateAndEditTourViewModel();
            _mapViewModel = new MapViewModel();
        }
    }
}
