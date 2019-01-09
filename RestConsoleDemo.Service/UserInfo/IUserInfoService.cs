using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service
{
    [ServiceContract(Namespace = "用户信息接口")]
    public interface IUserInfoService
    {
        // [OperationContract, WebInvoke(UriTemplate = "/GetAllUser", Method = "GET",
        // ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]

        // [Description("获取全部用户信息")]
        // string GetAllUser();

        [OperationContract, WebInvoke(UriTemplate = "/GetUserByID", Method = "POST",
         ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取指定用户信息")]
        string GetUserByID(Stream stream);
        //[OperationContract, WebInvoke(UriTemplate = "/GetUserByAccountName?accountName={accountName}&passWord={passWord}", Method = "GET",
        // ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract, WebInvoke(UriTemplate = "/GetUserByAccountName", Method = "POST",
         ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取指定用户信息")]
        string GetUserByAccountName(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetUserLogininfoByToken", Method = "POST",
        ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("验证Token信息")]
        string GetUserLogininfoByToken(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetUserMenu", Method = "POST",
        ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetUserMenu(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetMenuById", Method = "POST",
        ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetMenuById(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetButtonByMenuIdAndUserId", Method = "POST",
        ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetButtonByMenuIdAndUserId(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetAllMenuInfo", Method = "POST",
        ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetAllMenuInfo(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetCascaderMenuo", Method = "POST",
        ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetCascaderMenuo(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetTreeMenu", Method = "POST",
      ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetTreeMenu(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/GetMenuAllCount", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetMenuAllCount(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/AddMenu", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string AddMenu(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/DeleteMenu", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string DeleteMenu(Stream stream);
        [OperationContract, WebInvoke(UriTemplate = "/UpdateMenu", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string UpdateMenu(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetAllButtonForTransfer", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetAllButtonForTransfer(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetButtonByMenuIdnForTransfer", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetButtonByMenuIdnForTransfer(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/SetMenuButton", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string SetMenuButton(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetAllUserInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetAllUserInfo(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetAllUserViewInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetAllUserViewInfo(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetUserAllCount", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetUserAllCount(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/AddUser", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string AddUser(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/DeleteUser", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string DeleteUser(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/UpdateUser", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateUser(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetRoleByUserIdForTransfer", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetRoleByUserIdForTransfer(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetAllRoleForTransfer", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetAllRoleForTransfer(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/SetUserRole", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string SetUserRole(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/GetDeptByUserIdForTransfer", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetDeptByUserIdForTransfer(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetAllDeptForTransfer", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetAllDeptForTransfer(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/SetUserDept", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string SetUserDept(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetAllRoleInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string GetAllRoleInfo(Stream stream);
        [OperationContract, WebInvoke(UriTemplate = "/AddRole", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string AddRole(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/DeleteRole", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string DeleteRole(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/UpdateRole", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateRole(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetRoleById", Method = "POST",
    ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetRoleById(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetMenuByRoleIdForTree", Method = "POST",
 ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetMenuByRoleIdForTree(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetAllButtonByMenuIdForTransfer", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetAllButtonByMenuIdForTransfer(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetButtonByMenuIdRoleIdForTransfer", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetButtonByMenuIdRoleIdForTransfer(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/SetButtonByMenuIdRoleId", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string SetButtonByMenuIdRoleId(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/SetMenuRole", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string SetMenuRole(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetAllDeptInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetAllDeptInfo(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/AddDept", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string AddDept(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/DeleteDept", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string DeleteDept(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/UpdateDept", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateDept(Stream stream);




        [OperationContract, WebInvoke(UriTemplate = "/GetDeptById", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetDeptById(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetDeptAllCount", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetDeptAllCount(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetCascaderDept", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetCascaderDept(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetAllButtonInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetAllButtonInfo(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/AddButton", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string AddButton(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/DeleteButton", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string DeleteButton(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/UpdateButton", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateButton(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetButtonById", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetButtonById(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetButtonAllCount", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetButtonAllCount(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/LoginOut", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string LoginOut(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/UpdateUserAllinfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateUserAllinfo(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetTreeUser", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetTreeUser(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/Getaa", Method = "POST",
        ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [Description("获取指定用户信息")]
        string Getaa();
    }
}
