using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace KeyboardCat
{
    public class Settings
    {
        private const int DefaultKeyDownTimeOut = 250;
        private const int DefaultNoKeyDownTimeOut = 500;

        public int KeyDownTimeOut { get; private set; }
        public int NoKeyDownTimeOut { get; private set; }

        public Settings(int keyDownTimeOut, int noKeyDownTimeOut)
        {
            KeyDownTimeOut = keyDownTimeOut;
            NoKeyDownTimeOut = noKeyDownTimeOut;
        }

        public static Settings Load(string fileName) 
        {
            Settings settings = new Settings(DefaultKeyDownTimeOut, DefaultNoKeyDownTimeOut);

            if (File.Exists(fileName))
            {
                try
                {
                    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName));
                    return settings;
                }
                catch {}
            }
                        
            File.WriteAllText(fileName, JsonConvert.SerializeObject(settings));
            return settings;
        }
    }
}
