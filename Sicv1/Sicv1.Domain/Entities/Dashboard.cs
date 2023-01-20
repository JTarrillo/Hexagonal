using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicv1.Domain.Entities
{
    //public class Chart_1 {
    //    //public Int32[] days { get; set; }
    //    //public Int32[] totales { get; set; }
    //    //public object(Int32,Int32) totales2 { get; set; }
    //    public Int32 days { get; set; }
    //    public Int32 total { get; set; }
    //    public Chart_1()
    //    {
    //        days = new Int32[] { };
    //        totales = new Int32[] { };
    //    }
    //}

    public class Chart_2
    {
        public string[] days { get; set; }
        public int[] bien { get; set; }
        public int[] nutr { get; set; }
        public int[] rend { get; set; }
        public int[] rela { get; set; }
        public int[] entr { get; set; }
    }
    public class Chart_3
    {
        public string[] days { get; set; }
        public int[] generated { get; set; }
        public int[] used { get; set; }
    }
    public class Dashboard : Result
	{
		public int CounterSchedule { get; set; }
        public int CounterUserAll { get; set; }
        public int CounterUserActive { get; set; }
        public int CounterSession { get; set; }
        public int CounterCouponGenerated { get; set; }
		public int CounterCouponApproved { get; set; }
		public int CounterUserUpdateInfo { get; set; }
        public int CounterUserAndroid { get; set; }
        public int CounterUserIos { get; set; }
        public List<Int64[]> chart__1 { get; set; }
        public Chart_2 chart__2 { get; set; }
        public Chart_3 chart__3 { get; set; }


        public Dashboard() {
            CounterSchedule = 0;
            CounterUserAll = 0;
            CounterUserActive = 0;
            CounterSession = 0;
            CounterCouponGenerated = 0;
            CounterCouponApproved = 0;
            CounterUserUpdateInfo = 0;
            CounterUserAndroid = 0;
            CounterUserIos = 0;
            chart__1 = new List<Int64[]>();
            chart__2 = new Chart_2();
            chart__3 = new Chart_3();
            //chart_1 = new Chart_1();
        }
	}
}
