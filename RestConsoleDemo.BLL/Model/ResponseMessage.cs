using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public class ResponseMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public string DataCount { get; set; }
    }
}
