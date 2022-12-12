using iText.Layout;
using SudokuGraphicCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGraphicCreator.BookletLayout
{
    /// <summary>
    /// Layout for puzzle page of booklet.
    /// </summary>
    public interface IBookletPuzzlePageLayout
    {
        /// <summary>
        /// Inserts a page with puzzles to PDF puzzle booklet.
        /// </summary>
        /// <param name="document">Document for PDF export.</param>
        /// <param name="bookletPage">Puzzle booklet page.</param>
        /// <param name="pageNumber">Page number of currently inserted page.</param>
        void InsertPage(Document document, BookletPage bookletPage, int pageNumber);
    }
}
