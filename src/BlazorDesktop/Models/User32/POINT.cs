using System.Runtime.InteropServices;

namespace BlazorDesktop.Models.User32;

[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int x;
    public int y;
}