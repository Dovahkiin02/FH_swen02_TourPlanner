using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerUi.Services;

namespace TourPlannerUi.ViewModels {
    public class MainViewModel : ViewModel {

        private INavigationService _navigation;

        public INavigationService Navigation {
            get => _navigation;
            set {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        private TourListViewModel _tourListViewModel;

        public TourListViewModel TourListViewModel {
            get => _tourListViewModel; 
            set { 
                _tourListViewModel = value; 
                OnPropertyChanged();
            }
        }


        public MainViewModel(INavigationService navService, TourListViewModel tourListViewModel) {
            _navigation = navService;
            TourListViewModel = tourListViewModel; 
        }
    }
}
