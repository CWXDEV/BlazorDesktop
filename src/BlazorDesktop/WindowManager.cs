using System.Drawing;
using System.Runtime.InteropServices;
using BlazorDesktop.Models;
using BlazorDesktop.API.Windows;
using BlazorDesktop.Models.User32;
using BlazorDesktop.Options;
using Microsoft.Web.WebView2.Core;

namespace BlazorDesktop;

public class WindowManager
{
    private IntPtr handle;
    private CoreWebView2Controller webViewController;
    
    public async Task CreateWindow(AppOptions appOptions)
    {
        WNDCLASSEX windowClass = new()
        {
            cbSize = (uint)Marshal.SizeOf(typeof(WNDCLASSEX)),
            style = 0,
            lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new WndProcDelegate(WndProc)),
            hInstance = Kernal32.GetModuleHandle(null),
            lpszClassName = appOptions.Title,
            hbrBackground = Gdi32.CreateSolidBrush(RGB(appOptions.BackgroundColour.Red, appOptions.BackgroundColour.Green, appOptions.BackgroundColour.Blue)),
        };
        
        ushort classAtom = User32.RegisterClassEx(ref windowClass);
        if (classAtom == 0)
        {
            Console.WriteLine("Error registering window class.");
            return;
        }

        int styles = WinApiConsts.WS_EX_CONTROLPARENT | WinApiConsts.WS_EX_APPWINDOW;
        
        handle = User32.CreateWindowEx(
            styles,
            appOptions.Title,
            appOptions.Title,
            WinApiConsts.WS_OVERLAPPEDWINDOW | WinApiConsts.WS_VISIBLE,
            10,
            10,
            600,
            800,
            IntPtr.Zero,
            IntPtr.Zero,
            windowClass.hInstance,
            IntPtr.Zero
        );
        
        appOptions.Handle = handle;

        if (handle == IntPtr.Zero)
        {
            uint errorCode = Kernal32.GetLastError();  // Get error code after window creation failure
            Console.WriteLine($"Error creating window. Error Code: {errorCode}");
            return;
        }
        
        // Set window transparency (0 is fully transparent, 255 is fully opaque)
        byte transparency = 255; // Semi-transparent
        User32.SetLayeredWindowAttributes(handle, 0, transparency, WinApiConsts.LWA_ALPHA);
    }
    
    public async Task InitializeAsync(IntPtr hwnd, string url, Rectangle bounds)
    {
        var webView2Environment = await CoreWebView2Environment.CreateAsync();
        webViewController = await webView2Environment.CreateCoreWebView2ControllerAsync(hwnd);

        var webView2 = webViewController.CoreWebView2;
        webView2.Navigate(url);

        // Set bounds
        webViewController.Bounds = bounds;
    }
    
    public void Resize()
    {
        if (webViewController != null)
        {
            // get window size
            var test = GetClientSize(handle);
            webViewController.Bounds = new Rectangle(0, 0, test.Width, test.Height);
        }
    }
    
    public static RECT GetClientSize(IntPtr hwnd)
    {
        RECT rect;
        if (User32.GetClientRect(hwnd, out rect))
        {
            return rect; // Contains width and height of the client area
        }
        else
        {
            throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        }
    }

    public void Run()
    {
        // Message loop
        MSG msg;
        while (User32.GetMessage(out msg, IntPtr.Zero, 0, 0))
        {
            // Console.WriteLine($"GetMessage called with hWnd: {msg.hwnd}");
            User32.TranslateMessage(ref msg);
            User32.DispatchMessage(ref msg);
        }
    }
    
    private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        // Console.WriteLine($"WndProc called with hWnd: {msg}");
        switch (msg)
        {
            case WinApiConsts.WM_PAINT:
                PAINT ps;
                IntPtr hdc = User32.BeginPaint(hWnd, out ps);
                User32.EndPaint(hWnd, ref ps);
                return IntPtr.Zero;
            case WinApiConsts.WM_DESTROY:
                User32.PostQuitMessage(0);
                return IntPtr.Zero;
            case WinApiConsts.WM_SIZE:
                Resize();
                return IntPtr.Zero;
            default:
                return User32.DefWindowProc(hWnd, msg, wParam, lParam);
        }
    }
    
    private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    private uint RGB(byte r, byte g, byte b)
    {
        return (uint)(r | (g << 8) | (b << 16));
    }
}