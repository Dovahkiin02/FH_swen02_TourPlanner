using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlannerUi.Models;
using TourPlannerUi.Services;
using TransportType = TourPlannerUi.Models.TransportType;

namespace TourPlannerUi.ViewModels {
    public partial class CreateAndEditTourViewModel : ViewModel {

        [ObservableProperty]
        private Tour tour;

        private Tour unchangedTour;
        private TourModel model;

        private INavigationService _navigation;

        public IEnumerable<TransportType> TransportTypes {
            get => Enum.GetValues(typeof(TransportType)).Cast<TransportType>();
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateAndEditTourViewModel(Tour? tour, INavigationService navService, TourModel tourModel) {
            this.tour = tour ?? new();
            unchangedTour = tour;
            model = tourModel;

            this._navigation = navService;

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private async void OnSave() {
            HttpStatusCode status = await model.UpsertTourAsync(Tour);
            if (status == HttpStatusCode.OK) {
                _navigation.NavigateTo<TourViewModel>(tour);
            }
            
        }

        private void OnCancel() {
            _navigation.NavigateTo<TourViewModel>(unchangedTour);
        }

    }
}
