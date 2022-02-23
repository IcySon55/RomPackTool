using System.Drawing;
using System.Drawing.Drawing2D;

namespace RomPackTool.Core
{
    /// <summary>
    /// Helper extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Aligns the given value to the alignment.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static int Align(this int value, int alignment = 16) => value + (value % alignment <= 0 ? 0 : alignment - (value % alignment));

        /// <summary>
        /// Aligns the given value to the alignment.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static long Align(this long value, int alignment = 16) => value + (value % alignment <= 0 ? 0 : alignment - (value % alignment));

        /// <summary>
        /// Convert a number to a human readable file size.
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static string ToSizeString(this long fileSize)
        {
            var sizes = new[] { "B", "KB", "MB", "GB", "TB" };
            var size = (double)fileSize;
            int order = 0;
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return string.Format("{0:0.#}{1}", size, sizes[order]);
        }

        /// <summary>
        /// Produce a decent quality thumbnail of the icon.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetThumbnailImageHQ(this Image img, int width, int height)
        {
            Bitmap thumbnail = new(width, height);
            using var gfx = Graphics.FromImage(thumbnail);
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gfx.DrawImage(img, new Rectangle(0, 0, width, height));
            return thumbnail;
        }
    }
}
