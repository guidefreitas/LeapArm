using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapConsole
{
    public class Util
    {
        public static float Map(float x, float in_min, float in_max, float out_max, float out_min)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
    }
}
