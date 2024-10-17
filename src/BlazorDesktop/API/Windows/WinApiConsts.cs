namespace BlazorDesktop.API.Windows;

public class WinApiConsts
{
    public const int WS_OVERLAPPEDWINDOW = 0x00CF0000;
    public const int WS_VISIBLE = 0x10000000;
    public const int WM_DESTROY = 0x0002;
    public const int WM_PAINT = 0x000F;
    public const int WM_SIZE = 0x0005;
    public const int CW_USEDEFAULT = unchecked((int)0x80000000);
    public const int GWL_EXSTYLE = -20;
    public const int LWA_ALPHA = 0x2;
    public const int GCLP_HBRBACKGROUND = -10;
    public const int WS_EX_LAYERED = 0x00080000;
    public const int WS_EX_NOREDIRECTIONBITMAP = 0x00200000;
    public const int WS_EX_CONTROLPARENT = 0x00010000;
    public const int WS_EX_APPWINDOW = 0x00040000;
    public const int WS_EX_TOPMOST = 0x00000008;
}