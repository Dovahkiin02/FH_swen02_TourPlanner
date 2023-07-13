using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.CodeDom;
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

namespace TourPlannerUi.ViewModels {
    public partial class EditTourLogViewModel : ViewModel {
        private INavigationService _navigation;
        private ITourLogModel _tourLogModel;
        private Tour _tour;

        public IEnumerable<Difficulty> Difficulties {
            get => Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();
        }

        public IEnumerable<Rating> Ratings {
            get => Enum.GetValues(typeof(Rating)).Cast<Rating>();
        }

        [ObservableProperty]
        private TourLog tourLog;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditTourLogViewModel(Tour tour, INavigationService navService, ITourLogModel tourLogModel) {
            _navigation = navService;
            _tourLogModel = tourLogModel;
            _tour = tour;

            TourLog = new(_tour.Id);

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        public EditTourLogViewModel(TourLog? tourLog, Tour tour, INavigationService navService, ITourLogModel tourLogModel) {
            _navigation = navService;
            _tourLogModel = tourLogModel;
            _tour = tour;

            TourLog = tourLog ?? new(_tour.Id);

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private async void OnSave() {
            if (!TourLogValid(TourLog)) {
                MessageBox.Show("Invalid Tour Log", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            HttpStatusCode status = await _tourLogModel.UpsertTourLogAsync(TourLog);
            if (status == HttpStatusCode.Created) {
                if (_tour.Id == -1) {
                    await Console.Out.WriteLineAsync("what");
                }
                _navigation.NavigateTo<TourViewModel>(_tour);
            } else {
                MessageBox.Show(status.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool TourLogValid(TourLog tourLog) {
            var context = new ValidationContext(tourLog);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(tourLog, context, results, true);
        }

        private void OnCancel() {
            _navigation.NavigateTo<TourViewModel>(_tour);
        }
    }
}
