using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace RestConsoleDemo.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract(Namespace = "WCF跨域")]
    public interface IPublicService
    {
        [OperationContract, WebInvoke(UriTemplate = "/GetData?value={value}", Method = "GET",
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
       
        [Description("测试")]
        string GetData(int value);
      


        // TODO: 在此添加您的服务操作
    }

   
   

}
