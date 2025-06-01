using System.ComponentModel.DataAnnotations;
using BlazorDesktop.Window.Models;

namespace BlazorDesktop.Window.Options;

public class AppOptions
{
    public IntPtr? Handle { get; set; }
    public IntPtr? WebviewHandle { get; set; }


    public bool DisableResize { get; set; }
    public bool Fullscreen { get; set; }
    public bool Frameless { get; set; }
    public bool StartHidden { get; set; }
    public bool HideWindowOnClose { get; set; }
    public bool HideTitleBar { get; set; }
    public bool CustomTitleBar { get; set; }
    public bool DarkMode { get; set; }
    public bool ClientAreaTransparent { get; set; }
    public bool AlwaysOnTop { get; set; }

    public string Title { get; set; } = "Blazor Desktop App";

    [Required]
    public int Height { get; set; }
    [Required]
    public int Width { get; set; }
    public int? MinHeight { get; set; }
    public int? MinWidth { get; set; }
    public int? MaxHeight { get; set; }
    public int? MaxWidth { get; set; }
    public int? Top { get; set; } = 0;
    public int? Left { get; set; } = 0;

    public StartPosition StartPosition { get; set; } = StartPosition.Center;

    public RGB? BackgroundColour { get; set; }
    public RGB? TitleTextColor { get; set; } = new(0, 0, 0);
    public RGB? TitleBarColor { get; set; } = new(255, 255, 255);
    public RGB? TitleBorderColor { get; set; } = new(255, 255, 255);

    // Extra's in Wails for Go github
}

public enum StartPosition
{
    Center,
    Manual
}

public record RGB
{
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }

    public RGB(byte r, byte g, byte b)
    {
        Red = r;
        Green = g;
        Blue = b;
    }

    public RGB()
    {

    }
}
