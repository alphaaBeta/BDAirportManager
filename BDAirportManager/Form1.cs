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

		private DataTable UpdateDataGridOrGetDataTable(MySqlCommand sqlcomm, string tableName, DataGridView dataGrid = null)
		{
			if (dataGrid == null)
			{
				return sqlConnectionHandler.LoadNewData(sqlcomm, tableName);
			}
			else
			{
				dataGrid.DataSource = sqlConnectionHandler.LoadNewData(sqlcomm, tableName);

				dataGrid.Update();
				dataGrid.Refresh();
				return null;
			}
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
			UpdateDataGridOrGetDataTable(sqlCommand, "Airplane", dataGridView1);
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


			UpdateDataGridOrGetDataTable(sqlCommand, "Hangar", dataGridView2);
		}

		private void EmployeesRefreshButton_Click(object sender, EventArgs e)
		{
			string CmdText = null;
			
			if (pilotCheckBox.Checked && !employeeCheckBox.Checked)
			{
				CmdText = "SELECT * FROM person RIGHT JOIN pilot ON person.pesel = pilot.pesel";
			}
			else if (!pilotCheckBox.Checked && employeeCheckBox.Checked)
			{
				CmdText = "SELECT * FROM person RIGHT JOIN employee ON person.pesel = employee.pesel";
			}
			else
			{
				CmdText = "SELECT pesel, name, address_city, phone, address_street, address_homenumber FROM person";
			}

			MySqlCommand sqlCommand = new MySqlCommand(CmdText);

			if (!string.IsNullOrEmpty(employeeBox1.Text))
			{
				sqlCommand.AddWhereParameterClause("pesel", "@Pesel", employeeBox1.Text);
			}
			if (!string.IsNullOrEmpty(employeeBox2.Text))
			{
				sqlCommand.AddWhereParameterClause("name", "@Name", employeeBox2.Text);
			}
			

			UpdateDataGridOrGetDataTable(sqlCommand, "Employee", dataGridView3);
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

		struct ColumnMod
		{
			public string columnName;
			public Label valueLabel;
			public Label whereLabel;
			public TextBox valueTextBox;
			public TextBox whereTextBox;
			public Panel valuePanel;
			public Panel wherePanel;
		}

		private List<ColumnMod> columnMods = new List<ColumnMod>();

		private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
		{
			string tableName = comboBox1.Text;
			string CmdText = null;

			if (columnMods.Any())
			{
				foreach (var item in columnMods)
				{
					//Remove controls
					item.valuePanel.Controls.Remove(item.valueLabel);
					item.wherePanel.Controls.Remove(item.whereLabel);
					item.valuePanel.Controls.Remove(item.valueTextBox);
					item.wherePanel.Controls.Remove(item.whereTextBox);
					valuesFlowLayoutPanel.Controls.Remove(item.valuePanel);
					whereFlowLayoutPanel.Controls.Remove(item.wherePanel);

					item.valueLabel.Dispose();
					item.whereLabel.Dispose();
					item.valueTextBox.Dispose();
					item.whereTextBox.Dispose();
					item.valuePanel.Dispose();
					item.wherePanel.Dispose();
				}

				columnMods.Clear();
			}

			if (tableName.Equals("pilot"))
			{
				CmdText = "SELECT * FROM pilot JOIN person";
			}
			else if (tableName.Equals("employee"))
			{
				CmdText = "SELECT * FROM employee JOIN person";
			}
			else
			{
				CmdText = "SELECT * FROM " + tableName;
			}

			MySqlCommand sqlCommand = new MySqlCommand(CmdText);

			DataTable dataTable = UpdateDataGridOrGetDataTable(sqlCommand, tableName);
			columnMods = new List<ColumnMod>();

			foreach (DataColumn item in dataTable.Columns)
			{
				//Create two labels, one for value, one for where section
				Label valueLabel = new Label();
				valueLabel.Text = item.ColumnName;

				Label whereLabel = new Label();
				whereLabel.Text = item.ColumnName;

				TextBox valueTextBox = new TextBox();
				TextBox whereTextBox = new TextBox();

				valueTextBox.Dock = DockStyle.Bottom;
				whereTextBox.Dock = DockStyle.Bottom;

				Panel valuePanel = new Panel();
				Panel wherePanel = new Panel();

				valuePanel.Controls.Add(valueLabel);
				valuePanel.Controls.Add(valueTextBox);

				wherePanel.Controls.Add(whereLabel);
				wherePanel.Controls.Add(whereTextBox);

				valuePanel.Width = 135;
				valuePanel.Height = 45;

				wherePanel.Width = 135;
				wherePanel.Height = 45;

				ColumnMod columnMod = new ColumnMod
				{
					columnName = item.ColumnName,
					valueTextBox = valueTextBox,
					whereTextBox = whereTextBox,

					valueLabel = valueLabel,
					whereLabel = whereLabel,

					valuePanel = valuePanel,
					wherePanel = wherePanel
				};

				//Add everything to flow panels
				valuesFlowLayoutPanel.Controls.Add(valuePanel);
				whereFlowLayoutPanel.Controls.Add(wherePanel);


				columnMods.Add(columnMod);
			}
		}

		private void OptionSelectRadioButton1_CheckedChanged(object sender, EventArgs e)
		{
			if (optionSelectRadioButton1.Checked)
			{
				//On insert, disable the where container and enable the values container.
				groupBox9.Enabled = false;
				groupBox10.Enabled = true;
			}

		}

		private void OptionSelectRadioButton2_CheckedChanged(object sender, EventArgs e)
		{
			if (optionSelectRadioButton2.Checked)
			{
				//On update, enable both containers
				groupBox9.Enabled = true;
				groupBox10.Enabled = true;
			}
		}

		private void OptionSelectRadioButton3_CheckedChanged(object sender, EventArgs e)
		{
			if (optionSelectRadioButton3.Checked)
			{
				//On delete, only enable the where container
				groupBox9.Enabled = true;
				groupBox10.Enabled = false;
			}

		}

		private void Button1_Click(object sender, EventArgs e)
		{
			string tableName = comboBox1.Text;
			StringBuilder CmdText = new StringBuilder();
			MySqlCommand sqlCommand = null;

			if (tableName.Equals("pilot") || tableName.Equals("employee"))
			{
				//If a double-table has been selected, everything has to happen for each table
			}
			else
			{
				if (optionSelectRadioButton1.Checked == true)
				{
					//Handle INSERT statement
					CmdText.Append("INSERT INTO " + tableName + "(");

					//Start bulding values part
					StringBuilder ValuesString = new StringBuilder("VALUES (");

					//Add names of all columns and values
					foreach (ColumnMod column in columnMods)
					{
						if (columnMods.First().Equals(column))
						{
							CmdText.Append(column.columnName);
							ValuesString.Append("\"" + column.valueTextBox.Text + "\"");
							continue;
						}
						
						CmdText.Append("," + column.columnName);
						ValuesString.Append(",\"" + column.valueTextBox.Text + "\"");
					}

					//Add closing brace and connect two strings together
					CmdText.Append(") ");
					ValuesString.Append(");");

					CmdText.Append(ValuesString.ToString());
				}
				else if (optionSelectRadioButton2.Checked)
				{
					//Handle UPDATE statement
				}
				else if (optionSelectRadioButton3.Checked)
				{
					//Handle DELETE statement
				}
				else
				{ 
					//can't happen 
				}

				//Once command is ready, build the MySqlCommand object
				sqlCommand = new MySqlCommand(CmdText.ToString());
				sqlConnectionHandler.PerformQuery(sqlCommand);
			}

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
