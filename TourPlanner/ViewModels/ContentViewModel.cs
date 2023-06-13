using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.ViewModels
{
    public class ContentViewModel
    {
        public TourListViewModel TourListViewModel { get; set; } = new TourListViewModel();
        public TourLogsViewModel TourLogsViewModel { get; set; } = new TourLogsViewModel();
    

        public ContentViewModel(TourListViewModel tourListViewModel, TourLogsViewModel tourLogsViewModel) {
            TourListViewModel = tourListViewModel;
            TourLogsViewModel = tourLogsViewModel;

            TourListViewModel.PropertyChanged += TourListViewModel_PropertyChanged;
        }

        private void TourListViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
            throw new Exception("not implemented");
        }
    }
}
