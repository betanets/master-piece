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
            List<string> expressions = 
                GenerationService.generateExpressions(names, (int)numericUpDown_count.Value);

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
