using Dapper;
using Npgsql;

namespace DAL.Methods
{
    public class CommonDAL
    {
        public CommonDAL()
        {

        }
        public async Task<IEnumerable<T>> GetAllDataFromDbByFunNameAndClientName<T>(string funName, int projectId)
        {
            string query = $"SELECT * FROM {funName}('{projectId}')";
            try
            {
                using (var connection = new NpgsqlConnection(""))
                {
                    connection.Open();
                    var projectWiseParameters = await connection.QueryAsync<T>(query, null);

                    return projectWiseParameters;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
