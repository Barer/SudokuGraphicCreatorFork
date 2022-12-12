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
    /// Factory for <see cref="IBookletTitlePageLayout"/>.
    /// </summary>
    public class BookletTitlePageLayoutFactory
    {
        /// <summary>
        /// Gets layout for puzzle booklet title page.
        /// </summary>
        /// <param name="booklet">Puzzle booklet.</param>
        /// <returns>Booklet title page layout.</returns>
        public IBookletTitlePageLayout GetLayout(Booklet booklet)
        {
            // Only one layout available for now
            return new BookletTitlePageLayout();
        }
    }
}
