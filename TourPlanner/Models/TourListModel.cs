using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class TourListModel {
        private HttpClient _client;

        public ObservableCollection<Tour> Tours { get; set; }
        public TourListModel() {
            _client = new HttpClient();
            Tours = new ObservableCollection<Tour>();
            LoadToursAsync().Wait();
        }

        public async Task LoadToursAsync() {
            var response = await _client.GetAsync("https://localhost:7293/api/Tour");
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var tours = JsonSerializer.Deserialize<List<Tour>>(content);
                Tours.Clear();
                tours.ForEach((tour) => Tours.Add(tour));  
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
        int Id,
        string Name,
        string Description,
        string From,
        string To,
        TransportType TransportType,
        float Distance,
        TimeSpan EstTime,
        string Info
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
