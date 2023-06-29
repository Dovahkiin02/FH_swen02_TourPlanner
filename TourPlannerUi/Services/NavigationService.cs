using CommunityToolkit.Mvvm.ComponentModel;
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
        private readonly IViewModelFactory _viewModelFactory;

        public ViewModel CurrentView {
            get => _currentView;

            private set {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(IViewModelFactory viewModelFactory) {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>(object? param = null) where TViewModel : ViewModel {
            ViewModel viewModel = _viewModelFactory.Create<TViewModel>(param);
            CurrentView = viewModel;
        }
    }
}
