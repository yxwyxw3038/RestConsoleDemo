using RestConsoleDemo.BLL;
using RestConsoleDemo.BLL.SysInfo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "jsoncallback")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FlowInfoService : IFlowInfoService
    {

        public string GetAllFlowInfo(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string ParameterStr = nvc["ParameterStr"];
                int PageSize = Convert.ToInt32(nvc["PageSize"]);
                int CurrentPage = Convert.ToInt32(nvc["CurrentPage"]);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = FlowBill.GetAllFlowInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetFlowInfoByMenuId(Stream stream)
        {
            string MenuId = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                MenuId = nvc["MenuId"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int id = Convert.ToInt32(MenuId);
                string str = FlowBill.GetFlowInfoByMenuId(id);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetFlowByCode(Stream stream)
        {
            string Code = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                Code = nvc["Code"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = FlowBill.GetFlowByCode(Code);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string AddFlow(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                Helper.ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = FlowBill.AddFlow(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }


        public string UpdateFlow(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                Helper.ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string addStr = nvc["str"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = FlowBill.UpdateFlow(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string DeleteFlow(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                Helper.ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string delStr = nvc["str"];
                string[] list = delStr.Split(',');
                string str = FlowBill.DeleteFlow(list);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string UpdateFlowStatus(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
                Helper.ResponseHelper.SetHeaderInfo();
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Code = nvc["Code"];
                string oldStatus = nvc["oldStatus"];
                string newStatus = nvc["newStatus"];
                string Token = nvc["Token"];
                string UpdateBy = nvc["UpdateBy"];
                UserBill.CheckToken(Token);
                string str = FlowBill.UpdateFlowStatus(Code, Convert.ToInt32(oldStatus), Convert.ToInt32(newStatus), UpdateBy);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

    }
}
