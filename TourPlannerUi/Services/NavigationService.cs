﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerUi.ViewModels;

namespace TourPlannerUi.Services {

    public interface INavigationService { 
        ViewModel CurrentView { get; }
        void NavigateTo<T>(object? param = null) where T : ViewModel;

    }
    public class NavigationService : ObservableObject, INavigationService {

        private ViewModel _currentView;
        private readonly Func<IViewModelFactory> _viewModelFactoryProvider;

        public ViewModel CurrentView {
            get => _currentView;

            private set {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<IViewModelFactory> viewModelFactoryProvider) {
            _viewModelFactoryProvider = viewModelFactoryProvider;
        }

        public void NavigateTo<TViewModel>(object? param = null) where TViewModel : ViewModel {
            ViewModel viewModel = _viewModelFactoryProvider().Create<TViewModel>(param);
            CurrentView = viewModel;
        }
    }
}
