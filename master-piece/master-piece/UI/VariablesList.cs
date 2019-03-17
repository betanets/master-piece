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
        private LinguisticVariable selectedLinguisticVariable;

        public VariablesList(SQLiteConnection arg_dbConnection)
        {
            dbConnection = arg_dbConnection;
            InitializeComponent();
            refreshLinguisticTable();
        }

        private void refreshLinguisticTable()
        {
            dataGridViewLV.DataSource = dbConnection.Query<LinguisticVariable>("select * from LinguisticVariable where deleted = '0'");
        }

        private void refreshFuzzyTable(LinguisticVariable lv)
        {
            if (lv != null)
            {
                dataGridViewFV.DataSource = dbConnection.Query<FuzzyVariable>("select * from FuzzyVariable where linguisticVariableId = ? and deleted = '0'", lv.id);
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

        private void button_deleteLV_Click(object sender, EventArgs e)
        {
            if (dataGridViewLV.SelectedRows.Count > 0)
            {
                if(MessageBox.Show("Вы действительно хотите удалить эту лингвистическую переменную?", "Подтверждение удаления", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LinguisticVariable lv = dbConnection.Get<LinguisticVariable>(dataGridViewLV.SelectedRows[0].Cells["LVId"].Value);
                        lv.deleted = 1;
                        dbConnection.Update(lv);
                        refreshLinguisticTable();
                    }
                    catch (Exception ex)
                    {
                        //TODO: make custom exception window with error, stacktrace, etc
                        //Exception may occur when dbConnection is null
                        MessageBox.Show("Произошла ошибка при работе с базой данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                selectedLinguisticVariable = dbConnection.Get<LinguisticVariable>(dataGridViewLV.SelectedRows[0].Cells["LVId"].Value);
                refreshFuzzyTable(selectedLinguisticVariable);
            }
            else
            {
                MessageBox.Show("Необходимо выбрать лингвистическую переменную", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_addFV_Click(object sender, EventArgs e)
        {
            if (selectedLinguisticVariable != null)
            {
                EditFV editLV = new EditFV(dbConnection, selectedLinguisticVariable.id);
                if (editLV.ShowDialog() == DialogResult.OK)
                {
                    refreshFuzzyTable(selectedLinguisticVariable);
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать лингвистическую переменную", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_editFV_Click(object sender, EventArgs e)
        {
            if (selectedLinguisticVariable != null)
            {
                if (dataGridViewFV.SelectedRows.Count > 0)
                {
                    FuzzyVariable fv = dbConnection.Get<FuzzyVariable>(dataGridViewFV.SelectedRows[0].Cells["FVId"].Value);
                    EditFV editFV = new EditFV(dbConnection, selectedLinguisticVariable.id, fv);
                    if (editFV.ShowDialog() == DialogResult.OK)
                    {
                        refreshFuzzyTable(selectedLinguisticVariable);
                    }
                }
                else
                {
                    MessageBox.Show("Необходимо выбрать нечёткую переменную", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать лингвистическую переменную", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_deleteFV_Click(object sender, EventArgs e)
        {
            if (dataGridViewFV.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту нечёткую переменную?", "Подтверждение удаления",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        FuzzyVariable fv = dbConnection.Get<FuzzyVariable>(dataGridViewFV.SelectedRows[0].Cells["FVId"].Value);
                        fv.deleted = 1;
                        dbConnection.Update(fv);
                        refreshFuzzyTable(selectedLinguisticVariable);
                    }
                    catch (Exception ex)
                    {
                        //TODO: make custom exception window with error, stacktrace, etc
                        //Exception may occur when dbConnection is null
                        MessageBox.Show("Произошла ошибка при работе с базой данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать нечёткую переменную", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_setupFV_Click(object sender, EventArgs e)
        {
            if (dataGridViewFV.SelectedRows.Count > 0)
            {
                FVValues fVValues = new FVValues(dbConnection, (int)dataGridViewFV.SelectedRows[0].Cells["FVId"].Value);
                fVValues.ShowDialog();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать нечёткую переменную", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
