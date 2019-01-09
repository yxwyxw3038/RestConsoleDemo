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
    [ServiceContract(Namespace = "系统信息接口")]
    public interface IFileService
    {
        //上传文件
        [OperationContract, WebInvoke(UriTemplate = "/UpLoadFile?Token={Token}&DirName={DirName}&No={No}", Method = "POST",
          ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取指定用户信息")]
        string UpLoadFile(Stream stream, string Token, string DirName, string No);

       
    }
}
