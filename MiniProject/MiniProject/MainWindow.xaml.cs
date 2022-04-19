﻿using System.Windows;
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
                this.DragMove();
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
    }
}
