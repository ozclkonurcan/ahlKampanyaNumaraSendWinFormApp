using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarelSeviceFormApp.VM_Model
{
    public class VM_ApiGetCampaigns
    {
        public int CampID { get; set; }
        public string campName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string status { get; set; }
    }
}
