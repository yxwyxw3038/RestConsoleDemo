using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public   class UserViewModel
    {
        public int ID { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> IsAble { get; set; }
        public Nullable<int> IfChangePwd { get; set; }

        public string RoleName { get; set; }

        public string DepartmentName { get; set; }
    }
}
