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
using System.Windows.Shapes;

namespace Ecclesiastes3
{
    /// <summary>
    /// Interaction logic for CountdownDisplayWindow.xaml
    /// </summary>
    public partial class CountdownDisplayWindow : Window
    {
        public CountdownDisplayWindow()
        {
            InitializeComponent();
        }

        public void SetDisplayValue(String value)
        {
            CountdownDisplay.Content = value;
        }
    }
}
