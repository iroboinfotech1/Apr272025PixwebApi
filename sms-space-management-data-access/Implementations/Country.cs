using Microsoft.Extensions.Logging;
using Npgsql;
using Entites = sms.space.management.domain.Entities.Organization;
using sms.space.management.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sms.space.management.domain.Entities.Organization;
using System.Diagnostics.Metrics;

namespace sms.space.management.data.access.Implementations
{
    public class Country : ICountry
    {
        private readonly Sql _sql ;
        private readonly ILogger<Country> _logger;
        public Country(Sql sql, ILogger<Country> logger)
        {
            _sql = sql; 
            _logger = logger;
        }

        public Entites.Country Add(Entites.Country country)
        {
            string addStatement = "INSERT INTO countries (name, sortname, phonecode) VALUES (@name,@sortname,@PhoneCode) RETURNING id";
            object result;
            using (IDbConnection connection = _sql.Connection())
            {
                try
                {
                    List<NpgsqlParameter> parameters = new()
                    {
                        new NpgsqlParameter("@name", country.Name),
                        new NpgsqlParameter("@sortname", country.ShortName),
                        new NpgsqlParameter("@PhoneCode", country.PhoneCode)
                    };
                    result = _sql.ExecuteScalar(addStatement, connection, parameters);
                    country.ID = (int)result;
                }
                catch (Exception oEx)
                {
                    _logger.LogError(oEx.Message, oEx);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                finally
                {
                    connection.Dispose();
                }
            }
            return country;
        }

        public bool Delete(int ID)
        {
            string sqlStatement = "DELETE FROM countries WHERE ID = @ID";
            object? result = null;
            using (IDbConnection connection = _sql.Connection())
            {
                try
                {
                    List<NpgsqlParameter> parameters = new()
                    {
                        new NpgsqlParameter("@ID", ID),
                    };
                    result = _sql.ExecuteScalar(sqlStatement, connection, parameters);
                }
                catch (Exception oEx)
                {
                    _logger.LogError(oEx.Message, oEx);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                finally
                {
                    connection.Dispose();
                }
            }

            return result != null;
        }

        public Entites.Country? Get(int ID, bool includeSates = false)
        {
            string sqlStatement = @"SELECT id, sortname, name, phonecode FROM public.countries WHERE ID = @ID;";
            List<NpgsqlParameter> parameters = new()
            {
                new NpgsqlParameter("@ID", ID)
            };
            return Execute(sqlStatement, parameters, includeSates).SingleOrDefault();
        }

        public IEnumerable<Entites.Country> List(bool includeSates = false)
        {
            List<Country> countries = new();
            string sqlStatement = @"SELECT id, sortname, name, phonecode FROM countries;";
            return Execute(sqlStatement, includeSates);
        }

        public Entites.Country Update(int ID, Entites.Country country)
        {
            string addStatement = "UPDATE COUNTRY SET name = @name, shortname=@ShortName, phonecode = @PhoneCode WHERE id=@ID";
            
            using (IDbConnection connection = _sql.Connection())
            {
                try
                {
                    List<NpgsqlParameter> parameters = new()
                    {
                        new NpgsqlParameter("@ID",ID),
                        new NpgsqlParameter("@name", country.Name),
                        new NpgsqlParameter("@ShortName", country.ShortName),
                        new NpgsqlParameter("@PhoneCode", country.PhoneCode)
                    };
                    int result = _sql.ExecuteNonQuery(addStatement, connection, parameters);
                    if(!(result > 0))
                    {
                        throw new Exception($"Operation Failed for Country {country.ID}");
                    }
                }
                catch (Exception oEx)
                {
                    _logger.LogError(oEx.Message, oEx);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                finally
                {
                    connection.Dispose();
                }
            }
            return country;
        }

        private List<Entites.Country> Execute(string sqlStatement, bool includeSates = false)
        {
            return Execute(sqlStatement, null, includeSates);
        }
        private List<Entites.Country> Execute(string sqlStatement, List<NpgsqlParameter>? parameters = null, bool includeSates = false )
        {
            List<Entites.Country> countries = new();
            using (IDbConnection connection = _sql.Connection())
            {
                try
                {
                    using (IDataReader reader = _sql.ExecuteReader(sqlStatement, connection, parameters))
                    {
                        while (reader.Read())
                        {
                            countries.Add(NewInstance(reader, includeSates));
                        }
                    }
                }
                catch (Exception oEx)
                {
                    _logger.LogError(oEx.Message, oEx);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                finally
                {
                    connection.Dispose();
                }
            }
            return countries;
        }

        private Entites.Country NewInstance(IDataReader reader, bool includeSates = false)
        {
            Entites.Country country = new ()
            {
                ID = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                ShortName = reader.GetString(reader.GetOrdinal("sortname")),
                PhoneCode = reader.GetInt32(reader.GetOrdinal("phonecode")),
            };

            //if (includeSates)
            //    country.States = _states.List(country);

            return country;
        }
    }
}
