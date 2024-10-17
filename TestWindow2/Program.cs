using System.Drawing;
using WebView2Framework;

namespace TestWindow2;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var win32Window = new Win32Window();
        var hwnd = win32Window.Create("WebView2 Embedded Window", 1200, 800);

        var webViewManager = new WebViewManager();
        webViewManager.InitializeAsync(hwnd, "https://www.bing.com", new Rectangle(0, 0, 1200, 800));

        win32Window.RunMessageLoop();
    }
}