using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BDAirportManager
{
	public partial class MainScene : Form
	{
		MySqlConnection _connection;

		public MainScene()
		{
			InitializeComponent();
			string connectionString = "server = lab-bd-01.mysql.database.azure.com; user = sqladmin@lab-bd-01; database = mydb; port= 3306; password = Qt314iHjg";
			_connection = new MySqlConnection(connectionString);
			_connection.Open();
		}

		private void airplanesRefreshButton_Click(object sender, EventArgs e)
		{
			string sqlcomm = "SELECT airplaneID, stored_in, model, is_refuelled FROM airplane";
			MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlcomm, _connection);

			MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(mySqlDataAdapter);

			DataSet dataSet = new DataSet();

			mySqlDataAdapter.Fill(dataSet, "Airplane");

			dataGridView1.DataSource = dataSet;
			dataGridView1.DataMember = "Airplane";
		}

		private void hangarsRefreshButton_Click(object sender, EventArgs e)
		{
			string sqlcomm = "SELECT hangarID, location, capacity FROM hangar";
			MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlcomm, _connection);

			MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(mySqlDataAdapter);

			DataSet dataSet = new DataSet();

			mySqlDataAdapter.Fill(dataSet, "Hangar");

			dataGridView2.DataSource = dataSet;
			dataGridView2.DataMember = "Hangar";
		}

		private void employeesRefreshButton_Click(object sender, EventArgs e)
		{
			string sqlcomm = "SELECT pesel, name, address_city, phone, address_street, address_homenumber FROM person";
			MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlcomm, _connection);

			MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(mySqlDataAdapter);

			DataSet dataSet = new DataSet();

			mySqlDataAdapter.Fill(dataSet, "Person");

			dataGridView3.DataSource = dataSet;
			dataGridView3.DataMember = "Person";
		}

		private void airplanesUpdateButton_Click(object sender, EventArgs e)
		{

		}

		private void hangarsUpdateButton_Click(object sender, EventArgs e)
		{

		}

		private void employeesUpdateButton_Click(object sender, EventArgs e)
		{

		}
	}
}
