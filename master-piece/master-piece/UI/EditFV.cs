using master_piece.domain;
using SQLite;
using System;
using System.Windows.Forms;

namespace master_piece.UI
{
    public partial class EditFV : Form
    {
        private SQLiteConnection dbConnection;
        private FuzzyVariable fuzzyVariable;
        private int linguisticVariableId;

        public EditFV(SQLiteConnection arg_dbConnection, int arg_linguisticVariableId)
        {
            dbConnection = arg_dbConnection;
            linguisticVariableId = arg_linguisticVariableId;
            InitializeComponent();
        }

        public EditFV(SQLiteConnection arg_dbConnection, int arg_linguisticVariableId, FuzzyVariable arg_fuzzyVariable) : this(arg_dbConnection, arg_linguisticVariableId)
        {
            if (arg_fuzzyVariable != null)
            {
                fuzzyVariable = arg_fuzzyVariable;
                textBox_name.Text = fuzzyVariable.name;
                Text = "Редактирование нечёткой переменной";
            }
        }

        private void button_ok_Click(object sender, System.EventArgs e)
        {
            if (textBox_name.Text.Length > 0)
            {
                //FuzzyVariable could be null now
                if (fuzzyVariable == null)
                {
                    fuzzyVariable = new FuzzyVariable()
                    {
                        name = textBox_name.Text,
                        linguisticVariableId = linguisticVariableId
                    };
                }
                else
                {
                    fuzzyVariable.name = textBox_name.Text;
                    //Setting linguisticVariableId is not necessary - it is already setted up
                }

                try
                {
                    //FuzzyVariable is already initialized now, but could have id = 0
                    if (fuzzyVariable.id == 0)
                    {
                        dbConnection.Insert(fuzzyVariable);
                    }
                    else
                    {
                        dbConnection.Update(fuzzyVariable);
                    }
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    //Exception may occur when dbConnection is null
                    MessageBox.Show("Произошла ошибка при работе с базой данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Abort;
                }
                finally
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Необходимо ввести название нечёткой переменной", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_cancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
