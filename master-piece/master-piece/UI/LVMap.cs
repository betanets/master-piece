using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using master_piece.domain;
using SQLite;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media;

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

        private void loadMap()
        {
            Axis axisX = new Axis();
            axisX.Title = "Значение";
            cartesianChart_map.AxisX.Add(axisX);

            Axis axisY = new Axis();
            axisY.MinValue = 0;
            axisY.MaxValue = 1;
            axisY.Title = "Вероятность";
            cartesianChart_map.AxisY.Add(axisY);

            cartesianChart_map.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));

            List<FuzzyVariable> fuzzyVariables = dbConnection.Query<FuzzyVariable>("select * from FuzzyVariable where linguisticVariableId = ? and deleted = '0'", linguisticVariableId);
            foreach(FuzzyVariable fv in fuzzyVariables) {
                List <FuzzyVariableValue> fuzzyVariableValues = dbConnection.Query<FuzzyVariableValue>("select * from FuzzyVariableValue where fuzzyVariableId = ? and deleted = '0' order by value", fv.id);

                LineSeries series = new LineSeries
                {
                    Title = fv.name,
                    Values = new ChartValues<ObservablePoint>(),
                    LineSmoothness = 0
                };

                foreach (FuzzyVariableValue fvv in fuzzyVariableValues)
                {
                    series.Values.Add(new ObservablePoint(fvv.value, fvv.possibility));
                }
                cartesianChart_map.Series.Add(series);
            }
        }
    }
}
