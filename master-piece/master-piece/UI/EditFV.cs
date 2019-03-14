using master_piece.domain;
using SQLite;
using System.Windows.Forms;

namespace master_piece.UI
{
    public partial class EditFV : Form
    {
        private SQLiteConnection dbConnection;
        private FuzzyVariable linguisticVariable;

        public EditFV(SQLiteConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            InitializeComponent();
        }

        public EditFV(SQLiteConnection arg_dbConnection, FuzzyVariable arg_fuzzyVariable) : this(arg_dbConnection)
        {
            if (arg_fuzzyVariable != null)
            {
                linguisticVariable = arg_fuzzyVariable;
                textBox_name.Text = linguisticVariable.name;
                Text = "Редактирование нечёткой переменной";
            }
        }

        private void button_ok_Click(object sender, System.EventArgs e)
        {

        }

        private void button_cancel_Click(object sender, System.EventArgs e)
        {

        }
    }
}
