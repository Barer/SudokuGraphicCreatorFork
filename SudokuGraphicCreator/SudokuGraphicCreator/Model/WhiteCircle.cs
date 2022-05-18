﻿using System.Windows.Media;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents white circle placed between two cells.
    /// </summary>
    public class WhiteCircle : Circle
    {
        /// <summary>
        /// Initializes a new instance of <see cref="WhiteCircle"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance form left up corner of grid.</param>
        /// <param name="rowIndex">Index of row in sudoku table.</param>
        /// <param name="colIndex">Index of column in sudoku table.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="location">Type of location in table.</param>
        public WhiteCircle(double left, double top, int rowIndex, int colIndex, SudokuElementType type, ElementLocationType location)
        {
            Left = left;
            Top = top;
            RowIndex = rowIndex;
            ColIndex = colIndex;
            FillColor = Brushes.White;
            BorderColor = Brushes.Black;
            SudokuElemType = type;
            Location = location;
        }
    }
}
