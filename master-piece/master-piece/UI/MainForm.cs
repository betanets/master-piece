using master_piece.domain;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace master_piece
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            var databasePath = Path.Combine(Environment.CurrentDirectory, "mp.db"); //TODO: change path later

            var db = new SQLiteConnection(databasePath);
            db.CreateTable<FuzzyVariable>();
            db.CreateTable<FuzzyVariableValue>();
            InitializeComponent();
        }
    }
}
