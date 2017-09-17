using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourCore.Helpers
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 获得现在的时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <returns></returns>
        public static string GetNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
