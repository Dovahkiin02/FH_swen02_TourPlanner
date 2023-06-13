using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerUi.Models {
    public class TourModel {
        private HttpClient _client;

        public ObservableCollection<Tour> Tours { get; set; }
        public TourModel() {
            _client = new HttpClient();
            Tours = new ObservableCollection<Tour>();
        }

        public async Task LoadToursAsync() {
            var response = await _client.GetAsync("https://localhost:7293/api/Tour");
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(content);
                var tours = JsonConvert.DeserializeObject<List<Tour>>(content);
                Tours.Clear();
                tours?.ForEach((tour) => Tours.Add(tour));
            }
        }
    }

    public enum TransportType {
        FASTEST,
        SHORTEST,
        BICYCLE,
        PEDESTRIAN,
        MULTIMODAL,
    }

    public record Tour(
        [JsonProperty("id")] int Id,
        [JsonProperty("name")] string Name,
        [JsonProperty("description")] string Description,
        [JsonProperty("from")] string From,
        [JsonProperty("to")] string To,
        [JsonProperty("transportType")] TransportType TransportType,
        [JsonProperty("distance")] float Distance,
        [JsonProperty("estTime")] TimeSpan EstTime,
        [JsonProperty("info")] string Info
    ) {
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

        public override string ToString() {
            return $"Id: {Id}, Name: {Name}, Description: {Description}, From: {From}, To: {To}, TransportType: {TransportType}, Distance: {Distance}, EstTime: {EstTime}, Info: {Info}";
        }
    }
}
