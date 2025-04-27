using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IUserManagementService
    {
        Task<IReadOnlyList<UserManagement>> GetUserManagements(string accessToken, bool isvisitor);
        Task<UserManagement> GetUserManagement(string userId);
        Task<UserManagement> CreateUserManagement(UserManagement request);
        Task<bool> UpdateUserManagement(UserManagement request);
        Task<bool> DeleteUserManagement(string userId);
 
        Task<UserPreferences> GetUserPreferences(string userId);
        Task<UserPreferences> CreateUserPreferences(UserPreferences request);
        Task<bool> UpdateUserPreferences(UserPreferences request);
        Task<bool> DeleteUserPreferences(string userId);

        Task<IReadOnlyList<Role>> GetRoleDetails();
        Task<Role> GetRoleDetail(int roleId);
        Task<Role> CreateRole(Role request);
        Task<bool> UpdateRole(Role request);
        Task<bool> DeleteRole(int roleId);

        Task<Permission> GetPermissionDetail(int permissionId);
        Task<bool> UpdatePermission(Permission permssion);
        Task<Permission> CreatePermission(Permission permssion);
        Task<bool> DeletePermission(int permissionId);
    }
}
