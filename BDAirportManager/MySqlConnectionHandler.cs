using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAirportManager
{
	/// <summary>
	/// Class used to handle and manage connection to database
	/// </summary>
    internal partial class MySqlConnectionHandler
	{
	    private readonly List<Model.MySqlConnectionHandler.TableConnection> _tables = new List<Model.MySqlConnectionHandler.TableConnection>();

	    private MySqlConnection Connection { get; set; } = new MySqlConnection();

	    public ConnectionState ConnectionState => Connection.State;

		/// <summary>
		/// Attempt to connect to a database with given params
		/// </summary>
		/// <param name="hostname">Address of database server</param>
		/// <param name="port">Database port</param>
		/// <param name="login">Admin login</param>
		/// <param name="password">Admin password</param>
		/// <param name="database">Database name</param>
		/// <returns></returns>
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

		/// <summary>
		/// Sends query to database to perform it
		/// </summary>
		/// <param name="sqlCommand">Query to perform</param>
		/// <returns></returns>
		public object PerformQuery(MySqlCommand sqlCommand) 
		{
			sqlCommand.Connection = Connection;
			return(sqlCommand.ExecuteScalar());
		}

		/// <summary>
		/// Performs command and creates a datatable
		/// </summary>
		/// <param name="sqlCommand">Sql command to perform</param>
		/// <param name="tableName">Tablename to store under in cache</param>
		/// <returns></returns>
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
			var commandBuilder = new MySql.Data.MySqlClient.MySqlCommandBuilder();
			var data = new DataTable();


			mySqlDataAdapter.Fill(data);
			data.TableName = tableName;

			var tableConnection = new Model.MySqlConnectionHandler.TableConnection
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
