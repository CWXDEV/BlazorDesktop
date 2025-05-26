using System.Runtime.InteropServices;
using BlazorDesktop.Window.Models;

namespace BlazorDesktop.Window.API;

public class DWM
{
    [DllImport("dwmapi.dll")]
    public extern static int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
}
