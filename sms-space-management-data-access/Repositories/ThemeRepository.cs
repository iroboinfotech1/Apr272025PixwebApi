using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace sms.space.management.data.access.Repositories
{
    public class ThemeRepository : IThemeServiceRepository
    {

        private readonly DbSession _session;

        public ThemeRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<ManageTheme> Create(ManageTheme request)
        {
            var query = $@"INSERT INTO space_admin.manageTheme(themethumbnail, themename, themetype,logo,background, themedata)
                         VALUES (@themethumbnail,@themename,@themetype,@logo,@background,@themedata)
                         RETURNING id";
            request.Id = await _session.Connection.ExecuteScalarAsync<int>(query, request, _session.Transaction);
            return request;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "Delete from space_admin.manageTheme where id=@ID";
            var result = await _session.Connection.ExecuteAsync(query, new { ID = id }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<IReadOnlyList<ManageTheme>> GetAll ()
        {
            var query = "Select * from space_admin.manageTheme";
            var result = await _session.Connection.QueryAsync<ManageTheme>(query, null, _session.Transaction);
            return result.ToList();
        }

        public async Task<ManageTheme> GetById(int id)
        {
            var query = $@"Select * from space_admin.manageTheme where id=@ID";
            var result = await _session.Connection.QueryAsync<ManageTheme>(query, new { ID = id }, _session.Transaction);
            return result.FirstOrDefault();
        }

        public async Task<bool> Update(ManageTheme request)
        {
            var query = $@"UPDATE space_admin.manageTheme
                        SET themename = @themename,
                        themetype = @themetype,
                        logo = @logo,
                        background = @background,
                        themedata = @themedata
                        WHERE id=@ID";

            var result = await _session.Connection.ExecuteAsync(query, new
            {
                themename = request.themename,
                themetype = request.themetype,
                logo = request.logo,
                background = request.background,
                themedata = request.themedata,
                id =request.Id
            }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<IReadOnlyList<ThemeLanguage>> GetAllLanguages()
        {
            var query = "Select * from space_admin.manageLanguage";
            var result = await _session.Connection.QueryAsync<ThemeLanguage>(query, null, _session.Transaction);
            return result.ToList();
        }
         
        public async Task<IReadOnlyList<ThemeFont>> GetThemeFont()
        {
            var query = "select * from space_admin.utilities_master where utility_key in ('FONT_TYPES', 'LANGUAGES','FONT_SIZES')";
            var result = await _session.Connection.QueryAsync<ThemeFont>(query, null, _session.Transaction);
            return result.ToList();
        }
    }
}
