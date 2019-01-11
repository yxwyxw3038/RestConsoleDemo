using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestConsoleDemo.BLL.Helper;
using RestConsoleDemo.BLL.Model;
using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.SysInfo
{
    public class FlowBill
    {
        public static string GetAllFlowerInfo(string ParameterStr, int PageSize, int CurrentPage)
        {
            string str = string.Empty;


            try
            {
                List<FilterModel> whereList = new List<FilterModel>();
                if (!string.IsNullOrEmpty(ParameterStr))
                {
                    whereList = JsonConvert.DeserializeObject<List<FilterModel>>(ParameterStr);
                }
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<v_FlowViewInfo> tblist = new List<v_FlowViewInfo>();


                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.v_FlowViewInfo.Where(LinqHelper.GetFilterExpression<v_FlowViewInfo>(whereList).Compile()).Count<v_FlowViewInfo>();

                }
                else
                {
                    DataCount = myDbContext.v_FlowViewInfo.Count<v_FlowViewInfo>();
                }
                if (whereList.Count > 0)
                {
                    tblist = myDbContext.v_FlowViewInfo.Where(LinqHelper.GetFilterExpression<v_FlowViewInfo>(whereList).Compile()).OrderByDescending(p => p.CreateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (tblist == null)
                    {
                        tblist = new List<v_FlowViewInfo>();
                    }
                }
                else
                {
                    tblist = myDbContext.v_FlowViewInfo.OrderByDescending(p => p.CreateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }


                if (tblist != null && tblist.Count > 0)
                {

                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无流程信息", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetFlowerInfoByMenuId(int MenuId)
        {
            string str = string.Empty;


            try
            {
             
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<v_FlowViewInfo> tblist = new List<v_FlowViewInfo>();


                int DataCount = 0;

                tblist = myDbContext.v_FlowViewInfo.Where(p => p.MenuId == MenuId).ToList();
                if (tblist != null && tblist.Count > 0)
                {

                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无流程信息", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
    }
}
