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
using System.Diagnostics;

namespace TourPlannerUi.Services
{
    public interface IGenerateSumPdfService
    {
        void create(ObservableCollection<Tour> tours);
    }

    public class GenerateSumPdf : IGenerateSumPdfService
    {
        public void create(ObservableCollection<Tour> tours)
        {
            string outputPath = $"..\\..\\..\\..\\generatedPdf\\{tours[0].Name}_tourSumLog.pdf";

            PdfWriter writer = new(outputPath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

             LineSeparator separator = new LineSeparator(new SolidLine());
            Paragraph separatorParagraph = new Paragraph().Add(separator);

            StringBuilder title = new StringBuilder("Average from: ");

            foreach(var x in tours)
            {
                title.Append($"{x.Name} - ");
            }

            Paragraph tourTitleParagraph = new Paragraph(title.ToString())
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                .SetFontSize(16f)
                .SetMarginTop(0f); // Setze den oberen Rand der Überschrift auf 0
            document.Add(tourTitleParagraph);



            foreach (var x in tours)
            {
                if (!string.IsNullOrEmpty(x.MapImageUrl))
                {
                    document.Add(new Paragraph("Map:"));
                    Image image = new Image(ImageDataFactory.Create(x.MapImageUrl));

                    // Setze die Breite des Bildes auf die Breite des Seiteninhalts
                    image.SetWidth(document.GetPdfDocument().GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin());

                    // Platzierungsoptionen für das Bild
                    image.SetProperty(Property.FLOAT, FloatPropertyValue.LEFT);
                    image.SetMarginRight(10f);

                    document.Add(image);           
                }
                
                
                foreach (var y in x.TourLogs)
                {
                    document.Add(new Paragraph($"Tour Id: {y.Id}"));
                    document.Add(new Paragraph($"Tour Date: {y.Date}"));
                    document.Add(new Paragraph($"Tour Difficulty: {y.Difficulty}"));
                    document.Add(new Paragraph($"Tour Duration: {y.Duration}"));
                    document.Add(new Paragraph($"Tour Rating: {y.Rating}"));
                    document.Add(new Paragraph($"Tour Comment: {y.Comment}"));
                }

                document.Add(separator);
            }

            document.Close();


        }
    }
}
