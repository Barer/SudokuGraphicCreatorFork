using iText.Kernel.Pdf;
using iText.Layout;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.BookletLayout;

namespace SudokuGraphicCreator.IO
{
    /// <summary>
    /// Creates file with solution of booklet in PDF format and save on disk.
    /// </summary>
    public class PdfSolution
    {
        /// <summary>
        /// Creates file with solution of booklet in PDF format and save on disk.
        /// </summary>
        /// <param name="name">Name of file.</param>
        public void CreatePdfWithSolutions(string name)
        {
            var booklet = BookletStore.Instance.Booklet;

            using var writer = new PdfWriter(name);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            var layout = new BookletSolutionLayoutFactory().GetLayout(booklet);

            layout.InsertSolutionPages(document, booklet);
        }
    }
}
