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
    public static class ButtonBLL
    {
        public static string GetButtonByMenuIdAndUserId(int menuId, int userId)
        {
            string str = string.Empty;
            List<tbButton> temp = new List<tbButton>();
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
               
                AchieveDBEntities myDbContext = new AchieveDBEntities();

                var query = from ur in myDbContext.tbUserRole
                            join rmb in myDbContext.tbRoleMenuButton on ur.RoleId equals rmb.RoleId
                            join b in myDbContext.tbButton on rmb.ButtonId equals b.Id
                            where ur.UserId == userId
                            &&  rmb.MenuId== menuId
                            orderby b.Sort ascending
                            select b;

                if (query != null )
                {

                  
                    str = JsonConvert.SerializeObject(query, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无按钮权限", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string GetButtonByMenuIdnForTransfer(int menuId)
        {
            string str = string.Empty;
         
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();

                List<tbMenuButton> tblist = myDbContext.tbMenuButton.Where(p => p.MenuId == menuId).ToList();

                if (tblist != null&& tblist.Count>0)
                {
                    for(int i=0;i<tblist.Count;i++)
                    {
                        if(i== tblist.Count-1)
                        {
                            str = string.Concat(str, tblist[i].ButtonId.ToString());
                        }
                        else
                        {
                            str = string.Concat(str, tblist[i].ButtonId.ToString(),",");
                        }
                    }

                   // str = JsonConvert.SerializeObject(query, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    //str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("0", "无按钮权限", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string GetAllButtonForTransfer()
        {
            

            string str = string.Empty;
            List<TransferModel> temp = new List<TransferModel>();
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();

                List<tbButton> tblist = new List<tbButton>();
                tblist = myDbContext.tbButton.ToList();

                if (tblist != null&&tblist.Count>0)
                {
                    foreach(var tp in tblist)
                    {
                        TransferModel tb = new TransferModel();
                        tb.key = tp.Id.ToString();
                        tb.label = tp.Name;
                        tb.disabled = false;
                        temp.Add(tb);

                    }

                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无按钮权限", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetAllButtonByMenuIdForTransfer(int menuId)
        {


            string str = string.Empty;
            List<TransferModel> temp = new List<TransferModel>();
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();

                var query = from b in myDbContext.tbButton
                            join  mb in myDbContext.tbMenuButton on b.Id equals mb.ButtonId
                            where mb.MenuId== menuId
                            orderby b.Sort ascending
                            select b;


                if (query != null)
                {
                    foreach (var tp in query)
                    {
                        TransferModel tb = new TransferModel();
                        tb.key = tp.Id.ToString();
                        tb.label = tp.Name;
                        tb.disabled = false;
                        temp.Add(tb);

                    }

                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "菜单无按钮信息", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string GetButtonByMenuIdRoleIdForTransfer(int menuId,int roleId)
        {
            string str = string.Empty;

            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();

                List<tbRoleMenuButton> tblist = myDbContext.tbRoleMenuButton.Where(p => p.MenuId == menuId  && p.RoleId== roleId).ToList();

                if (tblist != null && tblist.Count > 0)
                {
                    for (int i = 0; i < tblist.Count; i++)
                    {
                        if (i == tblist.Count - 1)
                        {
                            str = string.Concat(str, tblist[i].ButtonId.ToString());
                        }
                        else
                        {
                            str = string.Concat(str, tblist[i].ButtonId.ToString(), ",");
                        }
                    }

                    // str = JsonConvert.SerializeObject(query, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    //str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("0", "无按钮权限", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string SetMenuButton(int menuId,string buttonStr)
        {

            string str = string.Empty;
            
            try
            {

                string [] list = buttonStr.Split(',');
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbMenuButton> tblist = myDbContext.tbMenuButton.Where(p => p.MenuId == menuId).ToList();
                foreach (tbMenuButton st in tblist)
                {
                    bool findbj = false;
                    if (list.Length > 0)
                    {
                        foreach (string st1 in list)
                        {
                            if (st.Id.ToString() == st1)
                            {
                                findbj = true;
                                break;
                            }
                        }
                    }
                    if (!findbj)
                    {
                        myDbContext.tbMenuButton.Remove(st);
                    }
                }

                if (list.Length > 0)
                {
                    foreach (string st1 in list)
                    {
                        bool findbj = false;
                        foreach (tbMenuButton st in tblist)
                        {
                            if (st.Id.ToString() == st1)
                            {
                                findbj = true;
                                break;
                            }

                        }
                        if (!findbj)
                        {
                            tbMenuButton newtb = new tbMenuButton()
                            {
                                ButtonId = Convert.ToInt32(st1),
                                 MenuId= menuId

                            };
                            myDbContext.tbMenuButton.Add(newtb);
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

        public static string SetButtonByMenuIdRoleId(int menuId, int roleId, string buttonStr)
        {

            string str = string.Empty;

            try
            {

                string[] list = buttonStr.Split(',');
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbRoleMenuButton> tblist = myDbContext.tbRoleMenuButton.Where(p => p.MenuId == menuId && p.RoleId== roleId).ToList();
                foreach (tbRoleMenuButton st in tblist)
                {
                   
                    bool findbj = false;
                    if (list.Length > 0)
                    {
                        foreach (string st1 in list)
                        {
                            if (st.Id.ToString() == st1)
                            {
                                findbj = true;
                                break;
                            }
                        }
                    }
                    if (!findbj)
                    {
                        myDbContext.tbRoleMenuButton.Remove(st);
                    }
                }

                if (list.Length > 0)
                {
                    foreach (string st1 in list)
                    {
                        if(string.IsNullOrEmpty(st1))
                        {
                            continue;
                        }
                        bool findbj = false;
                        foreach (tbRoleMenuButton st in tblist)
                        {
                            if (st.Id.ToString() == st1)
                            {
                                findbj = true;
                                break;
                            }

                        }
                        if (!findbj)
                        {
                            tbRoleMenuButton newtb = new tbRoleMenuButton()
                            {
                                ButtonId = Convert.ToInt32(st1),
                                MenuId = menuId,
                                 RoleId= roleId

                            };
                            myDbContext.tbRoleMenuButton.Add(newtb);
                        }
                    }
                }
                int DataCount = myDbContext.tbRoleMenu.Where(p => p.MenuId == menuId && p.RoleId == roleId).Count<tbRoleMenu>();
                if(DataCount<=0)
                {
                    tbRoleMenu temp = new tbRoleMenu()
                    {
                        MenuId = menuId,
                        RoleId = roleId
                    };
                    myDbContext.tbRoleMenu.Add(temp);
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

        public static string GetAllButtonInfo(string ParameterStr, int PageSize, int CurrentPage)
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
                List<tbButton> templist = new List<tbButton>();

                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.tbButton.Where(LinqHelper.GetFilterExpression<tbButton>(whereList).Compile()).Count<tbButton>();

                }
                else
                {
                    DataCount = myDbContext.tbButton.Count<tbButton>();
                }
                if (whereList.Count > 0)
                {
                    templist = myDbContext.tbButton.Where(LinqHelper.GetFilterExpression<tbButton>(whereList).Compile()).OrderByDescending(p => p.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (templist == null)
                    {
                        templist = new List<tbButton>();
                    }
                }
                else
                {
                    templist = myDbContext.tbButton.OrderByDescending(p => p.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
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
        public static string AddButton(string Deptstr)
        {
            string str = string.Empty;
            try
            {
                tbButton tb = JsonConvert.DeserializeObject<tbButton>(Deptstr);

                tbButton newtb = new tbButton()
                {
                    Name=tb.Name,
                    CreateBy = tb.CreateBy,
                    CreateTime = DateTime.Now,
                    Code = tb.Code,
                    Description = tb.Description,
                    Icon = tb.Icon,

                    Sort = tb.Sort,
                    UpdateBy = tb.CreateBy,
                    UpdateTime = DateTime.Now


                };
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                int DataCount = myDbContext.tbButton.Where(p => p.Name == tb.Name && p.Id != tb.Id).Count<tbButton>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("按钮名：{0}重复，请重新输入", tb.Name));
                }
                DataCount = myDbContext.tbButton.Where(p => p.Code == tb.Code && p.Id != tb.Id).Count<tbButton>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("按钮代码：{0}重复，请重新输入", tb.Code));
                }

                myDbContext.tbButton.Add(newtb);
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string DeleteButton(string[] ButtonList)
        {
            string str = string.Empty;
            try
            {

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                foreach (string temp in ButtonList)
                {
                    int delId = Convert.ToInt32(temp);
                    List<tbButton> delList = myDbContext.tbButton.Where(p => p.Id == delId).ToList();
                    if (delList != null && delList.Count > 0)
                    {
                        myDbContext.tbButton.Remove(delList[0]);
                    }
                    List<tbMenuButton> delMenuButtonList = new List<tbMenuButton>();
                    delMenuButtonList = myDbContext.tbMenuButton.Where(p => p.ButtonId == delId).ToList();
                    if (delMenuButtonList != null && delMenuButtonList.Count > 0)
                    {
                        foreach (var t in delMenuButtonList)
                        {
                            myDbContext.tbMenuButton.Remove(t);

                        }
                    }

                    List<tbRoleMenuButton> delRoleMenuButtonList = new List<tbRoleMenuButton>();
                    delRoleMenuButtonList = myDbContext.tbRoleMenuButton.Where(p => p.ButtonId == delId).ToList();
                    if (delRoleMenuButtonList != null && delRoleMenuButtonList.Count > 0)
                    {
                        foreach (var t in delRoleMenuButtonList)
                        {
                            myDbContext.tbRoleMenuButton.Remove(t);

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
        public static string UpdateButton(string Buttonstr)
        {
            string str = string.Empty;
            try
            {
                tbButton tb = JsonConvert.DeserializeObject<tbButton>(Buttonstr);
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbButton data = myDbContext.tbButton.Where(p => p.Id == tb.Id).FirstOrDefault();
                data.Code = tb.Code;
                data.Description = tb.Description;
                data.Icon = tb.Icon;
              
                data.Sort = tb.Sort;
                data.UpdateBy = tb.UpdateBy;
                data.UpdateTime = DateTime.Now;


                int DataCount = myDbContext.tbButton.Where(p => p.Name == data.Name && p.Id != data.Id).Count<tbButton>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("按钮名：{0}重复，请重新输入", data.Name));
                }
                DataCount = myDbContext.tbButton.Where(p => p.Code == data.Code && p.Id != data.Id).Count<tbButton>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("按钮代码：{0}重复，请重新输入", data.Code));
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


        public static string GetButtonById(int Id)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbButton temp = new tbButton();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbButton> templist = myDbContext.tbButton.Where(p => p.Id == Id).ToList();
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

    

        public static string GetButtonAllCount()
        {

            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                int Count = myDbContext.tbButton.Count<tbButton>();





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
