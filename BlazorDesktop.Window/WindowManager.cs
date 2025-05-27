using System.Numerics;
using System.Runtime.InteropServices;
using BlazorDesktop.Window.API;
using BlazorDesktop.Window.Constants;
using BlazorDesktop.Window.Models;
using BlazorDesktop.Window.Options;

namespace BlazorDesktop.Window;

public class WindowManager
{
    private IntPtr handle;
    // private CoreWebView2Controller webViewController;
    
    public async Task CreateWindow(AppOptions appOptions)
    {
        WNDCLASSEX windowClass = new()
        {
            cbSize = (uint)Marshal.SizeOf(typeof(WNDCLASSEX)),
            style = CS.CS_HREDRAW | CS.CS_VREDRAW,
            lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new WndProcDelegate(WndProc)),
            hInstance = KERNAL32.GetModuleHandle(null),
            lpszClassName = appOptions.Title,
            hbrBackground =  GDI32.CreateSolidBrush(RGB(appOptions.BackgroundColour.Red, appOptions.BackgroundColour.Green, appOptions.BackgroundColour.Blue)), // wails does 15 + 1
        };

        var classAtom = USER32.RegisterClassEx(ref windowClass);
        if (classAtom == 0)
        {
            Console.WriteLine("Error registering window class.");
            return;
        }

        var exStyle = CS.CS_HREDRAW | CS.CS_VREDRAW;
        
        if (appOptions.ClientAreaTransparent)
        {
            exStyle |= WS_EX.WS_EX_NOREDIRECTIONBITMAP;
        }
        
        var startingLocation = new Vector2(0, 0);
        if (appOptions.StartPosition == StartPosition.Manual)
        {
            startingLocation.Y = appOptions.Top;
            startingLocation.X = appOptions.Left;
        }
        else
        {
            startingLocation = GetScreenCentre(appOptions);
        }

        handle = USER32.CreateWindowEx(
            exStyle,
            appOptions.Title,
            appOptions.Title,
            WS.WS_OVERLAPPEDWINDOW | WS.WS_VISIBLE,
            (int)startingLocation.X,
            (int)startingLocation.Y,
            appOptions.Width,
            appOptions.Height,
            IntPtr.Zero,
            IntPtr.Zero,
            windowClass.hInstance,
            IntPtr.Zero
        );

        appOptions.Handle = handle;

        if (handle == IntPtr.Zero)
        {
            var errorCode = KERNAL32.GetLastError(); // Get error code after window creation failure
            Console.WriteLine($"Error creating window. Error Code: {errorCode}");
            return;
        }

        // currently does not work
        if (appOptions.DarkMode)
        {
            Console.WriteLine("Dark mode enabled");
            var winDark = 1;
            DWM.DwmSetWindowAttribute(handle, DWMWA.DwmwaUseImmersiveDarkMode, ref winDark, winDark);
            DWM.DwmSetWindowAttribute(handle, DWMWA.DwmwaCaptionColor, ref winDark, 255);
            DWM.DwmSetWindowAttribute(handle, DWMWA.DwmwaTextColor, ref winDark, 255);
            DWM.DwmSetWindowAttribute(handle, DWMWA.DwmwaBorderColor, ref winDark, 255);
            
        }
    }
    
    public void Resize()
    {
        
    }
    
    public RECT GetClientSize(IntPtr hwnd)
    {
        RECT rect;
        if (USER32.GetClientRect(hwnd, out rect))
        {
            return rect; // Contains width and height of the client area
        }
        else
        {
            throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        }
    }
    
    public Vector2 GetScreenCentre(AppOptions appOptions)
    {
        var width = USER32.GetSystemMetrics(SM.SM_CXSCREEN);
        var height = USER32.GetSystemMetrics(SM.SM_CYSCREEN);
        var windowWidth = appOptions.Width / 2;
        var windowHeight = appOptions.Height / 2;
        var x = width / 2 - windowWidth;
        var y = height / 2 - windowHeight;
        return new Vector2(x, y);
    }
    
    public void Run()
    {
        MSG msg;
        while (USER32.GetMessage(out msg, IntPtr.Zero, 0, 0))
        {
            // Console.WriteLine($"GetMessage called with hWnd: {msg.hwnd}");
            USER32.TranslateMessage(ref msg);
            USER32.DispatchMessage(ref msg);
        }
    }
    
    private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        // Console.WriteLine($"WndProc called with hWnd: {msg}");
        switch (msg)
        {
            case WM.WM_PAINT:
                PAINT ps;
                IntPtr hdc = USER32.BeginPaint(hWnd, out ps);
                USER32.EndPaint(hWnd, ref ps);
                return IntPtr.Zero;
            case WM.WM_DESTROY:
                USER32.PostQuitMessage(0);
                return IntPtr.Zero;
            case WM.WM_SIZE:
                Resize();
                return IntPtr.Zero;
            default:
                return USER32.DefWindowProc(hWnd, msg, wParam, lParam);
        }
    }

    private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    
    private uint RGB(byte r, byte g, byte b)
    {
        return (uint)(r | g << 8 | b << 16);
    }
}