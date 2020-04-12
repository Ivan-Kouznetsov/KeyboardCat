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
        private bool CatMode { 
            get 
            { 
                return rdoCatOn.Checked; 
            } 
            set 
            {
                if (value)
                {
                    rdoCatOn.Checked = true;
                    rdoCatOff.Checked = false;
                }
                else 
                {
                    rdoCatOn.Checked = false;
                    rdoCatOff.Checked = true;
                }
            } 
        }
        private readonly List<long> keypressLog = new List<long>();
        private CatDetector catDetector;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Settings settings = Settings.Load("settings.json");
            catDetector = new CatDetector(settings.TimeBetweenKeyPresses);
          
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
            Debug.WriteLine((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString() + "ms: "+ nCode 
                + " " + wParam + " " + lParam.flags + " " + lParam.vkCode);
#endif
            CatMode = CatMode || catDetector.IsCat(WindowsKeyboard.IsSpecialKey(lParam), nCode, wParam, lParam);

            if (CatMode)
            {
                // only ignore keydown events to prevent Ctrl, Shift, etc. from sticking
                if (WindowsKeyboard.IsKeyDown(wParam))
                {                
                    keypressLog.Add(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    return (IntPtr)1;
                }
            }            

            return WindowsKeyboard.CallNextHookEx(keyboardHandlerId, nCode, wParam, ref lParam);
        }
    }
}
