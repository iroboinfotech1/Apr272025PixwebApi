using sms.space.management.domain.Entities.ReportFault;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IReportFaultService
    {
        Task<IReadOnlyList<ReportFault>> GetReportFaults();
        Task<ReportFault> GetReportFault(int reportFaultId);
        Task<ReportFault> CreateReportFault(ReportFault request);
        Task<bool> UpdateReportFault(ReportFault request);
        Task<bool> DeleteReportFault(int reportFaultId);
        Task<IReadOnlyList<LookupReportFault>> GetLookupFaultReports();

    }
}
