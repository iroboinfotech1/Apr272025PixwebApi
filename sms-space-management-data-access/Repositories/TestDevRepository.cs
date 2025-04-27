using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.data.access.Repositories
{
    public class TestDevRepository : ITestDevRepository
    {
		private readonly DbSession _session;

		public TestDevRepository(DbSession session)
		{
			_session = session;
		}

		public async Task<TestDevs> Create(TestDevs entity)
		{
			var query = $@"INSERT INTO space_admin.test_dev(test_first)
							VALUES (@testfirst)
                            RETURNING test_id ";

			entity.Id = await _session.Connection.ExecuteScalarAsync<int>(query, entity, _session.Transaction);
			

			return entity;
		}
	}
}
