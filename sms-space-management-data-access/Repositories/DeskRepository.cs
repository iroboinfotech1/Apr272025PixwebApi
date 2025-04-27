using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Facilities;
using sms.space.management.domain.Entities.Organization;
using sms.space.management.domain.Entities.Spaces;

namespace sms.space.management.data.access.Repositories
{
    public class DeskRepository : IDeskRepository
    {
        private readonly DbSession _session;

        public DeskRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<List<AvailableSpace>> GetAllAvailableDesk(SpaceFilter filter)
        {
            //Get All
            var query = $@"SELECT sm.spacealiasname as spaceName, sm.space_id as spaceId, sm.space_type_id as spaceType, sm.coordinates, 
				b.building_id as buildingId, b.building_name as buildingName, b.address, om.image as organisationImage,
				fr.resource_id as resourceId, fr.icon as resourceIcon, fr.facility_id as facilityId,smt.start_date as startDate, 
                smt.end_date as endDate, fr.name as facilityName,f.floor_id as floorId, f.floor_name as floorName, f.floor_plan as floorImage,
                sm.org_id as orgId, sr.resource_count as resourceCount, case when (start_date between '{Convert.ToString(filter.StartDate)}' and '{Convert.ToString(filter.EndDate)}') 
			  	THEN 'false' 
			  	when (end_date between '{Convert.ToString(filter.StartDate)}' and '{Convert.ToString(filter.EndDate)}') 
			  	then 'false'
			  	else 'true' end as isAvailable
			  	FROM space_admin.space_master sm
			  	left join space_admin.organisation_master om on sm.org_id = om.org_id
				left join space_admin.buildings_master b on b.building_id = sm.building_id
				left join space_admin.floor f on f.building_id = b.building_id
				left join space_admin.space_resource sr on sm.space_id = sr.space_id
				left join space_admin.facility_resources fr on sr.resource_id = fr.resource_id and  fr.is_enabled='true'
				left join space_admin.space_meeting smt on smt.space_id=sm.space_id 
                and smt.start_date between '{Convert.ToString(filter.StartDate)}' and '{Convert.ToString(filter.EndDate)}'
				and smt.end_date between '{Convert.ToString(filter.StartDate)}' and '{Convert.ToString(filter.EndDate)}'
				where sm.org_id=@org_id and b.building_id=@building_id and f.floor_id=@floor_id and sm.space_type_id=@space_type_id";
            var result = await _session.Connection.QueryAsync<AvailableSpace>(
            query,
               param: new { org_id = filter.OrgId, building_id = filter.BuildingId, floor_Id = filter.FloorId, start_date = filter.StartDate, end_date = filter.EndDate, space_type_id=filter.space_type_id },
               transaction: _session.Transaction);

            return result.Distinct().ToList();
        }

        private static Desk MapTransaction(Desk desk, Spaces space, Floor floor, Building building, Organization org, FacilityResource facilityResource)
        {
            if (building != null)
            {
                desk.Building = new Building();
                desk.Building = building;
            }
            if (space != null)
            {
                desk.Space = new Spaces();
                desk.Space = space;
            }
            if (org != null)
            {
                desk.Organization = new Organization();
                desk.Organization = org;
            }
            if (floor != null)
            {
                desk.Floor = new Floor();
                desk.Floor = floor;
            }
            if (facilityResource != null)
            {
                desk.FacilityResource = new FacilityResource();
                desk.FacilityResource = facilityResource;
            }
            return desk;
        }
    }
}
