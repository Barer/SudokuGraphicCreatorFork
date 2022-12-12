using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font;
using iText.Layout.Properties;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.Properties.Resources;
using System.Text;
using System.IO;
using iText.Svg.Converter;
using System;

namespace SudokuGraphicCreator.BookletLayout.IO
{
    /// <summary>
    /// Helper methods for PDF export.
    /// </summary>
    public static class PdfExportHelper
    {
        /// <summary>
        /// Places image to PDF document.
        /// </summary>
        /// <inheritdoc cref="InsertImageDelegate" />
        public static void InsertImage(Document document, string imagePath, int pageNumber, float left, float bottom, float width, float height)
        {
            GetInsertImageMethod(imagePath).Invoke(document, imagePath, pageNumber, left, bottom, width, height);
        }

        /// <summary>
        /// Method that inserts image to PDF document.
        /// </summary>
        /// <param name="document">Document.</param>
        /// <param name="imagePath">Image source file path.</param>
        /// <param name="pageNumber">Number of page where the image is being inserted.</param>
        /// <param name="left">Position of image from left.</param>
        /// <param name="bottom">Position of image from bottom.</param>
        /// <param name="width">Width of image.</param>
        /// <param name="height">Height of image.</param>
        /// <exception cref="Exception">If any error occurs.</exception>
        delegate void InsertImageDelegate(Document document, string imagePath, int pageNumber, float left, float bottom, float width, float height);

        /// <summary>
        /// Factory for <see cref="InsertImageDelegate"/>.
        /// </summary>
        /// <param name="imagePath">Image source file path.</param>
        /// <returns>Method for inserting image.</returns>
        private static InsertImageDelegate GetInsertImageMethod(string imagePath)
        {
            if (imagePath.EndsWith(".svg"))
            {
                return PlaceSvgImage;
            }

            return PlaceBitmapImage;
        }

        /// <summary>
        /// Places bitmap image to PDF document.
        /// </summary>
        /// <inheritdoc cref="InsertImageDelegate" />
        public static void PlaceBitmapImage(Document document, string imagePath, int pageNumber, float left, float bottom, float width, float height)
        {
            try
            {
                var imgData = ImageDataFactory.Create(imagePath);
                var img = new Image(imgData)
                    .SetHeight(height)
                    .SetWidth(width)
                    .SetFixedPosition(pageNumber, left, bottom);
                var paragraph = new Paragraph()
                    .Add(img);
                document.Add(paragraph);
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot export image with path: {imagePath}", ex);
            }
        }

        /// <summary>
        /// Places SVG image to PDF document.
        /// </summary>
        /// <inheritdoc cref="InsertImageDelegate" />
        public static void PlaceSvgImage(Document document, string imagePath, int pageNumber, float left, float bottom, float width, float height)
        {
            try
            {
                using (var stream = File.Open(imagePath, FileMode.Open))
                {
                    var img = SvgConverter.ConvertToImage(stream, document.GetPdfDocument());
                    img.SetFixedPosition(pageNumber, left, bottom)
                       .ScaleAbsolute(width, height);
                    document.Add(img);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot export image with path: {imagePath}", ex);
            }
        }
    }
}
