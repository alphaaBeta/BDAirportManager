using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAirportManager
{
	class MySqlConnectionHandler
	{
		private MySqlConnection _connection = new MySqlConnection();
		private List<TableConnection> tables = new List<TableConnection>();

		public MySqlConnection Connection { get => _connection; }
		public ConnectionState ConnectionState { get => _connection.State; }
		
		struct TableConnection
		{
			public MySqlDataAdapter dataAdapter;
			public MySqlCommandBuilder commandBuilder;
			public DataTable dataTable;
		}

		public bool AttemptConnect(string hostname, int port, string login, string password, string database)
		{
			string connectionString = "server = " + hostname
										+ "; user = " + login
										+ "; database = " + database
										+ "; port = " + port.ToString()
										+ "; password = " + password;

			MySqlConnection newConnection = new MySqlConnection(connectionString);

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
			_connection = newConnection;

			return true;
		}

		public DataTable LoadNewData(MySqlCommand sqlCommand, string tableName)
		{
			//If a table alredy exists, return the data inside
			if (tables.Any(x => (x.dataTable.TableName == tableName)) == true)
			{
				TableConnection aux = tables.Find(x => (x.dataTable.TableName == tableName));

				sqlCommand.Connection = Connection;
				aux.dataAdapter.SelectCommand = sqlCommand;

				aux.dataTable.Clear();

				aux.dataAdapter.Fill(aux.dataTable);
				return aux.dataTable;
			}
			
			sqlCommand.Connection = Connection;
			MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
			MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(mySqlDataAdapter);
			DataTable data = new DataTable();


			mySqlDataAdapter.Fill(data);
			data.TableName = tableName;

			TableConnection tableConnection = new TableConnection
			{
				dataAdapter = mySqlDataAdapter,
				commandBuilder = commandBuilder,
				dataTable = data
			};

			tables.Add(tableConnection);

			return data;
		}

		public void UpdateData(DataTable data)
		{

		}
	}
}
