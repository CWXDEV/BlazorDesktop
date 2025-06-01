using System.Runtime.InteropServices;
using BlazorDesktop.Window.Models;

namespace BlazorDesktop.Window.API;

public class USER32
{
    [DllImport("user32.dll", SetLastError = true)]
    public extern static ushort RegisterClassEx([In] ref WNDCLASSEX lpwcx);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName,
        uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance,
        IntPtr lpParam
    );

    [DllImport("user32.dll", SetLastError = true)]
    public extern static IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
    
    [DllImport("user32.dll")]
    public extern static IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static IntPtr DispatchMessage(ref MSG lpmsg);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static IntPtr TranslateMessage(ref MSG lpmsg);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static void PostQuitMessage(int nExitCode);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static IntPtr BeginPaint(IntPtr hWnd, out PAINT lpPaint);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static bool EndPaint(IntPtr hWnd, [In] ref PAINT lpPaint);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetClassLongPtr", SetLastError = true)]
    public extern static IntPtr SetClassLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll", EntryPoint = "SetClassLongPtrW", SetLastError = true)]
    public extern static IntPtr SetClassLongPtrW(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    public extern static bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);
    
    [DllImport("user32.dll")]
    public extern static bool ReleaseCapture();
    
    [DllImport("user32.dll")]
    public extern static bool ShowWindow(IntPtr hWnd, int nCmdShow);
    
    [DllImport("user32.dll")]
    public extern static bool UpdateWindow(IntPtr hWnd);
    
    [DllImport("user32.dll")]
    public extern static IntPtr GetWindowDC(IntPtr hWnd);
    
    [DllImport("user32.dll")]
    public extern static int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    
    [DllImport("user32.dll")]
    public extern static int InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);
    
    [DllImport("user32.dll")]
    public extern static int InvalidateRgn(IntPtr hWnd, IntPtr hRgn, bool bErase);

    [DllImport("user32.dll")]
    public extern static int FillRect(IntPtr hDC, ref RECT lprc, IntPtr hbr);
}
