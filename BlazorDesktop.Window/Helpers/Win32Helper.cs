using System.Runtime.InteropServices;
using BlazorDesktop.Window.API;
using BlazorDesktop.Window.Constants;
using BlazorDesktop.Window.Models;
using BlazorDesktop.Window.Options;

namespace BlazorDesktop.Window.Helpers;

public class Win32Helper
{

    #region CreationMethods
    
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

    #endregion

    #region Other

    private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    #endregion

    #region GetMethods

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

    #endregion

    #region SetMethods

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

    #endregion

    #region HelperMethods

    public static int LastError()
    {
        return Marshal.GetLastWin32Error();
    }
    
    public static int GET_X_LPARAM(IntPtr lParam) => (short)(lParam.ToInt32() & 0xFFFF);
    public static int GET_Y_LPARAM(IntPtr lParam) => (short)((lParam.ToInt32() >> 16) & 0xFFFF);

    /// <summary>
    /// Converts RGB to UInt
    /// </summary>
    /// <param name="rgb"></param>
    public static uint RGBToUInt(RGB rgb)
    {
        return (uint) (rgb.Red | rgb.Green << 8 | rgb.Blue << 16);
    }

    #endregion
}
