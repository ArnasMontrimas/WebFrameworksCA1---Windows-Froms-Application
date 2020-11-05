using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFrameworksCA1.Questions.Question1;

namespace WebFrameworksCA1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lotto = new Lotto();

            
            foreach(var i in lotto.LottoLine())
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();

        }
    }
}
