using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookLogic
{
    class ChartAdapter:IChart
    {
        public System.Windows.Forms.DataVisualization.Charting.Chart Chart { get; set; }
        public void AddPoints()
        {
            
        }

        public void ClearPoints()
        {
           
        }
    }
}
