using master_piece.domain;
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

namespace master_piece.UI
{
    public partial class EditFVValue : Form
    {
        private SQLiteConnection dbConnection;
        private FuzzyVariableValue fuzzyVariableValue;
        private int fuzzyVariableId;

        public EditFVValue(SQLiteConnection arg_dbConnection, int arg_fuzzyVariableId)
        {
            dbConnection = arg_dbConnection;
            fuzzyVariableId = arg_fuzzyVariableId;
            InitializeComponent();
        }

        public EditFVValue(SQLiteConnection arg_dbConnection, int arg_fuzzyVariableId, FuzzyVariableValue arg_fuzzyVariableValue) : this(arg_dbConnection, arg_fuzzyVariableId)
        {
            if (arg_fuzzyVariableValue != null)
            {
                fuzzyVariableValue = arg_fuzzyVariableValue;
                textBox_value.Text = fuzzyVariableValue.value.ToString();
                textBox_possibility.Text = fuzzyVariableValue.possibility.ToString();
                Text = "Редактирование значения нечёткой переменной";
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            double value = 0;
            double possibility = 0;

            //Value length check
            if (textBox_value.Text.Length <= 0)
            {
                MessageBox.Show("Необходимо ввести значение нечёткой переменной", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Value format check
            //TODO: work with local data format
            try
            {
                value = Convert.ToDouble(textBox_value.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Значение нечёткой переменной введено неверно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Possibility length check
            if (textBox_possibility.Text.Length <= 0)
            {
                MessageBox.Show("Необходимо ввести вероятность значения нечёткой переменной", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Possibility format check
            //TODO: work with local data format
            try
            {
                possibility = Convert.ToDouble(textBox_possibility.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вероятность значения нечёткой переменной введена неверно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //FuzzyVariableValue could be null now
            if (fuzzyVariableValue == null)
            {
                fuzzyVariableValue = new FuzzyVariableValue()
                {
                    value = value,
                    possibility = possibility,
                    fuzzyVariableId = fuzzyVariableId
                };
            }
            else
            {
                fuzzyVariableValue.value = value;
                fuzzyVariableValue.possibility = possibility;
                //Setting fuzzyVariableId is not necessary - it is already setted up
            }

            try
            {
                //FuzzyVariableValue is already initialized now, but could have id = 0
                if (fuzzyVariableValue.id == 0)
                {
                    dbConnection.Insert(fuzzyVariableValue);
                }
                else
                {
                    dbConnection.Update(fuzzyVariableValue);
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

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
