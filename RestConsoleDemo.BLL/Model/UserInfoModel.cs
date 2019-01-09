using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
     public  class UserInfoModel
    {
        public int ID { get; set; }
        public string AccountName { get; set; }      
        public string RealName { get; set; }   
        public string Token { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
  
    }
}
