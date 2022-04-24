using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MiniProject
{
    public partial class TableWindow : Window
    {
        public string Interval { get; set; }
        public string Period { get; set; }
        public string Symbol { get; set; }
        public string Series_type { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<TableData> SMA
        {
            get;
            set;
        }

        public TableWindow()
        {
            InitializeComponent();
            DataContext = this;
            SMA = new ObservableCollection<TableData>();

            Interval = ((MainWindow)Application.Current.MainWindow).Interval.SelectedIndex.ToString();
            Period = ((MainWindow)Application.Current.MainWindow).Period.ToString();
            Symbol = ((MainWindow)Application.Current.MainWindow).Period.ToString();

            List<SmaData> high = ApiCommunication.LoadApiData(Symbol, Interval, Period, "high").SmaData;
            Console.WriteLine(high);
            List<SmaData> low = ApiCommunication.LoadApiData(Symbol, Interval, Period, "low").SmaData;
            Console.WriteLine(low);
            List<SmaData> open = ApiCommunication.LoadApiData(Symbol, Interval, Period, "open").SmaData;
            Console.WriteLine(open);
            List<SmaData> close = ApiCommunication.LoadApiData(Symbol, Interval, Period, "close").SmaData;
            Console.WriteLine(close);


            Dictionary<DateTime, SmaValues> x = new Dictionary<DateTime, SmaValues>(); 

            foreach(SmaData x1 in high)
            {
                x.Add(x1.Date, new SmaValues());

            }

            //SMA.Add(new TableData(x.Date, open.Value, low.Value, high.Value, close.Value));
            NotifyPropertyChanged("currencyData");
        }

    }

    public class SmaValues
    {
        public double Open { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Close { get; set; }
    }

    public class TableData : SmaValues
    {
        public DateTime Time { get; set; }

        public TableData()
        {

        }

        public TableData(DateTime time, double open, double low, double high, double close)
        {
            Time = time;
            Open = open;
            Close = close;
            Low = low;
            High = high;
        }
    }

}
