﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Standard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using TourPlannerUi.Models;
using TourPlannerUi.Services;

namespace TourPlannerUi.ViewModels {
    public partial class TourListViewModel : ViewModel {

        private TourModel _tourModel;
        private TourLogModel _tourLogModel;
        private INavigationService _navigation;
        private IGeneratePdfService _generatePdf;

        public INavigationService Navigation {
            get => _navigation;
            set {
                _navigation = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Tour> Tours => _tourModel.Tours;

        [ObservableProperty]
        private Tour selectedTour;

        private bool _suppressNavigation = false;

        [ObservableProperty]
        private String searchText;

        public ICommand CreateTourCommand { get; }
        public ICommand EditTourCommand { get; }
        public ICommand DeleteTourCommand { get; }
        public ICommand GeneratePdfCommand { get; }

        public TourListViewModel(INavigationService navService, TourModel tourModel, TourLogModel tourLogModel) {
            _tourModel = tourModel;
            _tourLogModel = tourLogModel;
            _generatePdf = new GeneratePdf();
 
            LoadAndAssignToursAsync();

            Navigation = navService;

            CreateTourCommand = new RelayCommand(OnCreateTour);
            EditTourCommand = new RelayCommand<Tour>(OnEditTour);
            DeleteTourCommand = new RelayCommand<Tour>(OnDeleteTour);
            GeneratePdfCommand = new RelayCommand(OnGeneratePdf);
        }

        private async void LoadAndAssignToursAsync() {
            await _tourModel.LoadToursAsync();
            _tourModel.Tours.ToList().ForEach(async (tour) => {
                await _tourLogModel.LoadTourLogsAsync(tour.Id);
                tour.TourLogs.Clear();
                _tourLogModel.TourLogs.ToList().ForEach(tour.TourLogs.Add);
            });
        }

        partial void OnSearchTextChanging(string query) {
            if (query == "") {
                LoadAndAssignToursAsync();
                return;
            }

            _tourModel.Tours.Clear();

            foreach (var tour in _tourModel.UnfilterdTourList) {
                if (tour.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    tour.Description.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    tour.From.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    tour.To.Contains(query, StringComparison.OrdinalIgnoreCase)) {
                    _tourModel.Tours.Add(tour);
                    continue;
                }

                foreach (var tourLog in tour.TourLogs) {
                    if (tourLog.Rating.ToString().Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        tourLog.Comment.Contains(query, StringComparison.OrdinalIgnoreCase)) {
                        _tourModel.Tours.Add(tour);
                        break;
                    }
                }
            }
        }

        partial void OnSelectedTourChanged(Tour tour) {
            if (_suppressNavigation) {
                _suppressNavigation = false;
                return;
            }
            if (tour != null) {
                Navigation.NavigateTo<TourViewModel>(tour);
            }
        }

        private void OnCreateTour() {
            _suppressNavigation = true;
            Navigation.NavigateTo<CreateAndEditTourViewModel>();
        }

        private void OnGeneratePdf()  {
            _generatePdf.create(_tourModel.Tours);
        }

        private void OnEditTour(Tour? tour) {
            Navigation.NavigateTo<CreateAndEditTourViewModel>(tour);
            _suppressNavigation = true;
            SelectedTour = tour;
        }

        private async void OnDeleteTour(Tour? tour) {
            var response = await _tourModel.RemoveTourAsync(tour.Id);
            LoadAndAssignToursAsync();
        }  
    }
}
