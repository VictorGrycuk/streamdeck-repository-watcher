using System.Drawing;

namespace RepositoryWatcher.Helpers
{
    internal static class ImageHelper
    {
        internal static Bitmap WriteOnImage(Bitmap bmp, string text, Point textLocation, Point maxDimension)
        {
            var font = new Font("Consolas", 142, FontStyle.Bold);
            var stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            using (var graphics = Graphics.FromImage(bmp))
            {
                font = ResizeFont(graphics, text, font, maxDimension);
                graphics.DrawString(text, font, Brushes.White, textLocation, stringFormat);
            }

            return bmp;
        }

        private static Font ResizeFont(Graphics graphics, string text, Font font, Point maxDimension)
        {
            var newSize = graphics.MeasureString(text, font);

            if (newSize.Width > maxDimension.X || newSize.Height > maxDimension.Y)
            {
                return ResizeFont(graphics, text, new Font("Consolas", font.Size - 2, FontStyle.Bold, GraphicsUnit.Pixel), maxDimension);
            }

            return font;
        }
    }
}
