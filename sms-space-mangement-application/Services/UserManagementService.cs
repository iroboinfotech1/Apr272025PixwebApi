using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserManagementRepository _repository;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public string? clientId { get; set; }

        public string? clientSecret { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? grantType { get; set; }
        public string? scope { get; set; }
        public string? tokenEndpoint { get; set; }

        public string? BaseAddress { get; set; }

        public UserManagementService(IConfiguration configuration, IUserManagementRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
            tokenEndpoint = _configuration["Keycloak:TokenEndpoint"];
            BaseAddress = _configuration["Keycloak:BaseAddress"];
            clientId = _configuration["Keycloak:ClientId"];
            clientSecret = _configuration["Keycloak:ClientSecret"];
            username = _configuration["Keycloak:Username"];
            password = _configuration["Keycloak:Password"];
            grantType = _configuration["Keycloak:GrantType"];
            scope = _configuration["Keycloak:Scope"];
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseAddress);
        }
        public async Task<IReadOnlyList<UserManagement>> GetUserManagements(string accessToken, bool isvisitor)
        {
            if(accessToken==null)
            {
                var token = await GetBearerTokenAsync(tokenEndpoint, clientId, clientSecret, username, password, grantType, scope);
                accessToken = token;
                //isvisitor = false;
            }
            var userManagement = await _repository.GetUserManagements();
            if (isvisitor)
            {
                List<UserManagement> result1 = new List<UserManagement>();
                if (userManagement != null)
                {
                    foreach (var user in userManagement)
                    {
                        List<string> idtypelist = new List<string>();
                        List<string> idvaluelist = new List<string>();
                        if (user.idtype != null&& user.idtype.Length >0)
                        {
                            idtypelist.AddRange(user.idtype);
                        }
                        if(user.idvalue !=null && user.idvalue.Length > 0)
                        {
                            idvaluelist.AddRange(user.idvalue);
                        }
                        result1.Add(new UserManagement { Email = user.Email, mobileno = user.mobileno, FirstName = user.FirstName, LastName = user.LastName,idvalue = idvaluelist.ToArray(),idtype= idtypelist.ToArray(), UserId= user.UserId, UserName=user.UserName });
                    }
                }
                return result1;
            }
            else
            {
                List<UserKeyCloack> users = await GetusersViaRestAPI(accessToken);
                List<UserManagement> result = new List<UserManagement>();
                if (users != null && users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        result.Add(new UserManagement { Email = user.Email, UserId = user.Id, UserName = user.FirstName + " " + user.LastName });
                    }
                }
                return result;
            }
        }


        [HttpGet]
        public async Task<List<UserKeyCloack>> GetusersViaRestAPI(string accessToken)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = await _client.GetAsync("users");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<UserKeyCloack>>();
                }
                else
                {
                    return new List<UserKeyCloack>
                        {
                            new UserKeyCloack
                            {
                                ErrorInfoDesc = new ErrorInfo
                                {
                                    Message = response.StatusCode.ToString()
                                }
                            }
                        };
                }
            }
            catch (Exception ex)
            {
                return new List<UserKeyCloack>
                {
                    new UserKeyCloack
                    {
                        ErrorInfoDesc = new ErrorInfo
                        {
                            Message = ex.Message // Removed semicolon
                        }
                    }
                };
            }
        }



        [HttpGet]
        public static async Task<string> GetBearerTokenAsync(
             string tokenEndpoint,
             string clientId,
             string clientSecret,
             string username,
             string password,
             string grantType,
             string scope)
        {
            using (var httpClient = new HttpClient())
            {
                var requestData = new StringContent(
                    $"client_id={clientId}&client_secret={clientSecret}&username={username}&password={password}&grant_type={grantType}&scope={scope}",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded");

                try
                {
                    var response = await httpClient.PostAsync(tokenEndpoint, requestData);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(responseContent);
                        return tokenResponse?.AccessToken;
                    }
                    else
                    {
                        return response.StatusCode.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        public async Task<UserManagement> GetUserManagement(string userId)
        {
            var userManagement = await _repository.GetByUserId(userId);
            return userManagement;
        }
        public async Task<UserManagement> CreateUserManagement(UserManagement request)
        {
            var userManagement = await _repository.CreateUserManagement(request);
            return userManagement;
        }
        public async Task<bool> UpdateUserManagement(UserManagement request)
        {
            var isupdated = await _repository.UpdateUserManagement(request);
            return isupdated;
        }
        public async Task<bool> DeleteUserManagement(string userId)
        {
            var isdeleted = await _repository.DeleteUserManagement(userId);
            return isdeleted;
        }


        public async Task<Role> GetRoleDetail(int roleId)
        {
            var role = await _repository.GetByRoleId(roleId);
            return role;
        }
        public async Task<IReadOnlyList<Role>> GetRoleDetails()
        {
            var role = await _repository.GetRoleDetails();
            return role;
        }

        public async Task<Role> CreateRole(Role request)
        {
            var role = await _repository.CreateRole(request);
            return role;
        }
        public Task<bool> UpdateRole(Role request)
        {
            var isupdated = _repository.UpdateRole(request);
            return isupdated;
        }
        public async Task<bool> DeleteRole(int roleId)
        {
            var isdeleted = await _repository.DeleteRole(roleId);
            return isdeleted;
        }


        public async Task<Permission> GetPermissionDetail(int permissionId)
        {
            var permssion = await _repository.GetPermissionByPermissionId(permissionId);
            return permssion;
        }
        public async Task<Permission> CreatePermission(Permission permssion)
        {
            var permssiondetail = await _repository.CreatePermission(permssion);
            return permssiondetail;
        }
        public  Task<bool> UpdatePermission(Permission permssion)
        {
            var isupdated = _repository.UpdatePermission(permssion);
            return isupdated;
        }
        public async Task<bool> DeletePermission(int permissionId)
        {
            var isdeleted = await _repository.DeletePermission(permissionId);
            return isdeleted;
        }

        public async Task<UserPreferences> GetUserPreferences(string userId)
        {
            var userpreferences = await _repository.GetUserUserPreferences(userId);
            return userpreferences;
        }

        public async Task<UserPreferences> CreateUserPreferences(UserPreferences request)
        {
            var userpreferences = await _repository.CreateUserPreferences(request);
            return userpreferences;
        }

        public async Task<bool> UpdateUserPreferences(UserPreferences request)
        {
            var userpreferences = await _repository.UpdateUserPreferences(request);
            return userpreferences;
        }

        public async Task<bool> DeleteUserPreferences(string userId)
        {
            var isdeleted = await _repository.DeleteUserPreferences(userId);
            return isdeleted;
        }   
     }
}