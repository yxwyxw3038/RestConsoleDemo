using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace RestConsoleDemo.BLL.Helper
{
     public class DataCacheHelper
    {
        public static List<T> GetData<T>() where T : class, new()
        {
            List<T> objList = new List<T>();
            string jsons = string.Empty;
            string className = GetClassName<T>();
            Cache cache = HttpRuntime.Cache;

            objList = cache.Get(string.Concat(className, "Cache")) as List<T>;
            if(objList==null)
            {
                objList = new List<T>();
            }
            return objList;
        }

        public static void Updata<T>(List<T> t) where T : class, new()
        {
            string className = GetClassName<T>();
            Cache cache = HttpRuntime.Cache;
            try
            {
                cache.Remove(string.Concat(className, "Cache"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (t != null)
            {
                cache.Insert(string.Concat(className, "Cache"), t);
            }

        }

        #region 反射对象名
        private static string GetClassName<T>() where T : class, new()
        {
            T t = new T();
            string name = t.GetType().FullName;
            string[] nameList = name.Split('.');
            return nameList[nameList.Length - 1];
        }
        #endregion
    }
}
