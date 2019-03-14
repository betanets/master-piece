using master_piece.domain;
using master_piece.UI;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace master_piece
{
    public partial class VariablesList : Form
    {
        private SQLiteConnection dbConnection;

        public VariablesList(SQLiteConnection arg_dbConnection)
        {
            dbConnection = arg_dbConnection;
            InitializeComponent();
            refreshTables();
        }

        private void refreshTables()
        {
            dataGridViewLV.DataSource = dbConnection.Query<LinguisticVariable>("select * from linguisticVariable");

            //TODO: init second dataGridView
        }

        private void button_addLV_Click(object sender, EventArgs e)
        {
            EditLV editLV = new EditLV(dbConnection);
            if(editLV.ShowDialog() == DialogResult.OK)
            {
                refreshTables();
            }
        }

        private void button_editLV_Click(object sender, EventArgs e)
        {
            LinguisticVariable lv = dbConnection.Get<LinguisticVariable>(dataGridViewLV.SelectedRows[0].Cells["LVId"].Value);
            EditLV editLV = new EditLV(dbConnection, lv);
            if (editLV.ShowDialog() == DialogResult.OK)
            {
                refreshTables();
            }
        }
    }
}
