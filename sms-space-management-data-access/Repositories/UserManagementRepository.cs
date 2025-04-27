using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.PlayerManagement;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace sms.space.management.data.access.Repositories
{
    public class UserManagementRepository: IUserManagementRepository
    {
        private readonly DbSession _session;

        public UserManagementRepository(DbSession session)
        {
            _session = session;
        }

        #region == User mangement ==

        public async Task<UserManagement> GetByUserId(string userId)
        {
            var query = $@"Select * from space_admin.User_management where user_id=@userId";
            var result = await _session.Connection.QueryAsync<UserManagement>(query, new { userId = userId }, _session.Transaction);
            return result.FirstOrDefault();
        }
        public async Task<IReadOnlyList<UserManagement>> GetUserManagements()
        {
            var query = "Select * from space_admin.User_management";
            var result = await _session.Connection.QueryAsync<UserManagement>(query, null, _session.Transaction);
            return result.ToList();
        }

        public async Task<UserManagement> CreateUserManagement(UserManagement request)
        {
                var query = $@"INSERT INTO  space_admin.User_management (user_name,email,role_id,joined,secret_word,repeat_secretWord,isvisitor,mobileno,idtype,idvalue,firstname,lastname)
						VALUES (@UserName,@Email,@RoleId,@Joined,@Secretword,@Repeatsecretword,@Isvisitor,@Mobileno,@IdType,@idvalue,@FirstName,@LastName)
						RETURNING user_id			
            ";
            //RETURNING id


            request.UserId = await _session.Connection.ExecuteScalarAsync<string>(query, request, _session.Transaction);
            return request;
        }
        public async Task<bool> UpdateUserManagement(UserManagement request)
        {
            var query = $@"UPDATE space_admin.User_management 
                        SET 
                        user_name = @UserName,
                        email = @Email,
                        role_id = @RoleId,
                        joined = @Joined,
                        secret_word = @SecretWord,
                        repeat_secretWord = @RepeatSecretWord,
                        isvisitor=@Isvisitor,
                        mobileno=@MobileNumber,
                        idtype=@IdType,
                        idvalue=@idvalue,
                        firstname=@FirstName,
                        lastname=@LastName
                        WHERE user_id = @UserId";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                UserId = request.UserId,
                UserName = request.UserName,
                Email = request.Email,
                RoleId = request.RoleId,
                Joined = request.Joined,
                SecretWord = request.SecretWord,
                RepeatSecretWord = request.RepeatSecretWord,
                IsVisitor = request.IsVisitor,
                MobileNumber = request.mobileno,
                IdType = request.idtype,
                IdValue = request.idvalue, // Corrected to match SQL placeholder
                FirstName = request.FirstName,
                LastName = request.LastName
            }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
        public async Task<bool> DeleteUserManagement(string userId)
        {
            var query = "Delete from space_admin.User_management where user_id=@userId";
            var result = await _session.Connection.ExecuteAsync(query, new { userId = userId }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }


        #endregion


        #region == Role ==


        public async Task<Role> GetByRoleId(int roleid)
        {
            var query = $@"Select * from space_admin.Role where  role_id =@role_id";
            var result = await _session.Connection.QueryAsync<Role>(query, new { role_id = roleid }, _session.Transaction);
            return result.FirstOrDefault();
        }
        public async Task<IReadOnlyList<Role>> GetRoleDetails()
        {
            var query = "Select * from space_admin.Role";
            var result = await _session.Connection.QueryAsync<Role>(query, null, _session.Transaction);
            return result.ToList();
        }

        public async Task<Role> CreateRole(Role request)
        {
            var query = $@"INSERT INTO  space_admin.Role(user_id, role_name,role_base)
						VALUES (@UserId,@RoleName,@RoleBase)
						RETURNING role_id			
            ";
            //RETURNING id


            request.RoleId = await _session.Connection.ExecuteScalarAsync<int>(query, request, _session.Transaction);
            return request;
        }
        public async Task<bool> UpdateRole(Role request)
        {
            var query = $@"UPDATE space_admin.Role 
                        SET 
                        user_id = @UserId ,
                        role_name = @RoleName ,
                        role_base = @RoleBase 
                        WHERE role_id = @RoleId";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                UserId = request.UserId,
                RoleId = request.RoleId,
                RoleName = request.RoleName,
                RoleBase = request.RoleBase

            }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
        public async Task<bool> DeleteRole(int roleId)
        {
            var query = "Delete from space_admin.Role where role_id=@roleId";
            var result = await _session.Connection.ExecuteAsync(query, new { roleId = roleId }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        #endregion


        public async Task<Permission> GetPermissionByPermissionId(int permissionId)
        {
            var query = $@"Select * from space_admin.Permission where permission_id=@permissionId";
            var result = await _session.Connection.QueryAsync<Permission>(query, new { permissionId = permissionId }, _session.Transaction);
            return result.FirstOrDefault();
        }
        public async Task<Permission> CreatePermission(Permission permssion)
        {
            var query = $@"INSERT INTO  space_admin.Permission (role_id,isconnectormanagement,isgroupmanagement,isbookroom,isbookservice,isusermanagement,isspacemanagement,
                          isbookdesk,isfindcolleague,isplayermangement,isorganizationmanagement,isbookparking,ismanagevisitor,issampledata1,issampledata2)
						VALUES (@RoleId,@IsConnectorManagement,@IsGroupManagement,@IsbookRoom,@IsBookService,@IsUserManagement,@IsSpaceManagement,@IsBookDesk,
                                @IsFindColleague,@IsPlayerMangement,@IsOrganizationManagement,@IsBookParking,@IsManageVisitor,@IsSampleData1,@IsSampleData2)
						RETURNING permission_id			
            ";
            //RETURNING id


            permssion.PermssionId = await _session.Connection.ExecuteScalarAsync<int>(query, permssion, _session.Transaction);
            return permssion;
        }
        public async Task<bool> UpdatePermission(Permission permssion)
        {
            var query = $@"UPDATE space_admin.Permission
                        SET
                        role_id = @RoleId,
                        isconnectormanagement = @IsConnectorManagement,
                        isgroupmanagement = @IsGroupManagement,
                        isbookroom = @IsbookRoom,
                        isbookservice = @IsBookService,
                        isbookdesk = @IsBookDesk,
                        isfindcolleague = @IsFindColleague,
                        isplayermangement = @IsPlayerMangement,
                        isorganizationmanagement = @IsOrganizationManagement,
                        isbookparking = @IsBookParking, 
                        ismanagevisitor = @IsManageVisitor, 
                        issampledata1 = @IsSampleData1, 
                        issampledata2 = @IsSampleData2 
                        WHERE permission_id = @PermssionId";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                PermssionId = permssion.PermssionId,
                RoleId = permssion.RoleId,
                IsConnectorManagement = permssion.IsConnectorManagement,
                IsGroupManagement = permssion.IsGroupManagement,
                IsbookRoom = permssion.IsbookRoom,
                IsBookService = permssion.IsBookService,
                IsBookDesk = permssion.IsBookDesk,
                IsFindColleague = permssion.IsFindColleague,
                IsPlayerMangement = permssion.IsPlayerMangement,
                IsOrganizationManagement = permssion.IsOrganizationManagement,
                IsBookParking = permssion.IsBookParking,
                IsManageVisitor = permssion.IsManageVisitor,
                IsSampleData1 = permssion.IsSampleData1,
                IsSampleData2 = permssion.IsSampleData2

            }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
        public async Task<bool> DeletePermission(int permissionId)
        {
            var query = "Delete from space_admin.Permission where permission_id=@permissionId";
            var result = await _session.Connection.ExecuteAsync(query, new { permissionId = permissionId }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

       public async Task<UserPreferences> CreateUserPreferences(UserPreferences request)
        {
            byte[]? userimage = string.IsNullOrEmpty(request.UserImage) ? null : Convert.FromBase64String(request.UserImage);
            var query = $@"INSERT INTO space_admin.tuser_preferences(user_id,df_building, df_timezone, user_image, timezone_interest,preferred_weekdays)
                 VALUES (@UserId, @buildingId, @DefaultTimeZone, @UserImage,@TimeZoneInterest,@PreferredWeekdays) RETURNING user_id";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                buildingId = request.BuildingId,
                DefaultTimeZone = request.DefaultTimeZone,
                UserImage = userimage,
                TimeZoneInterest = request.TimeZoneInterest,
                PreferredWeekdays = request.PreferredWeekdays,
                UserId = request.UserId
            }, _session.Transaction);

            return request;
        }

       public async Task<bool> UpdateUserPreferences(UserPreferences request)
        {
            byte[]? userimage = string.IsNullOrEmpty(request.UserImage)? null : Convert.FromBase64String(request.UserImage);
            var query = @"UPDATE space_admin.tuser_preferences 
              SET 
              df_building = @dfbuilding,
              df_timezone = @dftimezone,
              user_image = @UserImage,
              timezone_interest = @TimeZoneInterest,
              preferred_weekdays =@PreferredWeekdays
              WHERE user_id = @UserId";

            var result = await _session.Connection.ExecuteAsync(query, new
            {
                dfbuilding = request.BuildingId,
                dftimezone = request.DefaultTimeZone,
                UserImage = userimage,
                TimeZoneInterest = request.TimeZoneInterest,
                PreferredWeekdays =request.PreferredWeekdays,
                UserId = request.UserId
            }, _session.Transaction);

            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<UserPreferences?> GetUserUserPreferences(string userid)
        {
            var query = @"SELECT 
            user_id AS UserId,
            df_building AS BuildingId,
            df_timezone AS DefaultTimeZone,
            timezone_interest AS TimeZoneInterest,
            user_image AS UserImagebyte,
            preferred_weekdays as PreferredWeekdays
            FROM space_admin.tuser_preferences 
            WHERE user_id = @userId";
            var result = await _session.Connection.QueryAsync<UserPreferences>(query, new { userId = userid }, _session.Transaction);
            if (result.Any())
            {
                UserPreferences preferences = result.First();
                preferences.UserImage = preferences.UserImagebyte != null ? Convert.ToBase64String(preferences.UserImagebyte) : null;
            }
            return result?.FirstOrDefault();
        }


        public async Task<bool> DeleteUserPreferences(string userId)
        {

            var query = "DELETE FROM space_admin.tuser_preferences WHERE user_id = @UserId";
            var result = await _session.Connection.ExecuteAsync(query, new { UserId = userId }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
      
    }
}
