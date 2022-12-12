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
    /// Factory for <see cref="IBookletSolutionLayout"/>.
    /// </summary>
    public class BookletSolutionLayoutFactory
    {
        /// <summary>
        /// Gets layout for puzzle booklet solution.
        /// </summary>
        /// <param name="booklet">Puzzle booklet.</param>
        /// <returns>Booklet solution layout.</returns>
        public IBookletSolutionLayout GetLayout(Booklet booklet)
        {
            // Only one layout available for now
            return new BookletSolutionLayout();
        }
    }
}
