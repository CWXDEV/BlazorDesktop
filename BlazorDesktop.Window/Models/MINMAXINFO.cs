using System.Runtime.InteropServices;

namespace BlazorDesktop.Window.Models;

[StructLayout(LayoutKind.Sequential)]
public record MINMAXINFO
{
    public POINT ptReserved;
    public POINT ptMaxSize;
    public POINT ptMaxPosition;
    public POINT ptMinTrackSize;
    public POINT ptMaxTrackSize;
}
