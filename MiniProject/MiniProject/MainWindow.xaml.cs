using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MiniProject
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public LineChart lineChart { get; set; }

        public OhlcChart OhlcChart { get; set; }

        public List<string> foreignExchangeList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _intervalMD = "";
        public string IntervalMD
        {
            set
            {
                if (_intervalMD != value)
                {
                    _intervalMD = value;
                    OnPropertyChanged();
                }

            }
            get
            {
                return _intervalMD;
            }
        }

        private string _symbolMD = "";
        public string SymbolMD
        {
            set
            {
                if (_symbolMD != value)
                {
                    _symbolMD = value;
                    OnPropertyChanged();
                }

            }
            get
            {
                return _symbolMD;
            }
        }

        private string _periodMD = "";
        public string PeriodMD
        {
            set
            {
                if (_periodMD != value)
                {
                    _periodMD = value;
                    OnPropertyChanged();
                }

            }
            get
            {
                return _periodMD;
            }
        }
        private string _timeZoneMD = "";
        public string TimeZoneMD
        {
            set
            {
                if (_timeZoneMD != value)
                {
                    _timeZoneMD = value;
                    OnPropertyChanged();
                }

            }
            get
            {
                return _timeZoneMD;
            }
        }
        private string _lastRefreshedMD = "";
        public string LastRefreshedMD
        {
            set
            {
                if (_lastRefreshedMD != value)
                {
                    _lastRefreshedMD = value;
                    OnPropertyChanged();
                }

            }
            get
            {
                return _lastRefreshedMD;
            }
        }
        private string _seriesTypeMD = "";
        public string SeriesTypeMD
        {
            set
            {
                if (_seriesTypeMD != value)
                {
                    _seriesTypeMD = value;
                    OnPropertyChanged();
                }

            }
            get
            {
                return _seriesTypeMD;
            }
        }
        private string _indicatorMD = "";
        public string IndicatorMD
        {
            set
            {
                if (_indicatorMD != value)
                {
                    _indicatorMD = value;
                    OnPropertyChanged();
                }

            }
            get
            {
                return _indicatorMD;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            lineChart = new LineChart();
            OhlcChart = new OhlcChart();
            foreignExchangeList = CSV.loadCurrency();
            Symbol.ItemsSource = foreignExchangeList;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void maximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
            if (((MainWindow)Application.Current.MainWindow).YearSpan.SelectedItem == null)
            {
                MessageBox.Show("Enter desired year span.");
                return;
            }
            string Intervall = ((ComboBoxItem)((MainWindow)Application.Current.MainWindow).Interval.SelectedItem).Content.ToString();
            string Periodd = ((MainWindow)Application.Current.MainWindow).Period.Text;
            string Symboll = ((MainWindow)Application.Current.MainWindow).Symbol.Text;
            string Typee = GetSelectedType();
            string symbol = Symboll.Split('=')[0];
            string dataPeriod = ((ComboBoxItem)((MainWindow)Application.Current.MainWindow).YearSpan.SelectedItem).Content.ToString();

            ApiData data = ApiCommunication.LoadApiData(symbol, Intervall, Periodd, Typee);
            if (data == null) return;
            List<OhlcData> ohlcDatas = ApiCommunication.LoadOhclData(symbol);
            if (ohlcDatas == null) return;
            data.SmaData = FilterSmaData(data, dataPeriod);
            ohlcDatas = FilterOhlcData(ohlcDatas, dataPeriod);
            lineChart.setData(data);
            OhlcChart.SetData(ohlcDatas, symbol);

            SymbolMD = data.Symbol;
            IntervalMD = data.Interval;
            PeriodMD = data.TimePeriod.ToString();
            IndicatorMD = data.Function;
            TimeZoneMD = data.TimeZoneData;
            LastRefreshedMD = data.LastRefreshedDate;
            SeriesTypeMD = data.SeriesType;

            DataContext = this;
        }

        private List<SmaData> FilterSmaData(ApiData data, string dataPeriod)
        {
            List<SmaData> smaDatas = new List<SmaData>();
            if (dataPeriod == "1 year")
            {
                DateTime yearBefore = DateTime.Now.AddYears(-1);
                foreach (SmaData smaData in data.SmaData)
                {
                    if (DateTime.Compare(yearBefore, DateTime.Parse(smaData.DateTime)) <= 0)
                    {
                        smaDatas.Add(smaData);
                    }
                }
                return smaDatas;
            }
            else if (dataPeriod == "2 years")
            {
                DateTime twoYearsBefore = DateTime.Now.AddYears(-2);
                foreach (SmaData smaData in data.SmaData)
                {
                    if (DateTime.Compare(twoYearsBefore, DateTime.Parse(smaData.DateTime)) <= 0)
                    {
                        smaDatas.Add(smaData);
                    }
                }
                return smaDatas;
            }

            return data.SmaData;
        }

        private List<OhlcData> FilterOhlcData(List<OhlcData> ohlcDatas, string dataPeriod)
        {
            List<OhlcData> newOhlcDatas = new List<OhlcData>();
            if (dataPeriod == "1 year")
            {
                DateTime yearBefore = DateTime.Now.AddYears(-1);
                foreach (OhlcData ohlcData in ohlcDatas)
                {
                    if (DateTime.Compare(yearBefore, DateTime.Parse(ohlcData.Date)) <= 0)
                    {
                        newOhlcDatas.Add(ohlcData);
                    }
                }
                return newOhlcDatas;
            }
            else if (dataPeriod == "2 years")
            {
                DateTime twoYearsBefore = DateTime.Now.AddYears(-2);
                foreach (OhlcData ohlcData in ohlcDatas)
                {
                    if (DateTime.Compare(twoYearsBefore, DateTime.Parse(ohlcData.Date)) <= 0)
                    {
                        newOhlcDatas.Add(ohlcData);
                    }
                }
                return newOhlcDatas;
            }
            return ohlcDatas;
        }

        private void Clear_Button(object sender, RoutedEventArgs e)
        {
            lineChart.clearData();
            OhlcChart.ClearData();
        }

        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            TableWindow win2 = new TableWindow();
            win2.Show();
        }

        private string GetSelectedType()
        {
            List<RadioButton> radioButtons = PanelRadioButtons.Children.OfType<RadioButton>().ToList();
            foreach (RadioButton rb in radioButtons)
            {
                if ((bool)rb.IsChecked)
                {
                    return rb.Content.ToString();
                }
            }
            return null;
        }

    }
}
