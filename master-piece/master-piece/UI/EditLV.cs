using master_piece.domain;
using SQLite;
using System;
using System.Windows.Forms;

namespace master_piece.UI
{
    public partial class EditLV : Form
    {
        private SQLiteConnection dbConnection;
        private LinguisticVariable linguisticVariable;

        public EditLV(SQLiteConnection arg_dbConnection)
        {
            dbConnection = arg_dbConnection;
            InitializeComponent();
        }

        public EditLV(SQLiteConnection arg_dbConnection, LinguisticVariable arg_linguisticVariable) : this(arg_dbConnection)
        {
            if(arg_linguisticVariable != null)
            {
                linguisticVariable = arg_linguisticVariable;
                textBox_name.Text = linguisticVariable.name;
                Text = "Редактирование лингвистической переменной";
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (textBox_name.Text.Length > 0)
            {
                //LinguisticVariable could be null now
                if (linguisticVariable == null)
                {
                    linguisticVariable = new LinguisticVariable()
                    {
                        name = textBox_name.Text
                    };
                }
                else
                {
                    linguisticVariable.name = textBox_name.Text;
                }

                try
                {
                    //LinguisticVariable is already initialized now, but could have id = 0
                    if (linguisticVariable.id == 0)
                    {
                        dbConnection.Insert(linguisticVariable);
                    }
                    else
                    {
                        dbConnection.Update(linguisticVariable);
                    }
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    //TODO: make custom exception window with error, stacktrace, etc
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
                MessageBox.Show("Необходимо ввести название лингвистической переменной", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
