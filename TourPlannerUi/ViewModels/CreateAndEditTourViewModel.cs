﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
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
            HttpStatusCode status = await _tourModel.UpsertTourAsync(Tour);
            if (status == HttpStatusCode.Created) {
                _tourModel.LoadToursAsync().Wait(new TimeSpan(100));
                _navigation.NavigateTo<TourViewModel>(Tour);
            } else {
                MessageBox.Show(status.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void OnCancel() {
            _navigation.NavigateTo<TourViewModel>(unchangedTour);
        }

    }
}
