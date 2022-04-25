using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

namespace MiniProject
{
    class ApiCommunication
    {
        public static ApiData LoadApiData(string symbol, string interval, string time_period, string series_type)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=SMA&symbol={symbol}&interval={interval}&time_period={time_period}&series_type={series_type}&apikey=Q9CTERD3JMG0QU7L";
            Uri queryUri = new Uri(QUERY_URL);
            using (WebClient client = new WebClient())
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));
                dynamic meta_data = json_data["Meta Data"];
                dynamic sma_values = json_data["Technical Analysis: SMA"];
                List<SmaData> smaData = new List<SmaData>();
                foreach (string smaDate in sma_values.Keys)
                {
                    double value = Convert.ToDouble(sma_values[smaDate]["SMA"]);
                    SmaData sma = new SmaData(smaDate, value);
                    smaData.Add(sma);
                }
                string function = meta_data["2: Indicator"];
                string last_refreshed = meta_data["3: Last Refreshed"];
                int time_per = Convert.ToInt32(time_period);
                string time_zone = meta_data["7: Time Zone"];
                ApiData apiData = new ApiData(symbol, function, last_refreshed, interval, time_per, series_type,
                    time_zone, smaData);
                return apiData;
            }
        }

        public static Dictionary<string, string> GetMetaData(string symbol, string interval, string time_period, string series_type)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=SMA&symbol={symbol}&interval={interval}&time_period={time_period}&series_type={series_type}&apikey=Q9CTERD3JMG0QU7L";
            Uri queryUri = new Uri(QUERY_URL);
            using (WebClient client = new WebClient())
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));
                dynamic meta_data = json_data["Meta Data"];
                return meta_data;
            }
        }
    }
}
