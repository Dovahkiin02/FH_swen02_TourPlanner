﻿using iText.Kernel.Pdf;
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

namespace TourPlannerUi.Services
{
    public interface IGeneratePdfService
    {
        void create(Tour tour);
        void create(ObservableCollection<Tour> tours);
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
                .SetMarginTop(0f);
            document.Add(tourNameParagraph);

            foreach (TourLog log in tour.TourLogs)
            {
                document.Add(new Paragraph($"Tour: {log.Id}"));
                document.Add(new Paragraph($"Date: {log.Date}"));
                document.Add(new Paragraph($"Difficulty: {log.Difficulty}"));
                document.Add(new Paragraph($"Duration: {log.Duration}"));
                document.Add(new Paragraph($"Rating: {log.Rating}"));
                document.Add(new Paragraph($"Comment: {log.Comment}"));

                document.Add(separator);
            }


            if (!string.IsNullOrEmpty(tour.MapImageUrl))
            {
                document.Add(new Paragraph("Map:"));
                Image image = new Image(ImageDataFactory.Create(tour.MapImageUrl));

                image.SetWidth(document.GetPdfDocument().GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin()); 
                image.SetProperty(Property.FLOAT, FloatPropertyValue.LEFT);
                image.SetMarginRight(10f);

                document.Add(image);
            }


            document.Close();
        }

        public void create(ObservableCollection<Tour> tours)
        {
            string outputPath = $"..\\..\\..\\..\\generatedPdf\\{tours[0].Name}_tourSumLog.pdf";

            PdfWriter writer = new(outputPath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            LineSeparator separator = new LineSeparator(new SolidLine());
            Paragraph separatorParagraph = new Paragraph().Add(separator);

            StringBuilder title = new StringBuilder("Average from: ");

            foreach (var x in tours)
            {
                title.Append($"{x.Name} - ");
            }

            Paragraph tourTitleParagraph = new Paragraph(title.ToString())
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                .SetFontSize(16f)
                .SetMarginTop(0f); 
            document.Add(tourTitleParagraph);



            foreach (var x in tours)
            {
                if (!string.IsNullOrEmpty(x.MapImageUrl))
                {
                    document.Add(new Paragraph("Map:"));
                    Image image = new Image(ImageDataFactory.Create(x.MapImageUrl));

                    image.SetWidth(document.GetPdfDocument().GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin());
                    image.SetProperty(Property.FLOAT, FloatPropertyValue.LEFT);
                    image.SetMarginRight(10f);

                    document.Add(image);
                }


                foreach (var y in x.TourLogs)
                {
                    document.Add(new Paragraph($"Tour: {y.Id}"));
                    document.Add(new Paragraph($"Date: {y.Date}"));
                    document.Add(new Paragraph($"Difficulty: {y.Difficulty}"));
                    document.Add(new Paragraph($"Duration: {y.Duration}"));
                    document.Add(new Paragraph($"Rating: {y.Rating}"));
                    document.Add(new Paragraph($"Comment: {y.Comment}"));
                }

                document.Add(separator);
            }

            document.Close();


        }
    }
}
