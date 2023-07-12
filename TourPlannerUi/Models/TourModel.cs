using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace TourPlannerUi.Models {
    public interface ITourModel {
        ObservableCollection<Tour> TourList { get; set; }
        List<Tour> UnfilteredTourList { get; }
        Task LoadToursAsync();
        Task<Tour?> UpsertTourAsync(Tour? tour);
        Task<HttpStatusCode> RemoveTourAsync(int tourId);

    }
    public class TourModel : ITourModel {
        private HttpClient _httpClient = new();
        private IMapQuestModel _mapQuestModel;

        public ObservableCollection<Tour> TourList { get; set; } = new();
        public List<Tour> UnfilteredTourList { get; private set; } = new();
        public TourModel(IMapQuestModel mapQuestModel) {
            _httpClient.BaseAddress = new("https://localhost:7293/api/");
            _mapQuestModel = mapQuestModel;
        }

        public async Task LoadToursAsync() {
            var response = await _httpClient.GetAsync("Tour");
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var tours = JsonConvert.DeserializeObject<List<Tour>>(content);
                TourList.Clear();
                tours?.ForEach((tour) => TourList.Add(tour));
                UnfilteredTourList = TourList.ToList();
            } else {
                // Handle the error.
                throw new Exception("Failed to load tours.");
            }
        }

        public async Task<Tour?> UpsertTourAsync(Tour? tour) {
            if (tour != null) {
                tour = await _mapQuestModel.GetRouteInfoForTour(tour);

                var jTour = JObject.FromObject(new {
                    Id = tour.Id == -1 ? (int?)null : tour.Id,
                    tour.Name,
                    tour.Description,
                    tour.From,
                    tour.To,
                    tour.TransportType,
                    tour.Distance,
                    tour.EstTime
                }, new JsonSerializer { NullValueHandling = NullValueHandling.Ignore });

                using StringContent httpContent = new(jTour.ToString(), Encoding.UTF8, "application/json");
                using var response = await _httpClient.PostAsync("Tour/Upsert", httpContent);

                try {
                    return JsonConvert.DeserializeObject<Tour>(await response.Content.ReadAsStringAsync());
                } catch {
                    return null;
                }
            }
            return null;
        }

        public async Task<HttpStatusCode> RemoveTourAsync(int tourId) {
            if (tourId == null) {
                return HttpStatusCode.NotFound;
            }
            var response = await _httpClient.DeleteAsync($"Tour/{tourId}");

            return response.StatusCode;
        }
    }

    public enum TransportType {
        Fastest,
        Shortest,
        Bicycle,
        Pedestrian,
        Multimodal,
    }

    public record Tour {
        [JsonProperty("id")]
        public int Id { get; init; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; init; }

        [JsonProperty("description")]
        [Required]
        public string Description { get; init; }

        [JsonProperty("from")]
        [Required]
        public string From { get; init; }

        [JsonProperty("to")]
        [Required]
        public string To { get; init; }

        [JsonProperty("transportType")]
        [Required]
        public TransportType TransportType { get; init; }

        [JsonProperty("distance")]
        public float Distance { get; init; }

        [JsonProperty("estTime")]
        public TimeSpan EstTime { get; init; }

        public string MapImageUrl {
            get {
                string apiKey = "8C4bpxYEsGo8bNfYa815QRoUBlXrlnYH"; 
                string size = "600,400@2x"; 
                string format = "png"; 
                string routeColor = "812DD3"; 

                string url = $"https://www.mapquestapi.com/staticmap/v5/map?key={apiKey}&size={size}&format={format}&start={From}|flag-start&end={To}|flag-end&routeColor={routeColor}";

                return url;
            }
        }

        public List<TourLog> TourLogs { get; set; } = new();
        public int Popularity {
            get {
                if (TourLogs == null)
                    return 0;

                return TourLogs.Count;
            }
        }

        public bool ChildFriendly {
            get {
                if (TourLogs == null || TourLogs.Count == 0)
                    return false;

                return TourLogs.All(log => log.Difficulty <= Difficulty.EASY && log.Duration.TotalHours <= 2 && Distance <= 5);
            }
        }

        public Tour(int id, string name, string description, string from, string to,
                TransportType transportType, float distance, TimeSpan estTime) {
            Id = id;
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
            Distance = distance;
            EstTime = estTime;
        }
        public static Tour createRandomTour() {
            Random rnd = new Random();
            int id = rnd.Next(0, 50);
            int rndTransportType = rnd.Next(0, 4);
            return new Tour(
                id,
                $"Tour{id}",
                "nice description",
                "FromPlace",
                "ToPlace",
                (TransportType)rndTransportType,
                (float)rnd.NextDouble(),
                new TimeSpan());
        }

        public Tour() {
            Id = -1;
        }

        public override string ToString() {
            return $"Id: {Id}, Name: {Name}, Description: {Description}, From: {From}, To: {To}, TransportType: {TransportType}, Distance: {Distance}, EstTime: {EstTime}";
        }
    }
}
