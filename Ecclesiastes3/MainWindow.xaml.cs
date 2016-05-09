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
                _displayWindow.WindowStyle = WindowStyle.None;
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
                CountdownToTimeButton.Content = vm.TargetTimeButtonContent;
            });
        }

        private void ToggleClockMode(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.ClockMode = !vm.ClockMode;
        }

        private void ToggleFlashMode(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.FlashMode = !vm.FlashMode;
        }

        private void Click1030(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.EndTime = vm.NextTargetTimeValue;
            vm.ClockMode = false;
            vm.ReadyMode = false;
            vm.FlashMode = false;
        }

        private void Click5(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.EndTime = DateTime.Now.AddMinutes(5);
            vm.ClockMode = false;
            vm.ReadyMode = false;
            vm.FlashMode = false;
        }

        private void ClickReady(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.EndTime = DateTime.Now;
            vm.ClockMode = false;
            vm.ReadyMode = !vm.ReadyMode;
            vm.FlashMode = false;
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
                vm.FlashMode = false;
            }
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text = String.Empty;
        }

        private void ZeroClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += "0";
        }

        private void DecimalClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += ".";
        }

        private void OneClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += "1";
        }

        private void TwoClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += "2";
        }

        private void ThreeClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += "3";
        }

        private void FourClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += "4";
        }

        private void FiveClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += "5";
        }

        private void SixClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += "6";
        }

        private void SevenClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += "7";
        }

        private void EightClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += "8";
        }

        private void NineClick(object sender, RoutedEventArgs e)
        {
            CustomMinutes.Text += "9";
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
    }
}
