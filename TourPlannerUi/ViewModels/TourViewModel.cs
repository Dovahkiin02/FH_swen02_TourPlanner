using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlannerUi.Models;
using TourPlannerUi.Services;

namespace TourPlannerUi.ViewModels {
    public partial class TourViewModel : ViewModel {

        [ObservableProperty]
        private Tour selectedTour;

        private TourLogModel _tourLogModel;
        private INavigationService _navigation;

        public ObservableCollection<TourLog> TourLogs => _tourLogModel.TourLogs;

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CreateCommand { get; }

        public TourViewModel(Tour tour, TourLogModel tourLogModel, INavigationService navService) {
            this.selectedTour = tour;
            _tourLogModel = tourLogModel;
            _navigation = navService;

            Task.Run(() => LoadAndAssignTourLogsAsync()).GetAwaiter().GetResult();


            EditCommand = new RelayCommand<TourLog>(OnEdit);
            DeleteCommand = new RelayCommand<TourLog>(OnDelete);
            CreateCommand = new RelayCommand(OnCreate);
        }

        private async Task LoadAndAssignTourLogsAsync() {
            await _tourLogModel.LoadTourLogsAsync(SelectedTour.Id);
            SelectedTour.TourLogs = TourLogs.ToList();
        }


        private void OnEdit(TourLog? tourLog) {
            _navigation.NavigateTo<EditTourLogViewModel>(tourLog, SelectedTour);
        }

        private async void OnDelete(TourLog? tourLog) {
            var response = _tourLogModel.DeleteTourLogAsync(tourLog?.Id);
            await LoadAndAssignTourLogsAsync();
        }

        private void OnCreate() {
            _navigation.NavigateTo<EditTourLogViewModel>(SelectedTour);
        }
    }
}
