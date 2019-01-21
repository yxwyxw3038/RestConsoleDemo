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

namespace RestConsoleDemo.BLL
{
    public static class UserBill
    {


        public static string GetUserByID(int userID)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbUser temp = new tbUser();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbUser> templist = myDbContext.tbUser.Where(p => p.ID == userID).ToList();
                if (templist != null && templist.Count > 0)
                {
                    temp = templist[0];
                    temp.Password = "";

                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
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
        public static string GetUserByAccountName(string accountName, string passWord,string address,string port)
        {
            string str = string.Empty;
            try
            {
                passWord = Base64Helper.DecodeBase64(passWord);
                string md5passWord = Md5Helper.GetMD5String(passWord);
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                UserInfoModel temp = new UserInfoModel();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbUser> templist = myDbContext.tbUser.Where(p => p.AccountName == accountName).ToList();
                if (templist != null && templist.Count > 0)
                {
                    tbUser tempUser = templist[0];
                    if (tempUser.Password == md5passWord)
                    {
                        if(tempUser.IsAble!=1)
                        {
                             throw new Exception("帐号未启用！");
                        }
                       
                        string Token = Guid.NewGuid().ToString();
                        DateTime newDataTime = DateTime.Now;

                        List<tbUserToken> tempOldTokenlist = myDbContext.tbUserToken.Where(p => p.UserId == tempUser.ID  && p.IsLoginOut!=1).ToList();
                        foreach(var st in tempOldTokenlist)
                        {
                            st.IsLoginOut = 1;
                        }

                        tbUserToken newtb = new tbUserToken();
                        newtb.UserId = tempUser.ID;
                        newtb.Token = Token;
                        newtb.CreateTime = newDataTime;
                        newtb.UpdateTime = newDataTime;
                        newtb.Address = address;
                        newtb.Port = port;
                        newtb.IsLoginOut = 0;
                        myDbContext.tbUserToken.Add(newtb);
                        myDbContext.SaveChanges();

                        temp.ID = tempUser.ID;
                        temp.AccountName = tempUser.AccountName;
                        temp.CreateTime = DateTime.Now;
                        temp.RealName = tempUser.RealName;

                        temp.Token = Token;
                        temp.CreateTime = newDataTime;
                        str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                        str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                    }
                    else
                    {
                        str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                        str = ResponseHelper.ResponseMsg("-1", "密码错误", str);
                    }

                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "用户不存在", str);
                }

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;

        }

        public static string GetAllUser()
        {


            string str = string.Empty;
            try
            {
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbUser> templist = myDbContext.tbUser.ToList();
                str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                str = ResponseHelper.ResponseMsg("1", "取数成功", str);
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
            return str;
        }

        public static void CheckToken(string Token)
        {

            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbUserToken temp = myDbContext.tbUserToken.Where(p => p.Token == Token).FirstOrDefault();
                if (temp != null)
                {
                    if (temp.UpdateTime > DateTime.Now.AddHours(-1) && temp.CreateTime > DateTime.Now.AddHours(-12))
                    {
                        temp.UpdateTime = DateTime.Now;
                        myDbContext.SaveChanges();
                    }
                    else if(temp.IsLoginOut==1)
                    {
                        throw new Exception("用户已退出系统");
                    }
                    else
                    {
                        throw new Exception("验证Token已过期");
                    }


                }
                else
                {
                    throw new Exception("验证Token已过期");

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public static string GetUserLogininfoByToken(string Token)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbUserToken> templist = myDbContext.tbUserToken.Where(p => p.Token == Token).ToList();
                if (templist != null && templist.Count > 0)
                {
                    tbUserToken tempUser = templist[0];
                    if (tempUser.UpdateTime > DateTime.Now.AddHours(-1) && tempUser.CreateTime > DateTime.Now.AddHours(-12))
                    {


                        str = ResponseHelper.ResponseMsg("1", "验证Token成功", str);
                    }
                    else
                    {

                        str = ResponseHelper.ResponseMsg("-1", "验证Token已过期", str);
                    }

                }
                else
                {

                    str = ResponseHelper.ResponseMsg("-1", "Token不存在", str);
                }

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetAllUserInfo(string ParameterStr, int PageSize, int CurrentPage)
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
                List<tbUser> templist = new List<tbUser>();

                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.tbUser.Where(LinqHelper.GetFilterExpression<tbUser>(whereList).Compile()).Count<tbUser>();

                }
                else
                {
                    DataCount = myDbContext.tbUser.Count<tbUser>();
                }
                if (whereList.Count > 0)
                {
                    templist = myDbContext.tbUser.Where(LinqHelper.GetFilterExpression<tbUser>(whereList).Compile()).OrderByDescending(p => p.ID).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (templist == null)
                    {
                        templist = new List<tbUser>();
                    }
                }
                else
                {
                    templist = myDbContext.tbUser.OrderByDescending(p => p.ID).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }


                if (templist != null && templist.Count > 0)
                {


                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "用户信息不存在", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetAllUserViewInfo(string ParameterStr, int PageSize, int CurrentPage)
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
                List<tbUser> templist = new List<tbUser>();

                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.tbUser.Where(LinqHelper.GetFilterExpression<tbUser>(whereList).Compile()).Count<tbUser>();

                }
                else
                {
                    DataCount = myDbContext.tbUser.Count<tbUser>();
                }
                if (whereList.Count > 0)
                {
                    templist = myDbContext.tbUser.Where(LinqHelper.GetFilterExpression<tbUser>(whereList).Compile()).OrderByDescending(p => p.ID).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (templist == null)
                    {
                        templist = new List<tbUser>();
                    }
                }
                else
                {
                    templist = myDbContext.tbUser.OrderByDescending(p => p.ID).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }
                List<UserViewModel> tempViewlist = new List<UserViewModel>();


                if (templist != null && templist.Count > 0)
                {
                    foreach (tbUser t in templist)
                    {
                        string tempRoleName = string.Empty;
                        var query1 = from ur in myDbContext.tbUserRole
                                     join r in myDbContext.tbRole on ur.RoleId equals r.Id

                                     where ur.UserId == t.ID
                                     orderby r.Id ascending
                                     select r.RoleName;
                        if (query1 != null)
                        {
                            foreach (string st in query1)
                            {
                                tempRoleName = string.Concat(tempRoleName, st, ";");
                            }
                        }

                        string tempDepartmentName = string.Empty;

                        var query2 = from ur in myDbContext.tbUserDepartment
                                     join r in myDbContext.tbDepartment on ur.DepartmentId equals r.Id
                                     where ur.UserId == t.ID
                                     orderby r.Id ascending
                                     select r.DepartmentName;
                        if (query2 != null)
                        {
                            foreach (string st in query2)
                            {
                                tempDepartmentName = string.Concat(tempDepartmentName, st, ";");
                            }
                        }
                        UserViewModel addModel = new UserViewModel()
                        {
                            AccountName = t.AccountName,
                            CreateBy = t.CreateBy,
                            CreateTime = t.CreateTime,
                            DepartmentName = tempDepartmentName,
                            Description = t.Description,
                            Email = t.Email,
                            ID = t.ID,
                            IfChangePwd = t.IfChangePwd,
                            IsAble = t.IsAble,
                            MobilePhone = t.MobilePhone,
                            Password = "123456",
                            RealName = t.RealName,
                            RoleName = tempRoleName,
                            UpdateBy = t.UpdateBy,
                            UpdateTime = t.UpdateTime

                        };
                        tempViewlist.Add(addModel);


                    }



                    str = JsonConvert.SerializeObject(tempViewlist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "用户信息不存在", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string GetUserAllCount()
        {

            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                int Count = myDbContext.tbUser.Count<tbUser>();
                str1 = ResponseHelper.ResponseMsg("1", "取数成功", Count.ToString());

            }
            catch (Exception ex)
            {
                str1 = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str1;
        }
        public static string AddUser(string Userstr)
        {
            string str = string.Empty;
            try
            {
                tbUser tb = JsonConvert.DeserializeObject<tbUser>(Userstr);
                string  passWord = Base64Helper.DecodeBase64(tb.Password);
                string password = Md5Helper.GetMD5String(passWord);
                tbUser newtb = new tbUser()
                {
                    AccountName = tb.AccountName,
                    CreateBy = tb.CreateBy,
                    CreateTime = DateTime.Now,
                    Description = tb.Description,
                    Email = tb.Email,
                    IfChangePwd = tb.IfChangePwd,
                    IsAble = tb.IsAble,
                    MobilePhone = tb.MobilePhone,
                    Password = password,
                    RealName = tb.RealName,
                    UpdateTime = DateTime.Now,
                     UpdateBy = tb.CreateBy

                };
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                int   DataCount = myDbContext.tbUser.Where(p =>p.AccountName== newtb.AccountName).Count<tbUser>();
                if(DataCount>0)
                {
                    throw new Exception(string.Format("帐号名：{0}重复，请重新输入", newtb.AccountName));
                }
                DataCount = myDbContext.tbUser.Where(p => p.Email == newtb.Email).Count<tbUser>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("邮箱：{0}重复，请重新输入", newtb.Email));
                }
                DataCount = myDbContext.tbUser.Where(p => p.MobilePhone == newtb.MobilePhone).Count<tbUser>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("手机号：{0}重复，请重新输入", newtb.MobilePhone));
                }
                myDbContext.tbUser.Add(newtb);
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
        public static string DeleteUser(string[] UserList)
        {
            string str = string.Empty;
            try
            {

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                foreach (string temp in UserList)
                {
                    int delId = Convert.ToInt32(temp);
                    List<tbUser> delList = myDbContext.tbUser.Where(p => p.ID == delId).ToList();
                    if (delList != null && delList.Count > 0)
                    {
                        myDbContext.tbUser.Remove(delList[0]);
                    }
                    List<tbUserRole> delUserRoleList = new List<tbUserRole>();
                    delUserRoleList= myDbContext.tbUserRole.Where(p => p.UserId == delId).ToList();
                    if(delUserRoleList!=null&&delUserRoleList.Count>0)
                    {
                        foreach(var t in delUserRoleList)
                        {
                            myDbContext.tbUserRole.Remove(t);

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
        public static string UpdateUser(string Userstr)
        {
            string str = string.Empty;
            try
            {
                tbUser tb = JsonConvert.DeserializeObject<tbUser>(Userstr);
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbUser data = myDbContext.tbUser.Where(p => p.ID == tb.ID).FirstOrDefault();
            
                data.AccountName = tb.AccountName;
                data.Description = tb.Description;
                data.Email = tb.Email;
                data.IfChangePwd = tb.IfChangePwd;
                data.IsAble = tb.IsAble;
                data.MobilePhone = tb.MobilePhone;
     
                data.RealName = tb.RealName;
                data.UpdateBy = tb.UpdateBy;
                data.UpdateTime = DateTime.Now;


                int DataCount = myDbContext.tbUser.Where(p => p.AccountName == data.AccountName &&p.ID!= data.ID).Count<tbUser>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("帐号名：{0}重复，请重新输入", data.AccountName));
                }
                DataCount = myDbContext.tbUser.Where(p => p.Email == data.Email && p.ID != data.ID).Count<tbUser>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("邮箱：{0}重复，请重新输入", data.Email));
                }
                DataCount = myDbContext.tbUser.Where(p => p.MobilePhone == data.MobilePhone && p.ID != data.ID).Count<tbUser>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("手机号：{0}重复，请重新输入", data.MobilePhone));
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
        public static string UpdateUserAllinfo(string Userstr)
        {
            string str = string.Empty;
            try
            {
                tbUser tb = JsonConvert.DeserializeObject<tbUser>(Userstr);
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbUser data = myDbContext.tbUser.Where(p => p.ID == tb.ID).FirstOrDefault();
                string passWord = Base64Helper.DecodeBase64(tb.Password);
                string password = Md5Helper.GetMD5String(passWord);
                data.AccountName = tb.AccountName;
                data.Description = tb.Description;
                data.Email = tb.Email;
                data.IfChangePwd = tb.IfChangePwd;
                data.IsAble = tb.IsAble;
                data.MobilePhone = tb.MobilePhone;
                data.Password = password;
                data.RealName = tb.RealName;
                data.UpdateBy = tb.UpdateBy;
                data.UpdateTime = DateTime.Now;


                int DataCount = myDbContext.tbUser.Where(p => p.AccountName == data.AccountName && p.ID != data.ID).Count<tbUser>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("帐号名：{0}重复，请重新输入", data.AccountName));
                }
                DataCount = myDbContext.tbUser.Where(p => p.Email == data.Email && p.ID != data.ID).Count<tbUser>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("邮箱：{0}重复，请重新输入", data.Email));
                }
                DataCount = myDbContext.tbUser.Where(p => p.MobilePhone == data.MobilePhone && p.ID != data.ID).Count<tbUser>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("手机号：{0}重复，请重新输入", data.MobilePhone));
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

        public static string LoginOut(string Token,string accountName)
        {
            string str = string.Empty;
            try
            {
             
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
           
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbUserToken temp = myDbContext.tbUserToken.Where(p => p.Token == Token).FirstOrDefault();
                if (temp != null )
                {
                    if (temp != null)
                    {
                        if (temp.UpdateTime > DateTime.Now.AddHours(-1) && temp.CreateTime > DateTime.Now.AddHours(-12))
                        {
                           
                            tbUser tb = myDbContext.tbUser.Where(p => p.ID==temp.UserId ).FirstOrDefault();
                            if(tb!=null)
                            {
                                if(tb.AccountName== accountName)
                                {
                                    temp.IsLoginOut = 1;
                                    temp.LoginOutTime = DateTime.Now;
                                    myDbContext.SaveChanges();
                                }
                                else
                                {
                                    throw new Exception("用户信息不匹配");
                                }
                            }
                            else
                            {
                                throw new Exception("用户信息不存在");
                            }


                        }
                        else if (temp.IsLoginOut == 1)
                        {
                            throw new Exception("用户已退出系统");
                        }
                        else
                        {
                            throw new Exception("验证Token已过期");
                        }


                    }
                    else
                    {
                        throw new Exception("验证Token已过期");

                    }



                     
                        str = ResponseHelper.ResponseMsg("1", "取数成功", "");
                

                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "Token不存在", str);
                }

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;

        }
        public static string GetTreeUser()
        {
            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

             
                List<string> listtemp = new List<string>();
                string temp = string.Empty;

                string strchildrenall = GetTreeUserAll();
                string strall = "{\"id\":\"" + "all" + "\" ,\"label\":\"" + "全员" + "\" ,\"RealName\":\"全员\",\"value\":\"" + "all" + "\" ,\"key\":\"" + "all" + "\" ,\"title\":\"全员\", \"children\":" + strchildrenall + "  }";
                listtemp.Add(strall);

                string strchildrendept= GetTreeUserDept();
                string strdept = "{\"id\":\"" + "dept" + "\" ,\"label\":\"" + "部门" + "\",\"RealName\":\"部门\" ,\"value\":\"" + "dept" + "\" ,\"key\":\"" + "dept" + "\" ,\"title\":\"部门\", \"children\":" + strchildrendept + "  }";
                listtemp.Add(strdept);

                string strchildrenrole = GetTreeUserRole();
                string strrole = "{\"id\":\"" + "role" + "\" ,\"label\":\"" + "权限" + "\",\"RealName\":\"权限\",\"value\":\"" + "role" + "\" ,\"key\":\"" + "role" + "\" ,\"title\":\"权限\" , \"children\":" + strchildrenrole + "  }";
                listtemp.Add(strrole);

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

               


                temp = "[" + temp + "]";


                str1 = ResponseHelper.ResponseMsg("1", "取数成功", temp);

            }
            catch (Exception ex)
            {
                str1 = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str1;
        }

        public static string GetTreeUserAll()
        {
            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                var query = from m in myDbContext.tbUser
                           
                            orderby m.ID
                            select m;
                List<string> listtemp = new List<string>();
                string temp = string.Empty;
                if (query != null)
                {
                    foreach (var tp in query)
                    {




                        string str = "{\"id\":\"all-0-" + tp.ID.ToString() + "\" , \"label\":\"" + tp.AccountName + "\",\"RealName\":\"" + tp.RealName + "\",\"value\":\"all-0-" + tp.ID.ToString() + "\" , \"key\":\"all-0-" + tp.ID.ToString() + "\",\"title\":\"" + tp.RealName + "\"}";
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

                }
                else
                {
                    temp = "";
                }


                str1 = "[" + temp + "]";


               

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return str1;
        }

        public static string GetTreeUserDept()
        {
            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                var query = from m in myDbContext.tbDepartment
                            orderby m.Id
                            select m;
                List<string> listtemp = new List<string>();
                string temp = string.Empty;
                if (query != null)
                {
                    foreach (var tp in query)
                    {







                        string str = GetChildUserDept(tp.Id);
                        if (string.IsNullOrEmpty(str))
                        {


                            str = "{\"id\":\"dept-" + tp.Id.ToString() + "\" , \"label\":\"" + tp.DepartmentName + "\",\"RealName\":\""+tp.DepartmentName+ "\",\"value\":\"dept-" + tp.Id.ToString() + "\",\"key\":\"dept-" + tp.Id.ToString() + "\" , \"title\":\"" + tp.DepartmentName + "\"}";

                        }
                        else
                        {
                            str = "{\"id\":\"dept-" + tp.Id.ToString() + "\" ,\"label\":\"" + tp.DepartmentName + "\",\"RealName\":\""+tp.DepartmentName+ "\",\"value\":\"dept-" + tp.Id.ToString() + "\",\"key\":\"dept-" + tp.Id.ToString() + "\" , \"title\":\"" + tp.DepartmentName + "\" , \"children\":[" + str + "]  }";
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

                }
                else
                {
                    temp = "";
                }


                str1 = "[" + temp + "]";




            }
            catch (Exception ex)
            {
                throw ex;
            }

            return str1;
        }

        public static string GetChildUserDept(int Id)
        {

            AchieveDBEntities myDbContext = new AchieveDBEntities();
            List<string> listtemp = new List<string>();
            var query = from ur in myDbContext.tbUserDepartment
                        join u in myDbContext.tbUser on ur.UserId equals u.ID
                        where ur.DepartmentId==Id
                        orderby u.ID
                        select u;

            if (query != null)
            {
                foreach (var tp in query)
                {


                    string str = "{\"id\":\"dept-" + Id.ToString() + "-" + tp.ID.ToString() + "\" , \"label\":\"" + tp.AccountName + "\",\"RealName\":\"" + tp.RealName + "\",\"value\":\"dept-"+ Id.ToString() + "-" + tp.ID.ToString() + "\" , \"key\":\"dept-"  +Id.ToString() + "-" + tp.ID.ToString() + "\",\"title\":\"" + tp.RealName + "\"}";

                   

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



        public static string GetTreeUserRole()
        {
            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                var query = from m in myDbContext.tbRole
                            orderby m.Id
                            select m;
                List<string> listtemp = new List<string>();
                string temp = string.Empty;
                if (query != null)
                {
                    foreach (var tp in query)
                    {







                        string str = GetChildUserRole(tp.Id);
                        if (string.IsNullOrEmpty(str))
                        {


                            str = "{\"id\":\"role-" + tp.Id.ToString() + "\" , \"label\":\"" + tp.RoleName + "\",\"RealName\":\""+tp.RoleName+ "\",\"value\":\"role-" + tp.Id.ToString() + "\",\"key\":\"role-" + tp.Id.ToString() + "\" , \"title\":\"" + tp.RoleName + "\"}";

                        }
                        else
                        {
                            str = "{\"id\":\"role-" + tp.Id.ToString() + "\" ,\"label\":\"" + tp.RoleName + "\",\"RealName\":\""+tp.RoleName+ "\",\"value\":\"role-" + tp.Id.ToString() + "\",\"key\":\"role-" + tp.Id.ToString() + "\" , \"title\":\"" + tp.RoleName + "\" , \"children\":[" + str + "]  }";
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

                }
                else
                {
                    temp = "";
                }


                str1 = "[" + temp + "]";




            }
            catch (Exception ex)
            {
                throw ex;
            }

            return str1;
        }

        public static string GetChildUserRole(int Id)
        {

            AchieveDBEntities myDbContext = new AchieveDBEntities();
            List<string> listtemp = new List<string>();
            var query = from ur in myDbContext.tbUserRole
                        join u in myDbContext.tbUser on ur.UserId equals u.ID
                        where ur.RoleId == Id
                        orderby u.ID
                        select u;

            if (query != null)
            {
                foreach (var tp in query)
                {


                    string str = "{\"id\":\"role-" + Id.ToString() + "-" + tp.ID.ToString() + "\" , \"label\":\"" + tp.AccountName + "\",\"RealName\":\"" + tp.RealName + "\",\"value\":\"role-" + Id.ToString() + "-" + tp.ID.ToString() + "\" , \"key\":\"role-" + Id.ToString() + "-" + tp.ID.ToString() + "\",\"title\":\"" + tp.RealName + "\"}";



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


    }
}
