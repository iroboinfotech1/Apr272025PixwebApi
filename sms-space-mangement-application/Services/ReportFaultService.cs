using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.ReportFault;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public  class ReportFaultService: IReportFaultService
    {

        private readonly IReportFaultRepository _repository;
        public ReportFaultService(IReportFaultRepository repository)
        {
            _repository = repository;
        }
      
        public async Task<IReadOnlyList<ReportFault>> GetReportFaults()
        {
            var reportFault = await _repository.GetReportFaults();
            return reportFault;
        }

        public async Task<ReportFault> GetReportFault(int reportFaultId)
        {
            var reportFault = await _repository.GetReportFaultByFaultId(reportFaultId);
            return reportFault;
        }

        public async Task<ReportFault> CreateReportFault(ReportFault request)
        {
            var reportFault = await _repository.CreateReportFault(request);
            return reportFault;
        }

        public async Task<bool> UpdateReportFault(ReportFault request)
        {
            var isupdated = await _repository.UpdateReportFault(request);
            return isupdated;
        }

        public async Task<bool> DeleteReportFault(int reportFaultId)
        {
            var isdeleted = await _repository.DeleteReportFault(reportFaultId);
            return isdeleted;
        }

        public async Task<IReadOnlyList<LookupReportFault>> GetLookupFaultReports()
        {
            var lookupReportFaults = await _repository.GetLookupFaultReports();
            return lookupReportFaults;
        }
    }
}
