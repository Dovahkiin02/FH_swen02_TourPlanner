using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using TourPlannerUi.Models;
using iText.IO.Image;
using iText.Kernel.Font;
using Document = iText.Layout.Document;
using Paragraph = iText.Layout.Element.Paragraph;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.IO.Font.Constants;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System;
using System.IO;

namespace TourPlannerUi.Services
{
    public interface IGeneratePdfService {
        void create(Tour tour);
        void create(ObservableCollection<Tour> tours);
    }

    public class GeneratePdf : IGeneratePdfService {
        
        private const string path = "..\\..\\..\\..\\generatedPdf";
        public void create(Tour tour) {
            string fullPath = Path.Combine(path,$"{tour.Name}_tourLog.pdf");   

            PdfWriter writer = new(fullPath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            LineSeparator separator = new LineSeparator(new SolidLine());
            

            Paragraph tourNameParagraph = new Paragraph($"{tour.Name}")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                .SetFontSize(16f)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginTop(0f);
            document.Add(tourNameParagraph);
            document.Add(separator);
            
            foreach (TourLog log in tour.TourLogs) {
                
                document.Add(new Paragraph($"Tour: {log.Id}"));
                document.Add(new Paragraph($"Date: {log.Date}"));
                document.Add(new Paragraph($"Difficulty: {log.Difficulty}"));
                document.Add(new Paragraph($"Duration: {log.Duration}"));
                document.Add(new Paragraph($"Rating: {log.Rating}"));
                document.Add(new Paragraph($"Comment: {log.Comment}"));

                if(log != tour.TourLogs.Last()) {
                    document.Add(separator);
                }
            }

            if (!string.IsNullOrEmpty(tour.MapImageUrl)) {
                Image image = new Image(ImageDataFactory.Create(tour.MapImageUrl));

                image.SetWidth(document.GetPdfDocument().GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin()); 
                image.SetProperty(Property.FLOAT, FloatPropertyValue.LEFT);
                image.SetMarginRight(10f);

                document.Add(image);
            }

            document.Close();
        }

        public void create(ObservableCollection<Tour> tours) {
           
            string fullPath = Path.Combine(path, $"SumReport_{DateTime.Now.ToString("yyyyMMdd")}.pdf");

            PdfWriter writer = new(fullPath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            LineSeparator separator = new LineSeparator(new SolidLine());
            Paragraph separatorParagraph = new Paragraph().Add(separator);
   
            StringBuilder title = new StringBuilder("Summarize-Report of: ");
            title.Append(string.Join(", ", tours.Select(x => x.Name)));

            Paragraph tourTitleParagraph = new Paragraph(title.ToString())
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                .SetFontSize(16f)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginTop(0f); 
            document.Add(tourTitleParagraph);

            foreach (Tour tour in tours) {

                document.Add(new Paragraph($"{tour.Name}").SetBold());
                
                if (!string.IsNullOrEmpty(tour.MapImageUrl)) {
                    Image image = new Image(ImageDataFactory.Create(tour.MapImageUrl));

                    image.SetWidth(document.GetPdfDocument().GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin());
                    image.SetProperty(Property.FLOAT, FloatPropertyValue.LEFT);
                    image.SetMarginRight(10f);

                    document.Add(image);
                }

                foreach (TourLog log in tour.TourLogs) {
                    document.Add(new Paragraph($"Date: {log.Date}"));
                    document.Add(new Paragraph($"Difficulty: {log.Difficulty}"));
                    document.Add(new Paragraph($"Duration: {log.Duration}"));
                    document.Add(new Paragraph($"Rating: {log.Rating}"));
                    document.Add(new Paragraph($"Comment: {log.Comment}"));
                    document.Add(separator);
                }

                double averageDuration = tour.TourLogs.Average(log => log.Duration.TotalMilliseconds);
                double averageRating = tour.TourLogs.Average(log => (int)log.Rating);

                document.Add(new Paragraph($"Average Time: {TimeSpan.FromMilliseconds(averageDuration)}").SetBold());
                document.Add(new Paragraph($"Average Rating: {(Rating)averageRating}").SetBold());

                if (tour != tours.Last()) {
                    document.Add(new AreaBreak());
                }

            }

            document.Close();

        }
    }
}
