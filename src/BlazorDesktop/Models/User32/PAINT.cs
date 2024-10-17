using System.Runtime.InteropServices;

namespace BlazorDesktop.Models.User32;

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