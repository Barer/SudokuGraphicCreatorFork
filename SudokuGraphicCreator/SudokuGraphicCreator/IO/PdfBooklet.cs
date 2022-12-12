using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.BookletLayout;

namespace SudokuGraphicCreator.IO
{
    /// <summary>
    /// Creates booklet in PDF format and save on disk.
    /// </summary>
    public class PdfBooklet
    {
        /// <summary>
        /// Export booklet into PDF and save on disk.
        /// </summary>
        /// <param name="fileName">Name of file.</param>
        public void CreatedPdf(string fileName)
        {
            var booklet = BookletStore.Instance.Booklet;

            using var writer = new PdfWriter(fileName);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            pdf.SetDefaultPageSize(PageSize.A4);

            var titlePageLayout = new BookletTitlePageLayoutFactory().GetLayout(booklet);
            titlePageLayout.InsertPage(document, booklet);

            for (var i = 0; i < booklet.Pages.Count; i++)
            {
                var page = booklet.Pages[i];

                if (page.SudokuOnPage.Count > 0)
                {
                    var puzzlePageLayout = new BookletPuzzlePageLayoutFactory().GetLayout(page);
                    puzzlePageLayout.InsertPage(document, page, i + 2);
                }
            }
        }
    }
}
