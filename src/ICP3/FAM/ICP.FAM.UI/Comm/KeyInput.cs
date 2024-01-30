using System;
using System.Runtime.InteropServices;

namespace ICP.FAM.UI.Comm
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBDINPUT
    {
        public short wVk;
        public short wScan;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct Input
    {
        [FieldOffset(0)]
        public int type;
        [FieldOffset(4)]
        public MOUSEINPUT mi;
        [FieldOffset(4)]
        public KEYBDINPUT ki;
        [FieldOffset(4)]
        public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }

    internal class INPUT
    {
        public const int MOUSE = 0;
        public const int KEYBOARD = 1;
        public const int HARDWARE = 2;
    }

    internal static class KeyInput
    {
        [DllImport("user32.dll")]
        internal static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")]
        internal static extern byte MapVirtualKey(byte wCode, int wMap); 

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("User32.dll", EntryPoint = "SendInput", CharSet = CharSet.Auto)]
        internal static extern UInt32 SendInput(UInt32 nInputs, Input[] pInputs, Int32 cbSize);

        [DllImport("Kernel32.dll", EntryPoint = "GetTickCount", CharSet = CharSet.Auto)]
        internal static extern int GetTickCount();

        [DllImport("User32.dll", EntryPoint = "GetKeyState", CharSet = CharSet.Auto)]
        internal static extern short GetKeyState(int nVirtKey);

        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private static bool SendKeyDown(short key)
        {
            bool flag = true;
            try
            {
                Input[] input = new Input[1];
                input[0].type = INPUT.KEYBOARD;
                input[0].ki.wVk = key;
                input[0].ki.time = GetTickCount();

                if (SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
                {
                    //throw new Win32Exception(Marshal.GetLastWin32Error());
                    flag = false;
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        private static bool SendKeyUp(short key)
        {
            bool flag = true;
            try
            {
                Input[] input = new Input[1];
                input[0].type = INPUT.KEYBOARD;
                input[0].ki.wVk = key;
                input[0].ki.dwFlags = KeyboardConstaint.KEYEVENTF_KEYUP;
                input[0].ki.time = GetTickCount();

                if (SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
                {
                    //throw new Win32Exception(Marshal.GetLastWin32Error());
                    flag = false;
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public static bool SendKey(short key)
        {
            bool isKeyDown = false;
            bool isKeyUp = false;
            try
            {
                isKeyDown = SendKeyDown(key);
                isKeyUp = SendKeyUp(key);
                if (isKeyDown && isKeyUp)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
