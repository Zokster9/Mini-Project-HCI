using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

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

            Interval = ((ComboBoxItem)((MainWindow)Application.Current.MainWindow).Interval.SelectedItem).Content.ToString();
            Period = ((MainWindow)Application.Current.MainWindow).Period.Text;
            Symbol = ((ComboBoxItem)((MainWindow)Application.Current.MainWindow).Symbol.SelectedItem).Content.ToString();

            List<SmaData> high = ApiCommunication.LoadApiData(Symbol, Interval, Period, "high").SmaData;
            List<SmaData> low = ApiCommunication.LoadApiData(Symbol, Interval, Period, "low").SmaData;
            List<SmaData> open = ApiCommunication.LoadApiData(Symbol, Interval, Period, "open").SmaData;
            List<SmaData> close = ApiCommunication.LoadApiData(Symbol, Interval, Period, "close").SmaData;

            for (int i = 0; i < high.Count; i++)
            {
                SMA.Add(new TableData(high[i].DateTime, open[i].Value, low[i].Value, high[i].Value, close[i].Value));
            }

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
        public string Time { get; set; }

        public TableData()
        {

        }

        public TableData(string time, double open, double low, double high, double close)
        {
            Time = time;
            Open = open;
            Close = close;
            Low = low;
            High = high;
        }
    }

}
