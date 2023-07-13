using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlannerUi.Models;
using TourPlannerUi.Services;

namespace TourPlannerUi.ViewModels {
    public partial class TourViewModel : ViewModel {

        [ObservableProperty]
        private Tour selectedTour;

        private ITourLogModel _tourLogModel;
        private ITourModel _tourModel;
        private INavigationService _navigation;
        private IGeneratePdfService _generatePdf;

        public ObservableCollection<TourLog> TourLogs => _tourLogModel.TourLogs;

        [ObservableProperty]
        private BitmapImage tourImage;

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand GeneratePdfCommand { get; }

        public TourViewModel(Tour tour, ITourLogModel tourLogModel, INavigationService navService, IGeneratePdfService generatePdfService, ITourModel tourModel)
        {
            this.selectedTour = tour;
            _tourLogModel = tourLogModel;
            _tourModel = tourModel;
            _navigation = navService;
            _generatePdf = generatePdfService;

            EditCommand = new RelayCommand<TourLog>(OnEdit);
            DeleteCommand = new RelayCommand<TourLog>(OnDelete);
            CreateCommand = new RelayCommand(OnCreate);
            GeneratePdfCommand = new RelayCommand(OnGeneratePdf);

            LoadAndAssignTourLogsAsync();
        }

        private async void LoadAndAssignTourLogsAsync() {
            await _tourLogModel.LoadTourLogsAsync(SelectedTour.Id);
            TourImage = await _tourModel.GetMapImageAsync(SelectedTour);
            try {
                SelectedTour.TourLogs.Clear();
                TourLogs.ToList().ForEach(tourLog => SelectedTour.TourLogs.Add(tourLog));
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        private void OnEdit(TourLog? tourLog) {
            _navigation.NavigateTo<EditTourLogViewModel>(tourLog, SelectedTour);
        }

        private void OnGeneratePdf() {
            _generatePdf.create(SelectedTour);
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
