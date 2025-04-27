using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.PlayerManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace sms.space.management.data.access.Repositories
{
    public class PlayerManagementRepository:IPlayerManagementRepository
    {
        private readonly DbSession _session;

        public PlayerManagementRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<PlayerManagement> Create(PlayerManagement entity)
        {
            var query = $@"INSERT INTO  space_admin.player_master (serial_number, device_name,ip_address,department,location_name,contact_person,resolution ,space_name,theme,orientation,space_id)
						VALUES (@SerialNumber,@DeviceName,@IpAddress,@Department,@LocationName,@ContactPerson,@Resolution,@SpaceName,@Theme,@Orientation,@SpaceId)
						RETURNING serial_number			
            ";
            //RETURNING id


            entity.SerialNumber = await _session.Connection.ExecuteScalarAsync<string>(query, entity, _session.Transaction);
            return entity;
        }

        public async Task<bool> Delete(string serialNo)
        {
            var query = "Delete from space_admin.player_master where serial_number=@SerialNo";
            var result = await _session.Connection.ExecuteAsync(query, new { SerialNo = serialNo }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<IReadOnlyList<PlayerManagement>> GetAll()
        {
            var query = "Select * from space_admin.player_master";
            var result = await _session.Connection.QueryAsync<PlayerManagement>(query, null, _session.Transaction);
            return result.ToList();
        }
        public async Task<IReadOnlyList<UtilityPlayer>> RetrieveStaticData()
        {
            var query = "Select * from space_admin.Utility_player";
            var result = await _session.Connection.QueryAsync<UtilityPlayer>(query, null, _session.Transaction);
            return result.ToList();
        }
        public async Task<PlayerManagement> GetBySerialNumber(string serialNo)
        {
            var query = $@"Select * from space_admin.player_master where serial_number=@SerialNo";
            var result = await _session.Connection.QueryAsync<PlayerManagement>(query, new { SerialNo = serialNo }, _session.Transaction);
            return result.FirstOrDefault();
        }

        public async Task<bool> Update(PlayerManagement request)
        {
            var query = $@"UPDATE space_admin.player_master 
                        SET
                        device_name = @DeviceName,
                        ip_address = @IpAddress,
                        department = @Department,
                        location_name = @LocationName,
                        contact_person = @ContactPerson,
                        resolution = @Resolution,
                        theme = @Theme,
                        orientation = @Orientation,
                        space_name=@SpaceName,
                        space_id=@SpaceId
                        WHERE serial_number = @SerialNumber";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                SerialNumber = request.SerialNumber,
                DeviceName = request.DeviceName,
                IpAddress = request.IpAddress,
                Department = request.Department,
                LocationName = request.LocationName,
                ContactPerson = request.ContactPerson,
                Resolution = request.Resolution,
                SpaceName = request.SpaceName,
                Theme = request.Theme,
                Orientation = request.Orientation,
                SpaceId = request.SpaceId
            }, _session.Transaction);

            if (result > 0)
            {
                return true;
            }
            else return false;
        }


        public async Task<bool> InsertPlayerSensitiveInformation(PlayerSensitive request)
        {

            var query = $@"INSERT INTO  space_admin.player_sensitive (serial_number, sixdigitcode)
						VALUES (@SerialNumber,@sixdigitcode)
						RETURNING serial_number";
          
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                SerialNumber = request.SerialNumber,
                SixDigitCode = request.SixDigitCode

            }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> InsertPlayerLogs(PlayerLogs request)
        {
 
          var query = $@"INSERT INTO  space_admin.playermanagementlogs(serial_number,status,loginsertdate,activity)
						VALUES (@SerialNumber,@Status,@loginsertdate,@Activity)
						RETURNING serial_number";

            var result = await _session.Connection.ExecuteAsync(query, new
            {
                SerialNumber = request.SerialNumber,
                status = request.Status,
                loginsertdate = Convert.ToDateTime(request.Loginsertdate),
                activity =request.Activity
            }, _session.Transaction);
           
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
        public async Task<PlayerSensitive> RetrievePlayerSensitiveInformation(string serialNo)
        {
            var query = "Select * from space_admin.player_sensitive where serial_number=@SerialNo";
            var result = await _session.Connection.QueryAsync<PlayerSensitive>(query, new { SerialNo = serialNo }, _session.Transaction);
            return result.FirstOrDefault();
        }

        public async Task<PlayerLogs> GetPlayerLogsBySerialNumber(string serialNo)
        {
            var query = "Select * from space_admin.playermanagementlogs where serial_number=@SerialNo";
            var result = await _session.Connection.QueryAsync<PlayerLogs>(query, new { SerialNo = serialNo }, _session.Transaction);
            return result.FirstOrDefault();
        }
    }
}
