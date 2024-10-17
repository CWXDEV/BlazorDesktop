using Microsoft.Web.WebView2.Core;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace WebView2Framework
{
    public class WebViewManager
    {
        private CoreWebView2Controller webViewController;

        public async Task InitializeAsync(IntPtr hwnd, string url, Rectangle bounds)
        {
            var webView2Environment = await CoreWebView2Environment.CreateAsync();
            webViewController = await webView2Environment.CreateCoreWebView2ControllerAsync(hwnd);

            var webView2 = webViewController.CoreWebView2;
            webView2.Navigate(url);

            // Set bounds
            webViewController.Bounds = bounds;
        }

        public void Resize(Rectangle newBounds)
        {
            if (webViewController != null)
            {
                webViewController.Bounds = newBounds;
            }
        }
    }
}