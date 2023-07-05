using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.CodeDom;
using System.Collections.Generic;
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
        private TourLogModel _tourLogModel;
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

        public EditTourLogViewModel(Tour tour, INavigationService navService, TourLogModel tourLogModel) {
            _navigation = navService;
            _tourLogModel = tourLogModel;
            _tour = tour;

            TourLog = new(_tour.Id);

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        public EditTourLogViewModel(TourLog? tourLog, Tour tour, INavigationService navService, TourLogModel tourLogModel) {
            _navigation = navService;
            _tourLogModel = tourLogModel;
            _tour = tour;

            TourLog = tourLog ?? new(_tour.Id);

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private async void OnSave() {
            HttpStatusCode status = await _tourLogModel.UpsertTourLogAsync(TourLog);
            if (status == HttpStatusCode.Created) {
                _navigation.NavigateTo<TourViewModel>(_tour);
            } else {
                MessageBox.Show(status.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnCancel() {
            _navigation.NavigateTo<TourViewModel>(_tour);
        }
    }
}
