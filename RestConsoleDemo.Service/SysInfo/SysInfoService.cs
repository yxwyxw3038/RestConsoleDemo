using RestConsoleDemo.BLL;
using RestConsoleDemo.BLL.Helper;
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
using RestConsoleDemo.Service.Helper;
using RestConsoleDemo.Service.Model;
using RestConsoleDemo.BLL.SysInfo;

namespace RestConsoleDemo.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "jsoncallback")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SysInfoService:ISysInfoService
    {
        public string GetAllLogList(Stream stream)
        {
          

            StreamReader sr = new StreamReader(stream);


            try
            {
               string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
              
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);

                string str = LogBill.GetAllLogList();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string LogRead(Stream stream)
        {


            StreamReader sr = new StreamReader(stream);


            try
            {
               string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);

                string Token = nvc["Token"];
                string FileName = nvc["FileName"];
                UserBill.CheckToken(Token);

                string str = LogBill.LogRead(FileName);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetAllLoginViewInfo(Stream stream)
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
                string str = LoginBill.GetAllLoginViewInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetLoginAllCount(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);

            try
            {
               string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                string str = LoginBill.GetLoginAllCount();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }


        public string GetAllBlackListInfo(Stream stream)
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
                string str = BlackListBill.GetAllBlackListInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string AddBlackList(Stream stream)
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
                string str = BlackListBill.AddBlackList(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string DeleteBlackList(Stream stream)
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
                string str = BlackListBill.DeleteBlackList(list);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string UpdateBlackList(Stream stream)
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
                string str = BlackListBill.UpdateBlackList(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetBlackListById(Stream stream)
        {
            string ID = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
               string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                ID = nvc["Id"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int id = Convert.ToInt32(ID);
                string str = BlackListBill.GetBlackListById(id);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }


        public string GetAllNoticeInfo(Stream stream)
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
                string str = NoticeBill.GetAllNoticeInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }


        public string GetNoticeById(Stream stream)
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

                string str = NoticeBill.GetNoticeById(Code);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string GetNoticeItemById(Stream stream)
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

                string str = NoticeBill.GetNoticeItemById(Code);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string AddNotice(Stream stream)
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
                string str = NoticeBill.AddNotice(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string UpdateNotice(Stream stream)
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
                string str = NoticeBill.UpdateNotice(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string UpdateNoticeStatus(Stream stream)
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
                UserBill.CheckToken(Token);
                string str = NoticeBill.UpdateNoticeStatus(Code, Convert.ToInt32(oldStatus), Convert.ToInt32(newStatus));
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string DeleteNotice(Stream stream)
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
                string str = NoticeBill.DeleteNotice(list);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetTreeParameter(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            try
            {
               string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                RestConsoleDemo.Service.Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string Token = nvc["Token"];

                string str = ParameterBill.GetTreeParameter();
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetAllParameterInfo(Stream stream)
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
                string str = ParameterBill.GetAllParameterInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetParameterById(Stream stream)
        {
            string Id = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
               string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                Id = nvc["id"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);

                string str = ParameterBill.GetParameterById(Convert.ToInt32(Id));
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string AddParameter(Stream stream)
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
                string str = ParameterBill.AddParameter(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string UpdateParameter(Stream stream)
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
                string str = ParameterBill.UpdateParameter(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }


        public string DeleteParameter(Stream stream)
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
                string str = ParameterBill.DeleteParameter(list);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetParameterSelect(Stream stream)
        {


            StreamReader sr = new StreamReader(stream);


            try
            {
               string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);

                string Token = nvc["Token"];
                string Code = nvc["Code"];
                UserBill.CheckToken(Token);

                string str = ParameterBill.GetParameterSelect(Code);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetAllBillNoInfo(Stream stream)
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
                string str = BillNoBill.GetAllBillNoInfo(ParameterStr, PageSize, CurrentPage);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string AddBillNo(Stream stream)
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
                string str = BillNoBill.AddBillNo(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string DeleteBillNo(Stream stream)
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
                string str = BillNoBill.DeleteBillNo(list);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string UpdateBillNo(Stream stream)
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
                string str = BillNoBill.UpdateBillNo(addStr);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GetBillNoById(Stream stream)
        {
            string ID = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
               string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

                Helper.ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                ID = nvc["Id"];
                string Token = nvc["Token"];
                UserBill.CheckToken(Token);
                int id = Convert.ToInt32(ID);
                string str = BillNoBill.GetBillNoById(id);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

        public string GeetestInit(Stream stream)
        {

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);

               
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string GeetestId = nvc["GeetestId"];
                Helper.ResponseHelper.SetHeaderInfo();
                string str = GeetestBill.GeetestInit(GeetestId);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

        }

        public string GeetestCheck(Stream stream)
        {

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd(); s = RestConsoleDemo.BLL.Helper.Base64Helper.DecodeBase64NotEnd(s);


                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                string GeetestId = nvc["GeetestId"];
                string geetest_challenge = nvc["geetest_challenge"];
                string geetest_seccode = nvc["geetest_seccode"];
                string geetest_validate = nvc["geetest_validate"];
                Helper.ResponseHelper.SetHeaderInfo();
                string str = GeetestBill.GeetestCheck(GeetestId, geetest_challenge, geetest_seccode, geetest_validate);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

        }

    }
       

}
