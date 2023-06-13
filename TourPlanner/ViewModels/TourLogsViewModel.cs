using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {


    public class TourLogsViewModel {
        public ObservableCollection<TourLog> TourLogs { get; set; }

        public TourLogsViewModel() {
            this.TourLogs = new ObservableCollection<TourLog>();
            TourLogs.Add(TourLog.CreateRandomTourLog());
            TourLogs.Add(TourLog.CreateRandomTourLog());
            TourLogs.Add(TourLog.CreateRandomTourLog());
        }
    }
}
