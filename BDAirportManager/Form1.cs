using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BDAirportManager.Extensions;
using BDAirportManager.Model;
using MySql.Data.MySqlClient;

namespace BDAirportManager
{
    public partial class MainScene : Form
    {
        readonly MySqlConnectionHandler _sqlConnectionHandler;

        private readonly Thread _statusLabelUpdater = null;

        public MainScene()
        {
            InitializeComponent();
            _sqlConnectionHandler = new MySqlConnectionHandler();

            _sqlConnectionHandler.AttemptConnect("lab-bd-01.mysql.database.azure.com",
                                                    3306,
                                                    "sqladmin@lab-bd-01",
                                                    "Qt314iHjg",
                                                    "mydb");


            _statusLabelUpdater = new Thread(() =>
            {
                var tabState = ConnectionState.Closed;

                //Method to change status on the status tab
                void ChangeStatusBar(Color color, string text, bool disableForm)
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
                    var auxState = _sqlConnectionHandler.ConnectionState;

                    if (auxState != tabState)
                    {
                        //If current connection is other than the one on status tab, then

                        switch (auxState)
                        {
                            case ConnectionState.Open:
                                ChangeStatusBar(Color.Blue, "Connected", false);

                                tabState = ConnectionState.Open;
                                break;
                            case ConnectionState.Fetching:
                            case ConnectionState.Executing:
                            case ConnectionState.Connecting:
                                ChangeStatusBar(Color.DarkGreen, "Working", false);

                                tabState = ConnectionState.Executing;
                                break;
                            case ConnectionState.Closed:
                                ChangeStatusBar(Color.Red, "Disconnected", true);

                                tabState = ConnectionState.Closed;
                                break;
                            default:
                                ChangeStatusBar(Color.DarkRed, "Error connecting to server", true);

                                tabState = ConnectionState.Broken;
                                break;
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

            _statusLabelUpdater.Start();

        }

        private DataTable UpdateDataGridOrGetDataTable(MySqlCommand sqlcomm, string tableName, DataGridView dataGrid = null)
        {
            if (dataGrid == null)
            {
                return _sqlConnectionHandler.LoadNewData(sqlcomm, tableName);
            }
            else
            {
                dataGrid.DataSource = _sqlConnectionHandler.LoadNewData(sqlcomm, tableName);

                dataGrid.Update();
                dataGrid.Refresh();
                return null;
            }
        }

        private void AirplanesRefreshButton_Click(object sender, EventArgs e)
        {
            //Create a new command object
            var sqlCommand = new MySqlCommand(
                "SELECT airplaneID, stored_in, model, is_refuelled FROM airplane");

            //Set int var for TryParse method

            //Start checking if boxes have content in them, and if they do, add appropriate param to sql command
            if (!string.IsNullOrEmpty(airplaneBox1.Text))
            {
                sqlCommand.AddWhereParameterClause("airplaneID", "@AirplaneID", airplaneBox1.Text);
            }
            if (!string.IsNullOrEmpty(airplaneBox2.Text) && int.TryParse(airplaneBox2.Text, out var hangar))
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
            var sqlCommand = new MySqlCommand(
                "SELECT hangarID, location, capacity FROM hangar");

            if (!string.IsNullOrEmpty(hangarBox1.Text) && int.TryParse(hangarBox1.Text, out var hangarID))
            {
                sqlCommand.AddWhereParameterClause("hangarID", "@HangarID", hangarID);
            }
            if (!string.IsNullOrEmpty(hangarBox2.Text))
            {
                sqlCommand.AddWhereParameterClause("location", "@Location", hangarBox2.Text);
            }
            if (!string.IsNullOrEmpty(hangarBox3.Text) && int.TryParse(hangarBox3.Text, out var capacity))
            {
                sqlCommand.AddWhereParameterClause("capacity", "@Capacity", capacity);
            }


            UpdateDataGridOrGetDataTable(sqlCommand, "Hangar", dataGridView2);
        }

        private void EmployeesRefreshButton_Click(object sender, EventArgs e)
        {
            string cmdText = null;

            if (pilotCheckBox.Checked && !employeeCheckBox.Checked)
            {
                cmdText = "SELECT * FROM person RIGHT JOIN pilot ON person.pesel = pilot.pesel";
            }
            else if (!pilotCheckBox.Checked && employeeCheckBox.Checked)
            {
                cmdText = "SELECT * FROM person RIGHT JOIN employee ON person.pesel = employee.pesel";
            }
            else
            {
                cmdText = "SELECT pesel, name, address_city, phone, address_street, address_homenumber FROM person";
            }

            var sqlCommand = new MySqlCommand(cmdText);

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

        private void MainScene_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Stop the status label updater thread
            _statusLabelUpdater.Abort();
        }


        private List<ColumnMod> _columnMods = new List<ColumnMod>();

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var tableName = comboBox1.Text;
            string cmdText = null;

            if (_columnMods.Any())
            {
                foreach (var item in _columnMods)
                {
                    //Remove controls
                    valuesFlowLayoutPanel.Controls.Remove(item.ValuePanel);
                    whereFlowLayoutPanel.Controls.Remove(item.WherePanel);

                    item.Dispose();
                }

                _columnMods.Clear();
            }

            if (tableName.Equals("pilot"))
            {
                cmdText = "SELECT * FROM pilot JOIN person";
            }
            else if (tableName.Equals("employee"))
            {
                cmdText = "SELECT * FROM employee JOIN person";
            }
            else
            {
                cmdText = "SELECT * FROM " + tableName;
            }

            var sqlCommand = new MySqlCommand(cmdText);

            var dataTable = UpdateDataGridOrGetDataTable(sqlCommand, tableName);
            _columnMods = new List<ColumnMod>();

            if (dataTable.Columns.Contains("pesel1"))
            {
                dataTable.Columns.RemoveAt(dataTable.Columns.IndexOf("pesel1"));
            }



            foreach (DataColumn item in dataTable.Columns)
            {
                //Create two labels, one for value, one for where section
                var valueLabel = new Label { Text = item.ColumnName };

                var whereLabel = new Label { Text = item.ColumnName };

                var valueTextBox = new TextBox { Dock = DockStyle.Bottom };
                var whereTextBox = new TextBox { Dock = DockStyle.Bottom };

                var valuePanel = new Panel();
                valuePanel.Controls.Add(valueLabel);
                valuePanel.Controls.Add(valueTextBox);

                var wherePanel = new Panel();
                wherePanel.Controls.Add(whereLabel);
                wherePanel.Controls.Add(whereTextBox);

                valuePanel.Width = 135;
                valuePanel.Height = 45;

                wherePanel.Width = 135;
                wherePanel.Height = 45;

                //Add everything together
                var columnMod = new ColumnMod
                {
                    ColumnName = item.ColumnName,
                    ValueTextBox = valueTextBox,
                    WhereTextBox = whereTextBox,

                    ValueLabel = valueLabel,
                    WhereLabel = whereLabel,

                    ValuePanel = valuePanel,
                    WherePanel = wherePanel
                };

                //Add everything to flow panels
                valuesFlowLayoutPanel.Controls.Add(valuePanel);
                whereFlowLayoutPanel.Controls.Add(wherePanel);


                _columnMods.Add(columnMod);
            }
        }

        private void OptionSelectRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!optionSelectRadioButton1.Checked) return;
            //On insert, disable the where container and enable the values container.
            groupBox9.Enabled = false;
            groupBox10.Enabled = true;

        }

        private void OptionSelectRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!optionSelectRadioButton2.Checked) return;
            //On update, enable both containers
            groupBox9.Enabled = true;
            groupBox10.Enabled = true;
        }

        private void OptionSelectRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (!optionSelectRadioButton3.Checked) return;
            //On delete, only enable the where container
            groupBox9.Enabled = true;
            groupBox10.Enabled = false;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var tableName = comboBox1.Text;
            var isEmployeeOrPilot = tableName.Equals("pilot") || tableName.Equals("employee") ? true : false;
            var queryCounter = isEmployeeOrPilot ? 2 : 1;

            var mySqlCommands = new List<MySqlCommand>();

            var cmdText = new StringBuilder();

            if (optionSelectRadioButton1.Checked == true)
            {
                var valuesList = new List<InfoForQuery>();

                //Add values to the list
                foreach (var columnMod in _columnMods)
                {
                    if(columnMod.ColumnName != "pesel")
                        valuesList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.ValueTextBox.Text,
                            ColumnName = columnMod.ColumnName
                            });
                    else
                    {
                        isEmployeeOrPilot = false;
                        valuesList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.ValueTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });

                        tableName = "person";
                        valuesList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.ValueTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });
                    }                    
                }
                //Remove nulls or empty values from list
                valuesList.RemoveAll(x => string.IsNullOrEmpty(x.Value));

                //Add queries              
                mySqlCommands.AddRange(MySqlCommandBuilder.CreateInsertStatement(valuesList));
                
                
            }
            else if (optionSelectRadioButton2.Checked)
            {
                var valuesList = new List<InfoForQuery>();
                var conditionList = new List<InfoForQuery>();

                //Add values to the list
                foreach (var columnMod in _columnMods)
                {                   
                    if (columnMod.ColumnName != "pesel")
                    {
                        valuesList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.ValueTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });
                        conditionList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.WhereTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });
                    }
                    else
                    {
                        isEmployeeOrPilot = false;
                        valuesList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.ValueTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });
                        conditionList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.WhereTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });

                        tableName = "person";
                        valuesList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.ValueTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });
                        conditionList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.WhereTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });
                    }
                }
                //Remove nulls or empty values from list
                valuesList.RemoveAll(x => string.IsNullOrEmpty(x.Value));

                //Add queries
                mySqlCommands.AddRange(MySqlCommandBuilder.CreateUpdateStatement(valuesList, conditionList));
            }
            else if (optionSelectRadioButton3.Checked)
            {
                var conditionList = new List<InfoForQuery>();

                //Add values to the list
                foreach (var columnMod in _columnMods)
                {
                    if (columnMod.ColumnName != "pesel")
                        conditionList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.WhereTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });
                    else
                    {
                        isEmployeeOrPilot = false;
                        conditionList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.WhereTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });

                        tableName = "person";
                        conditionList.Add(new InfoForQuery
                        {
                            TableName = tableName,
                            Value = columnMod.WhereTextBox.Text,
                            ColumnName = columnMod.ColumnName
                        });
                    }
                }
                //Remove nulls or empty values from list
                conditionList.RemoveAll(x => string.IsNullOrEmpty(x.Value));

                //Add queries              
                mySqlCommands.AddRange(MySqlCommandBuilder.CreateDeleteStatement(conditionList));
            }

            foreach (var mySqlCommand in mySqlCommands)
            {
                try
                {
                    _sqlConnectionHandler.PerformQuery(mySqlCommand);

                    MessageBox.Show("Query completed successfully!", "Status");
                }
                catch (MySqlException exception)
                {
                    MessageBox.Show("Error executing query!\n\n" + exception.Message, "Status");
                }

            }

        }

    }
}

