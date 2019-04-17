using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public class NoticeViewModel
    {
        public string Code { get; set; }
        public string No { get; set; }
        public Nullable<int> ftypeid { get; set; }
        public string ftitle { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateBy { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> SendBeginTime { get; set; }
        public Nullable<System.DateTime> SendEndTime { get; set; }
        public int SendCount { get; set; }
        public int Count { get; set; }

        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }

    }
}
