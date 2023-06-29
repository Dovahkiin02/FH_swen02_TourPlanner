using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerUi.Models;

namespace TourPlannerUi.ViewModels {
    public partial class CreateAndEditTourViewModel : ViewModel {

        [ObservableProperty]
        private Tour _tour;

        public CreateAndEditTourViewModel(Tour? tour) {
            _tour = tour;
        }

    }
}
