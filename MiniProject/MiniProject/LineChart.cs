using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;

namespace MiniProject
{
    public class LineChart
    {
        public SeriesCollection SeriesCollection { get; set; }
        //public Func<double, string> Y_Axis { get; set; }
        public List<string> dates { get; set; }

        public LineChart()
        {
            SeriesCollection = new SeriesCollection();
            dates = new List<string>();
        }
        public void setData(ApiData data)
        {
            try
            {
                data.SmaData.Reverse();
                ChartValues<double> values = GetValues(data.SmaData);
                dates = GetDates(data.SmaData);
                values = GetValues(data.SmaData);
                LineSeries lineSeries = new LineSeries
                {
                    Title = data.Symbol,
                    Values = values,
                    PointGeometry = null,
                    //PointGeometrySize = 15
                };
                SeriesCollection.Add(lineSeries);
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void clearData()
        {
            SeriesCollection.Clear();
            dates.Clear();
        }

        public ChartValues<double> GetValues(List<SmaData> smaData)
        {
            ChartValues<double> values = new ChartValues<double>();
            foreach (SmaData sma in smaData)
            {
                values.Add(sma.Value);
            }
            return values;
        }

        public List<string> GetDates(List<SmaData> smaData)
        {
            List<string> dates = new List<string>();
            foreach (SmaData sma in smaData)
            {

                dates.Add(sma.Date.ToString().Split(' ')[0]);
            }
            return dates;
        }

    }


}
