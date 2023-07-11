using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using TourPlannerUi.Models;
using iText.IO.Image;
using iText.Kernel.Font;
using Document = iText.Layout.Document;
using Paragraph = iText.Layout.Element.Paragraph;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.IO.Font.Constants;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Text;

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

            StringBuilder title = new StringBuilder("Average from: ");

            foreach(var x in tours)
            {
                title.Append($"{x.Name} - ");
            }

            Paragraph tourTitleParagraph = new Paragraph(title.ToString());
            document.Add(tourTitleParagraph);

            foreach (var x in tours)
            {
                foreach (var y in x.TourLogs)
                {
                    Debug.Write($"{y.Id} ");

                }
            }

            document.Close();


        }
    }
}
