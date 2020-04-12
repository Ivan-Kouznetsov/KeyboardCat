using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace KeyboardCat
{
    public partial class FrmMain : Form
    {
        private IntPtr keyboardHandlerId;
        private WindowsKeyboard.HookHandlerDelegate hookHandler;
        private bool CatMode { get { return rdoCatOn.Checked; } }
        private readonly List<long> keypressLog = new List<long>();
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            rdoCatOff.Checked = true;
            rdoCatOn.Checked = false;
            hookHandler = new WindowsKeyboard.HookHandlerDelegate(KeyboardHookHandler);

            keyboardHandlerId = WindowsKeyboard.SetLowLevelHook(hookHandler);
            this.FormClosing += FrmMain_FormClosing;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowsKeyboard.UnhookWindowsHookEx(keyboardHandlerId);
        }

        private IntPtr KeyboardHookHandler(int nCode, IntPtr wParam, ref WindowsKeyboard.KBDLLHOOKSTRUCT lParam) 
        {
#if DEBUG
            Debug.WriteLine(nCode + " " + wParam + " " + lParam.flags + " " + lParam.vkCode);
#endif
            if (CatMode)
            {
                // only ignore keydown events to prevent Ctrl, Shift, etc. from sticking
                if (WindowsKeyboard.IsKeyDown(wParam))
                {                
                    keypressLog.Add(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    return (IntPtr)1;
                }
            }
            else
            {
                if (!WindowsKeyboard.IsSpecialKey(lParam))
                {
                    if (WindowsKeyboard.IsKeyDown(wParam)) 
                    {
                        KeyDownTimer.Start();
                    }
                    else if (WindowsKeyboard.IsKeyUp(wParam)) 
                    {    
                        KeyDownTimer.Stop();
                    }
                }                
            }

            return WindowsKeyboard.CallNextHookEx(keyboardHandlerId, nCode, wParam, ref lParam);
        }

        private void KeyboardTimer_Tick(object sender, EventArgs e)
        {
            KeyDownTimer.Stop();
            rdoCatOn.PerformClick();
        }

        private void NoKeyPressTimer_Tick(object sender, EventArgs e)
        {
            const int CoolDownTime = 500;
            if (!KeyDownTimer.Enabled &&
                keypressLog.Count > 0 &&
                ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - keypressLog.Last()) > CoolDownTime)
            {
                rdoCatOff.PerformClick();
            }
        }
    }
}
