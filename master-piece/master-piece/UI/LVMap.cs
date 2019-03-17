using master_piece.domain;
using SQLite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace master_piece.UI
{
    public partial class LVMap : Form
    {
        private SQLiteConnection dbConnection;
        private int linguisticVariableId;

        public LVMap(SQLiteConnection arg_dbConnection, int arg_linguisticVariableId)
        {
            dbConnection = arg_dbConnection;
            linguisticVariableId = arg_linguisticVariableId;
            InitializeComponent();
            loadMap();
        }

        //TODO: Possibly lagging on highload. It'll be better to add FuzzyVariableValue entities into FuzzyVariable data class
        private void loadMap()
        {
            int i = 0;
            Random random = new Random();
            chart_map.Series.Clear();

            List<FuzzyVariable> fuzzyVariables = dbConnection.Query<FuzzyVariable>("select * from FuzzyVariable where linguisticVariableId = ? and deleted = '0'", linguisticVariableId);
            foreach(FuzzyVariable fv in fuzzyVariables) {
                chart_map.Series.Add(new Series());
                chart_map.Series[i].ChartType = SeriesChartType.Area;
                chart_map.Series[i].IsValueShownAsLabel = true;

                chart_map.Series[i].Color = Color.FromArgb(128, random.Next() % 255, random.Next() % 255, random.Next() % 255);

                //TODO: Possibly there could be a better way than selection with ORDER BY statement
                List <FuzzyVariableValue> fuzzyVariableValues = dbConnection.Query<FuzzyVariableValue>("select * from FuzzyVariableValue where fuzzyVariableId = ? and deleted = '0' order by value", fv.id);
                foreach(FuzzyVariableValue fvv in fuzzyVariableValues)
                {
                    chart_map.Series[i].Points.AddXY(fvv.value, fvv.possibility);
                }
                i++;
            }
        }
    }
}
