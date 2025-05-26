using System.Runtime.InteropServices;

namespace BlazorDesktop.Window.API;

public class GDI32
{
    [DllImport("gdi32.dll", SetLastError = true)]
    public extern static IntPtr CreateSolidBrush(uint crColor);
    
    [DllImport("gdi32.dll", SetLastError = true)]
    public extern static IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
}
