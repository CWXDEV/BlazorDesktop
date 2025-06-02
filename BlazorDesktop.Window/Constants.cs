namespace BlazorDesktop.Window.Constants;

// Window message constants
public record WM
{
    public const int WM_APP = 32768;
    public const int WM_ACTIVATE = 6;
    public const int WM_ACTIVATEAPP = 28;
    public const int WM_AFXFIRST = 864;
    public const int WM_AFXLAST = 895;
    public const int WM_ASKCBFORMATNAME = 780;
    public const int WM_CANCELJOURNAL = 75;
    public const int WM_CANCELMODE = 31;
    public const int WM_CAPTURECHANGED = 533;
    public const int WM_CHANGECBCHAIN = 781;
    public const int WM_CHAR = 258;
    public const int WM_CHARTOITEM = 47;
    public const int WM_CHILDACTIVATE = 34;
    public const int WM_CLEAR = 771;
    public const int WM_CLOSE = 16;
    public const int WM_COMMAND = 273;
    public const int WM_COMPACTING = 65;
    public const int WM_COMPAREITEM = 57;
    public const int WM_CONTEXTMENU = 123;
    public const int WM_COPY = 769;
    public const int WM_COPYDATA = 74;
    public const int WM_CREATE = 1;
    public const int WM_CTLCOLORBTN = 309;
    public const int WM_CTLCOLORDLG = 310;
    public const int WM_CTLCOLOREDIT = 307;
    public const int WM_CTLCOLORLISTBOX = 308;
    public const int WM_CTLCOLORMSGBOX = 306;
    public const int WM_CTLCOLORSCROLLBAR = 311;
    public const int WM_CTLCOLORSTATIC = 312;
    public const int WM_CUT = 768;
    public const int WM_DEADCHAR = 259;
    public const int WM_DELETEITEM = 45;
    public const int WM_DESTROY = 2;
    public const int WM_DESTROYCLIPBOARD = 775;
    public const int WM_DEVICECHANGE = 537;
    public const int WM_DEVMODECHANGE = 27;
    public const int WM_DISPLAYCHANGE = 126;
    public const int WM_DRAWCLIPBOARD = 776;
    public const int WM_DRAWITEM = 43;
    public const int WM_DROPFILES = 563;
    public const int WM_ENABLE = 10;
    public const int WM_ENDSESSION = 22;
    public const int WM_ENTERIDLE = 289;
    public const int WM_ENTERMENULOOP = 529;
    public const int WM_ENTERSIZEMOVE = 561;
    public const int WM_ERASEBKGND = 20;
    public const int WM_EXITMENULOOP = 530;
    public const int WM_EXITSIZEMOVE = 562;
    public const int WM_FONTCHANGE = 29;
    public const int WM_GETDLGCODE = 135;
    public const int WM_GETFONT = 49;
    public const int WM_GETHOTKEY = 51;
    public const int WM_GETICON = 127;
    public const int WM_GETMINMAXINFO = 36;
    public const int WM_GETTEXT = 13;
    public const int WM_GETTEXTLENGTH = 14;
    public const int WM_HANDHELDFIRST = 856;
    public const int WM_HANDHELDLAST = 863;
    public const int WM_HELP = 83;
    public const int WM_HOTKEY = 786;
    public const int WM_HSCROLL = 276;
    public const int WM_HSCROLLCLIPBOARD = 782;
    public const int WM_ICONERASEBKGND = 39;
    public const int WM_INITDIALOG = 272;
    public const int WM_INITMENU = 278;
    public const int WM_INITMENUPOPUP = 279;
    public const int WM_INPUT = 0x00FF;
    public const int WM_INPUTLANGCHANGE = 81;
    public const int WM_INPUTLANGCHANGEREQUEST = 80;
    public const int WM_KEYDOWN = 256;
    public const int WM_KEYUP = 257;
    public const int WM_KILLFOCUS = 8;
    public const int WM_MDIACTIVATE = 546;
    public const int WM_MDICASCADE = 551;
    public const int WM_MDICREATE = 544;
    public const int WM_MDIDESTROY = 545;
    public const int WM_MDIGETACTIVE = 553;
    public const int WM_MDIICONARRANGE = 552;
    public const int WM_MDIMAXIMIZE = 549;
    public const int WM_MDINEXT = 548;
    public const int WM_MDIREFRESHMENU = 564;
    public const int WM_MDIRESTORE = 547;
    public const int WM_MDISETMENU = 560;
    public const int WM_MDITILE = 550;
    public const int WM_MEASUREITEM = 44;
    public const int WM_GETOBJECT = 0x003D;
    public const int WM_CHANGEUISTATE = 0x0127;
    public const int WM_UPDATEUISTATE = 0x0128;
    public const int WM_QUERYUISTATE = 0x0129;
    public const int WM_UNINITMENUPOPUP = 0x0125;
    public const int WM_MENURBUTTONUP = 290;
    public const int WM_MENUCOMMAND = 0x0126;
    public const int WM_MENUGETOBJECT = 0x0124;
    public const int WM_MENUDRAG = 0x0123;
    public const int WM_APPCOMMAND = 0x0319;
    public const int WM_MENUCHAR = 288;
    public const int WM_MENUSELECT = 287;
    public const int WM_MOVE = 3;
    public const int WM_MOVING = 534;
    public const int WM_NCACTIVATE = 134;
    public const int WM_NCCALCSIZE = 131;
    public const int WM_NCCREATE = 129;
    public const int WM_NCDESTROY = 130;
    public const int WM_NCHITTEST = 132;
    public const int WM_NCLBUTTONDBLCLK = 163;
    public const int WM_NCLBUTTONDOWN = 161;
    public const int WM_NCLBUTTONUP = 162;
    public const int WM_NCMBUTTONDBLCLK = 169;
    public const int WM_NCMBUTTONDOWN = 167;
    public const int WM_NCMBUTTONUP = 168;
    public const int WM_NCXBUTTONDOWN = 171;
    public const int WM_NCXBUTTONUP = 172;
    public const int WM_NCXBUTTONDBLCLK = 173;
    public const int WM_NCMOUSEHOVER = 0x02A0;
    public const int WM_NCMOUSELEAVE = 0x02A2;
    public const int WM_NCMOUSEMOVE = 160;
    public const int WM_NCPAINT = 133;
    public const int WM_NCRBUTTONDBLCLK = 166;
    public const int WM_NCRBUTTONDOWN = 164;
    public const int WM_NCRBUTTONUP = 165;
    public const int WM_NEXTDLGCTL = 40;
    public const int WM_NEXTMENU = 531;
    public const int WM_NOTIFY = 78;
    public const int WM_NOTIFYFORMAT = 85;
    public const int WM_NULL = 0;
    public const int WM_PAINT = 15;
    public const int WM_PAINTCLIPBOARD = 777;
    public const int WM_PAINTICON = 38;
    public const int WM_PALETTECHANGED = 785;
    public const int WM_PALETTEISCHANGING = 784;
    public const int WM_PARENTNOTIFY = 528;
    public const int WM_PASTE = 770;
    public const int WM_PENWINFIRST = 896;
    public const int WM_PENWINLAST = 911;
    public const int WM_POWER = 72;
    public const int WM_POWERBROADCAST = 536;
    public const int WM_PRINT = 791;
    public const int WM_PRINTCLIENT = 792;
    public const int WM_QUERYDRAGICON = 55;
    public const int WM_QUERYENDSESSION = 17;
    public const int WM_QUERYNEWPALETTE = 783;
    public const int WM_QUERYOPEN = 19;
    public const int WM_QUEUESYNC = 35;
    public const int WM_QUIT = 18;
    public const int WM_RENDERALLFORMATS = 774;
    public const int WM_RENDERFORMAT = 773;
    public const int WM_SETCURSOR = 32;
    public const int WM_SETFOCUS = 7;
    public const int WM_SETFONT = 48;
    public const int WM_SETHOTKEY = 50;
    public const int WM_SETICON = 128;
    public const int WM_SETREDRAW = 11;
    public const int WM_SETTEXT = 12;
    public const int WM_SETTINGCHANGE = 26;
    public const int WM_SHOWWINDOW = 24;
    public const int WM_SIZE = 5;
    public const int WM_SIZECLIPBOARD = 779;
    public const int WM_SIZING = 532;
    public const int WM_SPOOLERSTATUS = 42;
    public const int WM_STYLECHANGED = 125;
    public const int WM_STYLECHANGING = 124;
    public const int WM_SYSCHAR = 262;
    public const int WM_SYSCOLORCHANGE = 21;
    public const int WM_SYSCOMMAND = 274;
    public const int WM_SYSDEADCHAR = 263;
    public const int WM_SYSKEYDOWN = 260;
    public const int WM_SYSKEYUP = 261;
    public const int WM_TCARD = 82;
    public const int WM_THEMECHANGED = 794;
    public const int WM_TIMECHANGE = 30;
    public const int WM_TIMER = 275;
    public const int WM_UNDO = 772;
    public const int WM_USER = 1024;
    public const int WM_USERCHANGED = 84;
    public const int WM_VKEYTOITEM = 46;
    public const int WM_VSCROLL = 277;
    public const int WM_VSCROLLCLIPBOARD = 778;
    public const int WM_WINDOWPOSCHANGED = 71;
    public const int WM_WINDOWPOSCHANGING = 70;
    public const int WM_WININICHANGE = 26;
    public const int WM_KEYFIRST = 256;
    public const int WM_KEYLAST = 264;
    public const int WM_SYNCPAINT = 136;
    public const int WM_MOUSEACTIVATE = 33;
    public const int WM_MOUSEMOVE = 512;
    public const int WM_LBUTTONDOWN = 513;
    public const int WM_LBUTTONUP = 514;
    public const int WM_LBUTTONDBLCLK = 515;
    public const int WM_RBUTTONDOWN = 516;
    public const int WM_RBUTTONUP = 517;
    public const int WM_RBUTTONDBLCLK = 518;
    public const int WM_MBUTTONDOWN = 519;
    public const int WM_MBUTTONUP = 520;
    public const int WM_MBUTTONDBLCLK = 521;
    public const int WM_MOUSEWHEEL = 522;
    public const int WM_MOUSEFIRST = 512;
    public const int WM_XBUTTONDOWN = 523;
    public const int WM_XBUTTONUP = 524;
    public const int WM_XBUTTONDBLCLK = 525;
    public const int WM_MOUSELAST = 525;
    public const int WM_MOUSEHOVER = 0x2A1;
    public const int WM_MOUSELEAVE = 0x2A3;
    public const int WM_CLIPBOARDUPDATE = 0x031D;
}

// GetSystemMetrics constants
public record SM
{
    public const int SM_CXSCREEN = 0;
    public const int SM_CYSCREEN = 1;
    public const int SM_CXVSCROLL = 2;
    public const int SM_CYHSCROLL = 3;
    public const int SM_CYCAPTION = 4;
    public const int SM_CXBORDER = 5;
    public const int SM_CYBORDER = 6;
    public const int SM_CXDLGFRAME = 7;
    public const int SM_CYDLGFRAME = 8;
    public const int SM_CYVTHUMB = 9;
    public const int SM_CXHTHUMB = 10;
    public const int SM_CXICON = 11;
    public const int SM_CYICON = 12;
    public const int SM_CXCURSOR = 13;
    public const int SM_CYCURSOR = 14;
    public const int SM_CYMENU = 15;
    public const int SM_CXFULLSCREEN = 16;
    public const int SM_CYFULLSCREEN = 17;
    public const int SM_CYKANJIWINDOW = 18;
    public const int SM_MOUSEPRESENT = 19;
    public const int SM_CYVSCROLL = 20;
    public const int SM_CXHSCROLL = 21;
    public const int SM_DEBUG = 22;
    public const int SM_SWAPBUTTON = 23;
    public const int SM_RESERVED1 = 24;
    public const int SM_RESERVED2 = 25;
    public const int SM_RESERVED3 = 26;
    public const int SM_RESERVED4 = 27;
    public const int SM_CXMIN = 28;
    public const int SM_CYMIN = 29;
    public const int SM_CXSIZE = 30;
    public const int SM_CYSIZE = 31;
    public const int SM_CXFRAME = 32;
    public const int SM_CYFRAME = 33;
    public const int SM_CXMINTRACK = 34;
    public const int SM_CYMINTRACK = 35;
    public const int SM_CXDOUBLECLK = 36;
    public const int SM_CYDOUBLECLK = 37;
    public const int SM_CXICONSPACING = 38;
    public const int SM_CYICONSPACING = 39;
    public const int SM_MENUDROPALIGNMENT = 40;
    public const int SM_PENWINDOWS = 41;
    public const int SM_DBCSENABLED = 42;
    public const int SM_CMOUSEBUTTONS = 43;
    public const int SM_CXFIXEDFRAME = SM_CXDLGFRAME;
    public const int SM_CYFIXEDFRAME = SM_CYDLGFRAME;
    public const int SM_CXSIZEFRAME = SM_CXFRAME;
    public const int SM_CYSIZEFRAME = SM_CYFRAME;
    public const int SM_SECURE = 44;
    public const int SM_CXEDGE = 45;
    public const int SM_CYEDGE = 46;
    public const int SM_CXMINSPACING = 47;
    public const int SM_CYMINSPACING = 48;
    public const int SM_CXSMICON = 49;
    public const int SM_CYSMICON = 50;
    public const int SM_CYSMCAPTION = 51;
    public const int SM_CXSMSIZE = 52;
    public const int SM_CYSMSIZE = 53;
    public const int SM_CXMENUSIZE = 54;
    public const int SM_CYMENUSIZE = 55;
    public const int SM_ARRANGE = 56;
    public const int SM_CXMINIMIZED = 57;
    public const int SM_CYMINIMIZED = 58;
    public const int SM_CXMAXTRACK = 59;
    public const int SM_CYMAXTRACK = 60;
    public const int SM_CXMAXIMIZED = 61;
    public const int SM_CYMAXIMIZED = 62;
    public const int SM_NETWORK = 63;
    public const int SM_CLEANBOOT = 67;
    public const int SM_CXDRAG = 68;
    public const int SM_CYDRAG = 69;
    public const int SM_SHOWSOUNDS = 70;
    public const int SM_CXMENUCHECK = 71;
    public const int SM_CYMENUCHECK = 72;
    public const int SM_SLOWMACHINE = 73;
    public const int SM_MIDEASTENABLED = 74;
    public const int SM_MOUSEWHEELPRESENT = 75;
    public const int SM_XVIRTUALSCREEN = 76;
    public const int SM_YVIRTUALSCREEN = 77;
    public const int SM_CXVIRTUALSCREEN = 78;
    public const int SM_CYVIRTUALSCREEN = 79;
    public const int SM_CMONITORS = 80;
    public const int SM_SAMEDISPLAYFORMAT = 81;
    public const int SM_IMMENABLED = 82;
    public const int SM_CXFOCUSBORDER = 83;
    public const int SM_CYFOCUSBORDER = 84;
    public const int SM_TABLETPC = 86;
    public const int SM_MEDIACENTER = 87;
    public const int SM_STARTER = 88;
    public const int SM_SERVERR2 = 89;
    public const int SM_CMETRICS = 91;
    public const int SM_REMOTESESSION = 0x1000;
    public const int SM_SHUTTINGDOWN = 0x2000;
    public const int SM_REMOTECONTROL = 0x2001;
    public const int SM_CARETBLINKINGENABLED = 0x2002;
}

// GetWindowLong and GetWindowLongPtr constants
public record GWL
{
    public const int GWL_EXSTYLE = -20;
    public const int GWL_STYLE = -16;
    public const int GWL_WNDPROC = -4;
    public const int GWLP_WNDPROC = -4;
    public const int GWL_HINSTANCE = -6;
    public const int GWLP_HINSTANCE = -6;
    public const int GWL_HWNDPARENT = -8;
    public const int GWLP_HWNDPARENT = -8;
    public const int GWL_ID = -12;
    public const int GWLP_ID = -12;
    public const int GWL_USERDATA = -21;
    public const int GWLP_USERDATA = -21;
}

public record LWA
{
    public const int LWA_ALPHA = 0x00000002;
    public const int LWA_COLORKEY = 0x00000001;
}

public record GCLP
{
    public const int GCLP_HBRBACKGROUND = -10;
}

public record CW
{
    public const int CW_USEDEFAULT = unchecked((int) 0x80000000);
}

// ShowWindow constants
public record SW
{
    public const int SW_HIDE = 0;
    public const int SW_NORMAL = 1;
    public const int SW_SHOWNORMAL = 1;
    public const int SW_SHOWMINIMIZED = 2;
    public const int SW_MAXIMIZE = 3;
    public const int SW_SHOWMAXIMIZED = 3;
    public const int SW_SHOWNOACTIVATE = 4;
    public const int SW_SHOW = 5;
    public const int SW_MINIMIZE = 6;
    public const int SW_SHOWMINNOACTIVE = 7;
    public const int SW_SHOWNA = 8;
    public const int SW_RESTORE = 9;
    public const int SW_SHOWDEFAULT = 10;
    public const int SW_FORCEMINIMIZE = 11;
}

// Window class styles
public record CS
{
    public const int CS_VREDRAW = 0x00000001;
    public const int CS_HREDRAW = 0x00000002;
    public const int CS_KEYCVTWINDOW = 0x00000004;
    public const int CS_DBLCLKS = 0x00000008;
    public const int CS_OWNDC = 0x00000020;
    public const int CS_CLASSDC = 0x00000040;
    public const int CS_PARENTDC = 0x00000080;
    public const int CS_NOKEYCVT = 0x00000100;
    public const int CS_NOCLOSE = 0x00000200;
    public const int CS_SAVEBITS = 0x00000800;
    public const int CS_BYTEALIGNCLIENT = 0x00001000;
    public const int CS_BYTEALIGNWINDOW = 0x00002000;
    public const int CS_GLOBALCLASS = 0x00004000;
    public const int CS_IME = 0x00010000;
    public const int CS_DROPSHADOW = 0x00020000;
}

// Window style constants
// https://learn.microsoft.com/en-us/windows/win32/winmsg/window-styles
public record WS
{
    public const int WS_OVERLAPPED = 0x00000000;
    public const int WS_POPUP = unchecked((int) 0x80000000);
    public const int WS_CHILD = 0x40000000;
    public const int WS_MINIMIZE = 0x20000000;
    public const int WS_VISIBLE = 0x10000000;
    public const int WS_DISABLED = 0x08000000;
    public const int WS_CLIPSIBLINGS = 0x04000000;
    public const int WS_CLIPCHILDREN = 0x02000000;
    public const int WS_MAXIMIZE = 0x01000000;
    public const int WS_CAPTION = 0x00C00000;
    public const int WS_BORDER = 0x00800000;
    public const int WS_DLGFRAME = 0x00400000;
    public const int WS_VSCROLL = 0x00200000;
    public const int WS_HSCROLL = 0x00100000;
    public const int WS_SYSMENU = 0x00080000;
    public const int WS_THICKFRAME = 0x00040000;
    public const int WS_GROUP = 0x00020000;
    public const int WS_TABSTOP = 0x00010000;
    public const int WS_MINIMIZEBOX = 0x00020000;
    public const int WS_MAXIMIZEBOX = 0x00010000;
    public const int WS_TILED = 0x00000000;
    public const int WS_ICONIC = 0x20000000;
    public const int WS_SIZEBOX = 0x00040000;
    public const int WS_OVERLAPPEDWINDOW = 0x00000000 | 0x00C00000 | 0x00080000 | 0x00040000 | 0x00020000 | 0x00010000;
    public const int WS_POPUPWINDOW = unchecked((int) 0x80000000 | 0x00800000 | 0x00080000);
    public const int WS_CHILDWINDOW = 0x40000000;
}

// Extended window style constants
// https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
public record WS_EX
{
    public const int WS_EX_DLGMODALFRAME = 0x00000001;
    public const int WS_EX_NOPARENTNOTIFY = 0x00000004;
    public const int WS_EX_TOPMOST = 0x00000008;
    public const int WS_EX_ACCEPTFILES = 0x00000010;
    public const int WS_EX_TRANSPARENT = 0x00000020;
    public const int WS_EX_MDICHILD = 0x00000040;
    public const int WS_EX_TOOLWINDOW = 0x00000080;
    public const int WS_EX_WINDOWEDGE = 0x00000100;
    public const int WS_EX_CLIENTEDGE = 0x00000200;
    public const int WS_EX_CONTEXTHELP = 0x00000400;
    public const int WS_EX_RIGHT = 0x00001000;
    public const int WS_EX_LEFT = 0x00000000;
    public const int WS_EX_RTLREADING = 0x00002000;
    public const int WS_EX_LTRREADING = 0x00000000;
    public const int WS_EX_LEFTSCROLLBAR = 0x00004000;
    public const int WS_EX_RIGHTSCROLLBAR = 0x00000000;
    public const int WS_EX_CONTROLPARENT = 0x00010000;
    public const int WS_EX_STATICEDGE = 0x00020000;
    public const int WS_EX_APPWINDOW = 0x00040000;
    public const int WS_EX_OVERLAPPEDWINDOW = 0x00000100 | 0x00000200;
    public const int WS_EX_PALETTEWINDOW = 0x00000100 | 0x00000080 | 0x00000008;
    public const int WS_EX_LAYERED = 0x00080000;
    public const int WS_EX_NOINHERITLAYOUT = 0x00100000;
    public const int WS_EX_NOREDIRECTIONBITMAP = 0x00200000;
    public const int WS_EX_LAYOUTRTL = 0x00400000;
    public const int WS_EX_NOACTIVATE = 0x08000000;
}

// DWM window attributes
// https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute
public record DWMWA
{
    public const int DWMWA_NCRENDERING_ENABLED = 1;
    public const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    public const int DWMWA_BORDER_COLOR = 34;
    public const int DWMWA_CAPTION_COLOR = 35;
    public const int DWMWA_TEXT_COLOR = 36;
    public const int DWMWA_SYSTEMBACKDROP_TYPE = 38;
}


public record DWM_BB
{
    public const int DWM_BB_ENABLE = 1;
    public const int DWM_BB_BLURREGION = 2;
    public const int DWM_BB_TRANSITIONONMAXIMIZED = 4;
}

public record COLORREF
{
    public const int rgbRed = 0x000000FF;
    public const int rgbGreen = 0x0000FF00;
    public const int rgbBlue = 0x00FF0000;
    public const int rgbBlack = 0x00000000;
    public const int rgbWhite = 0x00FFFFFF;
}

// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-redrawwindow
public record RDW
{
    public const uint RDW_INVALIDATE = 0x0001;
    public const uint RDW_UPDATENOW = 0x0100;
    public const uint RDW_ERASE = 0x0004;
    public const uint RDW_FRAME = 0x0400;
}

public record HT
{
    public const int HTNOWHERE = 0;
    public const int HTCLIENT = 1;
    public const int HTCAPTION = 2;
    public const int HTSYSMENU = 3;
    public const int HTGROWBOX = 4;
    public const int HTMENU = 5;
    public const int HTHSCROLL = 6;
    public const int HTVSCROLL = 7;
    public const int HTMINBUTTON = 8;
    public const int HTLEFT = 10;
    public const int HTRIGHT = 11;
    public const int HTTOP = 12;
    public const int HTTOPLEFT = 13;
    public const int HTTOPRIGHT = 14;
    public const int HTBOTTOM = 15;
    public const int HTBOTTOMLEFT = 16;
    public const int HTBOTTOMRIGHT = 17;
    public const int HTBORDER = 18;
}

public record SWP
{
    public const int SWP_NOSIZE = 0x0001;
    public const int SWP_NOMOVE = 0x0002;
    public const int SWP_NOZORDER = 0x0004;
    public const int SWP_NOREDRAW = 0x0008;
    public const int SWP_NOACTIVATE = 0x0010;
    public const int SWP_FRAMECHANGED = 0x0020;
    public const int SWP_SHOWWINDOW = 0x0040;
    public const int SWP_HIDEWINDOW = 0x0080;
    public const int SWP_NOCOPYBITS = 0x0100;
    public const int SWP_NOOWNERZORDER = 0x0200;
    public const int SWP_NOSENDCHANGING = 0x0400;
    public const int SWP_DRAWFRAME = SWP_FRAMECHANGED;
    public const int SWP_NOREPOSITION = SWP_NOOWNERZORDER;
    public const int SWP_DEFERERASE = 0x2000;
    public const int SWP_ASYNCWINDOWPOS = 0x4000;
}
