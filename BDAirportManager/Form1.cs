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
		MySqlConnectionHandler sqlConnectionHandler;

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

		private void airplanesRefreshButton_Click(object sender, EventArgs e)
		{
			string sqlcomm = "SELECT airplaneID, stored_in, model, is_refuelled FROM airplane";


			dataGridView1.DataSource = sqlConnectionHandler.LoadNewDataSet(sqlcomm, "Airplanes");
			dataGridView1.DataMember = "Airplanes";
		}

		private void hangarsRefreshButton_Click(object sender, EventArgs e)
		{
			string sqlcomm = "SELECT hangarID, location, capacity FROM hangar";

			dataGridView2.DataSource = sqlConnectionHandler.LoadNewDataSet(sqlcomm, "Hangars");
			dataGridView2.DataMember = "Hangars";
		}

		private void employeesRefreshButton_Click(object sender, EventArgs e)
		{
			string sqlcomm = "SELECT pesel, name, address_city, phone, address_street, address_homenumber FROM person";

			dataGridView3.DataSource = sqlConnectionHandler.LoadNewDataSet(sqlcomm, "Employees");
			dataGridView3.DataMember = "Employees";
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
}
