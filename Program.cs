using System;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;

//Подключить: Project - Manage NuGet Packages - Browse - Microsoft.Win32.Registry

//Синяя каретка для английского языка
//Красная - для русского

namespace CaretColorChanger
{
    class Program
    {
        // Подключаем WinAPI, чтобы брать значение раскладки
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId([In] IntPtr hWnd, [Out, Optional] IntPtr lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern ushort GetKeyboardLayout([In] int idThread);

        static void Main(string[] args)
        {
            // 1033 - En
            // 1049 - Ru

            for (; ; )
            {
                if (GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero)) == 1033 && Registry.GetValue(@"HKEY_Users\S-1-5-21-1631277099-235724909-2667781766-1001\SOFTWARE\Microsoft\Accessibility\CursorIndicator", "IndicatorColor", null).ToString() != "16711680")
                {
                    using (RegistryKey key = Registry.Users.CreateSubKey(@"S-1-5-21-1631277099-235724909-2667781766-1001\SOFTWARE\Microsoft\Accessibility\CursorIndicator"))
                    {
                        key.SetValue("IndicatorColor", 0xff0000); //16711680, синий
                    }
                }
                else if (GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero)) == 1049 && Registry.GetValue(@"HKEY_Users\S-1-5-21-1631277099-235724909-2667781766-1001\SOFTWARE\Microsoft\Accessibility\CursorIndicator", "IndicatorColor", null).ToString() != "255")
                {
                    using (RegistryKey key = Registry.Users.CreateSubKey(@"S-1-5-21-1631277099-235724909-2667781766-1001\SOFTWARE\Microsoft\Accessibility\CursorIndicator"))
                    {
                        key.SetValue("IndicatorColor", 0x000000ff); //255, красный
                    }
                }
                Thread.Sleep(300);
            }
        }
    }
}
