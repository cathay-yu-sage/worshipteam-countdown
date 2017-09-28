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
                //_displayWindow.WindowStyle = WindowStyle.None;
            }
        }

        private void OnTimer(object source, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var vm = DataContext as ViewModel;
                PreviewLabel.Background = vm.PreviewBackgroundColor;
                PreviewLabel.Foreground = vm.PreviewForegroundColor;
                _displayWindow.CountdownDisplay.Background = vm.DisplayBackgroundColor;
                _displayWindow.CountdownDisplay.Foreground = vm.DisplayForegroundColor;
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

        private void ClickReady(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.EndTime = DateTime.Now;
            vm.ClockMode = false;
            vm.ReadyMode = !vm.ReadyMode;
            vm.FlashMode = false;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            _displayWindow.Close();
        }

        private void FlashButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.FlashMode = !vm.FlashMode;
        }

        private void TargetTime_Click(object sender, RoutedEventArgs e)
        {
            short hour, minutes;
            if (Int16.TryParse(HourTextBox.Text, out hour) && Int16.TryParse(MinuteTextBox.Text, out minutes)
                && (hour >= 0) && (hour < 24) && (minutes >= 0) && (minutes < 60))
            {
                DateTime newTargetTime = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    hour,
                    minutes,
                    0);
                var vm = DataContext as ViewModel;
                vm.EndTime = newTargetTime;
                vm.ClockMode = false;
            }
        }

        private void AddAMinute_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.EndTime = vm.EndTime.AddMinutes(1);
            vm.ClockMode = false;
        }
    }
}
