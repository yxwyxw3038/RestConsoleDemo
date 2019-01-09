using GeetestSDK;
using Newtonsoft.Json.Converters;
using RestConsoleDemo.BLL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.SysInfo
{
    public static class GeetestBill
    {
        public static string GeetestInit(string GeetestId)
        {
             string str = string.Empty;
             try
            {
                    IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                    timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                    string GeetestPublicKey = System.Configuration.ConfigurationManager.AppSettings["GeetestPublicKey"];
                    string GeetestPrivateKey = System.Configuration.ConfigurationManager.AppSettings["GeetestPrivateKey"];
                    GeetestLib geetest = new GeetestLib(GeetestPublicKey, GeetestPrivateKey);
                    Byte gtServerStatus = geetest.preProcess(GeetestId);
                    string returnStr = geetest.getResponseStr();
                    str = ResponseHelper.ResponseMsg("1", "取数成功", returnStr);
                 
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GeetestCheck(string GeetestId, string geetest_challenge, string geetest_seccode, string geetest_validate)
        {
            string str = string.Empty;
            try
            {
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                string GeetestPublicKey = System.Configuration.ConfigurationManager.AppSettings["GeetestPublicKey"];
                string GeetestPrivateKey = System.Configuration.ConfigurationManager.AppSettings["GeetestPrivateKey"];
                GeetestLib geetest = new GeetestLib(GeetestPublicKey, GeetestPrivateKey);
                var result = geetest.enhencedValidateRequest(geetest_challenge, geetest_validate, geetest_seccode, GeetestId);

                if (result == 1)
                {
                    str = ResponseHelper.ResponseMsg("1", "极验证成功", "");
                }
                else
                {
                    str = ResponseHelper.ResponseMsg("-1", "极验证失败", "");
                }


               

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;

        }
    }
}
