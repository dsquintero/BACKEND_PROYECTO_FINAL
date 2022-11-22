using API.Core.Interfaces;
using API.Models.Entities;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace API.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly EnvironmentConfig env;

        public RefreshTokenRepository(EnvironmentConfig env)
        {
            this.env = env;
        }

        public async Task<int> Create(RefreshToken refreshToken)
        {
            var query = "INSERT INTO Auth_RefreshToken(Id,Token,UserCode) " +
                "VALUES(@Id, @Token, @UserCode)";
            var parameters = new DynamicParameters();
            parameters.Add("Id", Guid.NewGuid().ToString(), DbType.String);
            parameters.Add("Token", refreshToken.Token, DbType.String);
            parameters.Add("UserCode", refreshToken.username, DbType.String);

            using (var connection = new SqlConnection(env.MSSQL_CONNECTION_STRING))
            {
                connection.Open();

                var result = await connection.ExecuteAsync(query, parameters);

                connection.Close();
                return result;
            }

        }

        public async Task<int> Delete(string username)
        {
            var query = "DELETE FROM Auth_RefreshToken WHERE UserCode = @username";

            var parameters = new DynamicParameters();
            parameters.Add("username", username, DbType.String);

            using (var connection = new SqlConnection(env.MSSQL_CONNECTION_STRING))
            {
                connection.Open();

                var result = await connection.ExecuteAsync(query, parameters);

                connection.Close();
                return result;
            }
        }

        public async Task<RefreshToken> GetByToken(string token)
        {

            var query = "SELECT Id,Token,UserCode AS username FROM Auth_RefreshToken WHERE Token = @Token";

            var parameters = new DynamicParameters();
            parameters.Add("Token", token, DbType.String);

            using (var connection = new SqlConnection(env.MSSQL_CONNECTION_STRING))
            {
                connection.Open();

                var result = await connection.QueryFirstOrDefaultAsync<RefreshToken>(query, parameters);

                connection.Close();
                return result;
            }
        }
    }
}
