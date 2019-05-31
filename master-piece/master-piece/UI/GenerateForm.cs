using master_piece.domain;
using master_piece.service.generation;
using master_piece.service.import_export;
using SQLite;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace master_piece.UI
{
    public partial class GenerateForm : Form
    {
        private SQLiteConnection dbConnection;

        public GenerateForm(SQLiteConnection arg_dbConnection)
        {
            InitializeComponent();
            this.dbConnection = arg_dbConnection;
            initLVComboBox();
        }

        private void initLVComboBox()
        {
            comboBox_selectLV.DataSource =
                dbConnection.Query<LinguisticVariable>("select * from LinguisticVariable where deleted = '0'");
            comboBox_selectLV.DisplayMember = "name";
            comboBox_selectLV.ValueMember = "id";
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            List<string> names = new List<string>(
                textBox_names.Text.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            List<string> fuzzyVariableNames = new List<string>();

            if (numericUpDown_ifBlockCountFrom.Value > numericUpDown_ifBlockCountTo.Value)
            {
                MessageBox.Show("Неверно указан диапазон числа блоков в выражениях ЕСЛИ",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numericUpDown_thenBlockCountFrom.Value > numericUpDown_thenBlockCountTo.Value)
            {
                MessageBox.Show("Неверно указан диапазон числа блоков в выражениях ТО",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numericUpDown_elseBlockCountFrom.Value > numericUpDown_elseBlockCountTo.Value)
            {
                MessageBox.Show("Неверно указан диапазон числа блоков в выражениях ИНАЧЕ",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (names.Count == 0)
            {
                MessageBox.Show("Необходимо указать хотя бы одно имя переменной",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(comboBox_selectLV.Enabled && comboBox_selectLV.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать лингвистическую переменную или запретить их использование",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(comboBox_selectLV.Enabled && comboBox_selectLV.SelectedIndex != -1)
            {
                List<FuzzyVariable> fvs = dbConnection.Query<FuzzyVariable>("select * from LinguisticVariable lv, FuzzyVariable fv where lv.deleted = '0' and fv.deleted = '0' and fv.linguisticVariableId = ?", 
                    comboBox_selectLV.SelectedValue);
                foreach(var fv in fvs)
                {
                    fuzzyVariableNames.Add(fv.name);
                }
            }

            List<string> expressions = GenerationService.generateExpressions(
                names, (int)numericUpDown_count.Value, checkBox_allowReuse.Checked,
                (int)numericUpDown_ifBlockCountFrom.Value, (int)numericUpDown_ifBlockCountTo.Value,
                (int)numericUpDown_thenBlockCountFrom.Value, (int)numericUpDown_thenBlockCountTo.Value,
                (int)numericUpDown_elseBlockCountFrom.Value, (int)numericUpDown_elseBlockCountTo.Value,
                fuzzyVariableNames);

            ImportExportResult result = GenerationService.exportGeneratedStrings(expressions);
            switch (result.status)
            {
                case ImportExportResultStatus.Success:
                    MessageBox.Show(result.messageString, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ImportExportResultStatus.Error:
                    MessageBox.Show(result.messageString, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void CheckBox_allowReuse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_allowReuse.Checked)
            {
                checkBox_allowRandomFuzzy.Enabled = true;
            }
            else
            {
                checkBox_allowRandomFuzzy.Enabled = false;
            }
        }

        private void CheckBox_allowRandomFuzzy_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_allowRandomFuzzy.Checked)
            {
                comboBox_selectLV.Enabled = true;
            }
            else
            {
                comboBox_selectLV.Enabled = false;
            }
        }
    }
}
