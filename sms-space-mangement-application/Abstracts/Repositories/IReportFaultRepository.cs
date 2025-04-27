using sms.space.management.domain.Entities.ReportFault;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public  interface IReportFaultRepository
    {
        Task<ReportFault> CreateReportFault(ReportFault request);
        Task<bool> UpdateReportFault(ReportFault request);
        Task<IReadOnlyList<ReportFault>> GetReportFaults();
        Task<ReportFault> GetReportFaultByFaultId(int faultId);
        Task<bool> DeleteReportFault(int faultId);
        Task<IReadOnlyList<LookupReportFault>> GetLookupFaultReports();

    }
}
