using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Packages;

namespace Ware.Schedules
{
    /// <summary>
    /// Packages for scheduling
    /// </summary>
    public class ScheduledPackage
    {
        private Package package;
        private string time;
        private DayOfWeek day;
        private TransferType transferType;

        /// <summary>
        /// Packages for scheduling
        /// </summary>
        /// <param name="packageToSchedule">Package object to schedule</param>
        /// <param name="timeSchedule">Time you want it to be sent/received</param>
        /// <param name="dayOfWeek">Day</param>
        public ScheduledPackage(Package packageToSchedule, string timeSchedule, DayOfWeek dayOfWeek)
        {
            package = packageToSchedule;
            time = timeSchedule;
            day = dayOfWeek;
        }

        /// <summary>
        /// TransferType enum
        /// </summary>
        public TransferType TransferTypes
        {
            get { return transferType; }
            set { transferType = value; }
        }

        /// <summary>
        /// Package object
        /// </summary>
        public Package Packages
        {
            get { return package; }
            set { package = value; }
        }

        /// <summary>
        /// Time of transfer
        /// </summary>
        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// DayOfWeek enum
        /// </summary>
        public DayOfWeek Day
        {
            get { return day; }
            set { day = value; }
        }
    }

}
