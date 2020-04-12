using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardCat
{
    public class CatDetector
    {
        // All time-related values are in ms

        private readonly List<(long TimeStamp, long HashCode)> keyboardEventLog = new List<(long TimeStamp, long HashCode)>();
        private readonly int timeBetweenKeyPresses;

        public CatDetector(int timeBetweenKeyPresses)
        {
            this.timeBetweenKeyPresses = timeBetweenKeyPresses;
        }

        public bool IsCat(bool isSpecialKey, int nCode, IntPtr wParam, WindowsKeyboard.KBDLLHOOKSTRUCT lParam) 
        {
           
            bool result = false;
            int hash = GetKeyboardEventHash(nCode, wParam, lParam);

            if (!isSpecialKey)
            {                
                if (keyboardEventLog.Count > 0)
                {
                    if (keyboardEventLog.Last().HashCode == hash &&
                       (GetTimeStamp() - keyboardEventLog.Last().TimeStamp) < timeBetweenKeyPresses)
                    {                        
                        result = true;
                    }
                }                
            }

            keyboardEventLog.Add((GetTimeStamp(), hash));
            return result;
        }

        public int GetKeyboardEventCount(int timeframe) 
        {
            return keyboardEventLog.FindAll(k => k.TimeStamp > (GetTimeStamp() - timeframe)).Count();
        } 

        private static int GetKeyboardEventHash(int nCode, IntPtr wParam, WindowsKeyboard.KBDLLHOOKSTRUCT lParam)
        {
            return (nCode.ToString() + wParam.ToInt32().ToString() + lParam.flags.ToString() + lParam.vkCode.ToString()).GetHashCode();
        }

        private static long GetTimeStamp() 
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
