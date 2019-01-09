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

namespace RestConsoleDemo.BLL.UserInfo
{
    public static class DeptBill
    {
        public static string GetDeptByUserIdForTransfer(int userId)
        {
            string str = string.Empty;

            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();

                List<tbUserDepartment> tblist = myDbContext.tbUserDepartment.Where(p => p.UserId == userId).ToList();

                if (tblist != null && tblist.Count > 0)
                {
                    for (int i = 0; i < tblist.Count; i++)
                    {
                        if (i == tblist.Count - 1)
                        {
                            str = string.Concat(str, tblist[i].DepartmentId.ToString());
                        }
                        else
                        {
                            str = string.Concat(str, tblist[i].DepartmentId.ToString(), ",");
                        }
                    }

                    // str = JsonConvert.SerializeObject(query, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    //str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("0", "无角色权限", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string GetAllDeptForTransfer()
        {


            string str = string.Empty;
            List<TransferModel> temp = new List<TransferModel>();
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();

                List<tbDepartment> tblist = new List<tbDepartment>();
                tblist = myDbContext.tbDepartment.ToList();

                if (tblist != null && tblist.Count > 0)
                {
                    foreach (var tp in tblist)
                    {
                        TransferModel tb = new TransferModel();
                        tb.key = tp.Id.ToString();
                        tb.label = tp.DepartmentName;
                        tb.disabled = false;
                        temp.Add(tb);

                    }

                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无角色权限", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string SetUserDept(int userId, string deptStr)
        {

            string str = string.Empty;

            try
            {

                string[] list = deptStr.Split(',');

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbUserDepartment> tblist = myDbContext.tbUserDepartment.Where(p => p.UserId == userId).ToList();
                foreach (tbUserDepartment st in tblist)
                {
                    bool findbj = false;
                    if (list.Length > 0)
                    {
                        foreach (string st1 in list)
                        {
                            if (st.DepartmentId.ToString() == st1)
                            {
                                findbj = true;
                                break;
                            }
                        }
                    }
                    if (!findbj)
                    {
                        myDbContext.tbUserDepartment.Remove(st);
                    }
                }

                if (list.Length > 0)
                {
                    foreach (string st1 in list)
                    {
                        if (!string.IsNullOrEmpty(st1))
                        {

                            bool findbj = false;
                            foreach (tbUserDepartment st in tblist)
                            {
                                if (st.DepartmentId.ToString() == st1)
                                {
                                    findbj = true;
                                    break;
                                }

                            }
                            if (!findbj)
                            {
                                tbUserDepartment newtb = new tbUserDepartment()
                                {
                                    DepartmentId = Convert.ToInt32(st1),
                                    UserId = userId

                                };
                                myDbContext.tbUserDepartment.Add(newtb);
                            }

                        }
                    }
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
        public static string GetAllDeptInfo(string ParameterStr, int PageSize, int CurrentPage)
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
                List<tbDepartment> templist = new List<tbDepartment>();

                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.tbDepartment.Where(LinqHelper.GetFilterExpression<tbDepartment>(whereList).Compile()).Count<tbDepartment>();

                }
                else
                {
                    DataCount = myDbContext.tbDepartment.Count<tbDepartment>();
                }
                if (whereList.Count > 0)
                {
                    templist = myDbContext.tbDepartment.Where(LinqHelper.GetFilterExpression<tbDepartment>(whereList).Compile()).OrderByDescending(p => p.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (templist == null)
                    {
                        templist = new List<tbDepartment>();
                    }
                }
                else
                {
                    templist = myDbContext.tbDepartment.OrderByDescending(p => p.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }


                if (templist != null && templist.Count > 0)
                {


                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "部门信息不存在", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string AddDept(string Deptstr)
        {
            string str = string.Empty;
            try
            {
                tbDepartment tb = JsonConvert.DeserializeObject<tbDepartment>(Deptstr);

                tbDepartment newtb = new tbDepartment()
                {
                    CreateBy = tb.CreateBy,
                    CreateTime = DateTime.Now,
                    DepartmentName = tb.DepartmentName,
                    ParentId = tb.ParentId,
                    Sort = tb.Sort,
                    UpdateBy = tb.CreateBy,
                    UpdateTime = DateTime.Now


                };
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                int DataCount = myDbContext.tbDepartment.Where(p => p.DepartmentName == newtb.DepartmentName).Count<tbDepartment>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("部门名：{0}重复，请重新输入", newtb.DepartmentName));
                }

                myDbContext.tbDepartment.Add(newtb);
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string DeleteDept(string[] DeptList)
        {
            string str = string.Empty;
            try
            {

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                foreach (string temp in DeptList)
                {
                    int delId = Convert.ToInt32(temp);
                    List<tbDepartment> delList = myDbContext.tbDepartment.Where(p => p.Id == delId).ToList();
                    if (delList != null && delList.Count > 0)
                    {
                        myDbContext.tbDepartment.Remove(delList[0]);
                    }
                    List<tbUserDepartment> delUserDeptList = new List<tbUserDepartment>();
                    delUserDeptList = myDbContext.tbUserDepartment.Where(p => p.DepartmentId == delId).ToList();
                    if (delUserDeptList != null && delUserDeptList.Count > 0)
                    {
                        foreach (var t in delUserDeptList)
                        {
                            myDbContext.tbUserDepartment.Remove(t);

                        }
                    }
                 


                }



                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string UpdateDept(string Deptstr)
        {
            string str = string.Empty;
            try
            {
                tbDepartment tb = JsonConvert.DeserializeObject<tbDepartment>(Deptstr);
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbDepartment data = myDbContext.tbDepartment.Where(p => p.Id == tb.Id).FirstOrDefault();
                data.DepartmentName = tb.DepartmentName;
                data.ParentId = tb.ParentId;
                data.Sort = tb.Sort;
                data.UpdateBy = tb.UpdateBy;
                data.UpdateTime = DateTime.Now;


                int DataCount = myDbContext.tbDepartment.Where(p => p.DepartmentName == data.DepartmentName && p.Id != data.Id).Count<tbDepartment>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("部门名：{0}重复，请重新输入", data.DepartmentName));
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


        public static string GetDeptById(int Id)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbDepartment temp = new tbDepartment();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbDepartment> templist = myDbContext.tbDepartment.Where(p => p.Id == Id).ToList();
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

        public static string GetCascaderDept()
        {
            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                var query = from m in myDbContext.tbDepartment
                            where m.ParentId == 0
                            orderby m.ParentId, m.Sort
                            select m;
                List<string> listtemp = new List<string>();
                string temp = string.Empty;
                if (query != null)
                {
                    foreach (var tp in query)
                    {
                        string str = GetChildDept(tp.Id);
                        if (string.IsNullOrEmpty(str))
                        {


                            str = "{\"value\":\"" + tp.Id.ToString() + "\" , \"label\":\"" + tp.DepartmentName + "\"}";

                        }
                        else
                        {
                            str = "{\"value\":\"" + tp.Id.ToString() + "\" ,\"label\":\"" + tp.DepartmentName + "\" , \"children\":[" + str + "]  }";
                        }

                        listtemp.Add(str);




                    }

                    for (int i = 0; i < listtemp.Count; i++)
                    {
                        if (i != listtemp.Count - 1)
                        {
                            temp = string.Concat(temp, listtemp[i], ",");
                        }
                        else
                        {
                            temp = string.Concat(temp, listtemp[i]);
                        }
                    }
                    // temp = string.Format("{0}", temp);

                }
                else
                {
                    temp = "";
                }


                temp = "[{\"value\":\"0\" , \"label\":\"部门根节点\", \"children\":[" + temp + "] }]";


                str1 = ResponseHelper.ResponseMsg("1", "取数成功", temp);

            }
            catch (Exception ex)
            {
                str1 = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str1;
        }
        public static string GetChildDept(int ParentId)
        {

            AchieveDBEntities myDbContext = new AchieveDBEntities();
            List<string> listtemp = new List<string>();
            var query = from m in myDbContext.tbDepartment
                        where m.ParentId == ParentId
                        orderby m.ParentId, m.Sort
                        select m;

            if (query != null)
            {
                foreach (var tp in query)
                {
                    string str = GetChildDept(tp.Id);
                    if (string.IsNullOrEmpty(str))
                    {


                        str = "{\"value\":\"" + tp.Id.ToString() + "\" , \"label\":\"" + tp.DepartmentName + "\"}";

                    }
                    else
                    {
                        str = "{\"value\":\"" + tp.Id.ToString() + "\" , \"label\":\"" + tp.DepartmentName + "\", \"children\":[" + str + "]  }";
                    }

                    listtemp.Add(str);




                }
                string temp = string.Empty;
                for (int i = 0; i < listtemp.Count; i++)
                {
                    if (i != listtemp.Count - 1)
                    {
                        temp = string.Concat(temp, listtemp[i], ",");
                    }
                    else
                    {
                        temp = string.Concat(temp, listtemp[i]);
                    }
                }
                return temp;
            }
            else
            {
                return "";
            }



        }

        public static string GetDeptAllCount()
        {

            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                int Count = myDbContext.tbDepartment.Count<tbDepartment>();





                str1 = ResponseHelper.ResponseMsg("1", "取数成功", Count.ToString());

            }
            catch (Exception ex)
            {
                str1 = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str1;
        }
    }
}
