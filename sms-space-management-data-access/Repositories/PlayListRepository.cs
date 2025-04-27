using Dapper;
using Npgsql;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.ContentManagement;
using sms.space.management.domain.Entities.PlayerManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace sms.space.management.data.access.Repositories
{
    public class PlayListRepository : IPlayListRepository
    {
        private readonly DbSession _session;
        private readonly Sql _sql;
        public PlayListRepository(DbSession session, Sql sql)
        {
            _session = session;
            _sql = sql;
        }

        public async Task<PlayList> Create(PlayList entity)
        {
            var query = $@"INSERT INTO  space_admin.play_list (play_list_name,media_name,thumbnail,duration_type,play_duration ,volume)
						VALUES (@PlayListName,@MediaName,@Thumbnail,@DurationType,@PlayDuration,@Volume)
						RETURNING id				
            ";
            //RETURNING id


            entity.id = await _session.Connection.ExecuteScalarAsync<int>(query, entity, _session.Transaction);
            return entity;
        }
        public async Task<bool> CreateMultiple(List<PlayList> entities)
        {
            var query = $@"INSERT INTO  space_admin.play_list (play_list_name,media_name,thumbnail,duration_type,play_duration ,volume,media_type)
						VALUES (@PlayListName,@MediaName,@Thumbnail,@DurationType,@PlayDuration,@Volume,@MediaType)
						RETURNING id				
            ";
            //RETURNING id
            using (IDbConnection connection = _sql.Connection())
            {
                try
                {
                    foreach (PlayList entity in entities) {
                        List<NpgsqlParameter> parameters = new()
                        {
                            new NpgsqlParameter("@PlayListName",entity.PlayListName),
                            new NpgsqlParameter("@MediaName", entity.MediaName),
                            new NpgsqlParameter("@Thumbnail",entity.Thumbnail),
                            new NpgsqlParameter("@DurationType", entity.DurationType),
                            new NpgsqlParameter("@PlayDuration", entity.PlayDuration),
                            new NpgsqlParameter("@Volume", entity.Volume),
                            new NpgsqlParameter("@MediaType", entity.MediaType)
                        };
                        int result = _sql.ExecuteNonQuery(query, connection, parameters);
                        if (!(result > 0))
                        {
                            throw new Exception($"Operation Failed for PlayList item {entity.PlayListName}");
                        }
                    }
                }
                catch (Exception oEx)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                finally
                {
                    connection.Dispose();
                }
            }
            return true;
        }

        public async Task<bool> Delete(string playListName)
        {
            var query = "Delete from space_admin.play_list where  play_list_name=@playListName";
            var result = await _session.Connection.ExecuteAsync(query, new { playListName = playListName }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> DeletePlayListItem(int id)
        {
            var query = "Delete from space_admin.play_list where  id=@id";
            var result = await _session.Connection.ExecuteAsync(query, new { id = id }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<IReadOnlyList<PlayList>> GetAll()
        {
            var query = "select min(id) id,play_list_name,min(thumbnail) thumbnail from space_admin.play_list group by play_list_name";
            var result = await _session.Connection.QueryAsync<PlayList>(query, null, _session.Transaction);
            return result.ToList();
        }
         
        public async Task<PlayList> GetById(int id)
        {
            var query = $@"Select * from space_admin.play_list where id==@id";
            var result = await _session.Connection.QueryAsync<PlayList>(query, new { id = id }, _session.Transaction);
            return result.FirstOrDefault();
        }

        public async Task<IReadOnlyList<PlayList>> GetByPlayListName(string playListName)
        {
            var query = $@"select * from space_admin.play_list where  play_list_name=@playListName";
            var result = await _session.Connection.QueryAsync<PlayList>(query, new { playListName = playListName }, _session.Transaction);
            return result.ToList();
        }

        public async Task<bool> Update(List<PlayList> request)
        {
            bool updated = true;
            if (request != null && request.Count > 0) {

                foreach (PlayList record in request) {
                    var query = $@"UPDATE space_admin.play_list
                        SET
                        duration_type = @DurationType,
                        play_duration = @PlayDuration,
                        volume = @Volume
                        WHERE id = @id";
                    var result = await _session.Connection.ExecuteAsync(query, new
                    {
                        id = record.id,
                        DurationType = record.DurationType,
                        PlayDuration = record.PlayDuration,
                        Volume = record.Volume,

                    }, _session.Transaction);
                    if (result > 0)
                    {
                        updated = true;
                    }
                }
            }
            return updated;
        }
    }
}
