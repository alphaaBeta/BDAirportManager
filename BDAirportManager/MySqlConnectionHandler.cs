using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAirportManager
{
    internal class MySqlConnectionHandler
	{
	    private readonly List<TableConnection> _tables = new List<TableConnection>();

		public MySqlConnection Connection { get; private set; } = new MySqlConnection();

	    public ConnectionState ConnectionState => Connection.State;

	    private struct TableConnection
		{
			public MySqlDataAdapter DataAdapter;
			public MySqlCommandBuilder CommandBuilder;
			public DataTable DataTable;
		}

		public bool AttemptConnect(string hostname, int port, string login, string password, string database)
		{
			string connectionString = "server = " + hostname
										+ "; user = " + login
										+ "; database = " + database
										+ "; port = " + port.ToString()
										+ "; password = " + password;

			var newConnection = new MySqlConnection(connectionString);

			try
			{
				newConnection.Open();
			}
			catch (Exception)
			{
				//Connection unsuccessful
				return false;
			}

			//Connection estabilished successfully
			Connection = newConnection;

			return true;
		}

		public object PerformQuery(MySqlCommand sqlCommand)
		{
			sqlCommand.Connection = Connection;
			return(sqlCommand.ExecuteScalar());
		}

		public DataTable LoadNewData(MySqlCommand sqlCommand, string tableName)
		{
			//If a table alredy exists, return the data inside
			if (_tables.Any(x => (x.DataTable.TableName == tableName)) == true)
			{
				var aux = _tables.Find(x => (x.DataTable.TableName == tableName));

				sqlCommand.Connection = Connection;
				aux.DataAdapter.SelectCommand = sqlCommand;

				aux.DataTable.Clear();

				aux.DataAdapter.Fill(aux.DataTable);
				return aux.DataTable;
			}
			
			sqlCommand.Connection = Connection;
			var mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
			var commandBuilder = new MySqlCommandBuilder(mySqlDataAdapter);
			var data = new DataTable();


			mySqlDataAdapter.Fill(data);
			data.TableName = tableName;

			var tableConnection = new TableConnection
			{
				DataAdapter = mySqlDataAdapter,
				CommandBuilder = commandBuilder,
				DataTable = data
			};

			_tables.Add(tableConnection);

			return data;
		}
        
	}
}
