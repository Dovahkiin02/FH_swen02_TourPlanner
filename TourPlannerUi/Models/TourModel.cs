using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace TourPlannerUi.Models {
    public class TourModel {
        private HttpClient _httpClient = new();

        public ObservableCollection<Tour> Tours { get; set; } = new();
        public TourModel() {
            _httpClient.BaseAddress = new("https://localhost:7293/api/");
        }

        public async Task LoadToursAsync() {
            var response = await _httpClient.GetAsync("Tour");
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(content);
                var tours = JsonConvert.DeserializeObject<List<Tour>>(content);
                Tours.Clear();
                tours?.ForEach((tour) => Tours.Add(tour));
            } else {
                // Handle the error.
                throw new Exception("Failed to load tours.");
            }
        }

        public async Task<HttpStatusCode> UpsertTourAsync(Tour? tour) {
            if (tour != null) {
                var json = JsonConvert.SerializeObject(tour);
                using StringContent httpContent = new(json, Encoding.UTF8, "application/json");
                using var response = await _httpClient.PostAsync("Tour/Upsert", httpContent);

                return response.StatusCode;
            }
            return HttpStatusCode.NotFound;
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
        public string Name { get; init; }

        [JsonProperty("description")]
        public string Description { get; init; }

        [JsonProperty("from")]
        public string From { get; init; }

        [JsonProperty("to")]
        public string To { get; init; }

        [JsonProperty("transportType")]
        public TransportType TransportType { get; init; }

        [JsonProperty("distance")]
        public float Distance { get; init; }

        [JsonProperty("estTime")]
        public TimeSpan EstTime { get; init; }

        [JsonProperty("info")]
        public string Info { get; init; }

        public Tour(int id, string name, string description, string from, string to,
                TransportType transportType, float distance, TimeSpan estTime, string info) {
            Id = id;
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
            Distance = distance;
            EstTime = estTime;
            Info = info;
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
                new TimeSpan(),
                "informativeInfo");
        }

        public Tour() { }

        public override string ToString() {
            return $"Id: {Id}, Name: {Name}, Description: {Description}, From: {From}, To: {To}, TransportType: {TransportType}, Distance: {Distance}, EstTime: {EstTime}, Info: {Info}";
        }
    }
}
