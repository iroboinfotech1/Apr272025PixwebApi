using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Organization;
using static Dapper.SqlMapper;

namespace sms.space.management.data.access.Repositories;

public class FacilitiesRepository : IFacilitiesRepository
{
	private readonly DbSession _session;

	public FacilitiesRepository(DbSession session)
	{
		_session = session;
	}
	public async Task<Facilities> Create(Facilities entity)
	{
		//Insert query

		var query = $@"INSERT INTO space_admin.facilities_master(
							 facility_name, org_id, email, escalation_period, escalation_email, notify_facilities, notify_organizer,facility_type_id)
							VALUES (@FacilityName,@OrgId,@Email,@EscalationPeriod,@EscalationEmail,@NotifyFacilities,@NotifyOrganizer,@FacilityTypeId)
							RETURNING facility_id
							";
		//RETURNING id


		entity.FacilityId = await _session.Connection.ExecuteScalarAsync<int>(query, entity, _session.Transaction);

		return entity;


	}

	public async Task<bool> Delete(int id)
	{
		//Delete query

		var query = "Delete from space_admin.facilities_master where facility_id=@ID";

		int isDeleted = await _session.Connection.ExecuteAsync(query, new { ID = id }, _session.Transaction);

		if (isDeleted > 0)
			return true;
		else
			return false;

	}

	public async Task<List<Facilities>> GetByOrganization(int orgId)
	{
		//Get All
		var query = @"SELECT * FROM space_admin.facilities_master f
					left join space_admin.facility_resources r on r.facility_id = f.facility_id
					where f.org_id=@OrgId";

		var txnMap = new Dictionary<long, Facilities>();

		//var result = await _session.Connection.QueryAsync<Facilities>(query, new { OrgId = orgId }, _session.Transaction);
		var result = await _session.Connection.QueryAsync<Facilities, Resource, Facilities>(
		  query,
		   param: new { OrgId = orgId },
		   transaction: _session.Transaction,
		   splitOn: "resource_id",
		   map: (txn, txnDetail) => MapTransaction(txn, txnDetail, txnMap));

		return result.Distinct().ToList();
	}

    public async Task<List<Facilities>> GetByOrganizationAndFacilityType(int orgId,int facilityTypeId)
    {
        //Get All
        var query = @"SELECT * FROM space_admin.facilities_master f
					left join space_admin.facility_resources r on r.facility_id = f.facility_id
					where f.org_id=@OrgId  and f.facility_type_id=@facilityTypeId";

        var txnMap = new Dictionary<long, Facilities>();

        //var result = await _session.Connection.QueryAsync<Facilities>(query, new { OrgId = orgId }, _session.Transaction);
        var result = await _session.Connection.QueryAsync<Facilities, Resource, Facilities>(
          query,
           param: new { OrgId = orgId, facilityTypeId = facilityTypeId },
           transaction: _session.Transaction,
           splitOn: "resource_id",
           map: (txn, txnDetail) => MapTransaction(txn, txnDetail, txnMap));

        return result.Distinct().ToList();
    }

    private static Facilities MapTransaction(Facilities txn, Resource txnDetail, Dictionary<long, Facilities> txnMap)
	{
		if (txnMap.TryGetValue(txn.FacilityId, out var existingOrder))
		{
			txn = existingOrder;
		}
		else
		{
			txn.Resources = new List<Resource>();
			txnMap.Add(txn.FacilityId, txn);
		}
		if(txnDetail!=null)
			txn.Resources!.Add(txnDetail);
		return txn;
	}

	public async Task<Facilities?> GetById(int id)
	{
		//Get By Id
		var query = $@"Select * from space_admin.facilities_master where facility_id=@ID";
		var result = await _session.Connection.QueryAsync<Facilities>(query, new { ID = id }, _session.Transaction);
		return result.FirstOrDefault();
	}

	public async Task<bool> Update(Facilities entity)
	{
		//update query
		var query = $@"update space_admin.facilities_master set 
							facility_name = @FacilityName, facility_type_id = @FacilityTypeId, 
							email=@Email,escalation_period=@EscalationPeriod,escalation_email=@EscalationEmail,
							notify_facilities=@NotifyFacilities,notify_organizer=@NotifyOrganizer
							where facility_id = @FacilityId";

		var result = await _session.Connection.ExecuteAsync(query, entity, _session.Transaction);

		if (result > 0)
			return true;
		else
			return false;
	}

	public async Task<List<Facilities>> GetAll()
	{
		//Get All
		var query = @"SELECT * FROM space_admin.facilities_master f
					left join space_admin.facility_resources r on r.facility_id = f.facility_id 
					order by f.facility_name, r.name";

		var txnMap = new Dictionary<long, Facilities>();

		//var result = await _session.Connection.QueryAsync<Facilities>(query, new { OrgId = orgId }, _session.Transaction);
		var result = await _session.Connection.QueryAsync<Facilities, Resource, Facilities>(
		query,
		   param: null,
		   transaction: _session.Transaction,
		   splitOn: "resource_id",
		   map: (txn, txnDetail) => MapTransaction(txn, txnDetail, txnMap));

		return result.Distinct().ToList();
	}

    public async Task<Facilities> GetFacilitiesById(int facilityId)
    {
        var query = @"SELECT * FROM space_admin.facilities_master f
					left join space_admin.facility_resources r on r.facility_id = f.facility_id
					where f.facility_id=@facilityID order by r.name";

        var txnMap = new Dictionary<long, Facilities>();

        var result = await _session.Connection.QueryAsync<Facilities, Resource, Facilities>(
        query,
           param: new { facilityID = facilityId },
           transaction: _session.Transaction,
           splitOn: "resource_id",
           map: (txn, txnDetail) => MapTransaction(txn, txnDetail, txnMap));

        return result.Distinct().First();
    }

    public async Task<List<FacilityTypes>> GetAllFacilityTypes() {
        //GetAllFacilityTypes
        var query = @"select * from space_admin.facility_types";

        var result = await _session.Connection.QueryAsync<FacilityTypes>(query,  _session.Transaction);
        return result.ToList();
    }
}


public class ResourcesRepository : IResourcesRepository
{
	private readonly DbSession _session;

	public ResourcesRepository(DbSession session)
	{
		_session = session;
	}
	public async Task<Resource> Create(Resource entity)
	{
		var query = $@"INSERT INTO space_admin.facility_resources(
							 type, is_enabled,icon,facility_id,name,count)
							VALUES (@Type,@IsEnabled,@Icon,@FacilityId,@Name,@Count)
							RETURNING facility_id
							";

		entity.FacilityId = await _session.Connection.ExecuteScalarAsync<int>(query, entity, _session.Transaction);

		return entity;


	}

	public async Task<bool> Delete(int id)
	{
		//Delete query

		var query = "Delete from space_admin.facilities_master where facility_id=@ID";

		int isDeleted = await _session.Connection.ExecuteAsync(query, new { ID = id }, _session.Transaction);

		if (isDeleted > 0)
			return true;
		else
			return false;

	}

	public async Task<List<Resource>> GetByFacility(int facilityId)
	{
        //Get All
        //var query = "Select * from space_admin.facilities_master where org_id=@Id";
        var query = "Select * from space_admin.facility_resources where facility_id=@Id";
        var result = await _session.Connection.QueryAsync<Resource>(query, new { Id = facilityId }, _session.Transaction);
		return result.ToList();
	}

    public async Task<IReadOnlyList<Resource>> GetResources(int floorId)
	{
        var query = "select * from space_admin.facility_resources where resource_id  in (SELECT unnest(servicingfacilities)  FROM space_admin.space_master where floor_id= @FLOORID)";
        var result = await _session.Connection.QueryAsync<Resource>(query, new { FLOORID = floorId }, _session.Transaction);
        return result.ToList();
    }

	public async Task<bool> Update(Resource entity)
	{
		//update query
		var query = $@"update space_admin.facilities_master set 
							facility_name = @FacilityName, 
							email=@Email,EscalationPeriod=@EsclationPeriod,escalation_email=@EscalationEmail,
							notify_facilities=@NotifyFacilities,notify_organiser=@NotifyOrganizer
							where facility_id = @FacilityId";

		var result = await _session.Connection.ExecuteAsync(query, entity, _session.Transaction);

		if (result > 0)
			return true;
		else
			return false;
	}

    public async Task<bool> UpdateResourceStatus(Resource entity)
    {
        //update query
        var query = $@"Update space_admin.facility_resources set is_enabled = @IsEnabled, count = @Count  where resource_id = @ResourceId";

        var result = await _session.Connection.ExecuteAsync(query, entity, _session.Transaction);

        if (result > 0)
		{
			if (entity.SpaceId != 0)
			{
				var query2 = $@"CALL spUpdateSpaceResource(@SpaceId,@FacilityId,@ResourceId,@Count)";

				var result2 = await _session.Connection.ExecuteAsync(query2, entity, _session.Transaction);
			}
			return true;
		}
		else
			return false;
    }
}