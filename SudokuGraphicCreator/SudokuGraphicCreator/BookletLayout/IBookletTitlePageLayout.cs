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
    /// Layout for title page of booklet.
    /// </summary>
    public interface IBookletTitlePageLayout
    {
        /// <summary>
        /// Inserts a title page to PDF puzzle booklet.
        /// </summary>
        /// <param name="document">Document for PDF export.</param>
        /// <param name="booklet">Puzzle booklet.</param>
        void InsertPage(Document document, Booklet booklet);
    }
}
