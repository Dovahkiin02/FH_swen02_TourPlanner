using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model {

    public class TourLogModel {
        public ObservableCollection<TourLog> TourLogs { get; set; } = new();

        public TourLogModel() {
            TourLogs.Add(TourLog.CreateRandomTourLog());
            TourLogs.Add(TourLog.CreateRandomTourLog());
            TourLogs.Add(TourLog.CreateRandomTourLog());
        }
    }

    public enum Difficulty {
        EASY,
        MEDIUM,
        HARD
    }
    public record TourLog(
        Guid Id,
        DateTime Date,
        Difficulty Difficulty,
        TimeSpan Duration,
        float Rating,
        string Comment) {
        public static TourLog CreateRandomTourLog() {
            return new TourLog(Guid.NewGuid(), DateTime.Now, GetRandomDifficulty(), new TimeSpan(1, 2, 3), 3.2f, "no comment");
        }

        static Difficulty GetRandomDifficulty() {
            Random random = new Random();
            return (Difficulty)random.Next(0, 2);
        }
    }
    
}
