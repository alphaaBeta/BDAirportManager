using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BDAirportManager
{
	public partial class MainScene : Form
	{
		MySqlConnectionHandler sqlConnectionHandler = null;

		private Thread statusLabelUpdater = null;

		public MainScene()
		{
			InitializeComponent();
			sqlConnectionHandler = new MySqlConnectionHandler();

			sqlConnectionHandler.AttemptConnect("lab-bd-01.mysql.database.azure.com",
													3306,
													"sqladmin@lab-bd-01",
													"Qt314iHjg",
													"mydb");


			statusLabelUpdater = new Thread(() =>
			{
				ConnectionState tabState = ConnectionState.Closed;
				ConnectionState auxState;

				//Method to change status on the status tab
				void changeStatusBar(Color color, string text, bool disableForm)
				{
					Invoke(new Action(() =>
					{
						toolStripConnectionStatusLabel.ForeColor = color;
						toolStripConnectionStatusLabel.Text = text;
						tabControl1.Enabled = !disableForm;
					}));
				};

				while (true)
				{
					auxState = sqlConnectionHandler.ConnectionState;

					if (!(auxState == tabState))
					{
						//If current connection is other than the one on status tab, then

						if (auxState == ConnectionState.Open)
						{
							changeStatusBar(Color.Blue, "Connected", false);

							tabState = ConnectionState.Open;
						}
						else if (auxState == ConnectionState.Fetching ||
								auxState == ConnectionState.Executing ||
								auxState == ConnectionState.Connecting)
						{
							changeStatusBar(Color.DarkGreen, "Working", false);

							tabState = ConnectionState.Executing;
						}
						else if (auxState == ConnectionState.Closed)
						{
							changeStatusBar(Color.Red, "Disconnected", true);

							tabState = ConnectionState.Closed;
						}
						else
						{
							changeStatusBar(Color.DarkRed, "Error connecting to server", true);

							tabState = ConnectionState.Broken;
						}
					}

					try
					{
						Thread.Sleep(1000);
					}
					catch (ThreadAbortException)
					{
						break;
					}
				}
			});

			statusLabelUpdater.Start();

		}

		private void UpdateDataGrid(DataGridView dataGrid, MySqlCommand sqlcomm, string tableName)
		{
			dataGrid.DataSource = sqlConnectionHandler.LoadNewData(sqlcomm, tableName);

			dataGrid.Update();
			dataGrid.Refresh();
		}


		private void AirplanesRefreshButton_Click(object sender, EventArgs e)
		{
			//Create a new command object
			MySqlCommand sqlCommand = new MySqlCommand(
				"SELECT airplaneID, stored_in, model, is_refuelled FROM airplane");

			//Set int var for TryParse method
			int hangar = 0;

			//Start checking if boxes have content in them, and if they do, add appropriate param to sql command
			if (!string.IsNullOrEmpty(airplaneBox1.Text))
			{
				sqlCommand.AddWhereParameterClause("airplaneID", "@AirplaneID", airplaneBox1.Text);
			}
			if (!string.IsNullOrEmpty(airplaneBox2.Text) && Int32.TryParse(airplaneBox2.Text, out hangar))
			{
				sqlCommand.AddWhereParameterClause("stored_in", "@StoredIn", hangar);
			}
			if (!string.IsNullOrEmpty(airplaneBox3.Text))
			{
				sqlCommand.AddWhereParameterClause("model", "@Model", airplaneBox3.Text);
			}
			if (!string.IsNullOrEmpty(airplaneBox4.Text) || !string.Equals(airplaneBox4.Text, "Don't care"))
			{
				if (airplaneBox4.Text.Equals("Yes"))
					sqlCommand.AddWhereParameterClause("is_refuelled", "@Refuelled", 1);
				else if (airplaneBox4.Text.Equals("No"))
					sqlCommand.AddWhereParameterClause("is_refuelled", "@Refuelled", 0);
			}

			//Force an update with new command. This will download the data again.
			UpdateDataGrid(dataGridView1, sqlCommand, "Airplane");
		}

		private void HangarsRefreshButton_Click(object sender, EventArgs e)
		{
			// Does basically the same as airplane refresh button
			MySqlCommand sqlCommand = new MySqlCommand(
				"SELECT hangarID, location, capacity FROM hangar");

			int hangarID = 0;
			int capacity = 0;

			if (!string.IsNullOrEmpty(hangarBox1.Text) && Int32.TryParse(hangarBox1.Text, out hangarID))
			{
				sqlCommand.AddWhereParameterClause("hangarID", "@HangarID", hangarID);
			}
			if (!string.IsNullOrEmpty(hangarBox2.Text))
			{
				sqlCommand.AddWhereParameterClause("location", "@Location", hangarBox2.Text);
			}
			if (!string.IsNullOrEmpty(hangarBox3.Text) && Int32.TryParse(hangarBox3.Text, out capacity))
			{
				sqlCommand.AddWhereParameterClause("capacity", "@Capacity", capacity);
			}


			UpdateDataGrid(dataGridView2, sqlCommand, "Hangar");
		}

		private void EmployeesRefreshButton_Click(object sender, EventArgs e)
		{

			MySqlCommand sqlCommand = new MySqlCommand(
				"SELECT pesel, name, address_city, phone, address_street, address_homenumber FROM person");


			if (!string.IsNullOrEmpty(employeeBox1.Text))
			{
				sqlCommand.AddWhereParameterClause("pesel", "@Pesel", employeeBox1.Text);
			}
			if (!string.IsNullOrEmpty(employeeBox2.Text))
			{
				sqlCommand.AddWhereParameterClause("name", "@Name", employeeBox2.Text);
			}
			if (!string.IsNullOrEmpty(employeeBox3.Text))
			{
				//TODO:
			}

			UpdateDataGrid(dataGridView3, sqlCommand, "Emplloyee");
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

		private void MainScene_FormClosing(object sender, FormClosingEventArgs e)
		{
			statusLabelUpdater.Abort();
		}
	}

	public static class AuxMethods
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
