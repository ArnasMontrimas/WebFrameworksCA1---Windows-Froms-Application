using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebFrameworks_CA1.Question2
{
    class RainfallData
    {
        public BindingList<int[,]> data { set; get; }

        public RainfallData(int rows, int columns)
        {
            data = new BindingList<int[,]>();
            data.Add(new int[rows,columns]);
            data[0][0, 0] = 150;
            data[0][0, 1] = 163;
            data[0][0, 2] = 147;
            data[0][0, 3] = 138;
            data[0][1, 0] = 100;
            data[0][1, 1] = 89;
            data[0][1, 2] = 88;
            data[0][1, 3] = 87;
            data[0][2, 0] = 157;
            data[0][2, 1] = 97;
            data[0][2, 2] = 96;
            data[0][2, 3] = 94;
            data[0][3, 0] = 184;
            data[0][3, 1] = 133;
            data[0][3, 2] = 129;
            data[0][3, 3] = 117;
        }

    }
}
