using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ThetaGangTracker.Utilities
{
    public interface IDbClient
    {
        Task ExecuteAsync(string sql, object parameters = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
    }

    public class DbClient : IDbClient
    {
        private IConfiguration _config;

        public DbClient(IConfiguration config)
        {
            _config = config;
        }

        public async Task ExecuteAsync(string sql, object parameters = null)
        {
            using (var connection = new NpgsqlConnection(GetConnectionString()))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            using (var connection = new NpgsqlConnection(GetConnectionString()))
            {
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }

        private string GetConnectionString()
        {
            return _config.GetValue<string>("ConnectionStrings:DefaultConnection");
        }
    }
}