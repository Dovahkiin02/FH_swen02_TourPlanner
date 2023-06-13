using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.ViewModels {
    public partial class TourListViewModel : ObservableObject {

        private TourListModel _tourListModel;

        public ObservableCollection<Tour> TourList => _tourListModel.Tours;

        [ObservableProperty]
        private Tour selectedTour;

        public TourListViewModel() {
            _tourListModel = new TourListModel();
        }

        public async Task LoadToursAsync() {
            await _tourListModel.LoadToursAsync();
        }
    }
}
