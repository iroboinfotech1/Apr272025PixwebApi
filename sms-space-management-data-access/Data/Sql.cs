using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace sms.space.management.data.access;
public class Sql
{
    string _connectionString = string.Empty;
    ILogger _logger = null; 
    public Sql(IConfiguration configuration, ILogger<Sql> logger)
    {
        _logger = logger;
        _connectionString = configuration.GetConnectionString("PostgreConn");

        string connectionStingFromEnv = Environment.GetEnvironmentVariable("conectionstring"); 
        if(!String.IsNullOrEmpty(connectionStingFromEnv)){
            _connectionString =  connectionStingFromEnv;
            Console.WriteLine ("The connection string is chosen from the environment");
        }

    }

    public IDbConnection Connection()
    {
        return new NpgsqlConnection(_connectionString);
    }
    public IDataReader ExecuteReader(string sqlStatement, IDbConnection? connection, List<NpgsqlParameter> parameters = null)
    {
        NpgsqlDataReader? reader = null;
        try
        {
            if (connection == null)
            {
                connection = Connection();
            }
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, connection as NpgsqlConnection))
            {
                if (parameters != null && parameters.Count() > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());
                reader = cmd.ExecuteReader();
            }
        }
        catch (Exception oEx)
        {
            _logger.LogError(oEx.Message, oEx);
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
        return reader;
    }

    public object ExecuteScalar(string sqlStatement, IDbConnection? connection, List<NpgsqlParameter> parameters = null)
    {
        object? result = null;
        try
        {
            if (connection == null)
            {
                connection = Connection();
            }
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, connection as NpgsqlConnection))
            {
                if(parameters != null && parameters.Count() > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());
                result = cmd.ExecuteScalar();
            }
        }
        catch (Exception oEx)
        {
            _logger.LogError(oEx.Message, oEx);
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
        return result;
    }

    public int ExecuteNonQuery(string sqlStatement, IDbConnection? connection, List<NpgsqlParameter> parameters = null)
    {
        int result = -1;
        try
        {
            if (connection == null)
            {
                connection = Connection();
            }
            if(connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, connection as NpgsqlConnection))
            {
                if (parameters != null && parameters.Count() > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception oEx)
        {
            _logger.LogError(oEx.Message, oEx);
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
        return result;
    }
}
