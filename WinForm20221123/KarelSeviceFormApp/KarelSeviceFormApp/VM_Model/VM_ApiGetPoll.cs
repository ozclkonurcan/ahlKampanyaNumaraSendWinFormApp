using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarelSeviceFormApp.VM_Model
{
    public class VM_ApiGetPoll
    {
        public int pollID { get; set; }
        public string pollName { get; set; }
        public string pollStartDate { get; set; }
        public string pollEndDate { get; set; }
        public string pollLang { get; set; }
        public int pollStatus { get; set; }
    }
}
