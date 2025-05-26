using System.Runtime.InteropServices;

namespace BlazorDesktop.Window.Models;


[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int x;
    public int y;
}
