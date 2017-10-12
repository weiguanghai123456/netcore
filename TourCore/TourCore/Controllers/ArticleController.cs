using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourCore.Helpers;
using TourCore.JsonDef;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourCore.Controllers
{
    public class ArticleController : Controller
    {
        /// <summary>
        /// 文章分页
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
            result.SetResult(SqliteHelper.Instacne.GetArticles(limit, offset));
            return Json(result);
        }
        /// <summary>
        /// 根据id获得文章内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetOne(string id)
        {
            ResponseResult result = new ResponseResult();
            id = StringUtil.Check(id);
            Article article = SqliteHelper.Instacne.GetArticle(id);
            if (article == null)
            {
                result.msg = "无此结果";
                return Json(result);
            }
            result.SetResult(article);
            return Json(result);
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("Permission")]
        [EnableCors("MyDomain")]
        public JsonResult Delete(string id)
        {
            ResponseResult result = new ResponseResult();
            id= StringUtil.Check(id);
            Article article = SqliteHelper.Instacne.GetArticle(id);
            if (article == null)
            {
                result.msg = "文章不存在";
                return Json(result);
            }
            bool ret = SqliteHelper.Instacne.DeleteArticle(id);
            if (!ret)
            {
                result.msg = "删除失败";
                return Json(result);
            }
            result.SetResult(ret);
            return Json(result);
        }
        /// <summary>
        /// 添加文章
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

            Article article = new Article();
            article.id = Guid.NewGuid().ToString();
            article.title = title;
            article.content = content;
            article.photo = photo;
            article.type = type;
            article.createTime = DateTimeHelper.GetNow();
            article.updateTime = DateTimeHelper.GetNow();
            bool ret = SqliteHelper.Instacne.InsertArticle(article);
            if (!ret)
            {
                result.msg = "插入文章失败";
                return Json(result);
            }
            result.SetResult(ret);
            return Json(result);
        }
        /// <summary>
        /// 更新文章
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
            Article article = SqliteHelper.Instacne.GetArticle(id);
            if (article == null)
            {
                result.msg = "文章不存在";
                return Json(result);
            }
            article.title = title;
            article.content = content;
            article.photo = photo;
            article.type = type;
            article.updateTime = DateTimeHelper.GetNow();
            bool ret = SqliteHelper.Instacne.UpdateArticle(article);
            if (!ret)
            {
                result.msg = "更新文章失败";
                return Json(result);
            }
            result.SetResult(ret);
            return Json(result);
        }
    }
}
