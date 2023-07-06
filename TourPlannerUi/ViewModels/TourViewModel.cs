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
        //public ICommand LoadedCommand { get; }

        public TourViewModel(Tour tour, TourLogModel tourLogModel, INavigationService navService) {
            this.selectedTour = tour;
            _tourLogModel = tourLogModel;
            _navigation = navService;

            EditCommand = new RelayCommand<TourLog>(OnEdit);
            DeleteCommand = new RelayCommand<TourLog>(OnDelete);
            CreateCommand = new RelayCommand(OnCreate);
            //LoadedCommand = new RelayCommand(async () => await LoadAndAssignTourLogsAsync());

            LoadAndAssignTourLogsAsync();
        }

        private async void LoadAndAssignTourLogsAsync() {
            await _tourLogModel.LoadTourLogsAsync(SelectedTour.Id);
            try {
                SelectedTour.TourLogs.Clear();
                TourLogs.ToList().ForEach(tourLog => SelectedTour.TourLogs.Add(tourLog));
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }


        private void OnEdit(TourLog tourLog) {
            _navigation.NavigateTo<EditTourLogViewModel>(tourLog, SelectedTour);
        }

        private void OnDelete(TourLog? tourLog) {
            var response = _tourLogModel.RemoveTourLogAsync(tourLog?.Id);
            LoadAndAssignTourLogsAsync();
        }

        private void OnCreate() {
            _navigation.NavigateTo<EditTourLogViewModel>(SelectedTour);
        }
    }
}
