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
     public  class BillNoBill
    {
         public static string GetAllBillNoInfo(string ParameterStr, int PageSize, int CurrentPage)
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
                List<tbBillNo> tblist = new List<tbBillNo>();


                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.tbBillNo.Where(LinqHelper.GetFilterExpression<tbBillNo>(whereList).Compile()).Count<tbBillNo>();

                }
                else
                {
                    DataCount = myDbContext.tbBillNo.Count<tbBillNo>();
                }
                if (whereList.Count > 0)
                {
                    tblist = myDbContext.tbBillNo.Where(LinqHelper.GetFilterExpression<tbBillNo>(whereList).Compile()).OrderByDescending(p => p.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (tblist == null)
                    {
                        tblist = new List<tbBillNo>();
                    }
                }
                else
                {
                    tblist = myDbContext.tbBillNo.OrderByDescending(p => p.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }


                if (tblist != null && tblist.Count > 0)
                {

                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无单据编号信息", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

         public static string AddBillNo(string BillNoStr)
        {
            string str = string.Empty;
            try
            {
                tbBillNo tb = JsonConvert.DeserializeObject<tbBillNo>(BillNoStr);


                tbBillNo newtb = new tbBillNo();
                string[] keys = { "Id" };
                ObjectHelper.CopyValueNotKey(tb, newtb, keys);
                newtb.CreateTime = DateTime.Now;
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                int DataCount = myDbContext.tbBillNo.Where(p => p.Code == tb.Code).Count<tbBillNo>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("单据编号：{0}重复，请重新输入", tb.Code));
                }
                myDbContext.tbBillNo.Add(newtb);
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

         public static string DeleteBillNo(string[] BillNoList)
        {
            string str = string.Empty;
            try
            {

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                foreach (string temp in BillNoList)
                {
                    int delId = Convert.ToInt32(temp);
                    List<tbBillNo> delList = myDbContext.tbBillNo.Where(p => p.Id == delId).ToList();
                    if (delList != null && delList.Count > 0)
                    {
                        myDbContext.tbBillNo.Remove(delList[0]);
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
         public static string UpdateBillNo(string tbBillNoStr)
        {
            string str = string.Empty;
            try
            {
                tbBillNo tb = JsonConvert.DeserializeObject<tbBillNo>(tbBillNoStr);
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbBillNo data = myDbContext.tbBillNo.Where(p => p.Id == tb.Id).FirstOrDefault();

                string[] keys = { "Id", "CurrentId", "CurrentBillNo", "CurrentTime" };
                ObjectHelper.CopyValueNotKey(tb, data, keys);
                data.UpdateTime = DateTime.Now;
                int DataCount = myDbContext.tbBillNo.Where(p => p.Code == data.Code && p.Id != data.Id).Count<tbBillNo>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("单据编号：{0}重复，请重新输入", data.Code));
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


         public static string GetBillNoById(int Id)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbBillNo temp = new tbBillNo();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbBillNo> templist = myDbContext.tbBillNo.Where(p => p.Id == Id).ToList();
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

         public static string GetBillNo(AchieveDBEntities DbContext, string Code)
         {
            string str = string.Empty;
            try
            {
                 tbBillNo temp= DbContext.tbBillNo.Where(p => p.Code == Code).FirstOrDefault();
                 if (temp == null)   throw  new Exception("单据编码信息未找到");
                 if (temp.EndLength < 0) throw new Exception("单据编码流水异常");
                 int nextnum = Convert.ToInt32( temp.CurrentId) + 1;
                 DateTime now = DateTime.Now;
                 DateTime CurrentTime = temp.CurrentTime == null ? now : Convert.ToDateTime( temp.CurrentTime);
                 int CurrentId =Convert.ToInt32( temp.CurrentId);
                 string ymd = string.Empty;
                 switch(temp.MaskInfo)
                 {
                     case "yyyy":
                           if(now.Year!=CurrentTime.Year)
                           {
                               CurrentId = 0;
                           }
                           ymd = now.ToString("yyyy");
                         break;
                     case "yyyyMM":
                         if (now.Year != CurrentTime.Year||now.Month!=CurrentTime.Month)
                         {
                             CurrentId = 0;
                         }
                         ymd = now.ToString("yyyyMM");
                         break;
                     case "yyyyMMdd":
                         if (now.Year != CurrentTime.Year || now.Month != CurrentTime.Month||now.Day!=CurrentTime.Day)
                         {
                             CurrentId = 0;
                         }
                         ymd = now.ToString("yyyyMMdd");
                         break;
                 }
                 CurrentId++;
                 str = string.Concat(temp.Code, ymd, CurrentId.ToString().PadLeft(temp.EndLength, '0'));
                 temp.CurrentTime = now;
                 temp.CurrentId = CurrentId;
                 temp.CurrentBillNo = str;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return str;
         }
     }
}
