using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourCore.JsonDef
{
    public class ResponseResult
    {
        public int code = -1;
        public string msg = "通用错误";
        public object result = null;

        public void SetResult(object obj)
        {
            this.code = 0;
            this.msg = string.Empty;
            this.result = obj;
        }
    }
}
