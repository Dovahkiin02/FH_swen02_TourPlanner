using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerUi.Models {

    public interface IMapQuestModel {
        public Task<Tour> GetRouteInfoForTour(Tour tour);
    }
    public class MapQuestModel : IMapQuestModel {
        private readonly HttpClient _httpClient = new();
        private readonly IConfiguration _configuration;

        public MapQuestModel(IConfiguration configuration) {
            _configuration = configuration; 
        }

        public async Task<Tour> GetRouteInfoForTour(Tour tour) {
            string apiKey = "8C4bpxYEsGo8bNfYa815QRoUBlXrlnYH";

            string requestUrl = $"https://www.mapquestapi.com/directions/v2/route?key={apiKey}&from={tour.From}&to={tour.To}&unit=k&outFormat=json&routeType={tour.TransportType.ToString().ToLower()}";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();


            JObject jsonResponse = JObject.Parse(responseBody);
            if ((jsonResponse["route"] as JObject).ContainsKey("routeError")) {
                throw new InvalidPlaceException();
            }

            return tour with {
                Distance = (float)jsonResponse["route"]["distance"],
                EstTime = TimeSpan.FromSeconds((double)jsonResponse["route"]["time"])
            };
        
        }
    }

    public class InvalidPlaceException : Exception {
        public InvalidPlaceException() {}
        public InvalidPlaceException(string message)
        : base(message) {
        }

        public InvalidPlaceException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}
