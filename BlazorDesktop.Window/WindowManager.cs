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

    /// <summary>
    /// Creates a new window based on the provided <see cref="AppOptions"/>.
    /// </summary>
    /// <param name="appOptions">The options for configuring the window.</param>
    /// <returns>An asynchronous task.</returns>
    /// <remarks>
    /// This method creates a new window using the specified options. It sets up the window class,
    /// registers the class, creates the window, and handles any errors that may occur during the process.
    /// </remarks>
    public async Task CreateWindow(AppOptions appOptions)
    {
        WNDCLASSEX windowClass = new()
        {
            cbSize = (uint)Marshal.SizeOf(typeof(WNDCLASSEX)),
            style = 0,
            lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new WndProcDelegate(WndProc)),
            hInstance = KERNAL32.GetModuleHandle(null),
            lpszClassName = appOptions.Title,
            hbrBackground = GDI32.CreateSolidBrush(RGB(appOptions.BackgroundColour.Red, appOptions.BackgroundColour.Green, appOptions.BackgroundColour.Blue)),
        };

        var classAtom = USER32.RegisterClassEx(ref windowClass);
        if (classAtom == 0)
        {
            Console.WriteLine("Error registering window class.");
            return;
        }

        var styles = WS_EX.WS_EX_LAYERED;

        var test = GetScreenCentre(appOptions);

        handle = USER32.CreateWindowEx(
            styles,
            appOptions.Title,
            appOptions.Title,
            WS.WS_OVERLAPPEDWINDOW | WS.WS_VISIBLE,
            (int)test.X,
            (int)test.Y,
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
        
        var colorkey = RGB(255, 0, 255);
        
        // // Set (whole window) window transparency (0 is fully transparent, 255 is fully opaque)
        byte transparency = 255; // Semi-transparent
        USER32.SetLayeredWindowAttributes(handle, colorkey, transparency, LWA.LWA_COLORKEY);
        // windowClass.hbrBackground = GDI32.CreateSolidBrush(colorkey);
        
        var style = USER32.GetWindowLong(handle, GWL.GWL_STYLE);
        var margins = new MARGINS
        {
            cxLeftWidth = -1,
            cxRightWidth = -1,
            cyTopHeight = -1,
            cyBottomHeight = -1
        };
        DWM.DwmExtendFrameIntoClientArea(handle, ref margins);
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