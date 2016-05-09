using System;
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

        public DateTime NextTargetTimeValue
        {
            get
            {
                if (DateTime.Now.Hour == 9)
                    return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 00, 0);
                if (DateTime.Now.Hour == 10)
                    return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0);
                if (DateTime.Now.Hour == 11)
                    return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 35, 0);
                return DateTime.Now;
            }
        }

        public String TargetTimeButtonContent
        {
            get
            {
                if (DateTime.Now >= NextTargetTimeValue)
                    return "Standby";
                return "Countdown to " + NextTargetTimeValue.ToShortTimeString();
            }
        }
    }
}
