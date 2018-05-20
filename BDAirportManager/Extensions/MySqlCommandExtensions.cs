using MySql.Data.MySqlClient;

namespace BDAirportManager.Extensions
{
    public static class MySqlCommandExtensions
    {
        public static void AddWhereParameterClause(this MySqlCommand sqlCommand, string columnName, string parameterName, object parameterValue)
        {
            if (sqlCommand.CommandText.Contains(" WHERE "))
            {
                sqlCommand.CommandText = sqlCommand.CommandText + " AND (" + columnName + "=" + parameterName + ")";
            }
            else
            {
                sqlCommand.CommandText = sqlCommand.CommandText + " WHERE (" + columnName + "=" + parameterName + ")";
            }

            sqlCommand.Parameters.AddWithValue(parameterName, parameterValue);
        }
    }
}
