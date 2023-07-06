using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlannerUi.Models;
using TourPlannerUi.Services;
using TransportType = TourPlannerUi.Models.TransportType;

namespace TourPlannerUi.ViewModels {
    public partial class CreateAndEditTourViewModel : ViewModel {

        [ObservableProperty]
        private Tour tour;

        private Tour unchangedTour;
        private TourModel _tourModel;

        private INavigationService _navigation;

        public IEnumerable<TransportType> TransportTypes {
            get => Enum.GetValues(typeof(TransportType)).Cast<TransportType>();
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateAndEditTourViewModel(INavigationService navService, TourModel tourModel) {
            this.tour = new();
            unchangedTour = tour;
            _tourModel = tourModel;

            this._navigation = navService;

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        public CreateAndEditTourViewModel(Tour tour, INavigationService navService, TourModel tourModel) {
            this.tour = tour;
            unchangedTour = tour;
            _tourModel = tourModel;

            this._navigation = navService;

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private async void OnSave() {
            if (!TourValid(Tour)) {
                MessageBox.Show("Invalid Tour", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try {
                Tour? createdTour = await _tourModel.UpsertTourAsync(Tour);
                if (createdTour != null) {
                    await _tourModel.LoadToursAsync();
                    _navigation.NavigateTo<TourViewModel>(createdTour);
                } else {
                    MessageBox.Show("Failed while trying to created new Tour", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } catch (InvalidPlaceException e) {
                MessageBox.Show("Invalid From or To. Please enter valid places", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private bool TourValid(Tour tour) {
            var context = new ValidationContext(tour);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(tour, context, results, true);
        }

        private void OnCancel() {
            _navigation.NavigateTo<TourViewModel>(unchangedTour);
        }

    }
}
