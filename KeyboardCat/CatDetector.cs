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

        private readonly List<(long TimeStamp, long HashCode)> keyPressLog = new List<(long TimeStamp, long HashCode)>();
        private readonly int timeBetweenKeyPresses;

        public bool IsCat(bool isSpecialKey, int nCode, IntPtr wParam, WindowsKeyboard.KBDLLHOOKSTRUCT lParam) 
        {
           
            bool result = false;
            int hash = GetKeyPressHash(nCode, wParam, lParam);

            if (!isSpecialKey)
            {                
                if (keyPressLog.Count > 0)
                {
                    if (keyPressLog.Last().HashCode == hash &&
                       (GetTimeStamp() - keyPressLog.Last().TimeStamp) < timeBetweenKeyPresses)
                    {                        
                        result = true;
                    }
                }                
            }

            keyPressLog.Add((GetTimeStamp(), hash));
            return result;
        }

        public CatDetector(int timeBetweenKeyPresses) 
        {
            this.timeBetweenKeyPresses = timeBetweenKeyPresses;
        }

        private static int GetKeyPressHash(int nCode, IntPtr wParam, WindowsKeyboard.KBDLLHOOKSTRUCT lParam)
        {
            return (nCode.ToString() + wParam.ToInt32().ToString() + lParam.flags.ToString() + lParam.vkCode.ToString()).GetHashCode();
        }

        private static long GetTimeStamp() 
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
