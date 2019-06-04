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
            this.cartesianChart_map = new LiveCharts.WinForms.CartesianChart();
            this.SuspendLayout();
            // 
            // cartesianChart_map
            // 
            this.cartesianChart_map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChart_map.Location = new System.Drawing.Point(0, 0);
            this.cartesianChart_map.Name = "cartesianChart_map";
            this.cartesianChart_map.Size = new System.Drawing.Size(989, 568);
            this.cartesianChart_map.TabIndex = 6;
            this.cartesianChart_map.Text = "cartesianChart_map";
            // 
            // LVMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 568);
            this.Controls.Add(this.cartesianChart_map);
            this.Name = "LVMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Графическое представление лингвистической переменной";
            this.ResumeLayout(false);

        }

        #endregion
        private LiveCharts.WinForms.CartesianChart cartesianChart_map;
    }
}