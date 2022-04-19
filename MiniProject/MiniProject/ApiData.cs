using System;
using System.Collections.Generic;

namespace MiniProject
{
    public class ApiData
    {
        public string Symbol { get; set; }
        public string Function { get; set; }
        public DateTime LastRefreshedDate { get; set; }
        public string Interval { get; set; }
        public int TimePeriod { get; set; }
        public string SeriesType { get; set; }
        public string TimeZoneData { get; set; }
        public List<SmaData> SmaData { get; set; }

        public ApiData()
        {
            SmaData = new List<SmaData>();
        }

        public ApiData(string symbol, string function, DateTime lastRefreshedDate, string interval, int timePeriod, 
            string seriesType, string timeZoneData, List<SmaData> smaData)
        {
            Symbol = symbol;
            Function = function;
            LastRefreshedDate = lastRefreshedDate;
            Interval = interval;
            TimePeriod = timePeriod;
            SeriesType = seriesType;
            TimeZoneData = timeZoneData;
            SmaData = smaData;
        }
    }

    public class SmaData
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }

        public SmaData()
        {

        }

        public SmaData(DateTime date, double value)
        {
            Date = date;
            Value = value;
        }
    }
}
