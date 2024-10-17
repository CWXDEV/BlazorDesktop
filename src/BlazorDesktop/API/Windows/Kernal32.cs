using System.Runtime.InteropServices;

namespace BlazorDesktop.API.Windows;

public static class Kernal32
{
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string? lpModuleName);
    
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern uint GetLastError();
}