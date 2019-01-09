using Newtonsoft.Json.Linq;
using RestConsoleDemo.BLL;
using RestConsoleDemo.BLL.SysInfo;
using RestConsoleDemo.Service.Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
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
    public class FileService : IFileService
    {
        public string UpLoadFile(Stream stream, string Token, string DirName, string No)
        {
           

           
             
            try
            {

              
                UserBill.CheckToken(Token);
                string str = FileBill.UpLoadFile(stream, DirName, No);
                ResponseHelper.SetHeaderInfo();
                return str;



            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
    }
}
