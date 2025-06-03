using System.Numerics;
using System.Runtime.InteropServices;
using BlazorDesktop.Window.API;
using BlazorDesktop.Window.Constants;
using BlazorDesktop.Window.Models;
using BlazorDesktop.Window.Options;
using BlazorDesktop.Window.Helpers;

namespace BlazorDesktop.Window;

public class WindowManager
{
    private IntPtr _handle;
    private AppOptions _appOptions;
    // private CoreWebView2Controller webViewController;

    public async Task<IntPtr> CreateWindow(AppOptions appOptions)
    {
        _appOptions = appOptions;

        if (_appOptions.BackgroundColour is null)
        {
            Console.WriteLine("AppOptions does not have a BackgroundColour Defined, Setting to 0, 0, 0");
            _appOptions.BackgroundColour = new RGB(0, 0, 0);
        }

        var windowClass = Win.CreateWindowClass(
            WndProc,
            _appOptions.Title,
            _appOptions.BackgroundColour
        );

        var classAtom = Win.RegisterWindowClass(ref windowClass);
        if (classAtom == 0)
        {
            Console.WriteLine("Error registering window class.");
            return IntPtr.Zero;
        }

        var exStyle = Win.GetEXStyle(_appOptions);
        var startingLocation = Win.GetStartingLocation(_appOptions);
        var dwStyle = Win.GetDWStyle(_appOptions);

        _handle = Win.CreateWindowHandle(
            exStyle,
            _appOptions,
            dwStyle,
            startingLocation,
            windowClass.hInstance
        );
        _appOptions.Handle = _handle;

        if (_handle == IntPtr.Zero)
        {
            var errorCode = Win.LastError();
            Console.WriteLine($"Error creating window. Error Code: {errorCode}");
            return IntPtr.Zero;
        }
        
        Win.SetWindowTheme(_handle, _appOptions);
        Win.SetTranslucencyType(_handle, _appOptions.WindowBackdropType);
        Win.SetWindowState(_handle, SW.SW_SHOW);
        Win.UpdateWindow(_handle);

        return _handle;
    }

    public void Run()
    {
        while (Win.GetMessage(out var msg))
        {
            Win.TranslateMessage(ref msg);
            Win.DispatchMessage(ref msg);
        }
    }

    private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        switch (msg)
        {
            case WM.WM_PAINT:
                PAINT ps;
                var hdc = USER32.BeginPaint(hWnd, out ps);
                USER32.EndPaint(hWnd, ref ps);
                return IntPtr.Zero;
            case WM.WM_DESTROY:
                USER32.PostQuitMessage(0);
                return IntPtr.Zero;
            case WM.WM_SIZE:
                return IntPtr.Zero;
            case WM.WM_GETMINMAXINFO:
                var mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);
                if (_appOptions.MinHeight is not null)
                {
                    mmi.ptMinTrackSize.Y = _appOptions.MinHeight.Value;
                }
                if (_appOptions.MinWidth is not null)
                {
                    mmi.ptMinTrackSize.X = _appOptions.MinWidth.Value;
                }
                if (_appOptions.MaxWidth is not null)
                {
                    mmi.ptMaxTrackSize.X = _appOptions.MaxWidth.Value;
                }
                if (_appOptions.MaxHeight is not null)
                {
                    mmi.ptMaxTrackSize.Y = _appOptions.MaxHeight.Value;
                }
                Marshal.StructureToPtr(mmi, lParam, true);
                return IntPtr.Zero;
        }

        if (_appOptions.WindowStyle == WindowStyle.FramelessWindowWithBorder)
        {
            switch (msg)
            {
                case WM.WM_ACTIVATE:
                    Win.SetFramelessStyling(hWnd);
                    return IntPtr.Zero;
                case WM.WM_NCACTIVATE:
                    return new IntPtr(1);
                case WM.WM_NCCALCSIZE:
                    // Remove all non-client area
                    if (wParam.ToInt32() == 1)
                    {
                        return IntPtr.Zero;
                    }
                    break;
                case WM.WM_CREATE:
                    Win.SetFramelessStyling(hWnd);
                    return IntPtr.Zero;
                case WM.WM_NCHITTEST:
                    var result = USER32.DefWindowProc(hWnd, msg, wParam, lParam);
                    var hitResult = result.ToInt32();
                    
                    switch (hitResult)
                    {
                        case HT.HTBORDER:
                        case HT.HTCLIENT:
                            var cursorPos = new POINT();
                            USER32.GetCursorPos(out cursorPos);
                            USER32.ScreenToClient(hWnd, ref cursorPos);
                            USER32.GetClientRect(hWnd, out var windowRect);

                            var borderWidth = 4;
                            
                            var left = cursorPos.X < borderWidth;
                            var right = cursorPos.X > windowRect.Right - borderWidth;
                            var top = cursorPos.Y < borderWidth;
                            var bottom = cursorPos.Y > windowRect.Bottom - borderWidth;

                            if (top && left) return new IntPtr(HT.HTTOPLEFT);
                            if (top && right) return new IntPtr(HT.HTTOPRIGHT);
                            if (bottom && left) return new IntPtr(HT.HTBOTTOMLEFT);
                            if (bottom && right) return new IntPtr(HT.HTBOTTOMRIGHT);
                            if (top) return new IntPtr(HT.HTTOP);
                            if (bottom) return new IntPtr(HT.HTBOTTOM);
                            if (left) return new IntPtr(HT.HTLEFT);
                            if (right) return new IntPtr(HT.HTRIGHT);

                            return new IntPtr(HT.HTCLIENT);
                    }
                    return result;
            }
        }

        return USER32.DefWindowProc(hWnd, msg, wParam, lParam);
    }
}
