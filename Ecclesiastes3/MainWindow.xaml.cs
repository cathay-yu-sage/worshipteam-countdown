using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Timers;

namespace Ecclesiastes3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer refreshTimer = new Timer(1000);
        private CountdownDisplayWindow displayWindow = new CountdownDisplayWindow();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
            refreshTimer.Elapsed += OnTimer;
            refreshTimer.Enabled = true;
            displayWindow.Show();
        }

        private void OnTimer(object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
                {
                    var vm = DataContext as ViewModel;
                    this.PreviewLabel.Content = vm.CountdownValue;
                    this.CurrentTimeLabel.Content = vm.CurrentTimeValue;
                    this.displayWindow.SetDisplayValue(vm.CountdownValue);
                }));
        }

        private void ToggleClockMode(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.ClockMode = !vm.ClockMode;
        }

        private void Click1030(object sender, RoutedEventArgs e)
        {
            var newEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0);
            var vm = DataContext as ViewModel;
            vm.EndTime = newEndTime;
            vm.ClockMode = false;
        }

        private void Click5(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.EndTime = DateTime.Now.AddMinutes(5);
            vm.ClockMode = false;
        }

        private void ClickReady(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.EndTime = DateTime.Now;
            vm.ClockMode = false;
        }

        private void CustomClick(object sender, RoutedEventArgs e)
        {
            double result;
            if (double.TryParse(this.CustomMinutes.Text, out result))
            {
                var vm = DataContext as ViewModel;
                vm.EndTime = DateTime.Now.AddMinutes(result);
                vm.ClockMode = false;
            }
        }
    }
}
