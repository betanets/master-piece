﻿namespace master_piece.UI
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_count)).BeginInit();
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
            this.numericUpDown_count.Name = "numericUpDown_count";
            this.numericUpDown_count.Size = new System.Drawing.Size(135, 20);
            this.numericUpDown_count.TabIndex = 1;
            // 
            // label_names
            // 
            this.label_names.AutoSize = true;
            this.label_names.Location = new System.Drawing.Point(13, 43);
            this.label_names.Name = "label_names";
            this.label_names.Size = new System.Drawing.Size(107, 13);
            this.label_names.TabIndex = 2;
            this.label_names.Text = "Имена переменных";
            // 
            // textBox_names
            // 
            this.textBox_names.Location = new System.Drawing.Point(16, 60);
            this.textBox_names.Multiline = true;
            this.textBox_names.Name = "textBox_names";
            this.textBox_names.Size = new System.Drawing.Size(282, 227);
            this.textBox_names.TabIndex = 3;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(143, 294);
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
            this.button_cancel.Location = new System.Drawing.Point(224, 294);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // GenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 325);
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
    }
}