using System.Runtime.InteropServices;

namespace BlazorDesktop.Window.API;

public class GDI32
{
    [DllImport("gdi32.dll", SetLastError = true)]
    public extern static IntPtr CreateSolidBrush(uint crColor);
    
    [DllImport("gdi32.dll", SetLastError = true)]
    public extern static IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
    
    [DllImport("gdi32.dll")]
    public extern static IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);
    
    [DllImport("gdi32.dll")]
    public extern static IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
    
    [DllImport("gdi32.dll")]
    public extern static bool RoundRect(IntPtr hdc, int left, int top, int right, int bottom, int width, int height);
    
    [DllImport("gdi32.dll")]
    public extern static IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);
    
    [DllImport("gdi32.dll")]
    public extern static bool DeleteObject(IntPtr hObject);
}
