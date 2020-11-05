using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFrameworks_CA1.Question1
{
    class Lotto
    {
        public List<int> LottoLine()
        {
            var line = new List<int>();

            var rand = new Random();

            for (int ctr = 0; ctr <= 6; ctr++)
            {
                line.Add(rand.Next(1, 50));
                line = line.Distinct().ToList();
                ctr = line.Count;
            }
            return line.OrderBy(x => x).ToList();
        }
    }
}
