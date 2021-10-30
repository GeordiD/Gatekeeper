using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ThetaGangTracker.Utilities;

namespace ThetaGangTracker.Repos
{
    public interface ILoginRepo
    {
        Task CreateLogin(string username, string password);
        Task<bool> CheckLogin(string username, string password);
    }

    public class LoginRepo : ILoginRepo
    {
        private IDbClient _db;

        public LoginRepo(IDbClient db)
        {
            _db = db;
        }

        public async Task CreateLogin(string username, string password)
        {
            await _db.ExecuteAsync(@"
                INSERT INTO login (username, password)
                VALUES (@username, crypt(@password, gen_salt('bf')))
            ", new { username, password });
        }

        public async Task<bool> CheckLogin(string username, string password)
        {
            var result = await _db.QueryAsync<string>(@"
                SELECT password = crypt(@password, password)
                FROM login
                WHERE username = @username
                
            ", new { username, password });

            return result.Any() &&
                bool.Parse(result.FirstOrDefault());
        }
    }
}