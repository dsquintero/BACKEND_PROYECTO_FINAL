using API.Core.Interfaces;
using API.Models.Entities;
using API.Models.Requests;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace API.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly EnvironmentConfig env;

        public AuthenticationRepository(EnvironmentConfig env)
        {
            this.env = env;
        }

        public async Task<User> GetByUserCode(string username)
        {
            var query = "SELECT userid, first_name, last_name, password, username FROM [User] WHERE username = @username";
            var parameters = new DynamicParameters();
            parameters.Add("username", username, DbType.String);

            using (var connection = new SqlConnection(env.MSSQL_CONNECTION_STRING))
            {
                connection.Open();

                var result = await connection.QueryFirstAsync<User>(query, parameters);

                connection.Close();
                return result;
            }
        }
        public async Task<int> Register(UserRequest user)
        {
            var query = "INSERT INTO [User] (first_name, last_name, password, username) " +
                "VALUES( @first_name, @last_name, @password, @username)";
            var parameters = new DynamicParameters();
            parameters.Add("first_name", user.First_name, DbType.String);
            parameters.Add("last_name", user.Last_name, DbType.String);
            parameters.Add("password", user.Password, DbType.String);
            parameters.Add("username", user.Username, DbType.String);

            using (var connection = new SqlConnection(env.MSSQL_CONNECTION_STRING))
            {
                connection.Open();

                var result = await connection.ExecuteAsync(query, parameters);

                connection.Close();
                return result;
            }
        }

    }
}
