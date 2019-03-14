using master_piece.domain;
using master_piece.UI;
using SQLite;
using System;
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
            refreshLinguisticTable();
        }

        private void refreshLinguisticTable()
        {
            dataGridViewLV.DataSource = dbConnection.Query<LinguisticVariable>("select * from LinguisticVariable");
        }

        private void refreshFuzzyTable(LinguisticVariable lv)
        {
            if (lv != null)
            {
                dataGridViewFV.DataSource = dbConnection.Query<FuzzyVariable>("select * from FuzzyVariable where linguisticVariableId = ?", lv.id);
                toolStripFVSelectStatus.Text = "Получен список нечётких переменных для лингвистической переменной: " + lv.name;
            }
            else
            {
                MessageBox.Show("Необходимо выбрать лингвистическую переменную", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_addLV_Click(object sender, EventArgs e)
        {
            EditLV editLV = new EditLV(dbConnection);
            if(editLV.ShowDialog() == DialogResult.OK)
            {
                refreshLinguisticTable();
            }
        }

        private void button_editLV_Click(object sender, EventArgs e)
        {
            if (dataGridViewLV.SelectedRows.Count > 0)
            {
                LinguisticVariable lv = dbConnection.Get<LinguisticVariable>(dataGridViewLV.SelectedRows[0].Cells["LVId"].Value);
                EditLV editLV = new EditLV(dbConnection, lv);
                if (editLV.ShowDialog() == DialogResult.OK)
                {
                    refreshLinguisticTable();
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать лингвистическую переменную", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void getFVList_Click(object sender, EventArgs e)
        {
            if (dataGridViewLV.SelectedRows.Count > 0)
            {
                LinguisticVariable lv = dbConnection.Get<LinguisticVariable>(dataGridViewLV.SelectedRows[0].Cells["LVId"].Value);
                refreshFuzzyTable(lv);
            }
            else
            {
                MessageBox.Show("Необходимо выбрать лингвистическую переменную", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_addFV_Click(object sender, EventArgs e)
        {
            EditFV editLV = new EditFV(dbConnection);
            if (editLV.ShowDialog() == DialogResult.OK)
            {
                refreshLinguisticTable();
            }
        }
    }
}
