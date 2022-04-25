using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject
{
    public class OhlcChart
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Dates { get; set; }

        public OhlcChart()
        {
            SeriesCollection = new SeriesCollection();
            Dates = new List<string>();
        }

        public void SetData(List<OhlcData> ohclDatas, string symbol)
        {
            try
            {
                ohclDatas.Reverse();
                ChartValues<OhlcPoint> values = GetValues(ohclDatas);
                Dates = GetDates(ohclDatas);
                OhlcSeries ohclSeries = new OhlcSeries
                {
                    Title = symbol,
                    Values = values,
                };
                SeriesCollection.Add(ohclSeries);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void ClearData()
        {
            SeriesCollection.Clear();
            Dates.Clear();
        }

        public ChartValues<OhlcPoint> GetValues(List<OhlcData> ohlcDatas)
        {
            ChartValues<OhlcPoint> values = new ChartValues<OhlcPoint>();
            foreach (OhlcData ohlcData in ohlcDatas)
            {
                values.Add(new OhlcPoint(ohlcData.Open, ohlcData.High, ohlcData.Low, ohlcData.Close));
            }
            return values;
        }

        public List<string> GetDates(List<OhlcData> ohlcDatas)
        {
            List<string> dates = new List<string>();
            foreach (OhlcData ohlcData in ohlcDatas)
            {
                dates.Add(ohlcData.Date);
            }
            return dates;
        }
    }
}
