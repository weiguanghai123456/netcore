using System;
using Microsoft.AspNetCore.Mvc;
using TourCore.JsonDef;
using TourCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourCore.Controllers
{
    public class NewController : Controller
    {
        /// <summary>
        /// 新闻分页
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public JsonResult GetList(int? limit, int? offset)
        {
            ResponseResult result = new ResponseResult();
            if (limit == null || offset == null)
            {
                return Json(result);
            }
            result.SetResult(SqliteHelper.Instacne.GetNews(limit, offset));
            return Json(result);
        }
        /// <summary>
        /// 根据id获得新闻内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetOne(string id)
        {
            ResponseResult result = new ResponseResult();
            id = StringUtil.Check(id);
            New @new = SqliteHelper.Instacne.GetNew(id);
            if (@new == null)
            {
                result.msg = "无此结果";
                return Json(result);
            }
            result.SetResult(@new);
            return Json(result);
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("Permission")]
        [EnableCors("MyDomain")]
        public JsonResult Delete(string id)
        {
            ResponseResult result = new ResponseResult();
            id = StringUtil.Check(id);
            New @new = SqliteHelper.Instacne.GetNew(id);
            if (@new == null)
            {
                result.msg = "新闻不存在";
                return Json(result);
            }
            bool ret = SqliteHelper.Instacne.DeleteNew(id);
            if (!ret)
            {
                result.msg = "删除失败";
                return Json(result);
            }
            result.SetResult(ret);
            return Json(result);
        }


        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="photo"></param>
        /// <param name="type"></param>
        /// <param name="createTime"></param>
        /// <returns></returns>
        [Authorize("Permission")]
        [EnableCors("MyDomain")]
        public JsonResult Insert(string title, string content, string photo, string type)
        {
            ResponseResult result = new ResponseResult();
            title = StringUtil.Check(title);
            content = StringUtil.Check(content);
            photo = StringUtil.Check(photo);
            type = StringUtil.Check(type);

            New @new = new New();
            @new.id = Guid.NewGuid().ToString();
            @new.title = title;
            @new.content = content;
            @new.photo = photo;
            @new.type = type;
            @new.createTime = DateTimeHelper.GetNow();
            @new.updateTime = DateTimeHelper.GetNow();
            bool ret = SqliteHelper.Instacne.InsertNew(@new);
            if (!ret)
            {
                result.msg = "插入新闻失败";
                return Json(result);
            }
            result.SetResult(ret);
            return Json(result);
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="photo"></param>
        /// <param name="type"></param>
        /// <param name="updateTime"></param>
        /// <returns></returns>
        [Authorize("Permission")]
        [EnableCors("MyDomain")]
        public JsonResult Update(string id, string title, string content, string photo, string type)
        {
            ResponseResult result = new ResponseResult();
            id = StringUtil.Check(id);
            title = StringUtil.Check(title);
            content = StringUtil.Check(content);
            photo = StringUtil.Check(photo);
            type = StringUtil.Check(type);
            New @new = SqliteHelper.Instacne.GetNew(id);
            if (@new == null)
            {
                result.msg = "新闻不存在";
                return Json(result);
            }
            @new.title = title;
            @new.content = content;
            @new.photo = photo;
            @new.type = type;
            @new.updateTime = DateTimeHelper.GetNow();
            bool ret = SqliteHelper.Instacne.UpdateNew(@new);
            if (!ret)
            {
                result.msg = "更新新闻失败";
                return Json(result);
            }
            result.SetResult(ret);
            return Json(result);
        }
    }
}
