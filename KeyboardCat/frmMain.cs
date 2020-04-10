using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace KeyboardCat
{
    public partial class frmMain : Form
    {
        private IntPtr keyboardHandlerId;
        private WindowsKeyboard.HookHandlerDelegate hookHandler;
        private bool CatMode { get { return rdoCatOn.Checked; } }
        private readonly List<long> keypressLog = new List<long>();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            rdoCatOff.Checked = true;
            rdoCatOn.Checked = false;
            hookHandler = new WindowsKeyboard.HookHandlerDelegate(KeyboardHookHandler);

            keyboardHandlerId = WindowsKeyboard.SetLowLevelHook(hookHandler);
        }

        private IntPtr KeyboardHookHandler(int nCode, IntPtr wParam, ref WindowsKeyboard.KBDLLHOOKSTRUCT lParam) 
        {
            if (CatMode)
            {
                keypressLog.Add(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                return (IntPtr)1;
            }
            else
            {
                 Debug.WriteLine(string.Format("nCode {0} wParam{1} lParam.flags{2} lParam.vkCode{3} time:{4}", nCode, wParam, lParam.flags, lParam.vkCode, DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
                if (!WindowsKeyboard.IsSpecialKey(lParam))
                {
                    const int KeyDown = 256;
                    const int KeyUp = 257;

                    switch (wParam.ToInt32())
                    {
                        case KeyDown:
                            KeyDownTimer.Start();
                            break;
                        case KeyUp:
                            KeyDownTimer.Stop();
                            break;
                    }
                }
                return WindowsKeyboard.CallNextHookEx(keyboardHandlerId, nCode, wParam, ref lParam);
            }
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
                ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - keypressLog.Last()) > CoolDownTime) rdoCatOff.PerformClick();
        }
    }
}
