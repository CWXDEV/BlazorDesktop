using System.Drawing;
using System.Numerics;
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

        var test = GetScreenCentre(appOptions);

        handle = User32.CreateWindowEx(
            styles,
            appOptions.Title,
            appOptions.Title,
            WinApiConsts.WS_OVERLAPPEDWINDOW | WinApiConsts.WS_VISIBLE,
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
            uint errorCode = Kernal32.GetLastError(); // Get error code after window creation failure
            Console.WriteLine($"Error creating window. Error Code: {errorCode}");
            return;
        }

        // Set window transparency (0 is fully transparent, 255 is fully opaque)
        byte transparency = 255; // Semi-transparent
        User32.SetLayeredWindowAttributes(handle, 0, transparency, WinApiConsts.LWA_ALPHA);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowManager"/> class.
    /// </summary>
    /// <param name="hwnd">The window handle.</param>
    /// <param name="url">The url to navigate to.</param>
    /// <returns>An asynchronous task.</returns>
    /// <remarks>
    /// This method creates a new instance of the <see cref="CoreWebView2Controller"/> and
    /// navigates to the given url. The bounds of the window are set to the client size of the
    /// window.
    /// </remarks>
    public async Task InitializeAsync(IntPtr hwnd, string url)
    {
        var webView2Environment = await CoreWebView2Environment.CreateAsync();
        webViewController = await webView2Environment.CreateCoreWebView2ControllerAsync(hwnd);

        var webView2 = webViewController.CoreWebView2;
        webView2.Navigate(url);

        // Set bounds
        var bounds = GetClientSize(hwnd);
        webViewController.Bounds = new Rectangle(0, 0, bounds.Width, bounds.Height);
    }

    /// <summary>
    /// Resizes the webview to the size of the window.
    /// </summary>
    /// <remarks>
    /// This method gets the size of the window and sets the bounds of the <see cref="CoreWebView2Controller"/>
    /// to the size of the window. This is done to ensure the webview is resized when the window is resized.
    /// </remarks>
    public void Resize()
    {
        if (webViewController != null)
        {
            // get window size
            var bounds = GetClientSize(handle);
            webViewController.Bounds = new Rectangle(0, 0, bounds.Width, bounds.Height);
        }
    }

    /// <summary>
    /// Gets the size of the client area of the specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <returns>The size of the client area of the window.</returns>
    /// <remarks>
    /// This method calls the <see cref="User32.GetClientRect"/> method to get the size of the client area of the window.
    /// </remarks>
    public RECT GetClientSize(IntPtr hwnd)
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

    /// <summary>
    /// Calculates the center position for a window on the screen based on the screen size and window dimensions.
    /// </summary>
    /// <param name="appOptions">The options containing the dimensions of the window.</param>
    /// <returns>A <see cref="Vector2"/> representing the x and y coordinates of the screen center for the window.</returns>
    /// <remarks>
    /// This method uses the system metrics to determine the screen size and calculates the center position
    /// by considering half of the window's dimensions.
    /// </remarks>
    public Vector2 GetScreenCentre(AppOptions appOptions)
    {
        var width = User32.GetSystemMetrics(WinApiConsts.SM_CXSCREEN);
        var height = User32.GetSystemMetrics(WinApiConsts.SM_CYSCREEN);
        var windowWidth = appOptions.Width / 2;
        var windowHeight = appOptions.Height / 2;
        var x = width / 2 - windowWidth;
        var y = height / 2 - windowHeight;
        return new Vector2(x, y);
    }

    /// <summary>
    /// Runs the message loop for the window.
    /// </summary>
    /// <remarks>
    /// This method calls the <see cref="User32.GetMessage"/> method to retrieve messages from the message queue,
    /// and then calls <see cref="User32.TranslateMessage"/> and <see cref="User32.DispatchMessage"/> to dispatch
    /// the messages to the window procedure. This is the main loop of the window, and it will continue to run until
    /// the window is closed.
    /// </remarks>
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

    /// <summary>
    /// Window procedure for handling window messages.
    /// </summary>
    /// <param name="hWnd">The window handle.</param>
    /// <param name="msg">The message to process.</param>
    /// <param name="wParam">An additional value associated with the message.</param>
    /// <param name="lParam">An additional value associated with the message.</param>
    /// <returns>The result of the message processing.</returns>
    /// <remarks>
    /// This method is called by the system for each window message received by the window.
    /// It processes the following messages:
    /// <list type="bullet">
    /// <item><description>WM_PAINT</description></item>
    /// <item><description>WM_DESTROY</description></item>
    /// <item><description>WM_SIZE</description></item>
    /// </list>
    /// For all other messages, it calls the <see cref="User32.DefWindowProc"/> method to handle the message.
    /// </remarks>
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

    /// <summary>
    /// Creates a packed RGB color value from the given red, green, and blue components.
    /// </summary>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    /// <returns>A <see cref="uint"/> containing the packed RGB color value.</returns>
    /// <remarks>
    /// The RGB color value is packed into a single <see cref="uint"/> in the following format:
    /// <code>
    /// 0x00BBGGRR
    /// </code>
    /// Where <c>BB</c> is the blue component, <c>GG</c> is the green component, and <c>RR</c> is the red component.
    /// </remarks>
    private uint RGB(byte r, byte g, byte b)
    {
        return (uint)(r | (g << 8) | (b << 16));
    }
}