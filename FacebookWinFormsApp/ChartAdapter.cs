using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using BasicFacebookFeatures;

namespace FacebookLogic
{
    class ChartAdapter : IChart
    {
        public Chart StatisticsChart { get; set; }
        public ChartAdapter(Chart i_StatisticsChart)
        {
            StatisticsChart = i_StatisticsChart;
        }

        public void AddPoints<T, K>(Dictionary<T, K> i_DictionaryToCreateChartFrom, string i_SeriesName)
        {
            foreach (var item in i_DictionaryToCreateChartFrom)
            {
                T xValueItem = item.Key;
                K yValueItem = item.Value;

                if (typeof(T) == typeof(eMonth))
                {
                    StatisticsChart.Series[i_SeriesName].Points.AddXY(xValueItem.ToString(), yValueItem);
                }
                else
                {
                    StatisticsChart.Series[i_SeriesName].Points.AddXY(xValueItem, yValueItem);
                }

            }
        }

        public void ClearPoints(string i_SeriesName)
        {
            StatisticsChart.Series[i_SeriesName].Points.Clear();
        }
    }
}
