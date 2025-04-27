using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.data.access.Repositories
{
	public class BuildingRepository : IBuildingRepository
	{
		private readonly DbSession _session;

		public BuildingRepository(DbSession session)
		{
			_session = session;
		}

		public async Task<Building> Create(Building entity)
		{

			var query = $@"INSERT INTO space_admin.buildings_master(building_name,org_id,group_name,address)
							VALUES (@BuildingName,@OrgId,@GroupName,@Address)
							RETURNING building_id";

			entity.BuildingId = await _session.Connection.ExecuteScalarAsync<int>(query, entity, _session.Transaction);

			foreach (var flooritem in entity.Floors)
			{
				flooritem.BuildingId = entity.BuildingId;
				var floorquery = $@"INSERT INTO space_admin.floor(building_id, floor_name, floor_plan)
								 VALUES (@BuildingId,@FloorName,@FloorPlan)
								 RETURNING floor_id
								 ";

				flooritem.FloorId = await _session.Connection.ExecuteScalarAsync<int>(floorquery, flooritem, _session.Transaction);
			}

			return entity;
		}
		public async Task<List<Building>> GetAll()
		{
			//Get All
			var query = @"SELECT * FROM space_admin.buildings_master f
						left join space_admin.organisation_master r on r.org_id = f.org_id
					";

			var txnMap = new Dictionary<long, Building>();

			//var result = await _session.Connection.QueryAsync<Facilities>(query, new { OrgId = orgId }, _session.Transaction);
			var result = await _session.Connection.QueryAsync<Building, Organization, Building>(
			query,
			   param: null,
			   transaction: _session.Transaction,
			   splitOn: "org_id",
			   map: (txn, txnDetail) => MapTransaction(txn, txnDetail, txnMap));

			return result.Distinct().ToList();

			//         var query = "Select * from space_admin.buildings_master";
			//var result = await _session.Connection.QueryAsync<Building>(query, null, _session.Transaction);
			//return result.ToList();
		}

		private static Building MapTransaction(Building txn, Organization txnDetail, Dictionary<long, Building> txnMap)
		{
			if (txnMap.TryGetValue(txn.BuildingId, out var existingOrder))
			{
				txn = existingOrder;
			}
			else
			{
				txn.Organization = new Organization();
				txnMap.Add(txn.BuildingId, txn);
			}

			txn.Organization = (txnDetail);
			return txn;
		}


		public async Task<List<Building>> GetById(int id)
		{
			var query = $@"Select * from space_admin.buildings_master where building_id=@ID";
			var result = await _session.Connection.QueryAsync<Building>(query, new { ID = id }, _session.Transaction);
			return result.ToList();
		}

		public async Task<bool> Delete(int id)
		{
			var query = "Delete from space_admin.buildings_master where building_id=@ID";
			var result = await _session.Connection.ExecuteAsync(query, new { ID = id }, _session.Transaction);
			if (result > 0)
			{
				return true;
			}	
			else return false;
		}
		public async Task<bool> Update(Building entity)
		{
			var query = $@"UPDATE space_admin.buildings_master
						SET building_name = @BuildingName,
						address = @Address,					
						org_id=@OrgId,
						group_name=@GroupName
						WHERE building_id=@BuildingId";

			var result = await _session.Connection.ExecuteAsync(query, entity, _session.Transaction);

			if (result > 0)
			{
                foreach (var flooritem in entity.Floors)
                {
                    flooritem.BuildingId = entity.BuildingId;
                    var floorquery = $@"INSERT INTO space_admin.floor(building_id, floor_name, floor_plan)
								 VALUES (@BuildingId,@FloorName,@FloorPlan)
								 RETURNING floor_id
								 ";

                    flooritem.FloorId = await _session.Connection.ExecuteScalarAsync<int>(floorquery, flooritem, _session.Transaction);
                }

                return true;
			}
			else return false;
		}

		public async Task<List<Building>> GetBuildingsByOrg(int id)
        {//Get BuildingsByOrg
            var query = @"SELECT * FROM space_admin.buildings_master where org_id = @ID";

            var txnMap = new Dictionary<long, Building>();

            //var result = await _session.Connection.QueryAsync<Facilities>(query, new { OrgId = orgId }, _session.Transaction);
            var result = await _session.Connection.QueryAsync<Building, Organization, Building>(
            query,
               param: new{ID=id },
               transaction: _session.Transaction,
               splitOn: "org_id",
               map: (txn, txnDetail) => MapTransaction(txn, txnDetail, txnMap));

            return result.Distinct().ToList();

            //         var query = "Select * from space_admin.buildings_master";
            //var result = await _session.Connection.QueryAsync<Building>(query, null, _session.Transaction);
            //return result.ToList();
        }
    }
}
