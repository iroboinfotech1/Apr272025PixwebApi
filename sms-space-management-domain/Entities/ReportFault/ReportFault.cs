using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.ReportFault
{
    public class ReportFault
    {
        public int FaultId { get; set; }
        public string FaultType { get; set; }
        public string Remarks { get; set; }
        public int FaultLookupId { get; set; }
        public List<string> FaultLookupValue{ get; set; }
        public int FloorId { get; set; }
        
    }
}
