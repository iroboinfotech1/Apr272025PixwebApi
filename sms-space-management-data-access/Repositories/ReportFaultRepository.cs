using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.ContentManagement;
using sms.space.management.domain.Entities.ReportFault;
using sms.space.management.domain.Entities.Spaces;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sms.space.management.data.access.Repositories.ReportFaultRepository;

namespace sms.space.management.data.access.Repositories
{
    public class ReportFaultRepository : IReportFaultRepository
    {
        private readonly DbSession _session;

        public ReportFaultRepository(DbSession session)
        {
            _session = session;
        }

        #region == ReportFault ==

        public async Task<ReportFault> GetReportFaultByFaultId(int faultId)
        {
            var query = $@"Select * from space_admin.report_fault where fault_id=@faultId";
            var result = await _session.Connection.QueryAsync<ReportFault>(query, new { faultId = faultId }, _session.Transaction);
            return result.FirstOrDefault();
        }
        public async Task<IReadOnlyList<ReportFault>> GetReportFaults()
        {
            var query = "Select * from space_admin.report_fault";
            var result = await _session.Connection.QueryAsync<ReportFault>(query, null, _session.Transaction);
            return result.ToList();
        }

        public async Task<ReportFault> CreateReportFault(ReportFault request)
        {
            if (request.FaultLookupValue != null && request.FaultLookupValue.Count > 0)
            {
                foreach (string lookupValue in request.FaultLookupValue)
                {
                    var lookquery = $@"Select * from space_admin.lookup_report_fault where lookup_fault_name=@lookupValue ";

                    var lookresult = await _session.Connection.QueryAsync<LookupReportFault>(lookquery, new { lookupValue = lookupValue.ToUpper()}, _session.Transaction);
                    if (lookresult != null && lookresult.Count() > 0)
                    {
                        request.FaultLookupId = lookresult.FirstOrDefault().LookupId;
                        if (lookresult != null && lookresult.Count() > 0)
                        {
                            request.FaultType = request.FaultType.ToUpper();
                            var query = $@"INSERT INTO  space_admin.report_fault (fault_type,remarks,lookup_id,floor_id)
						VALUES (@FaultType,@Remarks,@FaultLookupId,@FloorId)
						RETURNING fault_id ";
                            //RETURNING id


                            request.FaultId = await _session.Connection.ExecuteScalarAsync<int>(query, request, _session.Transaction);
                        }
                    }

                }
            }
            else
            {
                string lookupFaultType = request.FaultType;
                var lookquery = $@"Select * from space_admin.lookup_report_fault where lookup_fault_name='NONE' and lookup_fault_type=@lookupFaultType";

                var lookresult = await _session.Connection.QueryAsync<LookupReportFault>(lookquery, new { lookupFaultType = lookupFaultType }, _session.Transaction);
                if (lookresult != null && lookresult.Count() > 0)
                {


                    request.FaultLookupId = lookresult.FirstOrDefault().LookupId;
                    request.FaultType = request.FaultType.ToUpper();
                    var query = $@"INSERT INTO  space_admin.report_fault (fault_type,remarks,lookup_id,floor_id)
						VALUES (@FaultType,@Remarks,@FaultLookupId,@FloorId)
						RETURNING fault_id ";
                    //RETURNING id


                    request.FaultId = await _session.Connection.ExecuteScalarAsync<int>(query, request, _session.Transaction);
                }
            }
            return request;
            
        }
        public async Task<bool> UpdateReportFault(ReportFault request)
        {
            var query = $@"UPDATE space_admin.report_fault 
                        SET 
                        fault_type = @FaultType,
                        remarks = @Remarks,
                        lookup_id = @FaultLookupId,
                        lookup_value = @FaultLookupValue,
                        floor_id = @FloorId
                        WHERE fault_id = @FaultId";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                FaultId = request.FaultId,
                FaultType = request.FaultType,
                Remarks = request.Remarks,
                FaultLookupId = request.FaultLookupId,
                FaultLookupValue = request.FaultLookupValue,
                FloorId = request.FloorId,

            }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
        public async Task<bool> DeleteReportFault(int faultId)
        {
            var query = "Delete from space_admin.report_fault where fault_id=@faultId";
            var result = await _session.Connection.ExecuteAsync(query, new { faultId = faultId }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<List<AvailableSpace>>FindAllRooms(SpaceFilter filter)
        {

            //Get All
            var query = $@"SELECT sm.spacealiasname as spaceName, sm.space_id as spaceId, sm.space_type as spaceType, sm.coordinates, 
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
				where sm.org_id=@org_id and b.building_id=@building_id and f.floor_id=@floor_id";
            var result = await _session.Connection.QueryAsync<AvailableSpace>(
            query,
               param: new { org_id = filter.OrgId, building_id = filter.BuildingId, floor_Id = filter.FloorId, start_date = filter.StartDate, end_date = filter.EndDate },
               transaction: _session.Transaction);
            //map: (space, building, txnDetail) => MapTransaction(space, building, txnDetail, txnMap));

            return result.Distinct().ToList();


            //         var result = await _session.Connection.QueryAsync<Spaces>(query, null, _session.Transaction);
            //return result.ToList();
        }

        public async Task<IReadOnlyList<LookupReportFault>> GetLookupFaultReports()
        {
            var query = "Select * from space_admin.lookup_report_fault";
            var result = await _session.Connection.QueryAsync<LookupReportFault>(query, null, _session.Transaction);
            return result.ToList();
        }

        #endregion

    }
}