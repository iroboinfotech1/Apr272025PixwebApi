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
    public class IndustryRepository : IIndustryRepository
    {
        private readonly DbSession _session;

        public IndustryRepository(DbSession session)
        {
            _session = session;
        }

		public async Task<Industry> Create(Industry entity)
		{
			var query = $@"INSERT INTO space_admin.industry(industry_name)
						VALUES (@Name)
                        RETURNING industry_id
						";
			//RETURNING id


			entity.industry_id = await _session.Connection.ExecuteScalarAsync<int>(query, entity, _session.Transaction);

			return entity;
		}

		public async Task<bool> Delete(int id)
		{
			var query = "Delete from space_admin.industry where industry_id=@ID";
			var result = await _session.Connection.ExecuteAsync(query, new { ID = id }, _session.Transaction);
			if (result > 0)
			{
				return true;
			}
			else return false;
		}



		public async Task<List<Industry>> GetAll()
		{
			var query = "Select * from space_admin.industry";
			var result = await _session.Connection.QueryAsync<Industry>(query, null, _session.Transaction);
			return result.ToList();
		}

		public async Task<Industry?> GetById(int id)
		{
			var query = $@"Select * from space_admin.industry where industry_id=@ID";
			var result = await _session.Connection.QueryAsync<Industry>(query, new { ID = id }, _session.Transaction);
			return result.FirstOrDefault();
		}

		public async Task<bool> Update(Industry entity)
		{
			var query = $@"UPDATE space_admin.industry
                        SET industry_name = @Name,
                        WHERE industry_id=@ID";
			var result = await _session.Connection.ExecuteAsync(query, new
			{
                industry_id = entity.industry_id,
                industry_name = entity.industry_name,
			}, _session.Transaction);
			if (result > 0)
			{
				return true;
			}
			else return false;
		}
	}
}
