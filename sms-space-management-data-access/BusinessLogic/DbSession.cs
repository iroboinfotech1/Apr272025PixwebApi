using Microsoft.Extensions.Options;
using Npgsql;
using sms.space.management.data.access.Settings;
using System.Data;

namespace sms.space.management.data.access.BusinessLogic
{
    public sealed class DbSession : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; } = null!;

        public DbSession(IOptions<ConnectionStringSettings> connectionStrings)
        {
            var connString = connectionStrings.Value.PostgreConn;

            string connectionStingFromEnv = Environment.GetEnvironmentVariable("conectionstring"); 
            if(!String.IsNullOrEmpty(connectionStingFromEnv)){
                connString =  connectionStingFromEnv;
                Console.WriteLine ("The connection string is chosen from the environment");
            }

            _id = Guid.NewGuid();

            Connection = new NpgsqlConnection(connString);
            Connection.Open();
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public void Dispose() => Connection?.Dispose();
    }
}
