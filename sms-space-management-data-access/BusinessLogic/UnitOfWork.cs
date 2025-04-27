using sms.space.management.application.Abstracts;
using sms.space.management.application.Abstracts.Repositories;

namespace sms.space.management.data.access.BusinessLogic
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;

        public IOrganizationRepository Organization { get; }

        public UnitOfWork(DbSession session, IOrganizationRepository organization)
        {
            _session = session;
            _session.Transaction = _session.Connection.BeginTransaction();
            Organization = organization;
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
