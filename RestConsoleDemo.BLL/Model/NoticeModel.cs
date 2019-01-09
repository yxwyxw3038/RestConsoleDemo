using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public class NoticeModel
    {
        public tbNotice Main { get; set; }
        public List<NoticeUserModel> Item { get; set; }
    }
}
