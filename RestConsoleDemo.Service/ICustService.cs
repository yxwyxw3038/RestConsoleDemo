using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service
{
     [ServiceContract]
    public interface ICustService
    {
        [OperationContract, WebInvoke(UriTemplate = "/GetData?value={value}", Method = "GET",
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [Description("获取附件")]
        string GetData(int value);
    }
}
