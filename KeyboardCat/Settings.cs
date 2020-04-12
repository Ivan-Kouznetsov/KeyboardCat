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
        private const int DefaultTimeBetweenKeyPresses = 250;
        private const int DefaultCoolDownTime = 500;

        public int TimeBetweenKeyPresses { get; private set; }
        public int CoolDownTime { get; private set; }

        public Settings(int timeBetweenKeyPresses, int coolDownTime)
        {
            TimeBetweenKeyPresses = timeBetweenKeyPresses;
            CoolDownTime = coolDownTime;
        }

        public static Settings Load(string fileName) 
        {
            Settings settings = new Settings(DefaultTimeBetweenKeyPresses, DefaultCoolDownTime);

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
