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

        [ObservableProperty]
        private String searchText;

        public ICommand CreateTourCommand { get; }
        public ICommand EditTourCommand { get; }
        public ICommand DeleteTourCommand { get; }

        public TourListViewModel(INavigationService navService) {
            _tourListModel = new TourModel();
            LoadToursAsync().Wait(new TimeSpan(0, 0, 1));

            Navigation = navService;

            CreateTourCommand = new RelayCommand(OnCreateTour);
            EditTourCommand = new RelayCommand<Tour>(OnEditTour);
            DeleteTourCommand = new RelayCommand<Tour>(OnDeleteTour);
        }


        private void OnCreateTour() {
            Navigation.NavigateTo<CreateAndEditTourViewModel>();
            Console.WriteLine("something");
        }

        private void OnEditTour(Tour? tour) {
            Navigation.NavigateTo<CreateAndEditTourViewModel>(tour);
            Console.WriteLine(tour);
        }

        private void OnDeleteTour(Tour? tour) {
            Console.WriteLine(tour);
        }
        public async Task LoadToursAsync() {
            await _tourListModel.LoadToursAsync();
        }
    }
}
