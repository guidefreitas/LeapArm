using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace LeapConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            LeapListener listener = new LeapListener();
            Controller leap = new Controller();
            leap.AddListener(listener);
            while (true) { }
        }
    }
}
