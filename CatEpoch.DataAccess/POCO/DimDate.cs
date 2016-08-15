using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
    public class DimDate
    {
        public int PeriodId { get; set; }
        public Period Period { get; set; }
        
        public int DateKey { get; protected set; }
        public DateTime DateAltKey { get; private set; }
        public int Year { get; private set; }
        public int Quarter { get; private set; }
        public int Month { get; private set; }
        public string MonthName { get; private set; }
        public int DayOfMonth { get; private set; }
        public int DayOfWeek { get; private set; }
        public string DayName { get; private set; }
        public int WeekOfMonth { get; private set; }
        
    }
}
