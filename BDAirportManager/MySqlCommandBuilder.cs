using System.Collections.Generic;
using System.Linq;
using System.Text;
using BDAirportManager.Extensions;
using BDAirportManager.Model;
using MySql.Data.MySqlClient;

namespace BDAirportManager
{
    static class MySqlCommandBuilder
    {
        public static IEnumerable<MySqlCommand> CreateInsertStatement(List<InfoForQuery> valueList)
        {
            //Get tableName
            var tableNames = valueList.Select(x => x.TableName)
                            .ToList()
                            .Distinct()
                            .ToList();

            var returnedList = new List<MySqlCommand>();

            foreach (var tableName in tableNames)
            {
                //Get values of the specific tables only
                var valueListOfSpecificTable = valueList.Where(x => x.TableName == tableName).ToList();

                //Create SQL command
                var cmdText = new StringBuilder("INSERT INTO " + tableName + " (");


                //Add column names to query
                var columnNames = valueListOfSpecificTable.Select(x => x.ColumnName).ToArray();
                //Separate them with ,
                cmdText.Append(string.Join(",", columnNames));


                //Add values
                cmdText.Append(") VALUES(");                

                //Create an array of values
                var values = valueListOfSpecificTable.Select(x => x.Value).ToArray();
                //Add " surrounding to each value
                values = values.Select(x => "\"" + x + "\"").ToArray();
                //Separate values with , and add a closing bracket
                cmdText.Append(string.Join(",", values) + ")");              


                returnedList.Add(new MySqlCommand(cmdText.ToString()));
            }

            return returnedList;

        }

        public static IEnumerable<MySqlCommand> CreateUpdateStatement(List<InfoForQuery> valueList, List<InfoForQuery> conditionList)
        {
            //Get list of tables
            var tableNames = valueList.Select(x => x.TableName)
                            .ToList()
                            .Distinct()
                            .ToList();

            var returnedList = new List<MySqlCommand>();
			var peselHappened = false;

            var cmdText = new StringBuilder("UPDATE " + string.Join(",", tableNames.ToArray()) + " SET ");

            //Create a list of assignments
            var valueChanges = new List<string>();
            foreach (var infoForQuery in valueList)
            {
                valueChanges.Add(infoForQuery.TableName + "." + infoForQuery.ColumnName + " = \"" + infoForQuery.Value + "\"");
            }

            //Add assignments with , as separator
            cmdText.Append(string.Join(",", valueChanges.ToArray()));

            var mySqlCommand = new MySqlCommand(cmdText.ToString());

            //Add where clauses
            foreach (var infoForQuery in conditionList)
            {
                mySqlCommand.AddWhereParameterClause(infoForQuery.TableName + "." + infoForQuery.ColumnName, "@" + infoForQuery.ColumnName.ToUpper(), infoForQuery.Value);
            }

            returnedList.Add(mySqlCommand);
            return returnedList;



        }

        public static IEnumerable<MySqlCommand> CreateDeleteStatement(List<InfoForQuery> conditionList)
        {
            //Get list of tables
            var tableNames = conditionList.Select(x => x.TableName)
                        .ToList()
                        .Distinct()
                        .ToList();
            var mySqlCommands = new List<MySqlCommand>();

            

			foreach (var tableName in tableNames)
			{
				var cmdText = new StringBuilder("DELETE ");

				//Add it to cmd
				cmdText.Append("FROM ");

				//Add table name to cmd
				cmdText.Append(tableName + " ");

				var mySqlCommand = new MySqlCommand(cmdText.ToString());

				foreach (var infoForQuery in conditionList.Where(x=>x.TableName == tableName))
				{
					mySqlCommand.AddWhereParameterClause(infoForQuery.ColumnName, "@" + infoForQuery.ColumnName.ToUpper(), infoForQuery.Value);
				}

				mySqlCommands.Add(mySqlCommand);
			}
			
            return mySqlCommands;

        }
    }
}
