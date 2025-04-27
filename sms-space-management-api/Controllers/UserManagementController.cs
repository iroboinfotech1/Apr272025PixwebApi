using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities.PlayerManagement;
using sms.space.management.domain.Entities.UserManagement;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {

        private readonly IUserManagementService _service;
        public UserManagementController(IUserManagementService service)
        {
            this._service = service;
        }

        #region == User management ==

        // GET: api/UserManagement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserManagement>>> GetUserManagement(string accessToken, bool isvistor)
        {
            var response = await _service.GetUserManagements(accessToken, isvistor);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/GetUserManagement/admin
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserManagement>> GetUserManagement(string userId)
        {
            var response = await _service.GetUserManagement(userId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/PutUserManagement
        [HttpPut]
        public async Task<IActionResult> PutUserManagement(UserManagement userManagement)
        {
            if (string.IsNullOrEmpty(userManagement.UserId))
            {
                return BadRequest();
            }
            var response = await _service.UpdateUserManagement(userManagement);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }

        // POST: api/PostUserManagement
        [HttpPost]
        public async Task<ActionResult<UserManagement>> PostUserManagement(UserManagement userManagement)
        {
            var response = await _service.CreateUserManagement(userManagement);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/DeleteUserManagement/userId
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserManagement(string userId)
        {
            var response = await _service.DeleteUserManagement(userId);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        #endregion

        [HttpPost]
        [Route("GetUserPreferences")]
        public async Task<ActionResult<UserPreferences>> GetUserPreferences(string  userid)
        {
            var response = await _service.GetUserPreferences(userid);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }
       [HttpPost("CreateUserPreferences")]
        public async Task<ActionResult<UserPreferences>> CreateUserPreferences(UserPreferences request)
        {
            var userpreferences = await _service.CreateUserPreferences(request);
            return userpreferences;
        }
        [HttpPut("UpdateUserPreferences")]
        public async Task<bool> UpdateUserPreferences(UserPreferences request)
        {
            var userpreferences = await _service.UpdateUserPreferences(request);
            return userpreferences;
        }
        [HttpPost("DeleteUserPreferences")]
        public async Task<bool> DeleteUserPreferences(string userId)
        {
            var isdeleted = await _service.DeleteUserPreferences(userId);
            return isdeleted;
        }

        #region == Role  ==

        // GET: api/GetRoleDetails
        [HttpGet]
       [Route("Role")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoleDetails()
        {
            var response = await _service.GetRoleDetails();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/GetRoleDetail/roleName
        [HttpGet("Role/{roleId}")]

        public async Task<ActionResult<Role>> GetRoleDetail(int roleId)
        {
            var response = await _service.GetRoleDetail(roleId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

       // PUT: api/PutRole
        [HttpPut]
        [Route("Role")]
        public async Task<IActionResult> PutRole(Role role)
        {
            if (string.IsNullOrEmpty(role.RoleName))
            {
                return BadRequest();
            }
            var response = await _service.UpdateRole(role);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }

        // POST: api/PostRole
        [HttpPost]
        [Route("Role")]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            var response = await _service.CreateRole(role);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/DeleteRole/roleName
        [HttpDelete("Role/{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            var response = await _service.DeleteRole(roleId);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        #endregion

        #region == Permission  ==


        // GET: api/GetPermissionDetail/admin
        [HttpGet("Permssion/{permissionId}")]
        public async Task<ActionResult<Permission>> GetPermissionDetail(int permissionId)
        {
            var response = await _service.GetPermissionDetail(permissionId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/PutPermission
        [HttpPut]
        [Route("Permssion")]
        public async Task<IActionResult> PutPermission(Permission permssion)
        {
            var response = await _service.UpdatePermission(permssion);

            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }

        // POST: api/PostPermission
        [HttpPost]
        [Route("Permssion")]
        public async Task<ActionResult<Permission>> PostPermission(Permission permssion)
        {
            var response = await _service.CreatePermission(permssion);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/DeletePermission/permissionId
        [HttpDelete("Permssion/{permissionId}")]
        public async Task<IActionResult> DeletePermission(int permissionId)
        {
            var response = await _service.DeletePermission(permissionId);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        #endregion


    }
}
