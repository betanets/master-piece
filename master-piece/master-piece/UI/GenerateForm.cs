using master_piece.service.generation;
using master_piece.service.import_export;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace master_piece.UI
{
    public partial class GenerateForm : Form
    {
        public GenerateForm()
        {
            InitializeComponent();
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            List<string> names = new List<string>(
                textBox_names.Text.Split(new char[] { ' ', '\t', '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries));

            if (numericUpDown_blockCountFrom.Value > numericUpDown_blockCountTo.Value)
            {
                MessageBox.Show("Неверно указан диапазон числа блоков в выражении",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (names.Count == 0)
            {
                MessageBox.Show("Необходимо указать хотя бы одно имя переменной", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> expressions = GenerationService.generateExpressions(
                names, (int)numericUpDown_count.Value, checkBox_allowReuse.Checked,
                (int)numericUpDown_blockCountFrom.Value, (int)numericUpDown_blockCountTo.Value);

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
    }
}
