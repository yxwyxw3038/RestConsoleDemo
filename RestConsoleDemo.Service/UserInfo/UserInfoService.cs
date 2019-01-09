using RestConsoleDemo.BLL;
using RestConsoleDemo.BLL.UserInfo;
using RestConsoleDemo.Service.Helper;
using RestConsoleDemo.Service.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "jsoncallback")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class UserInfoService : IUserInfoService
    {
        //public string GetAllUser()
        //{

        //    ResponseHelper.SetHeaderInfo();

        //    string str = UserBill.GetAllUser();
        //    return str;


        //}
        public string GetUserByID(Stream stream)
        {
            string ID = string.Empty;
            StreamReader sr = new StreamReader(stream);
            try
            {
               string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                ID = nvc["ID"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int id = Convert.ToInt32(ID);
                string str = UserBill.GetUserByID(id);
                return str;
            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string GetUserByAccountName(Stream stream)
        {

            string accountName = string.Empty;
            string passWord = string.Empty;
            StreamReader sr = new StreamReader(stream);


            try
            {

                IPInfoModel iPInfo= RequestHelper.RequestInfo();

               string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s); 
                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                accountName = nvc["accountName"];
                passWord = nvc["passWord"];
                string str = UserBill.GetUserByAccountName(accountName, passWord, iPInfo.Address, iPInfo.Port);
                return str;
                //  sr.Dispose();
                //NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                //accountName = nvc["accountName"];
                //passWord = nvc["passWord"];

            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            //  ResponseHelper.SetHeaderInfo();

            // string str = UserBill.GetUserByAccountName(accountName, passWord);
            //   return str;
        }

        public string GetUserLogininfoByToken(Stream stream)
        {

            string Token = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                Token = nvc["Token"];

                string str = UserBill.GetUserLogininfoByToken(Token);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }


        }
        public string GetUserMenu(Stream stream)
        {
            string userID = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                userID = nvc["userID"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int id = Convert.ToInt32(userID);
                string str = MenuBill.GetUserMenu(id,Token);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetMenuById(Stream stream)
        {
            string ID = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                ID = nvc["Id"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int id = Convert.ToInt32(ID);
                string str = MenuBill.GetMenuById(id);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetButtonByMenuIdAndUserId(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string menuIds = nvc["menuId"];
                int menuId = Convert.ToInt32(menuIds);
                string userIds = nvc["userId"];
                int userId = Convert.ToInt32(userIds);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = ButtonBLL.GetButtonByMenuIdAndUserId(menuId, userId);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string GetAllMenuInfo(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string ParameterStr = nvc["ParameterStr"];
                int PageSize = Convert.ToInt32(nvc["PageSize"]);
                int CurrentPage = Convert.ToInt32(nvc["CurrentPage"]);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = MenuBill.GetAllMenuInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string GetCascaderMenuo(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];

                string str = MenuBill.GetCascaderMenu();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string GetTreeMenu(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];

                string str = MenuBill.GetTreeMenu();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetMenuAllCount(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);

            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = MenuBill.GetMenuAllCount();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string AddMenu(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = MenuBill.AddMenu(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string DeleteMenu(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string delStr = nvc["str"];
                string[] list = delStr.Split(',');
                string str = MenuBill.DeleteMenu(list);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string UpdateMenu(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = MenuBill.UpdateMenu(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetAllButtonForTransfer(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);

                ResponseHelper.SetHeaderInfo();
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = ButtonBLL.GetAllButtonForTransfer();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetButtonByMenuIdnForTransfer(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string menuIds = nvc["menuId"];
                int menuId = Convert.ToInt32(menuIds);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = ButtonBLL.GetButtonByMenuIdnForTransfer(menuId);
                return str;


            }
            catch (Exception ex)
            {
                return BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string Getaa()
        {
            //string accountName = "admin"; string passWord = "admin";
            //ResponseHelper.SetHeaderInfo();
            //string str = UserBill.GetUserByAccountName(accountName, passWord);
            return "";
        }
        public string SetMenuButton(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string menuIds = nvc["menuId"];
                int menuId = Convert.ToInt32(menuIds);
                string buttonStr = nvc["buttonStr"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = ButtonBLL.SetMenuButton(menuId, buttonStr);
                return str;


            }
            catch (Exception ex)
            {
                return BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetAllUserInfo(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string ParameterStr = nvc["ParameterStr"];
                int PageSize = Convert.ToInt32(nvc["PageSize"]);
                int CurrentPage = Convert.ToInt32(nvc["CurrentPage"]);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = UserBill.GetAllUserInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetAllUserViewInfo(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string ParameterStr = nvc["ParameterStr"];
                int PageSize = Convert.ToInt32(nvc["PageSize"]);
                int CurrentPage = Convert.ToInt32(nvc["CurrentPage"]);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = UserBill.GetAllUserViewInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetUserAllCount(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);

            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = UserBill.GetUserAllCount();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string AddUser(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = UserBill.AddUser(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string DeleteUser(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string delStr = nvc["str"];
                string[] list = delStr.Split(',');
                string str = UserBill.DeleteUser(list);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string UpdateUser(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = UserBill.UpdateUser(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string UpdateUserAllinfo(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = UserBill.UpdateUserAllinfo(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetAllRoleForTransfer(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);

                ResponseHelper.SetHeaderInfo();
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = RoleBill.GetAllRoleForTransfer();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetRoleByUserIdForTransfer(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string userIds = nvc["userId"];
                int userId = Convert.ToInt32(userIds);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = RoleBill.GetRoleByUserIdForTransfer(userId);
                return str;


            }
            catch (Exception ex)
            {
                return BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        

        public string GetMenuByRoleIdForTree(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Ids = nvc["Id"];
                int Id = Convert.ToInt32(Ids);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = RoleBill.GetMenuByRoleIdForTree(Id);
                return str;


            }
            catch (Exception ex)
            {
                return BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string SetUserRole(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string userIds = nvc["userId"];
                int userId = Convert.ToInt32(userIds);
                string roleStr = nvc["roleStr"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = RoleBill.SetUserRole(userId, roleStr);
                return str;


            }
            catch (Exception ex)
            {
                return BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }


        public string GetAllDeptForTransfer(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);

                ResponseHelper.SetHeaderInfo();
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = DeptBill.GetAllDeptForTransfer();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetDeptByUserIdForTransfer(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string userIds = nvc["userId"];
                int userId = Convert.ToInt32(userIds);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = DeptBill.GetDeptByUserIdForTransfer(userId);
                return str;


            }
            catch (Exception ex)
            {
                return BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string SetUserDept(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string userIds = nvc["userId"];
                int userId = Convert.ToInt32(userIds);
                string deptStr = nvc["deptStr"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = DeptBill.SetUserDept(userId, deptStr);
                return str;


            }
            catch (Exception ex)
            {
                return BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetAllRoleInfo(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string ParameterStr = nvc["ParameterStr"];
                int PageSize = Convert.ToInt32(nvc["PageSize"]);
                int CurrentPage = Convert.ToInt32(nvc["CurrentPage"]);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = RoleBill.GetAllRoleInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string AddRole(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str =RoleBill.AddRole(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string DeleteRole(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string delStr = nvc["str"];
                string[] list = delStr.Split(',');
                string str = RoleBill.DeleteRole(list);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string UpdateRole(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = RoleBill.UpdateRole(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string GetRoleById(Stream stream)
        {
            string ID = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                ID = nvc["Id"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int id = Convert.ToInt32(ID);
                string str = RoleBill.GetRoleById(id);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }


        public string GetAllButtonByMenuIdForTransfer(Stream stream)
        {
            string ID = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                ID = nvc["menuId"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int id = Convert.ToInt32(ID);
                string str = ButtonBLL.GetAllButtonByMenuIdForTransfer(id);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetButtonByMenuIdRoleIdForTransfer(Stream stream)
        {
            string menuIds = string.Empty;
            string roleIds = string.Empty;
            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                menuIds = nvc["menuId"];
                roleIds = nvc["roleId"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int menuId = Convert.ToInt32(menuIds);
                int roleId = Convert.ToInt32(roleIds);
                string str = ButtonBLL.GetButtonByMenuIdRoleIdForTransfer(menuId, roleId);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string SetButtonByMenuIdRoleId(Stream stream)
        {
            string menuIds = string.Empty;
            string roleIds = string.Empty;
            string buttonStr = string.Empty;
            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                menuIds = nvc["menuId"];
                roleIds = nvc["roleId"];
                buttonStr = nvc["buttonStr"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int menuId = Convert.ToInt32(menuIds);
                int roleId = Convert.ToInt32(roleIds);
                string str = ButtonBLL.SetButtonByMenuIdRoleId(menuId, roleId, buttonStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string SetMenuRole(Stream stream)
        {
            string menuIds = string.Empty;
            string roleIds = string.Empty;
            string menuStr = string.Empty;
            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
            
                roleIds = nvc["roleId"];
                menuStr = nvc["menuStr"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
          
                int roleId = Convert.ToInt32(roleIds);
                string str = RoleBill.SetMenuRole(roleId, menuStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetAllDeptInfo(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string ParameterStr = nvc["ParameterStr"];
                int PageSize = Convert.ToInt32(nvc["PageSize"]);
                int CurrentPage = Convert.ToInt32(nvc["CurrentPage"]);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = DeptBill.GetAllDeptInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }


        public string AddDept(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = DeptBill.AddDept(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string DeleteDept(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string delStr = nvc["str"];
                string[] list = delStr.Split(',');
                string str = DeptBill.DeleteDept(list);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string UpdateDept(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = DeptBill.UpdateDept(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetDeptById(Stream stream)
        {
            string ID = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                ID = nvc["Id"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int id = Convert.ToInt32(ID);
                string str = DeptBill.GetDeptById(id);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetDeptAllCount(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);

            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = DeptBill.GetDeptAllCount();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetCascaderDept(Stream stream)
        {

            StreamReader sr = new StreamReader(stream);

            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = DeptBill.GetCascaderDept();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

        }

        public string GetAllButtonInfo(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string ParameterStr = nvc["ParameterStr"];
                int PageSize = Convert.ToInt32(nvc["PageSize"]);
                int CurrentPage = Convert.ToInt32(nvc["CurrentPage"]);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str =ButtonBLL.GetAllButtonInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }


        public string AddButton(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = ButtonBLL.AddButton(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string DeleteButton(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string delStr = nvc["str"];
                string[] list = delStr.Split(',');
                string str = ButtonBLL.DeleteButton(list);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string UpdateButton(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = ButtonBLL.UpdateButton(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetButtonById(Stream stream)
        {
            string ID = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                ID = nvc["Id"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int id = Convert.ToInt32(ID);
                string str = ButtonBLL.GetButtonById(id);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetButtonAllCount(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);

            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = ButtonBLL.GetButtonAllCount();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }


        public string LoginOut(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string accountName = nvc["accountName"];

                string str = UserBill.LoginOut(Token, accountName);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetTreeUser(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];

                string str = UserBill.GetTreeUser();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

    }
 
}
