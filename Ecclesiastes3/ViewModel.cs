using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Ecclesiastes3
{
    public class ViewModel
    {
        public DateTime EndTime { get; set; }

        public Boolean ClockMode { get; set; }

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
                    return (DateTime.Now.Second % 2 == 0) ?
                        String.Empty : "READY";
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

    }
}
