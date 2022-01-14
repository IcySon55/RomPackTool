using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RomPackTool.WinForms
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
    }
}
