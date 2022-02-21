using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookLogic
{
    public interface IChart
    {
        void AddPoints<T, K>(Dictionary<T,K> i_DictionaryToCreateChartFrom, string i_SeriesName);

        void ClearPoints(string i_SeriesName);
    }
}
