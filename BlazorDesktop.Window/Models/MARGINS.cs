using System.Runtime.InteropServices;

namespace BlazorDesktop.Window.Models;

[StructLayout(LayoutKind.Sequential)]
public struct MARGINS
{
    public int cxLeftWidth;
    public int cxRightWidth;
    public int cyTopHeight;
    public int cyBottomHeight;
}

