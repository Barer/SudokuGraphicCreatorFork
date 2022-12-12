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
    /// Layout for solution booklet.
    /// </summary>
    public interface IBookletSolutionLayout
    {
        /// <summary>
        /// Generates PDF puzzle solution booklet.
        /// </summary>
        /// <param name="document">Document for PDF export.</param>
        /// <param name="booklet">Puzzle booklet.</param>
        void InsertSolutionPages(Document document, Booklet booklet);
    }
}
