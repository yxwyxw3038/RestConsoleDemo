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
    public static class NoticeBill
    {
        public static string GetAllNoticeInfo(string ParameterStr, int PageSize, int CurrentPage)
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
                List<tbNotice> tblist = new List<tbNotice>();
                List<NoticeViewModel> returnlist = new List<NoticeViewModel>();


                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.tbNotice.Where(LinqHelper.GetFilterExpression<tbNotice>(whereList).Compile()).Count<tbNotice>();

                }
                else
                {
                    DataCount = myDbContext.tbNotice.Count<tbNotice>();
                }
                if (whereList.Count > 0)
                {
                    tblist = myDbContext.tbNotice.Where(LinqHelper.GetFilterExpression<tbNotice>(whereList).Compile()).OrderByDescending(p => p.CreateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (tblist == null)
                    {
                        tblist = new List<tbNotice>();
                    }
                }
                else
                {
                    tblist = myDbContext.tbNotice.OrderByDescending(p => p.CreateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }


                if (tblist != null && tblist.Count > 0)
                {

                    foreach (tbNotice st in tblist)
                    {
                        NoticeViewModel temp = new NoticeViewModel();
                        int SendCount = myDbContext.tbNoticeUser.Where(p => p.NoticeCode == st.Code && p.SendFlag > 0).Count<tbNoticeUser>();
                        int Count = myDbContext.tbNoticeUser.Where(p => p.NoticeCode == st.Code).Count<tbNoticeUser>();
                  
                        ObjectHelper.CopyValue(st, temp);
                        temp.Count = Count;
                        temp.SendCount = SendCount;
                        returnlist.Add(temp);
                    }


                    str = JsonConvert.SerializeObject(returnlist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(returnlist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无通知信息", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetNoticeById(string Code)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbRole temp = new tbRole();
                //NoticeModel returnModel = new NoticeModel();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbNotice main = myDbContext.tbNotice.Where(p => p.Code == Code).ToList().FirstOrDefault();
         

              
                if (main != null)
                {


                    str = JsonConvert.SerializeObject(main, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(main, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "通知信息不存在", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetNoticeItemById(string Code)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbRole temp = new tbRole();
                //NoticeModel returnModel = new NoticeModel();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
               


                var query = from nu in myDbContext.tbNoticeUser
                            join u in myDbContext.tbUser on nu.UserId equals u.ID
                            where nu.NoticeCode == Code
                            orderby nu.id
                            select new NoticeUserModel
                            {
                                AccountName = u.AccountName,
                                CreateBy = nu.CreateBy,
                                CreateTime = nu.CreateTime,
                                id = nu.id,
                                NoticeCode = nu.NoticeCode,
                                SendFlag = nu.SendFlag,
                                SendTime = nu.SendTime,
                                UpdateBy = nu.UpdateBy,
                                UpdateTime = nu.UpdateTime,
                                UserId = nu.UserId,
                                RealName =u.RealName

                            };

                List<NoticeUserModel> item = new List<NoticeUserModel>();
                if (query != null )
                {
                    item = query.ToList();



                    str = JsonConvert.SerializeObject(item, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(item, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "通知信息不存在", str,0);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string AddNotice(string Noticetstr)
        {
            string str = string.Empty;
            try
            {
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                NoticeModel tb = JsonConvert.DeserializeObject<NoticeModel>(Noticetstr);
                string Code = Guid.NewGuid().ToString();
                string BillNo = BillNoBill.GetBillNo(myDbContext, "TZ");
                tbNotice main = tb.Main;
                List<NoticeUserModel> item = tb.Item;
                main.CreateTime = DateTime.Now;
                main.UpdateTime = DateTime.Now;
                main.NoticeTime = DateTime.Now;
                main.status = 0;
                main.Code = Code;
                main.No = BillNo;
                if (item != null && item.Count > 0)
                {
                    foreach (var st in item)
                    {
                        st.NoticeCode = Code;
                        st.SendFlag = 0;
                        st.UpdateTime = DateTime.Now;
                        st.CreateTime = DateTime.Now;
                    }
                }

                myDbContext.tbNotice.Add(main);
                foreach (var st in item)
                {
                    tbNoticeUser temp = new tbNoticeUser();
                    string[] keys = { "id" };
                    ObjectHelper.CopyValueNotKey(st, temp, keys);
                    myDbContext.tbNoticeUser.Add(temp);
                }
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", Code);

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string UpdateNotice(string Noticetstr)
        {
            string str = string.Empty;
            try
            {
                NoticeModel tb = JsonConvert.DeserializeObject<NoticeModel>(Noticetstr);

                tbNotice main = tb.Main;
                List<NoticeUserModel> item = tb.Item;

                main.UpdateTime = DateTime.Now;
                main.status = 0;

                if (item != null && item.Count > 0)
                {
                    foreach (var st in item)
                    {

                        st.UpdateTime = DateTime.Now;
                    }
                }
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbNotice mainDb = myDbContext.tbNotice.Where(p => p.Code == main.Code).ToList().FirstOrDefault();
                string[] keys = { "Code" };
                ObjectHelper.CopyValueNotKey(main, mainDb, keys);
                if (item != null && item.Count > 0)
                {
                    int?[] UserIds = new int?[item.Count];
                    int count = 0;
                    foreach (var st in item)
                    {
                         UserIds[count] = st.UserId;
                         count++;
                         tbNoticeUser temp = myDbContext.tbNoticeUser.Where(p => p.UserId == st.UserId && p.NoticeCode == main.Code).ToList().FirstOrDefault();
                          keys[0] = "id";
                         if (temp != null)
                          {

                                // st.NoticeCode = main.Code;
                                //ObjectHelper.CopyValueNotKey(st, temp, keys);
                           }
                           else
                           {
                                tbNoticeUser newtemp = new tbNoticeUser();
                                ObjectHelper.CopyValueNotKey(st, newtemp,keys);
                                newtemp.NoticeCode = main.Code;
                                myDbContext.tbNoticeUser.Add(newtemp);
                           }
                        

                    }

                    List<tbNoticeUser> tempList = myDbContext.tbNoticeUser.Where(p => !(UserIds).Contains(p.UserId) && p.NoticeCode == main.Code).ToList();
                    if(tempList!=null&&tempList.Count>0)
                    {
                        foreach(var st in  tempList)
                        {
                            myDbContext.tbNoticeUser.Remove(st);
                        }
                    }
                }
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", main.Code);

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string UpdateNoticeStatus(string Code, int oldStatus, int newStatus)
        {
            string str = string.Empty;
            try
            {
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbNotice main = myDbContext.tbNotice.Where(p => p.Code == Code).ToList().FirstOrDefault();
                if (main == null)
                {
                    throw new Exception("未找到相关数据！");
                }
                if (main.status != oldStatus)
                {
                    throw new Exception("数据已经被其它人操作，请刷新！");
                }
                main.status = newStatus;
                main.UpdateTime = DateTime.Now;
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;

        }

        public static string DeleteNotice(string[] NoticeList)
        {
            string str = string.Empty;
            try
            {

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                int i = 0;
                foreach (string temp in NoticeList)
                {

                    tbNotice delMain = myDbContext.tbNotice.Where(p => p.Code == temp).FirstOrDefault();
                    if (delMain == null)
                    {
                        continue;
                    }
                    if (delMain.status != 0)
                    {
                        continue;
                    }
                    myDbContext.tbNotice.Remove(delMain);
                    List<tbNoticeUser> item = myDbContext.tbNoticeUser.Where(p => p.NoticeCode == temp).ToList();
                    if (item != null && item.Count > 0)
                    {
                        foreach (var st in item)
                        {
                            myDbContext.tbNoticeUser.Remove(st);
                        }
                    }


                }



                myDbContext.SaveChanges();
                string msg = string.Format("提交{0}条数据，成功删除{1}条", NoticeList.Length, i);
                str = ResponseHelper.ResponseMsg("1", msg, "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }


    }
}
