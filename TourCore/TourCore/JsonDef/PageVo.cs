using System.Collections.Generic;

namespace TourCore.JsonDef
{
    public class PageVo<T>
    {
        public string orderby;
        public int limit;
        public int offset;
        public int total;
        public List<T> rows;
    }
}
