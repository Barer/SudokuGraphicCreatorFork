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
    /// Detault layout for booklet puzzle page.
    /// </summary>
    public class BookletPuzzlePageLayout : IBookletPuzzlePageLayout
    {
        private const float OneUnitIText = 2.83F;
        private const float CellSizePaper = 12 * 9;
        private const float HalfA4Width = 148.5F;

        #region Configurable layout parameters
        public float FirstGridLeftMargin { get; set; } = 20;       
        public float SecondGridLeftmargin { get; set; } = HalfA4Width + 5.5F;
        public float SudokuTableSizeIText { get; set; } = OneUnitIText * CellSizePaper;
        public float SudokuNameBottom { get; set; } = 185 * OneUnitIText;
        public float TextLeftWithoutMargin { get; set; } = 0;
        public float TextWidth { get; set; } = 123 * OneUnitIText;
        public float RulesBottom { get; set; } = 133 * OneUnitIText;
        public float RulesHeight { get; set; } = 52 * OneUnitIText;
        public float ImageLeftWithoutMargin { get; set; } = 7.5F;
        public float ImageBottom { get; set; } = 25 * OneUnitIText;
        public PdfFont BoldFont { get; set; } = PdfFontFactory.CreateFont("freesansbold.ttf", PdfEncodings.IDENTITY_H);
        public PdfFont Font { get; set; } = PdfFontFactory.CreateFont("freesans.ttf", PdfEncodings.IDENTITY_H);
        #endregion

        /// <inheritdoc />
        public void InsertPage(Document document, BookletPage bookletPage, int pageNumber)
        {
            var pdf = document.GetPdfDocument();

            var pdfPage = pdf.AddNewPage(new PageSize(PageSize.A4).Rotate());

            var bookletPageSudokus = bookletPage.SudokuOnPage;

            if (bookletPageSudokus.Count >= 1)
            {
                InsertSudoku(document, bookletPageSudokus[0], true, pageNumber);

                if (bookletPageSudokus.Count >= 2)
                {
                    InsertSudoku(document, bookletPageSudokus[1], false, pageNumber);
                }
            }
        }

        private void InsertSudoku(Document document, SudokuInBooklet sudokuInBooklet, bool isFirst, int pageNumber)
        {
            var gridLeftMargin = isFirst ? FirstGridLeftMargin : SecondGridLeftmargin;

            var rulesBottom = RulesBottom;
            var nameBottom = SudokuNameBottom;
            var textLeft = (TextLeftWithoutMargin + gridLeftMargin) * OneUnitIText;
            var textWidth = TextWidth;

            var hasOutsideClues = HasOutsideClues(sudokuInBooklet);

            var tableLeft = (ImageLeftWithoutMargin + gridLeftMargin) * OneUnitIText + (hasOutsideClues ? -10 * OneUnitIText : 0);
            var tableBottom = ImageBottom + (hasOutsideClues ? -10 * OneUnitIText : 0);
            var tableSize = SudokuTableSizeIText + (hasOutsideClues ? 20 * OneUnitIText : 0);

            InsertRules(document, sudokuInBooklet, pageNumber, textLeft, rulesBottom, textWidth);
            InsertSudokuName(document, sudokuInBooklet, pageNumber, textLeft, nameBottom, textWidth);
            PdfExportHelper.InsertImage(document, sudokuInBooklet.TableFullPath, pageNumber, tableLeft, tableBottom, tableSize, tableSize);
        }

        private void InsertSudokuName(Document document, SudokuInBooklet sudoku, int pageNumber, float left, float bottom, float width)
        {
            document.Add(new Paragraph(sudoku.GetNameWithPoints())
                .SetFont(BoldFont)
                .SetFontSize(12)
                .SetFixedPosition(pageNumber, left, bottom, width));
        }

        private void InsertRules(Document document, SudokuInBooklet sudoku, int pageNumber, float left, float bottom, float width)
        {
            document.Add(new Paragraph(sudoku.Rules)
                    .SetFont(Font)
                    .SetFontSize(10)
                    .SetHeight(RulesHeight)
                    .SetWidth(width)
                    .SetFixedPosition(pageNumber, left, bottom, width)
                    .SetTextAlignment(TextAlignment.JUSTIFIED));
        }

        private static bool HasOutsideClues(SudokuInBooklet sudoku)
        {
            var sudokuName = sudoku.Name;
            return sudokuName.Contains(Resources.SudokuOutside) || Resources.SudokuOutside.Contains(sudokuName) ||
                   sudokuName.Contains(Resources.SudokuSkyscrapers) || Resources.SudokuSkyscrapers.Contains(sudokuName) ||
                   sudokuName.Contains(Resources.SudokuNextToNine) || Resources.SudokuNextToNine.Contains(sudokuName) ||
                   sudokuName.Contains(Resources.SudokuLittleKiller) || Resources.SudokuLittleKiller.Contains(sudokuName);
        }
    }
}
