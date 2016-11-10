using System;
using System.Data;
using System.Windows.Media;

namespace Ecclesiastes3
{
    public class ViewModel
    {
        public DateTime EndTime { get; set; }

        public Boolean ClockMode { get; set; }

        public Boolean ReadyMode { get; set; }

        public Boolean FlashMode { get; set; }

        public Brush PreviewBackgroundColor
        {
            get
            {
                return (FlashMode && DateTime.Now.Second % 2 == 0) ?
                    Brushes.Yellow : Brushes.Black;
            }
        }

        public Brush DisplayBackgroundColor
        {
            get
            {
                return (FlashMode && DateTime.Now.Second % 2 == 0) ?
                    Brushes.White : Brushes.Black;
            }
        }

        public Brush PreviewForegroundColor
        {
            get
            {
                return (FlashMode && DateTime.Now.Second % 2 == 0) ?
                    Brushes.Black : Brushes.Yellow;
            }
        }

        public Brush DisplayForegroundColor
        {
            get
            {
                return (FlashMode && DateTime.Now.Second % 2 == 0) ?
                    Brushes.Black : Brushes.White;
            }
        }

        public String CountdownValue
        {
            get
            {
                if (ClockMode)
                {
                    return DateTime.Now.ToString("t");
                }

                if (DateTime.Now > EndTime)
                {
                    if (ReadyMode)
                    {
                        return (DateTime.Now.Second%2 == 0)
                            ? String.Empty
                            : "READY";
                    }
                    else
                    {
                        return "STANDBY";
                    }
                }
                var diffTime = EndTime - DateTime.Now;
                return diffTime.ToString(@"mm\:ss");
            }
        }

        public String CurrentTimeValue
        {
            get
            {
                return DateTime.Now.ToString("F");
            }
        }

        public String ReadyModeButtonContent
        {
            get { return ReadyMode ? "Standby" : "Ready"; }
        }
    }
}
