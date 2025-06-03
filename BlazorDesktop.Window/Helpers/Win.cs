using System.Runtime.InteropServices;
using BlazorDesktop.Window.API;
using BlazorDesktop.Window.Constants;
using BlazorDesktop.Window.Models;
using BlazorDesktop.Window.Options;

namespace BlazorDesktop.Window.Helpers;

public class Win
{
    public static WNDCLASSEX CreateWindowClass(Func<IntPtr, uint, IntPtr, IntPtr, IntPtr> wndProc, string appOptionsTitle, RGB appOptionsBackgroundColour)
    {
        var test = new WNDCLASSEX
        {
            cbSize = (uint) Marshal.SizeOf(typeof(WNDCLASSEX)),
            style = CS.CS_HREDRAW | CS.CS_VREDRAW,
            lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new WndProcDelegate(wndProc)),
            hInstance = KERNAL32.GetModuleHandle(null),
            hIcon = 0, // TODO
            hbrBackground = GDI32.CreateSolidBrush(RGBToUInt(appOptionsBackgroundColour)),
            lpszClassName = appOptionsTitle
        };

        return test;
    }
    
    public static ushort RegisterWindowClass(ref WNDCLASSEX wndClass)
    {
        return USER32.RegisterClassEx(ref wndClass);
    }
    
    public static nint CreateWindowHandle(int exStyles, AppOptions appOptions, int dwStyle, POINT startLocation, nint instanceHandle)
    {
        return USER32.CreateWindowEx(
            exStyles,
            appOptions.Title,
            appOptions.Title,
            (uint) dwStyle,
            startLocation.X,
            startLocation.Y,
            appOptions.Width,
            appOptions.Height,
            IntPtr.Zero,
            IntPtr.Zero,
            instanceHandle,
            IntPtr.Zero
        );
    }

    public static int GetEXStyle(AppOptions options)
    {
        var exStyle = 0;
        if (options.WindowStyle != WindowStyle.FramelessWindow)
        {
            exStyle = CS.CS_HREDRAW | CS.CS_VREDRAW;
        }
        if (options.AlwaysOnTop)
        {
            exStyle |= WS_EX.WS_EX_TOPMOST;
        }
        if (options.TranslucentWindow)
        {
            exStyle |= WS_EX.WS_EX_NOREDIRECTIONBITMAP;
        }

        return exStyle;
    }

    public static int GetDWStyle(AppOptions options)
    {
        var dwStyle = WS.WS_OVERLAPPEDWINDOW | WS.WS_VISIBLE;
        switch (options.WindowStyle)
        {
            case WindowStyle.FramelessWindowWithBorder:
                if (options.TranslucentWindow)
                {
                    // if this is not set to true
                    // the window wont allow for transparency for whatever reason
                    options.DarkMode = true; 
                }
                dwStyle = WS.WS_POPUP | WS.WS_THICKFRAME | WS.WS_VISIBLE;
                break;
            case WindowStyle.FramelessWindow:
                dwStyle = WS.WS_POPUP | WS.WS_VISIBLE;
                break;
            default:
                break;
        }

        return dwStyle;
    }

    public static POINT GetStartingLocation(AppOptions _appOptions)
    {
        // Starting location
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
            startingLocation = GetScreenCentre(_appOptions);
        }

        return startingLocation;
    }
    
    public static POINT GetScreenCentre(AppOptions _appOptions)
    {
        var width = USER32.GetSystemMetrics(SM.SM_CXSCREEN);
        var height = USER32.GetSystemMetrics(SM.SM_CYSCREEN);
        var windowWidth = _appOptions.Width / 2;
        var windowHeight = _appOptions.Height / 2;
        var left = width / 2 - windowWidth;
        var top = height / 2 - windowHeight;
        return new POINT(left, top);
    }

    public static bool GetMessage(out MSG msg)
    {
        return USER32.GetMessage(out msg, IntPtr.Zero, 0, 0);
    }

    public static nint TranslateMessage(ref MSG msg)
    {
        return USER32.TranslateMessage(ref msg);
    }
    
    public static nint DispatchMessage(ref MSG msg)
    {
        return USER32.DispatchMessage(ref msg);
    }
    
    public static void SetFramelessStyling(IntPtr hWnd)
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

    public static int SetDarkMode(IntPtr handle, bool darkMode)
    {
        var winDark = darkMode ? 1 : 0;
        return DWM.DwmSetWindowAttribute(handle, DWMWA.DWMWA_USE_IMMERSIVE_DARK_MODE, ref winDark, sizeof(uint));
    }

    public static int SetTitleBarCaptionColor(IntPtr handle, RGB rgb)
    {
        var titleBarColor = (int) RGBToUInt(rgb);
        return DWM.DwmSetWindowAttribute(handle, DWMWA.DWMWA_CAPTION_COLOR, ref titleBarColor, sizeof(int));
    }

    public static int SetTitleBarTextColor(IntPtr handle, RGB rgb)
    {
        var titleTextColor = (int) RGBToUInt(rgb);
        return DWM.DwmSetWindowAttribute(handle, DWMWA.DWMWA_TEXT_COLOR, ref titleTextColor, sizeof(int));
    }

    public static int SetTitleBarBorderColor(IntPtr handle, RGB rgb)
    {
        var titleBorderColor = (int) RGBToUInt(rgb);
        return DWM.DwmSetWindowAttribute(handle, DWMWA.DWMWA_BORDER_COLOR, ref titleBorderColor, sizeof(int));
    }

    public static int SetDWMRender(IntPtr handle, bool enabled)
    {
        var dwmRender = enabled ? 1 : 0;
        return DWM.DwmSetWindowAttribute(handle, DWMWA.DWMWA_NCRENDERING_ENABLED, ref dwmRender, sizeof(uint));
    }

    public static void SetTranslucencyType(IntPtr handle, WindowBackdropType backdrop)
    {
        var backdropType = (int) backdrop;
        DWM.DwmSetWindowAttribute(handle, DWMWA.DWMWA_SYSTEMBACKDROP_TYPE, ref backdropType, sizeof(WindowBackdropType));
    }

    public static void SetWindowTheme(IntPtr handle, AppOptions options)
    {
        SetDarkMode(handle, options.DarkMode);
        switch (options.WindowStyle)
        {
            case WindowStyle.WindowWithCustomTheme:
                SetCustomTheme(handle, options);
                break;
            case WindowStyle.Mica:
                // TODO
                break;
            default:
                break;
        }
    }

    public static bool SetWindowState(IntPtr handle, int state)
    {
        return USER32.ShowWindow(handle, state);
    }

    public static bool UpdateWindow(IntPtr handle)
    {
        return USER32.UpdateWindow(handle);
    }

    private static void SetCustomTheme(IntPtr handle, AppOptions options)
    {
        if (options.TitleBarColor is not null)
        {
            SetTitleBarCaptionColor(handle, options.TitleBarColor);
        }

        if (options.TitleTextColor is not null)
        {
            SetTitleBarTextColor(handle, options.TitleTextColor);
        }

        if (options.TitleBorderColor is not null)
        {
            SetTitleBarBorderColor(handle, options.TitleBorderColor);
        }
    }

    public static int LastError()
    {
        return Marshal.GetLastWin32Error();
    }
    
    public static int GET_X_LPARAM(IntPtr lParam) => (short)(lParam.ToInt32() & 0xFFFF);
    public static int GET_Y_LPARAM(IntPtr lParam) => (short)((lParam.ToInt32() >> 16) & 0xFFFF);
    
    private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    
    public static uint RGBToUInt(RGB rgb)
    {
        return (uint) (rgb.Red | rgb.Green << 8 | rgb.Blue << 16);
    }
}
