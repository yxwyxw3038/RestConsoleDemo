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
    public static  class BlackListBill
    {
        public static string GetAllBlackListInfo(string ParameterStr, int PageSize, int CurrentPage)
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
                List<tbBlackList> tblist = new List<tbBlackList>();
            

                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.tbBlackList.Where(LinqHelper.GetFilterExpression<tbBlackList>(whereList).Compile()).Count<tbBlackList>();

                }
                else
                {
                    DataCount = myDbContext.tbBlackList.Count<tbBlackList>();
                }
                if (whereList.Count > 0)
                {
                    tblist = myDbContext.tbBlackList.Where(LinqHelper.GetFilterExpression<tbBlackList>(whereList).Compile()).OrderByDescending(p => p.CreateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (tblist == null)
                    {
                        tblist = new List<tbBlackList>();
                    }
                }
                else
                {
                    tblist = myDbContext.tbBlackList.OrderByDescending(p => p.CreateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }


                if (tblist != null && tblist.Count > 0)
                {

                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无黑名单信息", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string AddBlackList(string BlackListStr)
        {
            string str = string.Empty;
            try
            {
                tbBlackList tb = JsonConvert.DeserializeObject<tbBlackList>(BlackListStr);

                //tbBlackList newtb = new tbBlackList()
                //{
                //    Address = tb.Address,
                //    IsAble = tb.IsAble,
                //    Notes = tb.Notes,
                //    Port = tb.Port,
                //    CreateTime = DateTime.Now
                //};
                tbBlackList newtb = new tbBlackList();
                string[] keys = { "Id" };
                ObjectHelper.CopyValueNotKey(tb, newtb, keys);
                newtb.CreateTime = DateTime.Now;
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                int DataCount = myDbContext.tbBlackList.Where(p => p.Address == tb.Address).Count<tbBlackList>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("黑名单IP：{0}重复，请重新输入", tb.Address));
                }
                myDbContext.tbBlackList.Add(newtb);
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string DeleteBlackList(string[] BlackList)
        {
            string str = string.Empty;
            try
            {

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                foreach (string temp in BlackList)
                {
                    int delId = Convert.ToInt32(temp);
                    List<tbBlackList> delList = myDbContext.tbBlackList.Where(p => p.Id == delId).ToList();
                    if (delList != null && delList.Count > 0)
                    {
                        myDbContext.tbBlackList.Remove(delList[0]);
                    }
                }



                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "删除成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string UpdateBlackList(string BlackListStr)
        {
            string str = string.Empty;
            try
            {
                tbBlackList tb = JsonConvert.DeserializeObject<tbBlackList>(BlackListStr);
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbBlackList data = myDbContext.tbBlackList.Where(p => p.Id == tb.Id).FirstOrDefault();
                //data.Address = tb.Address;
                //data.CreateTime = tb.CreateTime;
                //data.IsAble = tb.IsAble;
                //data.Notes = tb.Notes;
                //data.Port = tb.Port;
                string [] keys={"Id"};
                ObjectHelper.CopyValueNotKey(tb, data, keys);

                int DataCount = myDbContext.tbBlackList.Where(p => p.Address == data.Address && p.Id != data.Id).Count<tbBlackList>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("黑名单IP：{0}重复，请重新输入", data.Address));
                }
              

                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "更新成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }


        public static string GetBlackListById(int Id)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbBlackList temp = new tbBlackList();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbBlackList> templist = myDbContext.tbBlackList.Where(p => p.Id == Id).ToList();
                if (templist != null && templist.Count > 0)
                {

                    temp = templist[0];
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "部门不存在", str);
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
