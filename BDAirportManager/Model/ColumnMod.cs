using System;
using System.Windows.Forms;

namespace BDAirportManager.Model
{
    /// <summary>
    /// Struct used to contain references to objects on form. One instance of this struct equals to one pair
    /// value-condition with same column name.
    /// </summary>
    internal class ColumnMod : IDisposable
    {
        /// <summary>
        /// Column name (in db)
        /// </summary>
        public string ColumnName;

        /// <summary>
        /// Label above textbox used for inputting values
        /// </summary>
        public Label ValueLabel;

        /// <summary>
        /// Label above textbox used for inputting conditions
        /// </summary>
        public Label WhereLabel;

        /// <summary>
        /// Textbox used to input values
        /// </summary>
        public TextBox ValueTextBox;

        /// <summary>
        /// Textbox used to input conditions
        /// </summary>
        public TextBox WhereTextBox;

        /// <summary>
        /// Panel containing ValueTextBox and ValueLabel
        /// </summary>
        public Panel ValuePanel;

        /// <summary>
        /// Panel containing WhereTextBox and WhereLabel
        /// </summary>
        public Panel WherePanel;


        public void Dispose()
        {
            ValuePanel.Controls.Remove(ValueLabel);
            WherePanel.Controls.Remove(WhereLabel);
            ValuePanel.Controls.Remove(ValueTextBox);
            WherePanel.Controls.Remove(WhereTextBox);
            

            ValueLabel?.Dispose();
            WhereLabel?.Dispose();
            ValueTextBox?.Dispose();
            WhereTextBox?.Dispose();
            ValuePanel?.Dispose();
            WherePanel?.Dispose();
        }
    }

    internal struct InfoForQuery
    {
        public string TableName;
        public string ColumnName;
        public string Value;
    }
}
