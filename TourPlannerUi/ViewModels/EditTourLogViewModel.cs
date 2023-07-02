using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlannerUi.Models;
using TourPlannerUi.Services;

namespace TourPlannerUi.ViewModels {
    public partial class EditTourLogViewModel : ViewModel {
        private INavigationService _navigationService;
        private TourLogModel _tourLogModel;
        private Tour _tour;

        public IEnumerable<Difficulty> Difficulties {
            get => Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();
        }

        [ObservableProperty]
        private TourLog tourLog;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditTourLogViewModel(Tour tour, INavigationService navService, TourLogModel tourLogModel) {
            _navigationService = navService;
            _tourLogModel = tourLogModel;
            _tour = tour;

            TourLog = new(_tour.Id);

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        public EditTourLogViewModel(TourLog? tourLog, Tour tour, INavigationService navService, TourLogModel tourLogModel) {
            _navigationService = navService;
            _tourLogModel = tourLogModel;
            _tour = tour;

            TourLog = tourLog ?? new(_tour.Id);

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private void OnSave() {
            var response = _tourLogModel.UpsertTourLogAsync(TourLog);
            _navigationService.NavigateTo<TourViewModel>(_tour);
        }

        private void OnCancel() {
            _navigationService.NavigateTo<TourViewModel>(_tour);
        }
    }
}
