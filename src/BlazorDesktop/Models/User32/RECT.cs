using System.Runtime.InteropServices;

namespace BlazorDesktop.Models.User32;

[StructLayout(LayoutKind.Sequential)]
public struct RECT
{
    public int left;
    public int top;
    public int right;
    public int bottom;
    
    public int Width => right - left;
    public int Height => bottom - top;
}