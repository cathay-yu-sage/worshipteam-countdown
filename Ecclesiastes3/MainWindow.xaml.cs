using System;
using System.Linq;
using System.Windows;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Ecclesiastes3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Timer _refreshTimer = new Timer(1000);
        private readonly CountdownDisplayWindow _displayWindow = new CountdownDisplayWindow();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
            _refreshTimer.Elapsed += OnTimer;
            _refreshTimer.Enabled = true;
            _displayWindow.Show();

            MaximizeDisplayWindowOnSecondary();
            WindowState = WindowState.Maximized;
        }

        private void MaximizeDisplayWindowOnSecondary()
        {
            if (Screen.AllScreens.Length >= 2)
            {
                var secondary = Screen.AllScreens.First(s => !s.Primary);
                var secondaryArea = secondary.WorkingArea;
                _displayWindow.Left = secondaryArea.Left;
                _displayWindow.Top = secondaryArea.Top;
                _displayWindow.Width = secondaryArea.Width;
                _displayWindow.Height = secondaryArea.Height;
                _displayWindow.WindowState = WindowState.Maximized;
            }
        }

        private void OnTimer(object source, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var vm = DataContext as ViewModel;
                PreviewLabel.Content = vm.CountdownValue;
                CurrentTimeLabel.Content = vm.CurrentTimeValue;
                _displayWindow.SetDisplayValue(vm.CountdownValue);
                ReadyButton.Content = vm.ReadyModeButtonContent;
            });
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
            vm.ReadyMode = false;
        }

        private void Click5(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.EndTime = DateTime.Now.AddMinutes(5);
            vm.ClockMode = false;
            vm.ReadyMode = false;
        }

        private void ClickReady(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.EndTime = DateTime.Now;
            vm.ClockMode = false;
            vm.ReadyMode = !vm.ReadyMode;
        }

        private void CustomClick(object sender, RoutedEventArgs e)
        {
            double result;
            if (double.TryParse(this.CustomMinutes.Text, out result))
            {
                var vm = DataContext as ViewModel;
                vm.EndTime = DateTime.Now.AddMinutes(result);
                vm.ClockMode = false;
                vm.ReadyMode = false;
            }
        }
    }
}
