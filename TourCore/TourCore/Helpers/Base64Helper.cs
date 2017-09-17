using System;
using System.Security.Cryptography;
using System.Text;

namespace TourCore.Helpers
{
    public class Base64Helper
    {
        /// <summary>
        /// 解码(UTF-8)
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <returns></returns>
        public static string Decode(string str)
        {
            byte[] outputb = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(outputb);
        }
        /// <summary>
        /// 编码(UTF-8)
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <returns></returns>
        public static string Code(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
    }
    public class MD5Helper
    {
        /// <summary>
        /// MD5编码
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <returns></returns>
        public static string Code(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }
            return byte2String;
        }
    }
}
