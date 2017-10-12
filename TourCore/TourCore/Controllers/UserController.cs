using System;
using Microsoft.AspNetCore.Mvc;
using TourCore.Helpers;
using TourCore.JsonDef;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using AuthorizePolicy.JWT;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourCore.Controllers
{
    [Authorize("Permission")]
    [EnableCors("MyDomain")]
    public class UserController : Controller
    {
        /// <summary>
        /// 自定义策略参数
        /// </summary>
        PermissionRequirement _requirement;
        public UserController(IAuthorizationHandler authorizationHander)
        {
            _requirement = (authorizationHander as PermissionHandler).Requirement;
        }
        [AllowAnonymous]
        public JsonResult Login(string name, string pwd, string role="")
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
            //如果是基于角色的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
            var claims = new Claim[] { new Claim(ClaimTypes.Name, name), new Claim(ClaimTypes.Role, role) };
            //用户标识
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(claims);
            var authorization = JwtToken.BuildJwtTokenString(claims, _requirement);
            user.authorization = authorization;
            result.SetResult(user);
            return Json(result);
        }
        public IActionResult Logout()
        {
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("/api/denied")]
        public IActionResult Denied()
        {
            return new JsonResult(new
            {
                Status = false,
                Message = "无权限访问"
            });
        }

    }
}
