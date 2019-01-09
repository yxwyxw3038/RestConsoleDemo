using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestConsoleDemo.BLL.Helper;
using RestConsoleDemo.BLL.Model;
using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL
{
    public static class MenuBill
    {
        public static string GetUserMenu(int userID,string token)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbUser temp = new tbUser();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                int count=  myDbContext.tbUserToken.Where(p => p.UserId == userID && p.Token == token).Count<tbUserToken>();
                if(count<=0)
                {
                    throw new Exception("帐号异常,请退出系统！");
                }
                List<tbUser> templist = myDbContext.tbUser.Where(p => p.ID == userID).ToList();
                if (templist != null && templist.Count > 0)
                {
                    var query = from ur in myDbContext.tbUserRole
                                join rmb in myDbContext.tbRoleMenu on ur.RoleId equals rmb.RoleId
                                join m in myDbContext.tbMenu on rmb.MenuId equals m.Id
                                where ur.UserId == userID
                                && m.ParentId == 0
                                orderby m.ParentId, m.Sort
                                select m;
                    List<MenuTreeModel> list = new List<MenuTreeModel>();
                    if (query != null)
                    {
                        foreach (var tp in query)
                        {
                            MenuTreeModel m = new MenuTreeModel();
                            m.Id = tp.Id.ToString();
                            m.Icon = tp.Icon;
                            m.MenuName = tp.Name;
                            m.ParentId = tp.ParentId.ToString();
                            m.Url = tp.LinkAddress;
                            m.Node = GetChildMenu(userID, tp.Id);
                            list.Add(m);
                        }
                    }

                    str = JsonConvert.SerializeObject(list, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "用户ID不存在", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetMenuById(int Id)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbMenu temp = new tbMenu();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbMenu> templist = myDbContext.tbMenu.Where(p => p.Id == Id).ToList();
                if (templist != null && templist.Count > 0)
                {

                    temp = templist[0];
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "菜单不存在", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }


        public static List<MenuTreeModel> GetChildMenu(int userID, int ParentId)
        {
            AchieveDBEntities myDbContext = new AchieveDBEntities();
            List<MenuTreeModel> listTemp = new List<MenuTreeModel>();
            var query = from ur in myDbContext.tbUserRole
                        join rmb in myDbContext.tbRoleMenu on ur.RoleId equals rmb.RoleId
                        join m in myDbContext.tbMenu on rmb.MenuId equals m.Id
                        where ur.UserId == userID
                        && m.ParentId == ParentId
                        orderby m.ParentId, m.Sort
                        select m;

            if (query != null)
            {
                foreach (var tp in query)
                {
                    MenuTreeModel m = new MenuTreeModel();
                    m.Id = tp.Id.ToString();
                    m.Icon = tp.Icon;
                    m.MenuName = tp.Name;
                    m.ParentId = tp.ParentId.ToString();
                    m.Url = tp.LinkAddress;
                    m.Node = GetChildMenu(userID, tp.Id);
                    listTemp.Add(m);
                }
            }

            return listTemp;

        }
        public static string GetAllMenuInfo(string MenuName)
        {
            string str = string.Empty;
            MenuName = MenuName.Trim();
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbMenu> templist = new List<tbMenu>();
                if (String.IsNullOrEmpty(MenuName))
                {
                    templist = myDbContext.tbMenu.ToList();
                }
                else
                {
                    templist = myDbContext.tbMenu.Where(p => p.Name.Contains(MenuName)).ToList();
                }

                if (templist != null && templist.Count > 0)
                {


                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "菜单不存在", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string GetAllMenuInfo(string ParameterStr, int PageSize, int CurrentPage)
        {
            string str = string.Empty;
           
            try
            {
                List<FilterModel> whereList = new List<FilterModel>();
                if (!string.IsNullOrEmpty(ParameterStr))
                {
                    whereList = JsonConvert.DeserializeObject<List<FilterModel>>(ParameterStr);
                };

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbMenu> templist = new List<tbMenu>();
                //if (String.IsNullOrEmpty(MenuName))
                //{
                //    templist = myDbContext.tbMenu.ToList();
                //}
                //else
                //{
                //    templist = myDbContext.tbMenu.Where(p => p.Name.Contains(MenuName)).ToList();
                //}
                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.tbMenu.Where(LinqHelper.GetFilterExpression<tbMenu>(whereList).Compile()).Count<tbMenu>();

                }
                else
                {
                    DataCount = myDbContext.tbMenu.Count<tbMenu>();
                }
                if (whereList.Count > 0)
                {
                    templist = myDbContext.tbMenu.Where(LinqHelper.GetFilterExpression<tbMenu>(whereList).Compile()).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (templist == null)
                    {
                        templist = new List<tbMenu>();
                    }
                }
                else
                {
                    templist = myDbContext.tbMenu.OrderBy(p=> p.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }


                if (templist != null && templist.Count > 0)
                {


                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "菜单不存在", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetCascaderMenu()
        {
            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                var query = from m in myDbContext.tbMenu
                            where m.ParentId == 0
                            orderby m.ParentId, m.Sort
                            select m;
                List<string> listtemp = new List<string>();
                string temp = string.Empty;
                if (query != null)
                {
                    foreach (var tp in query)
                    {
                        string str = GetChildMenu(tp.Id);
                        if (string.IsNullOrEmpty(str))
                        {


                            str = "{\"value\":\"" + tp.Id.ToString() + "\" , \"label\":\"" + tp.Name + "\"}";

                        }
                        else
                        {
                            str = "{\"value\":\"" + tp.Id.ToString() + "\" ,\"label\":\"" + tp.Name + "\" , \"children\":[" + str + "]  }";
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


                temp = "[{\"value\":\"0\" , \"label\":\"菜单根节点\", \"children\":[" + temp + "] }]";


                str1 = ResponseHelper.ResponseMsg("1", "取数成功", temp);

            }
            catch (Exception ex)
            {
                str1 = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str1;
        }


        public static string GetChildMenu(int ParentId)
        {

            AchieveDBEntities myDbContext = new AchieveDBEntities();
            List<string> listtemp = new List<string>();
            var query = from m in myDbContext.tbMenu
                        where m.ParentId == ParentId
                        orderby m.ParentId, m.Sort
                        select m;

            if (query != null)
            {
                foreach (var tp in query)
                {
                    string str = GetChildMenu(tp.Id);
                    if (string.IsNullOrEmpty(str))
                    {


                        str = "{\"value\":\"" + tp.Id.ToString() + "\" , \"label\":\"" + tp.Name + "\"}";

                    }
                    else
                    {
                        str = "{\"value\":\"" + tp.Id.ToString() + "\" , \"label\":\"" + tp.Name + "\", \"children\":[" + str + "]  }";
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

        public static string GetTreeMenu()
        {
            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                var query = from m in myDbContext.tbMenu
                            where m.ParentId == 0
                            orderby m.ParentId, m.Sort
                            select m;
                List<string> listtemp = new List<string>();
                string temp = string.Empty;
                if (query != null)
                {
                    foreach (var tp in query)
                    {
                        string str = GetTreeMenu(tp.Id);
                        if (string.IsNullOrEmpty(str))
                        {


                            str = "{\"id\":\"" + tp.Id.ToString() + "\" , \"label\":\"" + tp.Name + "\"}";

                        }
                        else
                        {
                            str = "{\"id\":\"" + tp.Id.ToString() + "\" ,\"label\":\"" + tp.Name + "\" , \"children\":[" + str + "]  }";
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


                temp = "[" + temp + "]";


                str1 = ResponseHelper.ResponseMsg("1", "取数成功", temp);

            }
            catch (Exception ex)
            {
                str1 = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str1;
        }


        public static string GetTreeMenu(int ParentId)
        {

            AchieveDBEntities myDbContext = new AchieveDBEntities();
            List<string> listtemp = new List<string>();
            var query = from m in myDbContext.tbMenu
                        where m.ParentId == ParentId
                        orderby m.ParentId, m.Sort
                        select m;

            if (query != null)
            {
                foreach (var tp in query)
                {
                    string str = GetChildMenu(tp.Id);
                    if (string.IsNullOrEmpty(str))
                    {


                        str = "{\"id\":\"" + tp.Id.ToString() + "\" , \"label\":\"" + tp.Name + "\"}";

                    }
                    else
                    {
                        str = "{\"id\":\"" + tp.Id.ToString() + "\" , \"label\":\"" + tp.Name + "\", \"children\":[" + str + "]  }";
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

        public static string GetMenuAllCount()
        {

            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                int Count = myDbContext.tbMenu.Count<tbMenu>();





                str1 = ResponseHelper.ResponseMsg("1", "取数成功", Count.ToString());

            }
            catch (Exception ex)
            {
                str1 = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str1;
        }
        public static string AddMenu(string Menustr)
        {
            string str = string.Empty;
            try
            {
                tbMenu tb = JsonConvert.DeserializeObject<tbMenu>(Menustr);
                tbMenu newtb = new tbMenu()
                {
                    Code = tb.Code,
                    CreateTime = DateTime.Now,
                    LinkAddress = tb.LinkAddress,
                    CreateBy = tb.CreateBy,
                    Icon = tb.Icon,
                    Name = tb.Name,
                    ParentId = tb.ParentId,
                    Sort = tb.Sort,
                    UpdateBy = tb.CreateBy,
                    UpdateTime = DateTime.Now

                };
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                myDbContext.tbMenu.Add(newtb);
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "取数成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string DeleteMenu(string[] MenuList)
        {
            string str = string.Empty;
            try
            {

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                foreach (string temp in MenuList)
                {
                    int delId = Convert.ToInt32(temp);
                    List<tbMenu> delList = myDbContext.tbMenu.Where(p => p.Id == delId).ToList();
                    if (delList != null && delList.Count > 0)
                    {
                        myDbContext.tbMenu.Remove(delList[0]);
                    }
                    List<tbMenuButton> delMenuButtonList = new List<tbMenuButton>();
                    delMenuButtonList = myDbContext.tbMenuButton.Where(p => p.MenuId == delId).ToList();
                    if (delMenuButtonList != null && delMenuButtonList.Count > 0)
                    {
                        foreach (var t in delMenuButtonList)
                        {
                            myDbContext.tbMenuButton.Remove(t);
                        }
                    }
                    List<tbRoleMenu> delRoleMenuList = new List<tbRoleMenu>();
                    delRoleMenuList = myDbContext.tbRoleMenu.Where(p => p.MenuId == delId).ToList();
                    if(delRoleMenuList!=null&&delRoleMenuList.Count>0)
                    {
                        foreach(var t in delRoleMenuList)
                        {
                            myDbContext.tbRoleMenu.Remove(t);
                        }
                    }
                    List<tbRoleMenuButton> delRoleMenuButtonList = new List<tbRoleMenuButton>();
                    delRoleMenuButtonList = myDbContext.tbRoleMenuButton.Where(p => p.MenuId == delId).ToList();

                    if(delRoleMenuButtonList!=null&& delRoleMenuButtonList.Count>0)
                    {
                        foreach(var t in delRoleMenuButtonList)
                        {
                            myDbContext.tbRoleMenuButton.Remove(t);
                        }
                    }




                }



                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "取数成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string UpdateMenu(string Menustr)
        {
            string str = string.Empty;
            try
            {
                tbMenu tb = JsonConvert.DeserializeObject<tbMenu>(Menustr);
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbMenu data = myDbContext.tbMenu.Where(p => p.Id == tb.Id).FirstOrDefault();


                data.Code = tb.Code;
                data.LinkAddress = tb.LinkAddress;

                data.Icon = tb.Icon;
                data.Name = tb.Name;
                data.ParentId = tb.ParentId;
                data.Sort = tb.Sort;
                data.UpdateBy = tb.UpdateBy;
                data.UpdateTime = DateTime.Now;




                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "更新成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
    }
}
