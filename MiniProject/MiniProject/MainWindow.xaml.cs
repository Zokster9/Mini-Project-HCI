using System.Windows;
using System.Windows.Input;

namespace MiniProject
{
    public partial class MainWindow : Window
    {
        public LineChart lineChart { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            lineChart = new LineChart();
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
            ApiData data = ApiCommunication.LoadApiData("USDAUD", "weekly", "60", "open");
            lineChart.setData(data);

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

        private void GetSelectedType()
        {

        }


        private void FillMetaData()
        {
            string interval = this.Interval.SelectedIndex.ToString();
            string period = this.Period.ToString();
            string symbol = this.Symbol.ToString();
            //string type = GetSelectedType();

            //Dictionary<string, string> m = ApiCommunication.GetMetaData(symbol, interval, period, type);
        }
    }
}
