using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerUi.Models {
    public class TourLogModel {
        private HttpClient _httpClient = new();
        public ObservableCollection<TourLog> TourLogs { get; set; } = new();

        public TourLogModel() {
            _httpClient.BaseAddress = new("https://localhost:7293/api/");
        }

        public async Task LoadTourLogsAsync(int tourId) {
            var response = await _httpClient.GetAsync($"Tour/{tourId}/TourLogs");

            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                try {
                    var tourLogs = JsonConvert.DeserializeObject<List<TourLog>>(content);
                    TourLogs.Clear();
                    tourLogs?.ForEach(tourLog => TourLogs.Add(tourLog));
                } catch (Exception ex) {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            } else {
                TourLogs.Clear();
            }
        }

        public async Task<HttpStatusCode> RemoveTourLogAsync(Guid? tourLogId) {
            if (tourLogId == null) {
                return HttpStatusCode.NotFound;
            }
            var response = await _httpClient.DeleteAsync($"TourLog/{tourLogId}");

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpsertTourLogAsync(TourLog? tourLog) {
            if (tourLog != null) {
                tourLog = tourLog with { Date = tourLog.Date.ToUniversalTime() };

                var jTourLog = JObject.FromObject(tourLog);
                if (tourLog.Id == Guid.Empty) jTourLog.Remove("id");

                using StringContent httpContent = new(jTourLog.ToString(), Encoding.UTF8, "application/json");
                using var response = await _httpClient.PostAsync("TourLog/Upsert", httpContent);

                return response.StatusCode;
            }
            return HttpStatusCode.NotFound;
        }
    }

    public enum Difficulty {
        EASY,
        MEDIUM,
        HARD
    }

    public enum Rating {
        Poor,
        Fair,
        Good,
        VeryGood,
        Excellent
    }

    public record TourLog {
        [JsonProperty("id")]
        public Guid Id { get; init; }

        [JsonProperty("date")]
        public DateTime Date { get; init; }

        [JsonProperty("difficulty")]
        public Difficulty Difficulty { get; init; }

        [JsonProperty("duration")]
        public TimeSpan Duration { get; init; }

        [JsonProperty("rating")]
        public Rating Rating { get; init; }

        [JsonProperty("comment")]
        public string Comment { get; init; }

        [JsonProperty("tourId")]
        public int TourId { get; init; }

        public TourLog(Guid id, DateTime date, Difficulty difficulty, TimeSpan duration, Rating rating, string comment, int tourId) {
            Id = id;
            Date = date;
            Difficulty = difficulty;
            Duration = duration;
            Rating = rating;
            Comment = comment;
            TourId = tourId;
        }

        public TourLog(int tourId) {
            Date = DateTime.Today;
            TourId = tourId;
        }

        public TourLog() { }
    }
}
