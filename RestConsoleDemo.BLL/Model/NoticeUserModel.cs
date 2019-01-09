using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public class NoticeUserModel
    {

        public int id { get; set; }
        public string NoticeCode { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<int> SendFlag { get; set; }
        public Nullable<System.DateTime> SendTime { get; set; }
        public string AccountName { get; set; }
        public string RealName { get; set; }
    }
}
