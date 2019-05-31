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
            this.groupBox_ifBlockCount = new System.Windows.Forms.GroupBox();
            this.numericUpDown_ifBlockCountTo = new System.Windows.Forms.NumericUpDown();
            this.label_ifBlockCountTo = new System.Windows.Forms.Label();
            this.numericUpDown_ifBlockCountFrom = new System.Windows.Forms.NumericUpDown();
            this.label_ifBlockCountFrom = new System.Windows.Forms.Label();
            this.groupBox_thenBlockCount = new System.Windows.Forms.GroupBox();
            this.numericUpDown_thenBlockCountTo = new System.Windows.Forms.NumericUpDown();
            this.label_thenBlockCountTo = new System.Windows.Forms.Label();
            this.numericUpDown_thenBlockCountFrom = new System.Windows.Forms.NumericUpDown();
            this.label_thenBlockCountFrom = new System.Windows.Forms.Label();
            this.groupBox_elseBlockCount = new System.Windows.Forms.GroupBox();
            this.numericUpDown_elseBlockCountTo = new System.Windows.Forms.NumericUpDown();
            this.label_elseBlockCountTo = new System.Windows.Forms.Label();
            this.numericUpDown_elseBlockCountFrom = new System.Windows.Forms.NumericUpDown();
            this.label_elseBlockCountFrom = new System.Windows.Forms.Label();
            this.checkBox_allowRandomFuzzy = new System.Windows.Forms.CheckBox();
            this.comboBox_selectLV = new System.Windows.Forms.ComboBox();
            this.label_selectLV = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_count)).BeginInit();
            this.groupBox_ifBlockCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ifBlockCountTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ifBlockCountFrom)).BeginInit();
            this.groupBox_thenBlockCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_thenBlockCountTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_thenBlockCountFrom)).BeginInit();
            this.groupBox_elseBlockCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_elseBlockCountTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_elseBlockCountFrom)).BeginInit();
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
            this.label_names.Location = new System.Drawing.Point(13, 233);
            this.label_names.Name = "label_names";
            this.label_names.Size = new System.Drawing.Size(107, 13);
            this.label_names.TabIndex = 2;
            this.label_names.Text = "Имена переменных";
            // 
            // textBox_names
            // 
            this.textBox_names.Location = new System.Drawing.Point(16, 249);
            this.textBox_names.Multiline = true;
            this.textBox_names.Name = "textBox_names";
            this.textBox_names.Size = new System.Drawing.Size(297, 99);
            this.textBox_names.TabIndex = 3;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(158, 477);
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
            this.button_cancel.Location = new System.Drawing.Point(239, 477);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // checkBox_allowReuse
            // 
            this.checkBox_allowReuse.AutoSize = true;
            this.checkBox_allowReuse.Location = new System.Drawing.Point(16, 355);
            this.checkBox_allowReuse.Name = "checkBox_allowReuse";
            this.checkBox_allowReuse.Size = new System.Drawing.Size(298, 17);
            this.checkBox_allowReuse.TabIndex = 6;
            this.checkBox_allowReuse.Text = "Использовать случайные переменные в выражениях";
            this.checkBox_allowReuse.UseVisualStyleBackColor = true;
            this.checkBox_allowReuse.CheckedChanged += new System.EventHandler(this.CheckBox_allowReuse_CheckedChanged);
            // 
            // groupBox_ifBlockCount
            // 
            this.groupBox_ifBlockCount.Controls.Add(this.numericUpDown_ifBlockCountTo);
            this.groupBox_ifBlockCount.Controls.Add(this.label_ifBlockCountTo);
            this.groupBox_ifBlockCount.Controls.Add(this.numericUpDown_ifBlockCountFrom);
            this.groupBox_ifBlockCount.Controls.Add(this.label_ifBlockCountFrom);
            this.groupBox_ifBlockCount.Location = new System.Drawing.Point(16, 37);
            this.groupBox_ifBlockCount.Name = "groupBox_ifBlockCount";
            this.groupBox_ifBlockCount.Size = new System.Drawing.Size(297, 57);
            this.groupBox_ifBlockCount.TabIndex = 7;
            this.groupBox_ifBlockCount.TabStop = false;
            this.groupBox_ifBlockCount.Text = "Количество блоков в выражениях ЕСЛИ";
            // 
            // numericUpDown_ifBlockCountTo
            // 
            this.numericUpDown_ifBlockCountTo.Location = new System.Drawing.Point(184, 25);
            this.numericUpDown_ifBlockCountTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_ifBlockCountTo.Name = "numericUpDown_ifBlockCountTo";
            this.numericUpDown_ifBlockCountTo.Size = new System.Drawing.Size(107, 20);
            this.numericUpDown_ifBlockCountTo.TabIndex = 3;
            this.numericUpDown_ifBlockCountTo.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label_ifBlockCountTo
            // 
            this.label_ifBlockCountTo.AutoSize = true;
            this.label_ifBlockCountTo.Location = new System.Drawing.Point(159, 27);
            this.label_ifBlockCountTo.Name = "label_ifBlockCountTo";
            this.label_ifBlockCountTo.Size = new System.Drawing.Size(19, 13);
            this.label_ifBlockCountTo.TabIndex = 2;
            this.label_ifBlockCountTo.Text = "до";
            // 
            // numericUpDown_ifBlockCountFrom
            // 
            this.numericUpDown_ifBlockCountFrom.Location = new System.Drawing.Point(31, 25);
            this.numericUpDown_ifBlockCountFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_ifBlockCountFrom.Name = "numericUpDown_ifBlockCountFrom";
            this.numericUpDown_ifBlockCountFrom.Size = new System.Drawing.Size(107, 20);
            this.numericUpDown_ifBlockCountFrom.TabIndex = 1;
            this.numericUpDown_ifBlockCountFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label_ifBlockCountFrom
            // 
            this.label_ifBlockCountFrom.AutoSize = true;
            this.label_ifBlockCountFrom.Location = new System.Drawing.Point(6, 27);
            this.label_ifBlockCountFrom.Name = "label_ifBlockCountFrom";
            this.label_ifBlockCountFrom.Size = new System.Drawing.Size(18, 13);
            this.label_ifBlockCountFrom.TabIndex = 0;
            this.label_ifBlockCountFrom.Text = "от";
            // 
            // groupBox_thenBlockCount
            // 
            this.groupBox_thenBlockCount.Controls.Add(this.numericUpDown_thenBlockCountTo);
            this.groupBox_thenBlockCount.Controls.Add(this.label_thenBlockCountTo);
            this.groupBox_thenBlockCount.Controls.Add(this.numericUpDown_thenBlockCountFrom);
            this.groupBox_thenBlockCount.Controls.Add(this.label_thenBlockCountFrom);
            this.groupBox_thenBlockCount.Location = new System.Drawing.Point(16, 100);
            this.groupBox_thenBlockCount.Name = "groupBox_thenBlockCount";
            this.groupBox_thenBlockCount.Size = new System.Drawing.Size(297, 57);
            this.groupBox_thenBlockCount.TabIndex = 8;
            this.groupBox_thenBlockCount.TabStop = false;
            this.groupBox_thenBlockCount.Text = "Количество блоков в выражениях ТО";
            // 
            // numericUpDown_thenBlockCountTo
            // 
            this.numericUpDown_thenBlockCountTo.Location = new System.Drawing.Point(184, 25);
            this.numericUpDown_thenBlockCountTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_thenBlockCountTo.Name = "numericUpDown_thenBlockCountTo";
            this.numericUpDown_thenBlockCountTo.Size = new System.Drawing.Size(107, 20);
            this.numericUpDown_thenBlockCountTo.TabIndex = 3;
            this.numericUpDown_thenBlockCountTo.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label_thenBlockCountTo
            // 
            this.label_thenBlockCountTo.AutoSize = true;
            this.label_thenBlockCountTo.Location = new System.Drawing.Point(159, 27);
            this.label_thenBlockCountTo.Name = "label_thenBlockCountTo";
            this.label_thenBlockCountTo.Size = new System.Drawing.Size(19, 13);
            this.label_thenBlockCountTo.TabIndex = 2;
            this.label_thenBlockCountTo.Text = "до";
            // 
            // numericUpDown_thenBlockCountFrom
            // 
            this.numericUpDown_thenBlockCountFrom.Location = new System.Drawing.Point(31, 25);
            this.numericUpDown_thenBlockCountFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_thenBlockCountFrom.Name = "numericUpDown_thenBlockCountFrom";
            this.numericUpDown_thenBlockCountFrom.Size = new System.Drawing.Size(107, 20);
            this.numericUpDown_thenBlockCountFrom.TabIndex = 1;
            this.numericUpDown_thenBlockCountFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label_thenBlockCountFrom
            // 
            this.label_thenBlockCountFrom.AutoSize = true;
            this.label_thenBlockCountFrom.Location = new System.Drawing.Point(6, 27);
            this.label_thenBlockCountFrom.Name = "label_thenBlockCountFrom";
            this.label_thenBlockCountFrom.Size = new System.Drawing.Size(18, 13);
            this.label_thenBlockCountFrom.TabIndex = 0;
            this.label_thenBlockCountFrom.Text = "от";
            // 
            // groupBox_elseBlockCount
            // 
            this.groupBox_elseBlockCount.Controls.Add(this.numericUpDown_elseBlockCountTo);
            this.groupBox_elseBlockCount.Controls.Add(this.label_elseBlockCountTo);
            this.groupBox_elseBlockCount.Controls.Add(this.numericUpDown_elseBlockCountFrom);
            this.groupBox_elseBlockCount.Controls.Add(this.label_elseBlockCountFrom);
            this.groupBox_elseBlockCount.Location = new System.Drawing.Point(16, 163);
            this.groupBox_elseBlockCount.Name = "groupBox_elseBlockCount";
            this.groupBox_elseBlockCount.Size = new System.Drawing.Size(297, 57);
            this.groupBox_elseBlockCount.TabIndex = 9;
            this.groupBox_elseBlockCount.TabStop = false;
            this.groupBox_elseBlockCount.Text = "Количество блоков в выражениях ИНАЧЕ";
            // 
            // numericUpDown_elseBlockCountTo
            // 
            this.numericUpDown_elseBlockCountTo.Location = new System.Drawing.Point(184, 25);
            this.numericUpDown_elseBlockCountTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_elseBlockCountTo.Name = "numericUpDown_elseBlockCountTo";
            this.numericUpDown_elseBlockCountTo.Size = new System.Drawing.Size(107, 20);
            this.numericUpDown_elseBlockCountTo.TabIndex = 3;
            this.numericUpDown_elseBlockCountTo.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label_elseBlockCountTo
            // 
            this.label_elseBlockCountTo.AutoSize = true;
            this.label_elseBlockCountTo.Location = new System.Drawing.Point(159, 27);
            this.label_elseBlockCountTo.Name = "label_elseBlockCountTo";
            this.label_elseBlockCountTo.Size = new System.Drawing.Size(19, 13);
            this.label_elseBlockCountTo.TabIndex = 2;
            this.label_elseBlockCountTo.Text = "до";
            // 
            // numericUpDown_elseBlockCountFrom
            // 
            this.numericUpDown_elseBlockCountFrom.Location = new System.Drawing.Point(31, 25);
            this.numericUpDown_elseBlockCountFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_elseBlockCountFrom.Name = "numericUpDown_elseBlockCountFrom";
            this.numericUpDown_elseBlockCountFrom.Size = new System.Drawing.Size(107, 20);
            this.numericUpDown_elseBlockCountFrom.TabIndex = 1;
            this.numericUpDown_elseBlockCountFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label_elseBlockCountFrom
            // 
            this.label_elseBlockCountFrom.AutoSize = true;
            this.label_elseBlockCountFrom.Location = new System.Drawing.Point(6, 27);
            this.label_elseBlockCountFrom.Name = "label_elseBlockCountFrom";
            this.label_elseBlockCountFrom.Size = new System.Drawing.Size(18, 13);
            this.label_elseBlockCountFrom.TabIndex = 0;
            this.label_elseBlockCountFrom.Text = "от";
            // 
            // checkBox_allowRandomFuzzy
            // 
            this.checkBox_allowRandomFuzzy.AutoSize = true;
            this.checkBox_allowRandomFuzzy.Enabled = false;
            this.checkBox_allowRandomFuzzy.Location = new System.Drawing.Point(16, 379);
            this.checkBox_allowRandomFuzzy.Name = "checkBox_allowRandomFuzzy";
            this.checkBox_allowRandomFuzzy.Size = new System.Drawing.Size(272, 17);
            this.checkBox_allowRandomFuzzy.TabIndex = 10;
            this.checkBox_allowRandomFuzzy.Text = "Использовать случайные нечеткие переменные";
            this.checkBox_allowRandomFuzzy.UseVisualStyleBackColor = true;
            this.checkBox_allowRandomFuzzy.CheckedChanged += new System.EventHandler(this.CheckBox_allowRandomFuzzy_CheckedChanged);
            // 
            // comboBox_selectLV
            // 
            this.comboBox_selectLV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_selectLV.Enabled = false;
            this.comboBox_selectLV.FormattingEnabled = true;
            this.comboBox_selectLV.Location = new System.Drawing.Point(15, 423);
            this.comboBox_selectLV.Name = "comboBox_selectLV";
            this.comboBox_selectLV.Size = new System.Drawing.Size(298, 21);
            this.comboBox_selectLV.TabIndex = 11;
            // 
            // label_selectLV
            // 
            this.label_selectLV.AutoSize = true;
            this.label_selectLV.Location = new System.Drawing.Point(13, 407);
            this.label_selectLV.Name = "label_selectLV";
            this.label_selectLV.Size = new System.Drawing.Size(214, 13);
            this.label_selectLV.TabIndex = 12;
            this.label_selectLV.Text = "Выберите лингвистическую переменную";
            // 
            // GenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 512);
            this.Controls.Add(this.label_selectLV);
            this.Controls.Add(this.comboBox_selectLV);
            this.Controls.Add(this.checkBox_allowRandomFuzzy);
            this.Controls.Add(this.groupBox_elseBlockCount);
            this.Controls.Add(this.groupBox_thenBlockCount);
            this.Controls.Add(this.groupBox_ifBlockCount);
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
            this.groupBox_ifBlockCount.ResumeLayout(false);
            this.groupBox_ifBlockCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ifBlockCountTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ifBlockCountFrom)).EndInit();
            this.groupBox_thenBlockCount.ResumeLayout(false);
            this.groupBox_thenBlockCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_thenBlockCountTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_thenBlockCountFrom)).EndInit();
            this.groupBox_elseBlockCount.ResumeLayout(false);
            this.groupBox_elseBlockCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_elseBlockCountTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_elseBlockCountFrom)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox_ifBlockCount;
        private System.Windows.Forms.NumericUpDown numericUpDown_ifBlockCountTo;
        private System.Windows.Forms.Label label_ifBlockCountTo;
        private System.Windows.Forms.NumericUpDown numericUpDown_ifBlockCountFrom;
        private System.Windows.Forms.Label label_ifBlockCountFrom;
        private System.Windows.Forms.GroupBox groupBox_thenBlockCount;
        private System.Windows.Forms.NumericUpDown numericUpDown_thenBlockCountTo;
        private System.Windows.Forms.Label label_thenBlockCountTo;
        private System.Windows.Forms.NumericUpDown numericUpDown_thenBlockCountFrom;
        private System.Windows.Forms.Label label_thenBlockCountFrom;
        private System.Windows.Forms.GroupBox groupBox_elseBlockCount;
        private System.Windows.Forms.NumericUpDown numericUpDown_elseBlockCountTo;
        private System.Windows.Forms.Label label_elseBlockCountTo;
        private System.Windows.Forms.NumericUpDown numericUpDown_elseBlockCountFrom;
        private System.Windows.Forms.Label label_elseBlockCountFrom;
        private System.Windows.Forms.CheckBox checkBox_allowRandomFuzzy;
        private System.Windows.Forms.ComboBox comboBox_selectLV;
        private System.Windows.Forms.Label label_selectLV;
    }
}