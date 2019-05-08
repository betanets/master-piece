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

            //First and last fuzzy variable points
            List<Tuple<double, double>> points = new List<Tuple<double, double>>();
            double? minValue = null, maxValue = null;

            List<FuzzyVariable> fuzzyVariables = dbConnection.Query<FuzzyVariable>("select * from FuzzyVariable where linguisticVariableId = ? and deleted = '0'", linguisticVariableId);
            foreach(FuzzyVariable fv in fuzzyVariables) {
                chart_map.Series.Add(new Series());
                chart_map.Series[i].ChartType = SeriesChartType.Area;
                chart_map.Series[i].IsValueShownAsLabel = true;
                chart_map.Series[i].Name = fv.name;

                chart_map.Series[i].Color = Color.FromArgb(128, random.Next() % 255, random.Next() % 255, random.Next() % 255);

                //TODO: Possibly there could be a better way than selection with ORDER BY statement
                List <FuzzyVariableValue> fuzzyVariableValues = dbConnection.Query<FuzzyVariableValue>("select * from FuzzyVariableValue where fuzzyVariableId = ? and deleted = '0' order by value", fv.id);
                foreach(FuzzyVariableValue fvv in fuzzyVariableValues)
                {
                    chart_map.Series[i].Points.AddXY(fvv.value, fvv.possibility);
                }

                int count = fuzzyVariableValues.Count;
                if(count > 0)
                {
                    if (i == 0)
                    {
                        minValue = fuzzyVariableValues[0].value;
                    }
                    if (i == fuzzyVariables.Count - 1)
                    {
                        maxValue = fuzzyVariableValues[count - 1].value;
                    }

                    //Adding first 2 points
                    if (i != 0 && count >= 2)
                    {
                        if (fuzzyVariableValues[0].possibility != 0)
                        {
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[0].value, 0));
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[0].value, fuzzyVariableValues[0].possibility));
                        }
                        else
                        {
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[0].value, 0));
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[1].value, fuzzyVariableValues[1].possibility));
                        }
                    }

                    //Adding last 2 points
                    if (i != fuzzyVariables.Count - 1 && count >= 2)
                    {
                        if (fuzzyVariableValues[count - 1].possibility != 0)
                        {
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[count - 1].value, fuzzyVariableValues[count - 1].possibility));
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[count - 1].value, 0));
                        }
                        else
                        {
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[count - 2].value, fuzzyVariableValues[count - 2].possibility));
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[count - 1].value, 0));
                        }
                    }
                }
                

                i++;
            }

            foreach (var item in points)
            {
                richTextBox_points.AppendText("(" + item.Item1 + "; " + item.Item2 + ") ");
            }

            List<double> intersections = new List<double>();

            //Counting X coordinate of intersection point
            for (int indx = 0; indx < points.Count; indx += 4)
            {
                double x1, x2, x3, x4, y1, y2, y3, y4;
                try
                {
                    x1 = points[indx].Item1;
                    x2 = points[indx + 1].Item1;
                    x3 = points[indx + 2].Item1;
                    x4 = points[indx + 3].Item1;

                    y1 = points[indx].Item2;
                    y2 = points[indx + 1].Item2;
                    y3 = points[indx + 2].Item2;
                    y4 = points[indx + 3].Item2;
                }
                catch (Exception)
                {
                    //TODO: throw error message into RichTextBox?
                    break;
                }
                intersections.Add(
                    ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) 
                    / 
                    ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4))
                );
            }

            richTextBox_points.AppendText("\n");
            if (minValue.HasValue && maxValue.HasValue)
            {
                richTextBox_points.AppendText("\n" + minValue.Value + "; " + maxValue.Value + "\n");
            }

            foreach(double isc in intersections)
            {
                richTextBox_points.AppendText(isc + "; ");
            }

            //Creating ranges
            if (!minValue.HasValue || !maxValue.HasValue)
            {
                return;
            }

            List<Tuple<double, double>> ranges = new List<Tuple<double, double>>();
            for (int indx = 0; indx < intersections.Count; indx++)
            {
                if (indx == 0)
                {
                    ranges.Add(new Tuple<double, double>(minValue.Value, intersections[indx]));
                }
                else if (indx == intersections.Count - 1)
                {
                    ranges.Add(new Tuple<double, double>(intersections[indx - 1], intersections[indx]));
                    ranges.Add(new Tuple<double, double>(intersections[indx], maxValue.Value));
                }
                else
                {
                    ranges.Add(new Tuple<double, double>(intersections[indx - 1], intersections[indx]));
                }
            }
            richTextBox_points.AppendText("\n");
            foreach (var r in ranges)
            {
                richTextBox_points.AppendText("(" + r.Item1 + "; " + r.Item2 + ") ");
            }
        }
    }
}
