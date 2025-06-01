using System.Runtime.InteropServices;

namespace BlazorDesktop.Window.Models;


[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int X;
    public int Y;

    public POINT()
    {
        
    }

    public POINT(int x, int y)
    {
        X = x;
        Y = y;
    }
}
