using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public  class LoginViewModel
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
        public Nullable<System.DateTime> LoginOutTime { get; set; }
        public Nullable<int> IsLoginOut { get; set; }
        public string AccountName { get; set; }

        public string RealName { get; set; }
    }
}
