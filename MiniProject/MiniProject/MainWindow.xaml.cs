using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MiniProject
{
    public partial class MainWindow : Window
    {
        public LineChart lineChart { get; set; }

        public List<string> foreignExchangeList { get; set; }

        public string IntervalMD { get; set; }
        public string SymbolMD { get; set; }
        public string PeriodMD { get; set; }
        public string TimeZoneMD { get; set; }
        public string LastRefreshedMD { get; set; }
        public string SeriesTypeMD { get; set; }
        public string IndicatorMD { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            lineChart = new LineChart();
            foreignExchangeList = CSV.loadCurrency();
            Symbol.ItemsSource = foreignExchangeList;
            
            // add dinamic combobox data
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
