using System;

namespace Ecclesiastes3
{
    public class ViewModel
    {
        public DateTime EndTime { get; set; }

        public Boolean ClockMode { get; set; }

        public Boolean ReadyMode { get; set; }

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
