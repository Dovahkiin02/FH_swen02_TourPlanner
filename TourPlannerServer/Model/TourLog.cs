using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TourPlannerServer.Model {

    public enum Difficulty {
        EASY,
        MEDIUM,
        HARD
    }
    public record TourLog {
        public Guid Id { get; init; }
        public DateTime Date { get; init; }
        public Difficulty Difficulty { get; init; }
        public TimeSpan Duration { get; init; }
        public float Rating { get; init; }
        public string Comment { get; init; }
        public int TourId { get; init; }  // foreign key
        [JsonIgnore, NotMapped]
        public Tour Tour { get; init; }
    }

    public record TourLogDto {
        public Guid Id { get; init; }
        public DateTime Date { get; init; }
        public Difficulty Difficulty { get; init; }
        public TimeSpan Duration { get; init; }
        public float Rating { get; init; }
        public string Comment { get; init; }
        public int TourId { get; init; }  // foreign key
    }
}
