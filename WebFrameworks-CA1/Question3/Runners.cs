using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFrameworks_CA1.Question3
{
    class Runners
    {

        string[][] runners { get; set; }


        public Runners()
        {
            runners = new string[3][];
            runners[0] = new string[] { "Blue Jay", "Fireside", "Not Again" };
            runners[1] = new string[] { "Summers Night", "Coriolanus", "Blue Rinse", "Silver Shadow", "SLK" };
            runners[2] = new string[] { "Purple Rain", "Last Ditch", "Forty Fives", "Too Double" };
        }

        public string[] ListRunnersRace1()
        {

            return runners[0];
        }
        public string[] ListRunnersRace2()
        {

            return runners[1];
        }
        public string[] ListRunnersRace3()
        {
            return runners[2];
        }
    }
}
