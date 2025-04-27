using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.data.access.Repositories
{
    public class FloorRepository : IFloorRepository
    {
        private readonly DbSession _session;

        public FloorRepository(DbSession session)
        {
            _session = session;
        }

		public async Task<Floor> Create(Floor entity)
		{
			var query = $@"INSERT INTO space_admin.floor(building_id, floor_name,floor_plan)
						VALUES (@BuildingId,@FloorName,@FloorPlan)
						RETURNING floor_id
						";
			//RETURNING id


			entity.FloorId = await _session.Connection.ExecuteScalarAsync<int>(query, entity, _session.Transaction);

			return entity;
		}

		public async Task<bool> Delete(int id)
		{
			var query = "Delete from space_admin.floor where floor_id=@ID";
			var result = await _session.Connection.ExecuteAsync(query, new { ID = id }, _session.Transaction);
			if (result > 0)
			{
				return true;
			}
			else return false;
		}



		public async Task<List<Floor>> GetAll()
		{
			var query = "Select * from space_admin.floor";
			var result = await _session.Connection.QueryAsync<Floor>(query, null, _session.Transaction);
			return result.ToList();
		}

		public async Task<Floor?> GetById(int id)
		{
			var query = $@"Select * from space_admin.floor where floor_id=@ID";
			var result = await _session.Connection.QueryAsync<Floor>(query, new { ID = id }, _session.Transaction);
			return result.FirstOrDefault();
		}

		public async Task<List<Floor>> GetFloorByBuilding(int id)
		{
            var query = "Select * from space_admin.floor where building_id=@ID";
            var result = await _session.Connection.QueryAsync<Floor>(query, new { ID=id}, _session.Transaction);
            return result.ToList();
        }

		public async Task<bool> Update(Floor entity)
		{
			var query = $@"UPDATE space_admin.floor
                        SET building_id = @BuildingId,
                        floor_name = @FloorName,
                        floor_plan = @FloorPlan
                        WHERE floor_id=@ID";
			var result = await _session.Connection.ExecuteAsync(query, new
			{
				ID = entity.FloorId,
				BuildingId = entity.BuildingId,
				FloorName = entity.FloorName,
				FloorPlan = entity.FloorPlan,
			}, _session.Transaction);
			if (result > 0)
			{
				return true;
			}
			else return false;
		}
	}
}
