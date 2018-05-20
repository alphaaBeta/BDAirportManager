using System.Data;
using MySql.Data.MySqlClient;

namespace BDAirportManager.Model
{
    internal partial class MySqlConnectionHandler
	{
	    internal struct TableConnection
		{
			public MySqlDataAdapter DataAdapter;
			public MySql.Data.MySqlClient.MySqlCommandBuilder CommandBuilder;
			public DataTable DataTable;
		}
        
	}
}
