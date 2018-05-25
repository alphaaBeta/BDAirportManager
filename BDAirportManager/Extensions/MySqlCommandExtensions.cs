using MySql.Data.MySqlClient;

namespace BDAirportManager.Extensions
{
	/// <summary>
	/// Contains class extensions
	/// </summary>
    public static class MySqlCommandExtensions
    {
		/// <summary>
		/// Adds a 'Where' clause to sql command
		/// </summary>
		/// <param name="sqlCommand">Command to add clause to</param>
		/// <param name="columnName">Name of column it's regarding</param>
		/// <param name="parameterName">Parameter name to store it under</param>
		/// <param name="parameterValue">Value</param>
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

			try
			{
				sqlCommand.Parameters.AddWithValue(parameterName, parameterValue);
			}
			catch(System.Exception)
			{ }
        }
    }
}
