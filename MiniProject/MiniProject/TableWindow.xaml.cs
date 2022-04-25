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
        public string Symboll { get; set; }
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
            if (((MainWindow)Application.Current.MainWindow).Interval.SelectedItem == null)
            {
                MessageBox.Show("Enter desired interval.");
                return;
            }
            if (((MainWindow)Application.Current.MainWindow).Period.Text == "")
            {
                MessageBox.Show("Enter desired period.");
                return;
            }
            if (((MainWindow)Application.Current.MainWindow).Symbol.Text == "")
            {
                MessageBox.Show("Enter desired symbol.");
                return;
            }
            Interval = ((ComboBoxItem)((MainWindow)Application.Current.MainWindow).Interval.SelectedItem).Content.ToString();
            Period = ((MainWindow)Application.Current.MainWindow).Period.Text;
            Symboll = ((MainWindow)Application.Current.MainWindow).Symbol.Text;
            ApiData highData = ApiCommunication.LoadApiData(Symboll.Split('=')[0], Interval, Period, "high");
            if (highData == null) return;
            List<SmaData> high = highData.SmaData;
            ApiData lowData = ApiCommunication.LoadApiData(Symboll.Split('=')[0], Interval, Period, "low");
            if (lowData == null) return;
            List<SmaData> low = lowData.SmaData;
            ApiData openData = ApiCommunication.LoadApiData(Symboll.Split('=')[0], Interval, Period, "open");
            if (openData == null) return;
            List<SmaData> open = lowData.SmaData;
            ApiData closeData = ApiCommunication.LoadApiData(Symboll.Split('=')[0], Interval, Period, "close");
            if (closeData == null) return;
            List<SmaData> close = lowData.SmaData;
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
