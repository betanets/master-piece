using master_piece.domain;
using SQLite;
using System;
using System.Windows.Forms;

namespace master_piece.UI
{
    public partial class FVValues : Form
    {
        private int fuzzyVariableId;

        private SQLiteConnection dbConnection;

        public FVValues(SQLiteConnection arg_dbConnection, int arg_fuzzyVariableId)
        {
            dbConnection = arg_dbConnection;
            fuzzyVariableId = arg_fuzzyVariableId;
            InitializeComponent();
            refreshFVValuesTable();
        }

        private void refreshFVValuesTable()
        {
            dataGridView_FVValues.DataSource = dbConnection.Query<FuzzyVariableValue>("select * from FuzzyVariableValue where fuzzyVariableId = ? and deleted = '0'", fuzzyVariableId);
        }

        private void button_addValue_Click(object sender, System.EventArgs e)
        {
            EditFVValue editFVValue = new EditFVValue(dbConnection, fuzzyVariableId);
            if (editFVValue.ShowDialog() == DialogResult.OK)
            {
                refreshFVValuesTable();
            }
        }

        private void button_editValue_Click(object sender, System.EventArgs e)
        {
            if (dataGridView_FVValues.SelectedRows.Count > 0)
            {
                FuzzyVariableValue fvValue = dbConnection.Get<FuzzyVariableValue>(dataGridView_FVValues.SelectedRows[0].Cells["ID"].Value);
                EditFVValue editFVValue = new EditFVValue(dbConnection, fuzzyVariableId, fvValue);
                if (editFVValue.ShowDialog() == DialogResult.OK)
                {
                    refreshFVValuesTable();
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать значение нечёткой переменной", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_deleteValue_Click(object sender, System.EventArgs e)
        {
            if (dataGridView_FVValues.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить это значение нечёткой переменной?", "Подтверждение удаления",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        FuzzyVariableValue fvValue = dbConnection.Get<FuzzyVariableValue>(dataGridView_FVValues.SelectedRows[0].Cells["ID"].Value);
                        fvValue.deleted = 1;
                        dbConnection.Update(fvValue);
                        refreshFVValuesTable();
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
                MessageBox.Show("Необходимо выбрать значение нечёткой переменной", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
