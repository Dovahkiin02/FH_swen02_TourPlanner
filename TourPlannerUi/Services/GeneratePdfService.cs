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

namespace TourPlannerUi.Services
{
    public interface IGeneratePdfService
    {
        void create(Tour tour);
    }

    public class GeneratePdf : IGeneratePdfService
    {
        public void create(Tour tour)
        {
            string outputPath = $"..\\..\\..\\..\\generatedPdf\\{tour.Name}_tourLog.pdf";

            PdfWriter writer = new(outputPath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            LineSeparator separator = new LineSeparator(new SolidLine());
            Paragraph separatorParagraph = new Paragraph().Add(separator);

            Paragraph tourNameParagraph = new Paragraph($"Tour: {tour.Name}")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                .SetFontSize(16f)
                .SetMarginTop(0f); // Setze den oberen Rand der Überschrift auf 0
            document.Add(tourNameParagraph);

            foreach (TourLog log in tour.TourLogs)
            {
                document.Add(new Paragraph($"Tour Id: {log.Id}"));
                document.Add(new Paragraph($"Tour Date: {log.Date}"));
                document.Add(new Paragraph($"Tour Difficulty: {log.Difficulty}"));
                document.Add(new Paragraph($"Tour Duration: {log.Duration}"));
                document.Add(new Paragraph($"Tour Rating: {log.Rating}"));
                document.Add(new Paragraph($"Tour Comment: {log.Comment}"));

                document.Add(separator);
            }


            if (!string.IsNullOrEmpty(tour.MapImageUrl))
            {
                document.Add(new Paragraph("Map:"));
                Image image = new Image(ImageDataFactory.Create(tour.MapImageUrl));

                // Setze die Breite des Bildes auf die Breite des Seiteninhalts
                image.SetWidth(document.GetPdfDocument().GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin());

                // Platzierungsoptionen für das Bild
                image.SetProperty(Property.FLOAT, FloatPropertyValue.LEFT);
                image.SetMarginRight(10f);

                document.Add(image);
            }


            document.Close();
        }
    }
}
