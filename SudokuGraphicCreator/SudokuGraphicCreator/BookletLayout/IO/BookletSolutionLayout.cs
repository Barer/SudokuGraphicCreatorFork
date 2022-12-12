using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Properties;
using SudokuGraphicCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGraphicCreator.BookletLayout.IO
{
    /// <summary>
    /// Default layout for solution page.
    /// </summary>
    public class BookletSolutionLayout : IBookletSolutionLayout
    {
        private const float OneUnitIText = 2.83F;

        #region Configurable layout parameters
        public PdfFont BoldFont { get; set; } = PdfFontFactory.CreateFont("freesansbold.ttf", PdfEncodings.IDENTITY_H);
        public float RoundNameBottom { get; set; } = 277 * OneUnitIText;
        public float RoundNameLeft { get; set; } = 20 * OneUnitIText;
        public float RoundNameWidth { get; set; } = 200 * OneUnitIText;
        public float SudokuTableSize { get; set; } = 55 * OneUnitIText;
        public float SudokuTableLeft { get; set; } = 10 * OneUnitIText;
        public float RoundNameFontSize { get; set; } = 12;
        public float SolutionInfoFontSize { get; set; } = 9;
        #endregion

        /// <inheritdoc />
        public void InsertSolutionPages(Document document, Booklet booklet)
        {
            var pdfDocument = document.GetPdfDocument();
            pdfDocument.SetDefaultPageSize(PageSize.A4);

            PdfExportHelper.InsertParagraph(document, $"{booklet.RoundNumber}. {booklet.RoundName}", BoldFont, RoundNameFontSize, TextAlignment.LEFT, 1, RoundNameLeft, RoundNameBottom, RoundNameWidth);
            PlaceSolutions(document, booklet.RoundNumber, booklet.Puzzles().GetEnumerator());
        }

        private void PlaceSolutions(Document document, string roundNumber, IEnumerator<SudokuInBooklet> puzzleEnumerator)
        {
            var order = 0;

            for (var pageNumber = 1; ; pageNumber++)
            {
                for (var row = 0; row < 4; row++)
                {
                    for (var col = 0; col < 3; col++)
                    {
                        if (!puzzleEnumerator.MoveNext())
                        {
                            return;
                        }
                        order++;

                        var puzzle = puzzleEnumerator.Current;

                        var left = col * (SudokuTableLeft + SudokuTableSize) + SudokuTableLeft;
                        var textBottom = (10 * OneUnitIText + SudokuTableSize) * (4 - row);
                        var tableBottom = textBottom - SudokuTableSize;

                        PdfExportHelper.InsertParagraph(document, $"{roundNumber}.{order} {puzzle}", BoldFont, SolutionInfoFontSize, TextAlignment.LEFT, pageNumber, left, textBottom, SudokuTableSize);
                        PdfExportHelper.InsertImage(document, puzzle.SolutionFullPath, pageNumber, left, tableBottom, SudokuTableSize, SudokuTableSize);
                    }
                }
            }
        }
    }
}
