using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlannerUi.Models;
using TourPlannerUi.Services;

namespace TourPlannerUi.ViewModels {
    public partial class TourListViewModel : ViewModel {

        private TourModel _tourListModel;

        private INavigationService _navigation;

        public INavigationService Navigation {
            get => _navigation;
            set {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Tour> Tours => _tourListModel.Tours;

        [ObservableProperty]
        private Tour selectedTour;

        private bool _suppressNavigation = false;

        [ObservableProperty]
        private String searchText;

        public ICommand CreateTourCommand { get; }
        public ICommand EditTourCommand { get; }
        public ICommand DeleteTourCommand { get; }

        public TourListViewModel(INavigationService navService, TourModel tourModel) {
            _tourListModel = tourModel;
            LoadToursAsync().Wait(new TimeSpan(100));

            Navigation = navService;

            CreateTourCommand = new RelayCommand(OnCreateTour);
            EditTourCommand = new RelayCommand<Tour>(OnEditTour);
            DeleteTourCommand = new RelayCommand<Tour>(OnDeleteTour);
        }

        public async Task LoadToursAsync() {
            await _tourListModel.LoadToursAsync();
        }

        partial void OnSelectedTourChanged(Tour tour) {
            if (_suppressNavigation) {
                _suppressNavigation = false;
                return;
            }
            Navigation.NavigateTo<TourViewModel>(tour);
        }


        private void OnCreateTour() {
            _suppressNavigation = true;
            Navigation.NavigateTo<CreateAndEditTourViewModel>();
        }

        private void OnEditTour(Tour? tour) {
            Navigation.NavigateTo<CreateAndEditTourViewModel>(tour);
            _suppressNavigation = true;
            SelectedTour = tour;
        }

        private void OnDeleteTour(Tour? tour) {
            Console.WriteLine(tour);
        }
        
    }
}
