using sms.space.management.application.Abstracts.Repositories;

namespace sms.space.management.application.Abstracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();

        IOrganizationRepository Organization { get; }
    }
}
