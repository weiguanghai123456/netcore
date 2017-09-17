using Microsoft.Data.Sqlite;
using System.IO;
using System.Data;
using TourCore.JsonDef;
using Microsoft.DotNet.PlatformAbstractions;

namespace TourCore.Helpers
{
    public class SqliteHelper
    {
        static SqliteHelper instance = new SqliteHelper();
        private string connStr = string.Empty;
        private SqliteHelper()
        {
            string currentDirectory = ApplicationEnvironment.ApplicationBasePath;
            string dbPath = currentDirectory + @"tour.db;";
            connStr = @"Data Source=" + dbPath;
        }
        public static SqliteHelper Instacne { get { return instance; } }

        /// <summary>
        /// 是否存在这个账号密码
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>true 表示存在  false 表示不存在</returns>
        public bool CheckUser(string name, string pwd, out User user)
        {
            user = new User();
            SqliteParameter[] parameters = new SqliteParameter[] { new SqliteParameter("@name", name), new SqliteParameter("@pwd", pwd) };
            string sql = "SELECT * FROM users WHERE name=@name AND pwd=@pwd";
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                SqliteCommand commd = conn.CreateCommand();
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                commd.Parameters.AddRange(parameters);
                SqliteDataReader dr = commd.ExecuteReader();
                while (dr.Read())
                {
                    user.id = dr["id"].ToString();
                    user.name = dr["name"].ToString();
                    user.pwd = dr["pwd"].ToString();
                    user.power = dr["power"].ToString();
                    user.photo = dr["photo"].ToString();
                    user.alias = dr["alias"].ToString();
                    string test= dr["enable"].ToString();
                    user.enable = test;
                    user.createTime = dr["createTime"].ToString();
                    user.updateTime = dr["updateTime"].ToString();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 查询某个表的所有纪录
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        private int SelectTableCount(string tableName)
        {
            string sql = "select count(*) from " + tableName;
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                SqliteCommand commd = conn.CreateCommand();
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                SqliteDataReader dr = commd.ExecuteReader();
                if (dr.Read())
                {
                    return int.Parse(dr[0].ToString());
                }
            }
            return 0;
        }
        /// <summary>
        /// 获得新闻分页
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public PageVo<New> GetNews(int? limit, int? offset)
        {
            //string sql = string.Format("select* from news order by id limit 0,5");
            string tableName = "news";
            PageVo<New> pageVo = new PageVo<New>();
            pageVo.total = SelectTableCount(tableName);
            pageVo.rows = new System.Collections.Generic.List<New>();
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = string.Format("select * from {0} limit {1},{2}", tableName, limit, offset);
                SqliteCommand commd = conn.CreateCommand();
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                SqliteDataReader dr = commd.ExecuteReader();
                while (dr.Read())
                {
                    New n = new New();
                    n.id = dr["id"].ToString();
                    n.title = dr["title"].ToString();
                    n.content = dr["content"].ToString();
                    n.type = dr["type"].ToString();
                    n.photo = dr["photo"].ToString();
                    n.createTime = dr["createTime"].ToString();
                    n.updateTime = dr["updateTime"].ToString();
                    pageVo.rows.Add(n);
                }
            }
            return pageVo;
        }
        /// <summary>
        /// 获得文章分页
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public PageVo<Article> GetArticles(int? limit, int? offset)
        {
            //string sql = string.Format("select* from news order by id limit 0,5");
            string tableName = "articles";
            PageVo<Article> pageVo = new PageVo<Article>();
            pageVo.total = SelectTableCount(tableName);
            pageVo.rows = new System.Collections.Generic.List<Article>();
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = string.Format("select * from {0} limit {1},{2}", tableName, limit, offset);
                SqliteCommand commd = conn.CreateCommand();
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                SqliteDataReader dr = commd.ExecuteReader();
                while (dr.Read())
                {
                    Article n = new Article();
                    n.id = dr["id"].ToString();
                    n.title = dr["title"].ToString();
                    n.content = dr["content"].ToString();
                    n.type = dr["type"].ToString();
                    n.photo = dr["photo"].ToString();
                    n.createTime = dr["createTime"].ToString();
                    n.updateTime = dr["updateTime"].ToString();
                    pageVo.rows.Add(n);
                }
            }
            return pageVo;
        }

        /// <summary>
        /// 根据id获得文章内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Article GetArticle(string id)
        {
            string tableName = "articles";
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = string.Format("select * from {0} where id={1}", tableName, id);
                SqliteCommand commd = conn.CreateCommand();
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                SqliteDataReader dr = commd.ExecuteReader();
                while (dr.Read())
                {
                    Article article = new Article();
                    article.id = dr["id"].ToString();
                    article.title = dr["title"].ToString();
                    article.content = dr["content"].ToString();
                    article.content = dr["type"].ToString();
                    article.photo = dr["photo"].ToString();
                    article.createTime = dr["createTime"].ToString();
                    article.updateTime = dr["updateTime"].ToString();
                    return article;
                }
            }
            return null;
        }

        /// <summary>
        /// 根据id获得新闻内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public New GetNew(string id)
        {
            string tableName = "news";
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = string.Format("select * from {0} where id={1}", tableName, id);
                SqliteCommand commd = conn.CreateCommand();
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                SqliteDataReader dr = commd.ExecuteReader();
                while (dr.Read())
                {
                    New n = new New();
                    n.id = dr["id"].ToString();
                    n.title = dr["title"].ToString();
                    n.content = dr["content"].ToString();
                    n.content = dr["type"].ToString();
                    n.photo = dr["photo"].ToString();
                    n.createTime = dr["createTime"].ToString();
                    n.updateTime = dr["updateTime"].ToString();
                    return n;
                }
            }
            return null;
        }

        public bool DeleteArticle(string id)
        {
            string tableName = "articles";
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = string.Format("delete * from {0} where id={1}", tableName, id);
                SqliteCommand commd = conn.CreateCommand();
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                int ret = commd.ExecuteNonQuery();
                return (ret > 0);
            }
        }

        public bool DeleteNew(string id)
        {
            string tableName = "news";
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = string.Format("delete * from {0} where id={1}", tableName, id);
                SqliteCommand commd = conn.CreateCommand();
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                int ret = commd.ExecuteNonQuery();
                return (ret > 0);
            }
        }

        public bool InsertArticle(Article article)
        {
            string tableName = "articles";
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = string.Format("insert into {0} (id,title,content,type,photo,createTime,updateTime) values(@id,@title,@content,@type,@photo,@createTime,@updateTime)", tableName);
                SqliteParameter[] parameters = new SqliteParameter[] {
                    new SqliteParameter("@id", article.id),
                    new SqliteParameter("@title", article.title),
                    new SqliteParameter("@content", article.content),
                    new SqliteParameter("@type", article.type),
                    new SqliteParameter("@photo", article.photo),
                    new SqliteParameter("@createTime", article.createTime),
                    new SqliteParameter("@updateTime", article.updateTime)
                };
                SqliteCommand commd = conn.CreateCommand();
                commd.Parameters.AddRange(parameters);
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                int ret = commd.ExecuteNonQuery();
                return ret > 0;
            }
        }

        public bool InsertNew(New @new)
        {
            string tableName = "news";
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = string.Format("insert into {0} (id,title,content,type,photo,createTime,updateTime) values(@id,@title,@content,@type,@photo,@createTime,@updateTime)", tableName);
                SqliteParameter[] parameters = new SqliteParameter[] {
                    new SqliteParameter("@id", @new.id),
                    new SqliteParameter("@title", @new.title),
                    new SqliteParameter("@content", @new.content),
                    new SqliteParameter("@type", @new.type),
                    new SqliteParameter("@photo", @new.photo),
                    new SqliteParameter("@createTime", @new.createTime),
                    new SqliteParameter("@updateTime", @new.updateTime)
                };
                SqliteCommand commd = conn.CreateCommand();
                commd.Parameters.AddRange(parameters);
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                int ret = commd.ExecuteNonQuery();
                return ret > 0;
            }
        }

        public bool UpdateArticle(Article article)
        {
            string tableName = "articles";
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = string.Format("update {0} set title=@title,content=@content,type=@type,photo=@photo,updateTime=@updateTime) where id = @id", tableName);
                SqliteParameter[] parameters = new SqliteParameter[] {
                    new SqliteParameter("@id", article.id),
                    new SqliteParameter("@title", article.title),
                    new SqliteParameter("@content", article.content),
                    new SqliteParameter("@type", article.type),
                    new SqliteParameter("@photo", article.photo),
                    new SqliteParameter("@updateTime", DateTimeHelper.GetNow())
                };
                SqliteCommand commd = conn.CreateCommand();
                commd.Parameters.AddRange(parameters);
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                int ret = commd.ExecuteNonQuery();
                return ret > 0;
            }
        }

        public bool UpdateNew(New @new)
        {
            string tableName = "news";
            using (SqliteConnection conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = string.Format("update {0} set title=@title,content=@content,type=@type,photo=@photo,updateTime=@updateTime) where id = @id", tableName);
                SqliteParameter[] parameters = new SqliteParameter[] {
                    new SqliteParameter("@id", @new.id),
                    new SqliteParameter("@title", @new.title),
                    new SqliteParameter("@content", @new.content),
                    new SqliteParameter("@type", @new.type),
                    new SqliteParameter("@photo", @new.photo),
                    new SqliteParameter("@updateTime", DateTimeHelper.GetNow())
                };
                SqliteCommand commd = conn.CreateCommand();
                commd.Parameters.AddRange(parameters);
                commd.CommandType = CommandType.Text;
                commd.CommandText = sql;
                int ret = commd.ExecuteNonQuery();
                return ret > 0;
            }
        }


    }
}

