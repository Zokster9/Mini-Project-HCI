using System;
using System.Collections.Generic;

namespace MiniProject
{
    public enum SeriesType { LOW, HIGH, OPEN, CLOSE }
    public class ApiData
    {
        public string Symbol { get; set; }
        public string Function { get; set; }
        public string LastRefreshedDate { get; set; }
        public string Interval { get; set; }
        public int TimePeriod { get; set; }
        public string SeriesType { get; set; }
        public string TimeZoneData { get; set; }
        public List<SmaData> SmaData { get; set; }

        public ApiData()
        {
            SmaData = new List<SmaData>();
        }

        public ApiData(string symbol, string function, string lastRefreshedDate, string interval, int timePeriod, 
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
        public string DateTime { get; set; }
        public double Value { get; set; }

        public SmaData()
        {

        }

        public SmaData(string dateTime, double value)
        {
            DateTime = dateTime;
            Value = value;
        }
    }
}
