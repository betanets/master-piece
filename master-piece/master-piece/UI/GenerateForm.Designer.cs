namespace master_piece.UI
{
    partial class GenerateForm
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
            this.label_count = new System.Windows.Forms.Label();
            this.numericUpDown_count = new System.Windows.Forms.NumericUpDown();
            this.label_names = new System.Windows.Forms.Label();
            this.textBox_names = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.checkBox_allowReuse = new System.Windows.Forms.CheckBox();
            this.groupBox_blockCount = new System.Windows.Forms.GroupBox();
            this.label_blockCountFrom = new System.Windows.Forms.Label();
            this.numericUpDown_blockCountFrom = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_blockCountTo = new System.Windows.Forms.NumericUpDown();
            this.label_blockCountTo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_count)).BeginInit();
            this.groupBox_blockCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_blockCountFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_blockCountTo)).BeginInit();
            this.SuspendLayout();
            // 
            // label_count
            // 
            this.label_count.AutoSize = true;
            this.label_count.Location = new System.Drawing.Point(13, 13);
            this.label_count.Name = "label_count";
            this.label_count.Size = new System.Drawing.Size(127, 13);
            this.label_count.TabIndex = 0;
            this.label_count.Text = "Количество выражений";
            // 
            // numericUpDown_count
            // 
            this.numericUpDown_count.Location = new System.Drawing.Point(163, 11);
            this.numericUpDown_count.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericUpDown_count.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_count.Name = "numericUpDown_count";
            this.numericUpDown_count.Size = new System.Drawing.Size(150, 20);
            this.numericUpDown_count.TabIndex = 1;
            this.numericUpDown_count.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label_names
            // 
            this.label_names.AutoSize = true;
            this.label_names.Location = new System.Drawing.Point(13, 108);
            this.label_names.Name = "label_names";
            this.label_names.Size = new System.Drawing.Size(107, 13);
            this.label_names.TabIndex = 2;
            this.label_names.Text = "Имена переменных";
            // 
            // textBox_names
            // 
            this.textBox_names.Location = new System.Drawing.Point(16, 124);
            this.textBox_names.Multiline = true;
            this.textBox_names.Name = "textBox_names";
            this.textBox_names.Size = new System.Drawing.Size(297, 138);
            this.textBox_names.TabIndex = 3;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(157, 292);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 4;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.Button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(238, 292);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // checkBox_allowReuse
            // 
            this.checkBox_allowReuse.AutoSize = true;
            this.checkBox_allowReuse.Location = new System.Drawing.Point(16, 269);
            this.checkBox_allowReuse.Name = "checkBox_allowReuse";
            this.checkBox_allowReuse.Size = new System.Drawing.Size(298, 17);
            this.checkBox_allowReuse.TabIndex = 6;
            this.checkBox_allowReuse.Text = "Использовать случайные переменные в выражениях";
            this.checkBox_allowReuse.UseVisualStyleBackColor = true;
            // 
            // groupBox_blockCount
            // 
            this.groupBox_blockCount.Controls.Add(this.numericUpDown_blockCountTo);
            this.groupBox_blockCount.Controls.Add(this.label_blockCountTo);
            this.groupBox_blockCount.Controls.Add(this.numericUpDown_blockCountFrom);
            this.groupBox_blockCount.Controls.Add(this.label_blockCountFrom);
            this.groupBox_blockCount.Location = new System.Drawing.Point(16, 37);
            this.groupBox_blockCount.Name = "groupBox_blockCount";
            this.groupBox_blockCount.Size = new System.Drawing.Size(297, 57);
            this.groupBox_blockCount.TabIndex = 7;
            this.groupBox_blockCount.TabStop = false;
            this.groupBox_blockCount.Text = "Количество блоков в выражениях";
            // 
            // label_blockCountFrom
            // 
            this.label_blockCountFrom.AutoSize = true;
            this.label_blockCountFrom.Location = new System.Drawing.Point(6, 27);
            this.label_blockCountFrom.Name = "label_blockCountFrom";
            this.label_blockCountFrom.Size = new System.Drawing.Size(18, 13);
            this.label_blockCountFrom.TabIndex = 0;
            this.label_blockCountFrom.Text = "от";
            // 
            // numericUpDown_blockCountFrom
            // 
            this.numericUpDown_blockCountFrom.Location = new System.Drawing.Point(31, 25);
            this.numericUpDown_blockCountFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_blockCountFrom.Name = "numericUpDown_blockCountFrom";
            this.numericUpDown_blockCountFrom.Size = new System.Drawing.Size(107, 20);
            this.numericUpDown_blockCountFrom.TabIndex = 1;
            this.numericUpDown_blockCountFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_blockCountTo
            // 
            this.numericUpDown_blockCountTo.Location = new System.Drawing.Point(184, 25);
            this.numericUpDown_blockCountTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_blockCountTo.Name = "numericUpDown_blockCountTo";
            this.numericUpDown_blockCountTo.Size = new System.Drawing.Size(107, 20);
            this.numericUpDown_blockCountTo.TabIndex = 3;
            this.numericUpDown_blockCountTo.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label_blockCountTo
            // 
            this.label_blockCountTo.AutoSize = true;
            this.label_blockCountTo.Location = new System.Drawing.Point(159, 27);
            this.label_blockCountTo.Name = "label_blockCountTo";
            this.label_blockCountTo.Size = new System.Drawing.Size(19, 13);
            this.label_blockCountTo.TabIndex = 2;
            this.label_blockCountTo.Text = "до";
            // 
            // GenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 325);
            this.Controls.Add(this.groupBox_blockCount);
            this.Controls.Add(this.checkBox_allowReuse);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_names);
            this.Controls.Add(this.label_names);
            this.Controls.Add(this.numericUpDown_count);
            this.Controls.Add(this.label_count);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GenerateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Генератор выражений";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_count)).EndInit();
            this.groupBox_blockCount.ResumeLayout(false);
            this.groupBox_blockCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_blockCountFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_blockCountTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_count;
        private System.Windows.Forms.NumericUpDown numericUpDown_count;
        private System.Windows.Forms.Label label_names;
        private System.Windows.Forms.TextBox textBox_names;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.CheckBox checkBox_allowReuse;
        private System.Windows.Forms.GroupBox groupBox_blockCount;
        private System.Windows.Forms.NumericUpDown numericUpDown_blockCountTo;
        private System.Windows.Forms.Label label_blockCountTo;
        private System.Windows.Forms.NumericUpDown numericUpDown_blockCountFrom;
        private System.Windows.Forms.Label label_blockCountFrom;
    }
}