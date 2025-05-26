using System.Runtime.InteropServices;
using BlazorDesktop.Window.Models;

namespace BlazorDesktop.Window.Models;

[StructLayout(LayoutKind.Sequential)]
public struct PAINT
{
    public IntPtr hdc;
    public bool fErase;
    public RECT rcPaint;
    public bool fRestore;
    public bool fIncUpdate;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] rgbReserved;
}
