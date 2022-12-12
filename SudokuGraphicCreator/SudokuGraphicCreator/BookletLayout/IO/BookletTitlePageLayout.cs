using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Extensions.Primitives;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Properties.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGraphicCreator.BookletLayout.IO
{
    /// <summary>
    /// Detault layout for booklet title page.
    /// </summary>
    public class BookletTitlePageLayout : IBookletTitlePageLayout
    {
        private const float OneUnitIText = 2.83F;

        #region Configurable layout parameters
        public float Logo1Left { get; set; } = 10 * OneUnitIText;
        public float Logo1Bottom { get; set; } = 257 * OneUnitIText;
        public float Logo2Left { get; set; } = 170 * OneUnitIText;
        public float Logo2Bottom { get; set; } = 257 * OneUnitIText;
        public float Logo3Left { get; set; } = 10 * OneUnitIText;
        public float Logo3Bottom { get; set; } = 257 * OneUnitIText;
        public float LogoSize { get; set; } = 30 * OneUnitIText;
        public float TournamentNameLeft { get; set; } = 40 * OneUnitIText;
        public float TournamentNameBottom { get; set; } = 257 * OneUnitIText;
        public float TournamentNameWidth { get; set; } = 120 * OneUnitIText;
        public float InfoLeft { get; set; } = 10 * OneUnitIText;
        public float InfoBottom { get; set; } = 207 * OneUnitIText;
        public float InfoWidth { get; set; } = 190 * OneUnitIText;
        public PdfFont BoldFont { get; set; } = PdfFontFactory.CreateFont("freesansbold.ttf", PdfEncodings.IDENTITY_H);
        public PdfFont Font { get; set; } = PdfFontFactory.CreateFont("freesans.ttf", PdfEncodings.IDENTITY_H);
        public float TournamentNameFontSize { get; set; } = 18;
        public float TextFontSize { get; set; } = 10;
        #endregion

        /// <inheritdoc />
        public void InsertPage(Document document, Booklet booklet)
        {
            var pdf = document.GetPdfDocument();

            var pdfPage = pdf.AddNewPage(new PageSize(PageSize.A4));
            var pageNumber = 1;

            var informationText = CreateInformationText(booklet);

            PlaceLogo(document, booklet.Logo1FullPath(), pageNumber, Logo1Left, Logo1Bottom, LogoSize, LogoSize);
            PlaceLogo(document, booklet.Logo2FullPath(), pageNumber, Logo2Left, Logo2Bottom, LogoSize, LogoSize);
            PlaceLogo(document, booklet.Logo3FullPath(), pageNumber, Logo3Left, Logo3Bottom, LogoSize, LogoSize);
            PdfExportHelper.InsertParagraph(document, booklet.TournamentName, BoldFont, TournamentNameFontSize, TextAlignment.CENTER, pageNumber, TournamentNameLeft, TournamentNameBottom, TournamentNameWidth);
            PdfExportHelper.InsertParagraph(document, informationText, Font, TextFontSize, TextAlignment.CENTER, pageNumber, InfoLeft, InfoBottom, InfoWidth);
            PlacePuzzleList(document, booklet, pageNumber);
            PlaceNameAndPoints(document, pageNumber);
        }

        private void PlaceNameAndPoints(Document document, int pageNumber)
        {
            const string emptyLine = "........................................................................";

            var table = new Table(2)
                .SetBorder(Border.NO_BORDER)
                .SetFixedPosition(pageNumber, 70 * OneUnitIText, 20 * OneUnitIText, 50 * OneUnitIText)
                    .AddCell(new Cell().Add(new Paragraph(Resources.NameOfPlayer).SetFont(Font).SetFontSize(TextFontSize)).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER))
                    .AddCell(new Cell().Add(new Paragraph(emptyLine).SetFont(Font).SetFontSize(TextFontSize)).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER))
                    .AddCell(new Cell().Add(new Paragraph(Resources.PointsOfPlayer).SetFont(Font).SetFontSize(TextFontSize)).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER))
                    .AddCell(new Cell().Add(new Paragraph(emptyLine).SetFont(Font).SetFontSize(TextFontSize)).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));

            document.Add(table);
        }

        private void PlacePuzzleList(Document document, Booklet booklet, int pageNumber)
        {
            var table = new Table(3)
                .SetBorder(Border.NO_BORDER)
                .SetFixedPosition(pageNumber, 80 * OneUnitIText, InfoBottom - (booklet.PuzzleCount() * TextFontSize * OneUnitIText), 50 * OneUnitIText);

            var order = 1;
            foreach (var puzzle in booklet.Puzzles())
            {
                table
                    .AddCell(new Cell().Add(new Paragraph($"{order}.").SetFont(Font).SetFontSize(TextFontSize)).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER))
                    .AddCell(new Cell().Add(new Paragraph(puzzle.Name).SetFont(Font).SetFontSize(TextFontSize)).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER))
                    .AddCell(new Cell().Add(new Paragraph($"{puzzle.Points} b.").SetFont(Font).SetFontSize(TextFontSize)).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER)); // TODO: localization
                order++;
            }

            document.Add(table);

        }

        private static string CreateInformationText(Booklet booklet)
        {
            return new StringBuilder()
                .Append(booklet.TournamentDate.ToShortDateString())
                .Append(", ")
                .Append(booklet.Location)
                .AppendLine()
                .AppendLine()
                .Append(booklet.RoundNumber)
                .Append(". ")
                .AppendLine(Resources.Round)
                .AppendLine(booklet.RoundNumber)
                .AppendLine()
                .Append(Resources.TimeForSolving)
                .Append(" ")
                .Append(booklet.TimeForSolving)
                .Append("         ")
                .Append(Resources.TotalPoints)
                .Append(booklet.TotalPoints.ToString())
                .ToString();

        }

        private static void PlaceLogo(Document document, string path, int pageNumber, float left, float bottom, float width, float height)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            PdfExportHelper.InsertImage(document, path, pageNumber, left, bottom, width, height);
        }
    }

    internal static class BookletPropertyHelper
    {
        public static string Logo1FullPath(this Booklet booklet)
        {
            return booklet.LogoOneFullPath;
        }

        public static string Logo2FullPath(this Booklet booklet)
        {
            return booklet.LogoTwoFullPath;
        }

        public static string Logo3FullPath(this Booklet booklet)
        {
            return booklet.LogoThreeFullPath;
        }

        public static int PuzzleCount(this Booklet booklet)
        {
            return booklet?.Pages?.Sum(page => page?.SudokuOnPage?.Count) ?? 0;
        }

        public static IEnumerable<SudokuInBooklet> Puzzles(this Booklet booklet)
        {
            return booklet?.Pages?.SelectMany(page => page?.SudokuOnPage ?? Enumerable.Empty<SudokuInBooklet>()) ?? Enumerable.Empty<SudokuInBooklet>();
        }
    }
}
