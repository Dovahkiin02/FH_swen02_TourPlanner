using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerUi.ViewModels {
    public class MainViewModel {
        private ToursViewModel _toursViewModel;
        private CreateAndEditTourViewModel _createAndEditTourViewModel;

        public MainViewModel() {
            _toursViewModel = new ToursViewModel();
            _createAndEditTourViewModel = new CreateAndEditTourViewModel();
        }
    }
}
