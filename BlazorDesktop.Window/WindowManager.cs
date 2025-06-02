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
    private int _currentCornerRadius = 8;
    private uint _currentBorderColor = 0x808080;
    private int _currentBorderWidth = 1;

    public async Task<IntPtr> CreateWindow(AppOptions appOptions)
    {
        _appOptions = appOptions;

        if (_appOptions.BackgroundColour is null)
        {
            Console.WriteLine("AppOptions does not have a BackgroundColour Defined, Setting to 0, 0, 0");
            _appOptions.BackgroundColour = new RGB(0, 0, 0);
        }

        var windowClass = Win32Helper.CreateWindowClass(
            WndProc,
            _appOptions.Title,
            _appOptions.BackgroundColour
        );

        var classAtom = Win32Helper.RegisterWindowClass(ref windowClass);
        if (classAtom == 0)
        {
            Console.WriteLine("Error registering window class.");
            return IntPtr.Zero;
        }

        var exStyle = CS.CS_HREDRAW | CS.CS_VREDRAW;
        if (_appOptions.AlwaysOnTop)
        {
            exStyle |= WS_EX.WS_EX_TOPMOST;
        }
        if (_appOptions.ClientAreaTransparent)
        {
            exStyle |= WS_EX.WS_EX_NOREDIRECTIONBITMAP;
        }

        var startingLocation = new POINT(0, 0);
        if (_appOptions.StartPosition == StartPosition.Manual)
        {
            if (_appOptions.Top is not null)
            {
                startingLocation.Y = _appOptions.Top.Value;
            }

            if (_appOptions.Left is not null)
            {
                startingLocation.X = _appOptions.Left.Value;
            }
        }
        else
        {
            startingLocation = Win32Helper.GetScreenCentre(_appOptions);
        }

        var dwStyle = WS.WS_OVERLAPPEDWINDOW | WS.WS_VISIBLE;

        if (_appOptions.Frameless)
        {
            dwStyle = WS.WS_POPUP | WS.WS_THICKFRAME | WS.WS_VISIBLE;
        }

        _handle = Win32Helper.CreateWindowHandle(
            exStyle,
            _appOptions,
            dwStyle,
            startingLocation,
            windowClass.hInstance
        );

        _appOptions.Handle = _handle;

        if (_handle == IntPtr.Zero)
        {
            var errorCode = Win32Helper.LastError();
            Console.WriteLine($"Error creating window. Error Code: {errorCode}");
            return IntPtr.Zero;
        }

        Win32Helper.SetDarkMode(_handle, _appOptions.DarkMode);

        if (_appOptions.CustomTitleBar)
        {
            if (_appOptions.TitleBarColor is not null)
            {
                Win32Helper.SetTitleBarCaptionColor(_handle, _appOptions.TitleBarColor);
            }

            if (_appOptions.TitleTextColor is not null)
            {
                Win32Helper.SetTitleBarTextColor(_handle, _appOptions.TitleTextColor);
            }

            if (_appOptions.TitleBorderColor is not null)
            {
                Win32Helper.SetTitleBarBorderColor(_handle, _appOptions.TitleBorderColor);
            }
        }

        // var test = USER32.GetWindowLongPtr(_handle, GWL.GWL_STYLE);
        // test &= ~WS.WS_CAPTION;
        // USER32.SetWindowLongPtr(_handle, GWL.GWL_STYLE, test);
        // USER32.SetWindowPos(_handle, default, 0, 0, 0, 0, SWP.SWP_NOMOVE | SWP.SWP_NOSIZE | SWP.SWP_NOZORDER | SWP.SWP_FRAMECHANGED);

        USER32.ShowWindow(_handle, SW.SW_SHOW);
        USER32.UpdateWindow(_handle);

        return _handle;
    }

    public void Run()
    {
        MSG msg;
        while (USER32.GetMessage(out msg, IntPtr.Zero, 0, 0))
        {
            USER32.TranslateMessage(ref msg);
            USER32.DispatchMessage(ref msg);
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

        if (_appOptions.Frameless)
        {
            switch (msg)
            {
                case WM.WM_ACTIVATE:
                    ApplyFramelessStyling(hWnd);
                    return IntPtr.Zero;
                case WM.WM_NCACTIVATE:
                    // ApplyFramelessStyling(hWnd);
                    return new IntPtr(1);
                case WM.WM_NCCALCSIZE:
                    // Remove all non-client area
                    if (wParam.ToInt32() == 1)
                    {
                        return IntPtr.Zero;
                    }
                    break;
                case WM.WM_CREATE:
                    ApplyFramelessStyling(hWnd);
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

                            var borderWidth = 5;
                            
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

    private void ApplyFramelessStyling(IntPtr hWnd)
    {
        var margins = new MARGINS
        {
            cxLeftWidth = 1,
            cxRightWidth = 1,
            cyTopHeight = 1,
            cyBottomHeight = 1
        };
        DWM.DwmExtendFrameIntoClientArea(hWnd, ref margins);

        USER32.InvalidateRect(hWnd, IntPtr.Zero, true);
    }
}
