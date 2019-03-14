namespace master_piece
{
    partial class VariablesList
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
            this.dataGridViewLV = new System.Windows.Forms.DataGridView();
            this.LVId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewFV = new System.Windows.Forms.DataGridView();
            this.FVId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.getFVList = new System.Windows.Forms.Button();
            this.button_addLV = new System.Windows.Forms.Button();
            this.button_editLV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFV)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewLV
            // 
            this.dataGridViewLV.AllowUserToAddRows = false;
            this.dataGridViewLV.AllowUserToDeleteRows = false;
            this.dataGridViewLV.AllowUserToResizeRows = false;
            this.dataGridViewLV.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewLV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LVId,
            this.LVName});
            this.dataGridViewLV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewLV.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewLV.MultiSelect = false;
            this.dataGridViewLV.Name = "dataGridViewLV";
            this.dataGridViewLV.ReadOnly = true;
            this.dataGridViewLV.RowHeadersVisible = false;
            this.dataGridViewLV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLV.Size = new System.Drawing.Size(386, 486);
            this.dataGridViewLV.TabIndex = 0;
            // 
            // LVId
            // 
            this.LVId.DataPropertyName = "id";
            this.LVId.HeaderText = "ID";
            this.LVId.Name = "LVId";
            this.LVId.ReadOnly = true;
            this.LVId.Visible = false;
            // 
            // LVName
            // 
            this.LVName.DataPropertyName = "name";
            this.LVName.HeaderText = "Имя лингвистической переменной";
            this.LVName.Name = "LVName";
            this.LVName.ReadOnly = true;
            this.LVName.Width = 350;
            // 
            // dataGridViewFV
            // 
            this.dataGridViewFV.AllowUserToAddRows = false;
            this.dataGridViewFV.AllowUserToDeleteRows = false;
            this.dataGridViewFV.AllowUserToResizeRows = false;
            this.dataGridViewFV.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewFV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FVId,
            this.FVName});
            this.dataGridViewFV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewFV.Location = new System.Drawing.Point(448, 12);
            this.dataGridViewFV.MultiSelect = false;
            this.dataGridViewFV.Name = "dataGridViewFV";
            this.dataGridViewFV.ReadOnly = true;
            this.dataGridViewFV.RowHeadersVisible = false;
            this.dataGridViewFV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFV.Size = new System.Drawing.Size(393, 486);
            this.dataGridViewFV.TabIndex = 1;
            // 
            // FVId
            // 
            this.FVId.HeaderText = "ID";
            this.FVId.Name = "FVId";
            this.FVId.ReadOnly = true;
            this.FVId.Visible = false;
            // 
            // FVName
            // 
            this.FVName.HeaderText = "Имя нечеткой переменной";
            this.FVName.Name = "FVName";
            this.FVName.ReadOnly = true;
            this.FVName.Width = 350;
            // 
            // getFVList
            // 
            this.getFVList.Location = new System.Drawing.Point(404, 252);
            this.getFVList.Name = "getFVList";
            this.getFVList.Size = new System.Drawing.Size(38, 23);
            this.getFVList.TabIndex = 2;
            this.getFVList.Text = "-->";
            this.getFVList.UseVisualStyleBackColor = true;
            // 
            // button_addLV
            // 
            this.button_addLV.Location = new System.Drawing.Point(12, 504);
            this.button_addLV.Name = "button_addLV";
            this.button_addLV.Size = new System.Drawing.Size(191, 37);
            this.button_addLV.TabIndex = 3;
            this.button_addLV.Text = "Добавить\r\nлингвистическую переменную";
            this.button_addLV.UseVisualStyleBackColor = true;
            this.button_addLV.Click += new System.EventHandler(this.button_addLV_Click);
            // 
            // button_editLV
            // 
            this.button_editLV.Location = new System.Drawing.Point(209, 504);
            this.button_editLV.Name = "button_editLV";
            this.button_editLV.Size = new System.Drawing.Size(189, 37);
            this.button_editLV.TabIndex = 4;
            this.button_editLV.Text = "Редактировать\r\nлингвистическую переменную";
            this.button_editLV.UseVisualStyleBackColor = true;
            this.button_editLV.Click += new System.EventHandler(this.button_editLV_Click);
            // 
            // VariablesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 551);
            this.Controls.Add(this.button_editLV);
            this.Controls.Add(this.button_addLV);
            this.Controls.Add(this.getFVList);
            this.Controls.Add(this.dataGridViewFV);
            this.Controls.Add(this.dataGridViewLV);
            this.Name = "VariablesList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список лингвистических переменных";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewLV;
        private System.Windows.Forms.DataGridView dataGridViewFV;
        private System.Windows.Forms.DataGridViewTextBoxColumn FVId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FVName;
        private System.Windows.Forms.Button getFVList;
        private System.Windows.Forms.DataGridViewTextBoxColumn LVId;
        private System.Windows.Forms.DataGridViewTextBoxColumn LVName;
        private System.Windows.Forms.Button button_addLV;
        private System.Windows.Forms.Button button_editLV;
    }
}