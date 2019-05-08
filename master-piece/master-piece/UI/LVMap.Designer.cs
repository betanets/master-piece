namespace master_piece.UI
{
    partial class LVMap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chart_map = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.richTextBox_points = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart_map)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_map
            // 
            chartArea1.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chartArea1.AxisX.LineWidth = 2;
            chartArea1.AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chartArea1.AxisY.LineWidth = 2;
            chartArea1.Name = "ChartArea_map";
            this.chart_map.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_map.Legends.Add(legend1);
            this.chart_map.Location = new System.Drawing.Point(0, 0);
            this.chart_map.Name = "chart_map";
            this.chart_map.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea_map";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series1.Legend = "Legend1";
            series1.LegendText = " ";
            series1.LegendToolTip = "Карта значений";
            series1.Name = "Series_map";
            this.chart_map.Series.Add(series1);
            this.chart_map.Size = new System.Drawing.Size(988, 566);
            this.chart_map.TabIndex = 5;
            this.chart_map.Text = "Карта значений нечёткой переменной";
            title1.Name = "Title_header";
            title1.Text = "Карта значений лингвистической переменной";
            title2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            title2.Name = "Title_possibility";
            title2.Text = "Вероятность";
            title3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title3.Name = "Title_value";
            title3.Text = "Значение";
            this.chart_map.Titles.Add(title1);
            this.chart_map.Titles.Add(title2);
            this.chart_map.Titles.Add(title3);
            // 
            // richTextBox_points
            // 
            this.richTextBox_points.Location = new System.Drawing.Point(13, 573);
            this.richTextBox_points.Name = "richTextBox_points";
            this.richTextBox_points.Size = new System.Drawing.Size(964, 131);
            this.richTextBox_points.TabIndex = 6;
            this.richTextBox_points.Text = "";
            // 
            // LVMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 716);
            this.Controls.Add(this.richTextBox_points);
            this.Controls.Add(this.chart_map);
            this.Name = "LVMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Карта значений лингвистической переменной";
            ((System.ComponentModel.ISupportInitialize)(this.chart_map)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_map;
        private System.Windows.Forms.RichTextBox richTextBox_points;
    }
}