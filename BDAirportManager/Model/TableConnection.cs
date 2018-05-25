using System.Data;
using MySql.Data.MySqlClient;

namespace BDAirportManager.Model
{
    internal partial class MySqlConnectionHandler
	{
		/// <summary>
		/// Keeps tract of connection of database, by keeping references to objects
		/// </summary>
	    internal struct TableConnection
		{
			public MySqlDataAdapter DataAdapter;
			public MySql.Data.MySqlClient.MySqlCommandBuilder CommandBuilder;
			public DataTable DataTable;
		}
        
	}
}
