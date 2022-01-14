using System;

namespace RomPackTool.Core.PSV
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum PsvFlags : int
    {
        FLAG_NONE = 0,
        FLAG_TRIMMED = 1,
        FLAG_DIGITAL = 2,
        FLAG_COMPRESSED = 4,
        PLAG_NOINTRO = 8
    }
}
