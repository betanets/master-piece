namespace master_piece.UI
{
    partial class EditFVValue
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
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.label_value = new System.Windows.Forms.Label();
            this.textBox_possibility = new System.Windows.Forms.TextBox();
            this.label_possibility = new System.Windows.Forms.Label();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_value
            // 
            this.textBox_value.Location = new System.Drawing.Point(135, 12);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(239, 20);
            this.textBox_value.TabIndex = 7;
            // 
            // label_value
            // 
            this.label_value.AutoSize = true;
            this.label_value.Location = new System.Drawing.Point(13, 15);
            this.label_value.Name = "label_value";
            this.label_value.Size = new System.Drawing.Size(99, 13);
            this.label_value.TabIndex = 6;
            this.label_value.Text = "Введите значение";
            // 
            // textBox_possibility
            // 
            this.textBox_possibility.Location = new System.Drawing.Point(135, 38);
            this.textBox_possibility.Name = "textBox_possibility";
            this.textBox_possibility.Size = new System.Drawing.Size(239, 20);
            this.textBox_possibility.TabIndex = 9;
            // 
            // label_possibility
            // 
            this.label_possibility.AutoSize = true;
            this.label_possibility.Location = new System.Drawing.Point(13, 41);
            this.label_possibility.Name = "label_possibility";
            this.label_possibility.Size = new System.Drawing.Size(116, 13);
            this.label_possibility.TabIndex = 8;
            this.label_possibility.Text = "Введите вероятность";
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(299, 71);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 11;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(218, 71);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 10;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // EditFVValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 106);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_possibility);
            this.Controls.Add(this.label_possibility);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.label_value);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditFVValue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление значения нечёткой переменной";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.Label label_value;
        private System.Windows.Forms.TextBox textBox_possibility;
        private System.Windows.Forms.Label label_possibility;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
    }
}