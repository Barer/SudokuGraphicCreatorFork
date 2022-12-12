using SudokuGraphicCreator.BookletLayout.IO;
using SudokuGraphicCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGraphicCreator.BookletLayout
{
    /// <summary>
    /// Factory for <see cref="IBookletPuzzlePageLayout"/>.
    /// </summary>
    public class BookletPuzzlePageLayoutFactory
    {
        /// <summary>
        /// Gets layout for puzzle booklet page.
        /// </summary>
        /// <param name="bookletPage">Puzzle booklet page.</param>
        /// <returns>Booklet puzzle page layout.</returns>
        public IBookletPuzzlePageLayout GetLayout(BookletPage bookletPage)
        {
            // Only one layout available for now
            return new BookletPuzzlePageLayout();
        }
    }
}
