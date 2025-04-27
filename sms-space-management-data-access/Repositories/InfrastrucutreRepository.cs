using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Building;

namespace sms.space.management.data.access.Repositories
{
	public class InfrastructureRepository : IInfrastructureRepository
	{
		private readonly DbSession _session;

		public InfrastructureRepository(DbSession session)
		{
			_session = session;
		}

		public async Task<Infrastructure> Create(Infrastructure entity)
		{
			var query = $@"INSERT INTO space_admin.infrastructure(infra_name,infracount,enabled,category,room_id)
						VALUES (@infra_name,@infracount,@enabled,@category,@room_id)
						RETURNING infra_id
						";
			//RETURNING id


			entity.infra_id = await _session.Connection.ExecuteScalarAsync<int>(query, entity, _session.Transaction);

			return entity;
		}

		public async Task<bool> Delete(int id)
		{
			var query = "Delete from space_admin.infrastructure where Infrastructure_id=@infra_id";
			var result = await _session.Connection.ExecuteAsync(query, new { ID = id }, _session.Transaction);
			if (result > 0)
			{
				return true;
			}
			else return false;
		}



		public async Task<List<Infrastructure>> GetAll()
		{
			var query = "Select * from space_admin.infrastructure";
			var result = await _session.Connection.QueryAsync<Infrastructure>(query, null, _session.Transaction);
			return result.ToList();
		}

		public async Task<Infrastructure?> GetById(int id)
		{
			var query = $@"Select * from space_admin.infrastructure where infra_id=@infra_id";
			var result = await _session.Connection.QueryAsync<Infrastructure>(query, new { infra_id = id }, _session.Transaction);
			return result.FirstOrDefault();
		}

		public async Task<bool> Update(Infrastructure entity)
		{
			var query = $@"UPDATE space_admin.infrastructure
						SET infra_name = @infra_name,
						infracount = @infracount
						enabled = @enabled
						category = @category
						room_id = @ID
						WHERE infra_id=@infra_id";
			var result = await _session.Connection.ExecuteAsync(query, new
			{
				infra_id = entity.infra_id,
				infra_name = entity.infra_name,
				infracount = entity.infracount,
				enabled = entity.enabled,
				category = entity.category,
				room_id = entity.Room.ID,
			}, _session.Transaction);
			if (result > 0)
			{
				return true;
			}
			else return false;
		}
	}
}
