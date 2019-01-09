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
     public static  class RoleBill
    {
        public static string GetRoleByUserIdForTransfer(int userId)
        {
            string str = string.Empty;

            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();

                List<tbUserRole> tblist = myDbContext.tbUserRole.Where(p => p.UserId == userId).ToList();

                if (tblist != null && tblist.Count > 0)
                {
                    for (int i = 0; i < tblist.Count; i++)
                    {
                        if (i == tblist.Count - 1)
                        {
                            str = string.Concat(str, tblist[i].RoleId.ToString());
                        }
                        else
                        {
                            str = string.Concat(str, tblist[i].RoleId.ToString(), ",");
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
        public static string GetAllRoleForTransfer()
        {


            string str = string.Empty;
            List<TransferModel> temp = new List<TransferModel>();
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();

                List<tbRole> tblist = new List<tbRole>();
                tblist = myDbContext.tbRole.ToList();

                if (tblist != null && tblist.Count > 0)
                {
                    foreach (var tp in tblist)
                    {
                        TransferModel tb = new TransferModel();
                        tb.key = tp.Id.ToString();
                        tb.label = tp.RoleName;
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

        public static string SetUserRole(int userId, string roleStr)
        {

            string str = string.Empty;

            try
            {

                string[] list = roleStr.Split(',');

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbUserRole> tblist = myDbContext.tbUserRole.Where(p => p.UserId == userId).ToList();
                foreach (tbUserRole st in tblist)
                {
                    bool findbj = false;
                    if (list.Length > 0)
                    {
                        foreach (string st1 in list)
                        {
                            if (st.RoleId.ToString() == st1)
                            {
                                findbj = true;
                                break;
                            }
                        }
                    }
                    if (!findbj)
                    {
                        myDbContext.tbUserRole.Remove(st);
                    }
                }

                if (list.Length > 0)
                {
                    foreach (string st1 in list)
                    {
                        if (!string.IsNullOrEmpty(st1))
                        {

                            bool findbj = false;
                            foreach (tbUserRole st in tblist)
                            {
                                if (st.RoleId.ToString() == st1)
                                {
                                    findbj = true;
                                    break;
                                }

                            }
                            if (!findbj)
                            {
                                tbUserRole newtb = new tbUserRole()
                                {
                                    RoleId = Convert.ToInt32(st1),
                                    UserId = userId

                                };
                                myDbContext.tbUserRole.Add(newtb);
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
        public static string GetAllRoleInfo(string ParameterStr, int PageSize, int CurrentPage)
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
                List<tbRole> templist = new List<tbRole>();

                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.tbRole.Where(LinqHelper.GetFilterExpression<tbRole>(whereList).Compile()).Count<tbRole>();

                }
                else
                {
                    DataCount = myDbContext.tbRole.Count<tbRole>();
                }
                if (whereList.Count > 0)
                {
                    templist = myDbContext.tbRole.Where(LinqHelper.GetFilterExpression<tbRole>(whereList).Compile()).OrderByDescending(p => p.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (templist == null)
                    {
                        templist = new List<tbRole>();
                    }
                }
                else
                {
                    templist = myDbContext.tbRole.OrderByDescending(p => p.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }


                if (templist != null && templist.Count > 0)
                {


                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "角色信息不存在", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string AddRole(string Rolestr)
        {
            string str = string.Empty;
            try
            {
                tbRole tb = JsonConvert.DeserializeObject<tbRole>(Rolestr);

                tbRole newtb = new tbRole()
                {
                    
                    RoleName=tb.RoleName,
                    CreateBy = tb.CreateBy,
                    CreateTime = DateTime.Now,
                    Description = tb.Description,
                    UpdateTime = DateTime.Now,
                    UpdateBy = tb.CreateBy

                };
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                int DataCount = myDbContext.tbRole.Where(p => p.RoleName == newtb.RoleName).Count<tbRole>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("角色名：{0}重复，请重新输入", newtb.RoleName));
                }
       
                myDbContext.tbRole.Add(newtb);
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string DeleteRole(string[] RoleList)
        {
            string str = string.Empty;
            try
            {

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                foreach (string temp in RoleList)
                {
                    int delId = Convert.ToInt32(temp);
                    List<tbRole> delList = myDbContext.tbRole.Where(p => p.Id == delId).ToList();
                    if (delList != null && delList.Count > 0)
                    {
                        myDbContext.tbRole.Remove(delList[0]);
                    }
                    List<tbUserRole> delUserRoleList = new List<tbUserRole>();
                    delUserRoleList = myDbContext.tbUserRole.Where(p => p.RoleId == delId).ToList();
                    if (delUserRoleList != null && delUserRoleList.Count > 0)
                    {
                        foreach (var t in delUserRoleList)
                        {
                            myDbContext.tbUserRole.Remove(t);

                        }
                    }
                    List<tbRoleMenu> delRoleMenuList = new List<tbRoleMenu>();
                    delRoleMenuList = myDbContext.tbRoleMenu.Where(p => p.RoleId == delId).ToList();
                    if (delRoleMenuList != null && delRoleMenuList.Count > 0)
                    {
                        foreach (var t in delRoleMenuList)
                        {
                            myDbContext.tbRoleMenu.Remove(t);
                        }
                    }
                    List<tbRoleMenuButton> delRoleMenuButtonList = new List<tbRoleMenuButton>();
                    delRoleMenuButtonList = myDbContext.tbRoleMenuButton.Where(p => p.RoleId == delId).ToList();

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
        public static string UpdateRole(string Rolestr)
        {
            string str = string.Empty;
            try
            {
                tbRole tb = JsonConvert.DeserializeObject<tbRole>(Rolestr);
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbRole data = myDbContext.tbRole.Where(p => p.Id == tb.Id).FirstOrDefault();

                data.Description = tb.Description;
                data.RoleName = tb.RoleName;
                data.UpdateBy = tb.UpdateBy;
                data.UpdateTime = DateTime.Now;


                int DataCount = myDbContext.tbRole.Where(p => p.RoleName == data.RoleName && p.Id != data.Id).Count<tbRole>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("角色名：{0}重复，请重新输入", data.RoleName));
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


        public static string GetRoleById(int Id)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbRole temp = new tbRole();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbRole> templist = myDbContext.tbRole.Where(p => p.Id == Id).ToList();
                if (templist != null && templist.Count > 0)
                {

                    temp = templist[0];
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "角色不存在", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetMenuByRoleIdForTree(int Id)
        {
            string str = string.Empty;

            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();

                List<tbRoleMenu> tblist = myDbContext.tbRoleMenu.Where(p => p.RoleId == Id).ToList();

                if (tblist != null && tblist.Count > 0)
                {
                    for (int i = 0; i < tblist.Count; i++)
                    {
                        int MenuId =Convert.ToInt32( tblist[i].MenuId);
                       int findBj=myDbContext.tbMenu.Where(p => p.ParentId == MenuId).Count<tbMenu>();
                        if (findBj <= 0)
                        {
                            str = string.Concat(str, tblist[i].MenuId.ToString(), ",");
                        }
                        
                    }
                    if(!string.IsNullOrEmpty(str))
                    {
                        str = str.Substring(0, str.Length - 1);
                    }
                    // str = JsonConvert.SerializeObject(query, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    //str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("0", "角色无菜单权限", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string SetMenuRole(int roleId, string menuStr)
        {
            string str = string.Empty;

            try
            {

                string[] list = menuStr.Split(',');

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbRoleMenu> tblist = myDbContext.tbRoleMenu.Where(p => p.RoleId == roleId).ToList();
                foreach (tbRoleMenu st in tblist)
                {
                    bool findbj = false;
                    if (list.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(list[0]))
                        {



                            foreach (string st1 in list)
                            {
                                if (st.MenuId.ToString() == st1)
                                {
                                    findbj = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!findbj)
                    {
                        //找子菜单 如果没有任何子菜单有权限则删除

                        findbj= CheckChildNode(myDbContext,Convert.ToInt32( st.MenuId), list);
                        int MenuId = Convert.ToInt32(st.MenuId);
                        if (!findbj)
                        {
                            myDbContext.tbRoleMenu.Remove(st);
                            List<tbRoleMenuButton> templist = myDbContext.tbRoleMenuButton.Where(p => p.MenuId == MenuId && p.RoleId == roleId).ToList();
                            if(templist!=null&&templist.Count>0)
                            {
                                foreach(tbRoleMenuButton del in templist)
                                {
                                    myDbContext.tbRoleMenuButton.Remove(del);
                                }
                            }
                        }


                    }
                }

                if (list.Length > 0)
                {
                    List<int> addlist = new List<int>();
                    foreach (string st1 in list)
                    {
                        if (!string.IsNullOrEmpty(st1))
                        {

                            bool findbj = false;
                            foreach (tbRoleMenu st in tblist)
                            {
                                if (st.Id.ToString() == st1)
                                {
                                    findbj = true;
                                    break;
                                }

                            }
                            if (!findbj)
                            {
                                //tbRoleMenu newtb = new tbRoleMenu()
                                //{
                                //    MenuId = Convert.ToInt32(st1),
                                //    RoleId= roleId

                                //};
                                //myDbContext.tbRoleMenu.Add(newtb);
                                //找父菜单并添加权限


                                SetParentNode(myDbContext, Convert.ToInt32(st1), roleId, addlist);

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
        public static bool CheckChildNode(AchieveDBEntities myDbContext, int MenuId, string[] Childlist)
        {
            bool findbj = false;
            if (string.IsNullOrEmpty(Childlist[0]))
            {
                return findbj;
            }
      
            List<tbMenu> menuList = myDbContext.tbMenu.Where(p => p.ParentId == MenuId).ToList();
            if (menuList != null && menuList.Count > 0)
            {
                foreach (tbMenu temp in menuList)
                {
                    foreach (string st in Childlist)
                    {
                        if (temp.Id.ToString() == st)
                        {
                            findbj = true;
                            return findbj;
                        }
                    }
                    findbj = CheckChildNode(myDbContext,temp.Id, Childlist);
                    if(findbj)
                    {
                        return findbj;
                    }

                }
            }
             return findbj;
        }

        public static void SetParentNode(AchieveDBEntities myDbContext, int MenuId, int roleId, List<int> addlist)
        {
            tbMenu temp = myDbContext.tbMenu.Where(p => p.Id == MenuId).FirstOrDefault();
            if(temp!=null)
            {
                int DataCount = myDbContext.tbRoleMenu.Where(p => p.MenuId == MenuId && p.RoleId == roleId).Count<tbRoleMenu>();
                int addCount = 1;
                if(addlist!=null)
                {
                    foreach(int st in addlist)
                    {
                        if(MenuId==st)
                        {
                            addCount = 0;
                            break;
                        }
                    }
                }
                if(DataCount<=0&& addCount==1)
                {
                    tbRoleMenu tbnew = new tbRoleMenu()
                    {
                        MenuId = MenuId,
                        RoleId = roleId
                    };
                    myDbContext.tbRoleMenu.Add(tbnew);
                    addlist.Add(MenuId);
                    if (temp.ParentId == null)
                    {

                    }
                    else
                    {
                        SetParentNode(myDbContext, Convert.ToInt32( temp.ParentId), roleId, addlist);
                    }

                }
            }
        }


    }
}
