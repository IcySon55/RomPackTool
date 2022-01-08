using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RomPackTool
{
    /// <summary>
    /// Helper extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Allows us to send messages the old fashion way.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="w"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);

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
        /// Sets the state of the progress bar. (Changes bar color.)
        /// </summary>
        /// <param name="pbr"></param>
        /// <param name="state"></param>
        public static void SetState(this ProgressBar pbr, ProgressBarStyle state)
        {
            try
            {
                SendMessage(pbr.Handle, 1040, (IntPtr)state, IntPtr.Zero);
            }
            catch (Exception) { }
        }

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
    }
}
