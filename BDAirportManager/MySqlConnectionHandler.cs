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
		private DataSet _dataSet = new DataSet();

		public MySqlConnection Connection { get => _connection; }
		public DataSet DataSet { get => _dataSet; }

		public ConnectionState ConnectionState { get => _connection.State; } 

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

		public DataSet LoadNewDataSet(string sqlCommand, string tableName)
		{
			if (!(_dataSet.Tables.Contains(tableName)))
			{
				//If table with that name doesn't exist, add one.
				MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlCommand, Connection);
				MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(mySqlDataAdapter);

				mySqlDataAdapter.Fill(_dataSet, tableName);
			}
			//Else just return the dataset.
			
			return _dataSet;
		}
	}
}
