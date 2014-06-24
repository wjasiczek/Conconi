using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conconi
{
    public class PlotData
    {
        public List<int> xValues { set; get; }
        public List<int> yValues { set; get; }

        public PlotData()
        {
            xValues = new List<int>();
            yValues = new List<int>();
        }

        public double ComputeSpeed(int time)
        {
            return ((double)time) / 200.0 * (1000.0 / 60.0);
        }

        public int GetMax(List<int> values)
        {
            var max = 0;
            for (var i = 0; i < values.Count; i++)
            {
                if (max < values[i])
                {
                    max = values[i];
                }
            }
            return max;
        }

        public int GetMin(List<int> values)
        {
            var min = 2000;
            for (var i = 0; i < values.Count; i++)
            {
                if (min > values[i])
                {
                    min = values[i];
                }
            }
            return min;
        }
    }
}
