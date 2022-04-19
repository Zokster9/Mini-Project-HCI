using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MiniProject
{
    public partial class TableWindow : Window
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Period { get; set; }

        public ObservableCollection<TableData> SMA
        {
            get;
            set;
        }

        public TableWindow()
        {
            InitializeComponent();
            DataContext = this;
            SMA = new ObservableCollection<TableData>
            {
                new TableData { Time = DateTime.Now, Open = 5.3, Low = 4.5, High = 3.2, Close = 1.2 }
            };

            StartDate = ((MainWindow)Application.Current.MainWindow).StartDate.SelectedDate.Value.Date;
            EndDate = ((MainWindow)Application.Current.MainWindow).EndDate.SelectedDate.Value.Date;
            Period = int.Parse(((MainWindow)Application.Current.MainWindow).Period.SelectedItem.ToString());
        }
    }

    public class TableData
    {
        public DateTime Time { get; set; }
        public double Open { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Close { get; set; }

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
