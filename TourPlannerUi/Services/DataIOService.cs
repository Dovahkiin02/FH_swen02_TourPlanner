using iText.Kernel.Geom;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using TourPlannerUi.Models;
using Path = System.IO.Path;

namespace TourPlannerUi.Services
{
    public interface IDataIOService { 
        void exportData(Tour tour);
        Tour importData();
    }

    public class DataIOService : IDataIOService {

        private Tour? importedTour;
        private const string path = "..\\..\\..\\exports\\json";

        public void exportData(Tour tour) {
            
            string fullPath = Path.Combine(path, $"export_{tour.Name}.json");
            string jsonString = JsonSerializer.Serialize(tour, new JsonSerializerOptions { WriteIndented = true });
            
            File.WriteAllText(fullPath, jsonString);
        }

        public Tour importData() {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Dateien (*.json)|*.json";
            openFileDialog.InitialDirectory = Path.GetFullPath(path);

            if (openFileDialog.ShowDialog() == true) {
                string fileName = openFileDialog.FileName;

                try {
                    importedTour = JsonSerializer.Deserialize<Tour>(File.ReadAllText(fileName));
                }
                catch (JsonException ex) {
                    MessageBox.Show("Invalid Data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine($"data dont contain valid json {ex}");   
                }
            }
            return importedTour;
        }
    }
}
