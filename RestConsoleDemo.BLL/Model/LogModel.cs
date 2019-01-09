using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public class LogModel
    {
        public string LogDateTime { get; set; }
        public string RunClassName { get; set; }

        public string LogType { get; set; }
        public string LogInfo { get; set; }
    }
}
