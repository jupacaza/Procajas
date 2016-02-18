using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercializadora.Repository
{
    public class SqlServerRepository : IRepository
    {
        private string connectionString;

        public SqlServerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<bool> Persist<T>(T entity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    string insertStatement;
                    command.CommandText = insertStatement;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch(System.Data.Common.DbException e)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<T> Query<T>(IDictionary<string, string> filter = null)
        {
            T entity;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM {1} WHERE", connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {

                    }
                }
            }

            return entity;
        }
    }
}
