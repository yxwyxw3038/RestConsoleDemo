using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public class FlowModel
    {
        public tbFlow Flow { get; set; }
        public List<tbFlowStep> FlowStep { get; set; }
        public List<tbFlowStepUser> FlowStepUser { get; set; }
    }
}
