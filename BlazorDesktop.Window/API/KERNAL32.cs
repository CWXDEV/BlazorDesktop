using System.Runtime.InteropServices;

namespace BlazorDesktop.Window.API;

public class KERNAL32
{
    [DllImport("kernel32.dll", SetLastError = true)]
    public extern static IntPtr GetModuleHandle(string? lpModuleName);
    
    [DllImport("kernel32.dll", SetLastError = true)]
    public extern static uint GetLastError();
}
