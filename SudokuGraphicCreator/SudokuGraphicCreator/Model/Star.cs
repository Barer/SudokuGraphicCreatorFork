﻿using System.Windows.Media;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents graphic element in shape of star in <see cref="Brushes.DarkGray"/> color.
    /// </summary>
    public class Star : OneSpaceElement
    {
        /// <summary>
        /// Color of star.
        /// </summary>
        public Brush FillColor { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Star"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="rowIndex">Index of row in sudoku table.</param>
        /// <param name="colIndex">Index of column in sudoku table.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="location">Location on grid.</param>
        public Star(double left, double top, int rowIndex, int colIndex, SudokuElementType type, ElementLocationType location)
        {
            Left = left;
            Top = top;
            RowIndex = rowIndex;
            ColIndex = colIndex;
            FillColor = Brushes.DarkGray;
            SudokuElemType = type;
            Location = location;
        }
    }
}
