using System.Runtime.InteropServices;

namespace BlazorDesktop.Window.Models;

[StructLayout(LayoutKind.Sequential)]
public class BLURBEHIND
{
    public int dwFlags;
    public bool fEnable;
    public IntPtr hRgnBlur;
    public bool fTransitionOnMaximized;
}
