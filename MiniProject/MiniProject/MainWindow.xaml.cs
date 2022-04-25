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
            string Intervall = ((ComboBoxItem)((MainWindow)Application.Current.MainWindow).Interval.SelectedItem).Content.ToString();
            string Periodd = ((MainWindow)Application.Current.MainWindow).Period.Text;
            string Symboll = ((MainWindow)Application.Current.MainWindow).Symbol.Text;
            string Typee = GetSelectedType();

            ApiData data = ApiCommunication.LoadApiData(Symboll.Split('=')[0], Intervall, Periodd, Typee);
            lineChart.setData(data);

            SymbolMD = data.Symbol;
            IntervalMD = data.Interval;
            PeriodMD = data.TimePeriod.ToString();
            IndicatorMD = data.Function;
            TimeZoneMD = data.TimeZoneData;
            LastRefreshedMD = data.LastRefreshedDate;
            SeriesTypeMD = data.SeriesType;

            DataContext = this;
        }

        private void Clear_Button(object sender, RoutedEventArgs e)
        {
            lineChart.clearData();
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
