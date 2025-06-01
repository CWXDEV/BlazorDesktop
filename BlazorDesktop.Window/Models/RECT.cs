using System.Runtime.InteropServices;

namespace BlazorDesktop.Window.Models;

[StructLayout(LayoutKind.Sequential)]
public struct RECT
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
    
    public int Width
    {
        get
        {
            return Right - Left;
        }
    }

    public int Height
    {
        get
        {
            return Bottom - Top;
        }
    }
}
