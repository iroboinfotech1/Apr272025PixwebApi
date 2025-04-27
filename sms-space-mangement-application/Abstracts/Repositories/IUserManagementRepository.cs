using sms.space.management.domain.Entities.PlayerManagement;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IUserManagementRepository
    {
        Task<UserManagement> CreateUserManagement(UserManagement request);
        Task<bool> UpdateUserManagement(UserManagement request);
        Task<IReadOnlyList<UserManagement>> GetUserManagements();
        Task<UserManagement> GetByUserId(string userId);
        Task<bool> DeleteUserManagement(string userId);
 
 
        Task<UserPreferences> CreateUserPreferences(UserPreferences request);
         Task<bool> UpdateUserPreferences(UserPreferences request);
        Task<UserPreferences?> GetUserUserPreferences(string userid);
        Task<bool> DeleteUserPreferences(string userId);

        Task<Role> CreateRole(Role request);
        Task<bool> UpdateRole(Role request);
        Task<IReadOnlyList<Role>> GetRoleDetails();
        Task<Role> GetByRoleId(int roleId);
        Task<bool> DeleteRole(int roleId);

        Task<Permission> CreatePermission(Permission permssion);
        Task<bool> UpdatePermission(Permission permssion);
        Task<Permission> GetPermissionByPermissionId(int permissionId);
        Task<bool> DeletePermission(int permissionId);
    }
}

