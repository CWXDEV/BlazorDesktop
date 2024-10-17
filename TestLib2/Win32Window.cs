using System;
using System.Runtime.InteropServices;

namespace WebView2Framework
{
    public class Win32Window
    {
        // Constants for window creation
        private const string WINDOW_CLASS_NAME = "WebView2WindowClass";
        private const int WS_OVERLAPPEDWINDOW = 0x00CF0000;
        private const int WS_VISIBLE = 0x10000000;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName,
            uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance,
            IntPtr lpParam
        );

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern ushort RegisterClassEx([In] ref WNDCLASSEX lpWndClass);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        private static extern bool TranslateMessage(ref MSG lpMsg);

        [DllImport("user32.dll")]
        private static extern IntPtr DispatchMessage(ref MSG lpMsg);

        [DllImport("user32.dll")]
        private static extern IntPtr PostQuitMessage(int nExitCode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string? lpModuleName);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        [StructLayout(LayoutKind.Sequential)]
        private struct WNDCLASSEX
        {
            public uint cbSize;
            public uint style;
            public IntPtr lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public IntPtr lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        private IntPtr hwnd;

        // Delegate for handling window messages
        private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public IntPtr Create(string title, int width, int height)
        {
            var wc = new WNDCLASSEX
            {
                cbSize = (uint)Marshal.SizeOf(typeof(WNDCLASSEX)),
                style = 0,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new WndProcDelegate(WindowProc)),
                hInstance = GetModuleHandle(null),
                lpszClassName = "Test",
                hbrBackground = CreateSolidBrush(RGB(255, 255, 255))
            };

            IntPtr classAtom = RegisterClassEx(ref wc);

            if (classAtom == 0)
            {
                Console.WriteLine("Error registering window class.");
            }

            hwnd = CreateWindowEx(
                0,
                "Test",
                "Test",
                WS_OVERLAPPEDWINDOW | WS_VISIBLE,
                100,
                100,
                width,
                height,
                IntPtr.Zero,
                IntPtr.Zero,
                wc.hInstance,
                IntPtr.Zero
            );
            
            if (hwnd == IntPtr.Zero)
            {
                Console.WriteLine($"Error creating window");
            }

            return hwnd;
        }

        private static uint RGB(byte r, byte g, byte b)
        {
            return (uint)(r | (g << 8) | (b << 16));
        }

        public void RunMessageLoop()
        {
            MSG msg;
            while (GetMessage(out msg, IntPtr.Zero, 0, 0))
            {
                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
        }

        private IntPtr WindowProc(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            switch (uMsg)
            {
                case 0x0002: // WM_DESTROY
                    PostQuitMessage(0);
                    return IntPtr.Zero;
                default:
                    return DefWindowProc(hwnd, uMsg, wParam, lParam);
            }
        }
    }
}