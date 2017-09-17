using System;
using Microsoft.AspNetCore.Mvc;
using TourCore.Helpers;
using TourCore.JsonDef;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourCore.Controllers
{

    public class UserController : Controller
    {
        public JsonResult Login(string name, string pwd)
        {
            ResponseResult result = new ResponseResult();
            if (name == null || pwd == null)
            {
                return Json(result);
            }
            User user;
            if (!SqliteHelper.Instacne.CheckUser(name, pwd, out user))
            {
                result.code = -2;
                result.msg = "账号或密码错误";
                return Json(result);
            }
            string value = MD5Helper.Code(name + pwd + "royalnu" + DateTimeHelper.GetNow());
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddHours(2);
            Response.Cookies.Append("auth", value, options);
            result.SetResult(user);
            return Json(result);
        }

    }
}
