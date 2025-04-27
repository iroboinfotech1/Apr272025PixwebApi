using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.ReportFault
{
    public  class LookupReportFault
    {
        public int LookupId { get; set; }
        public string LookupFaultType { get; set; }
        public string LookupFaultName { get; set; }
        public string Remarks { get; set; }
    }
}
