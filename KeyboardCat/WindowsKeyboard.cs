using System;
using System.Runtime.InteropServices;

namespace KeyboardCat
{
    public static class WindowsKeyboard
    {
        private const int WH_KEYBOARD_LL = 13;

        private const int VK_BACK = 8;
        private const int VK_SHIFT = 160;
        private const int VK_CONTROL = 162;
        private const int VK_MENU = 164; // Alt
        private const int VK_VOLUME_DOWN = 174;
        private const int VK_VOLUME_UP = 175;

        private const int VK_ARROW_START = 37;
        private const int VK_ARROW_END = 40;

        private const int KEY_DOWN = 256;
        private const int KEY_UP = 257;

        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode; 
            public int flags;

            public KBDLLHOOKSTRUCT(int vkCode, int flags)
            {
                this.vkCode = vkCode;
                this.flags = flags;
            }
        }

        public delegate IntPtr HookHandlerDelegate(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);
               
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookHandlerDelegate lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);
      
        public static IntPtr SetLowLevelHook(HookHandlerDelegate callback) 
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, callback, IntPtr.Zero, 0);
        }

        public static bool IsSpecialKey(KBDLLHOOKSTRUCT lParam) 
        {
            int vkCode = lParam.vkCode;

            return (vkCode == VK_BACK ||
                    vkCode == VK_CONTROL ||
                    vkCode == VK_MENU ||
                    vkCode == VK_SHIFT ||
                    vkCode == VK_VOLUME_DOWN ||
                    vkCode == VK_VOLUME_UP || (vkCode >= VK_ARROW_START && vkCode <= VK_ARROW_END));
        }

        public static bool IsKeyDown(IntPtr wParam) 
        {
            return wParam.ToInt32() == KEY_DOWN;
        }

        public static bool IsKeyUp(IntPtr wParam)
        {
            return wParam.ToInt32() == KEY_UP;
        }
    }
}
