using System;

namespace TourCore.Helpers
{
    public class JsonHelper
    {
        public static string ObjToString(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static T StringToObj<T>(string json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }
    }
}
