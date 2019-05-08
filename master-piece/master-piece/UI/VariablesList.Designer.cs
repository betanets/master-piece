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
            this.LVDeleted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewFV = new System.Windows.Forms.DataGridView();
            this.getFVList = new System.Windows.Forms.Button();
            this.button_addLV = new System.Windows.Forms.Button();
            this.button_editLV = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripFVSelectStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.button_deleteLV = new System.Windows.Forms.Button();
            this.button_addFV = new System.Windows.Forms.Button();
            this.button_editFV = new System.Windows.Forms.Button();
            this.button_deleteFV = new System.Windows.Forms.Button();
            this.button_setupFV = new System.Windows.Forms.Button();
            this.button_LVmap = new System.Windows.Forms.Button();
            this.FVId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FVdeleted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linguisticVariableID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rangeStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.randeEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFV)).BeginInit();
            this.statusStrip1.SuspendLayout();
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
            this.LVName,
            this.LVDeleted});
            this.dataGridViewLV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewLV.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewLV.MultiSelect = false;
            this.dataGridViewLV.Name = "dataGridViewLV";
            this.dataGridViewLV.ReadOnly = true;
            this.dataGridViewLV.RowHeadersVisible = false;
            this.dataGridViewLV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLV.Size = new System.Drawing.Size(519, 486);
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
            this.LVName.Width = 490;
            // 
            // LVDeleted
            // 
            this.LVDeleted.DataPropertyName = "deleted";
            this.LVDeleted.HeaderText = "deleted";
            this.LVDeleted.Name = "LVDeleted";
            this.LVDeleted.ReadOnly = true;
            this.LVDeleted.Visible = false;
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
            this.FVdeleted,
            this.linguisticVariableID,
            this.FVName,
            this.rangeStart,
            this.randeEnd});
            this.dataGridViewFV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewFV.Location = new System.Drawing.Point(605, 12);
            this.dataGridViewFV.MultiSelect = false;
            this.dataGridViewFV.Name = "dataGridViewFV";
            this.dataGridViewFV.ReadOnly = true;
            this.dataGridViewFV.RowHeadersVisible = false;
            this.dataGridViewFV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFV.Size = new System.Drawing.Size(581, 486);
            this.dataGridViewFV.TabIndex = 1;
            // 
            // getFVList
            // 
            this.getFVList.Location = new System.Drawing.Point(537, 249);
            this.getFVList.Name = "getFVList";
            this.getFVList.Size = new System.Drawing.Size(62, 23);
            this.getFVList.TabIndex = 2;
            this.getFVList.Text = "-->";
            this.getFVList.UseVisualStyleBackColor = true;
            this.getFVList.Click += new System.EventHandler(this.getFVList_Click);
            // 
            // button_addLV
            // 
            this.button_addLV.Location = new System.Drawing.Point(12, 504);
            this.button_addLV.Name = "button_addLV";
            this.button_addLV.Size = new System.Drawing.Size(169, 37);
            this.button_addLV.TabIndex = 3;
            this.button_addLV.Text = "Добавить\r\nлингвистическую переменную";
            this.button_addLV.UseVisualStyleBackColor = true;
            this.button_addLV.Click += new System.EventHandler(this.button_addLV_Click);
            // 
            // button_editLV
            // 
            this.button_editLV.Location = new System.Drawing.Point(187, 504);
            this.button_editLV.Name = "button_editLV";
            this.button_editLV.Size = new System.Drawing.Size(169, 37);
            this.button_editLV.TabIndex = 4;
            this.button_editLV.Text = "Редактировать\r\nлингвистическую переменную";
            this.button_editLV.UseVisualStyleBackColor = true;
            this.button_editLV.Click += new System.EventHandler(this.button_editLV_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripFVSelectStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 550);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1198, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripFVSelectStatus
            // 
            this.toolStripFVSelectStatus.Name = "toolStripFVSelectStatus";
            this.toolStripFVSelectStatus.Size = new System.Drawing.Size(267, 17);
            this.toolStripFVSelectStatus.Text = "Получен список лингвистических переменных";
            // 
            // button_deleteLV
            // 
            this.button_deleteLV.Location = new System.Drawing.Point(362, 504);
            this.button_deleteLV.Name = "button_deleteLV";
            this.button_deleteLV.Size = new System.Drawing.Size(169, 37);
            this.button_deleteLV.TabIndex = 6;
            this.button_deleteLV.Text = "Удалить\r\nлингвистическую переменную";
            this.button_deleteLV.UseVisualStyleBackColor = true;
            this.button_deleteLV.Click += new System.EventHandler(this.button_deleteLV_Click);
            // 
            // button_addFV
            // 
            this.button_addFV.Location = new System.Drawing.Point(605, 504);
            this.button_addFV.Name = "button_addFV";
            this.button_addFV.Size = new System.Drawing.Size(143, 37);
            this.button_addFV.TabIndex = 7;
            this.button_addFV.Text = "Добавить\r\nнечеткую переменную";
            this.button_addFV.UseVisualStyleBackColor = true;
            this.button_addFV.Click += new System.EventHandler(this.button_addFV_Click);
            // 
            // button_editFV
            // 
            this.button_editFV.Location = new System.Drawing.Point(754, 504);
            this.button_editFV.Name = "button_editFV";
            this.button_editFV.Size = new System.Drawing.Size(146, 37);
            this.button_editFV.TabIndex = 8;
            this.button_editFV.Text = "Редактировать\r\nнечеткую переменную";
            this.button_editFV.UseVisualStyleBackColor = true;
            this.button_editFV.Click += new System.EventHandler(this.button_editFV_Click);
            // 
            // button_deleteFV
            // 
            this.button_deleteFV.Location = new System.Drawing.Point(906, 504);
            this.button_deleteFV.Name = "button_deleteFV";
            this.button_deleteFV.Size = new System.Drawing.Size(143, 37);
            this.button_deleteFV.TabIndex = 9;
            this.button_deleteFV.Text = "Удалить\r\nнечеткую переменную";
            this.button_deleteFV.UseVisualStyleBackColor = true;
            this.button_deleteFV.Click += new System.EventHandler(this.button_deleteFV_Click);
            // 
            // button_setupFV
            // 
            this.button_setupFV.Location = new System.Drawing.Point(1055, 504);
            this.button_setupFV.Name = "button_setupFV";
            this.button_setupFV.Size = new System.Drawing.Size(131, 37);
            this.button_setupFV.TabIndex = 10;
            this.button_setupFV.Text = "Задать значения нечеткой переменной";
            this.button_setupFV.UseVisualStyleBackColor = true;
            this.button_setupFV.Click += new System.EventHandler(this.button_setupFV_Click);
            // 
            // button_LVmap
            // 
            this.button_LVmap.Location = new System.Drawing.Point(537, 278);
            this.button_LVmap.Name = "button_LVmap";
            this.button_LVmap.Size = new System.Drawing.Size(62, 37);
            this.button_LVmap.TabIndex = 11;
            this.button_LVmap.Text = "Карта\r\nзначений";
            this.button_LVmap.UseVisualStyleBackColor = true;
            this.button_LVmap.Click += new System.EventHandler(this.button_LVmap_Click);
            // 
            // FVId
            // 
            this.FVId.DataPropertyName = "id";
            this.FVId.HeaderText = "ID";
            this.FVId.Name = "FVId";
            this.FVId.ReadOnly = true;
            this.FVId.Visible = false;
            // 
            // FVdeleted
            // 
            this.FVdeleted.DataPropertyName = "deleted";
            this.FVdeleted.HeaderText = "deleted";
            this.FVdeleted.Name = "FVdeleted";
            this.FVdeleted.ReadOnly = true;
            this.FVdeleted.Visible = false;
            // 
            // linguisticVariableID
            // 
            this.linguisticVariableID.DataPropertyName = "linguisticVariableId";
            this.linguisticVariableID.HeaderText = "linguisticVariableID";
            this.linguisticVariableID.Name = "linguisticVariableID";
            this.linguisticVariableID.ReadOnly = true;
            this.linguisticVariableID.Visible = false;
            // 
            // FVName
            // 
            this.FVName.DataPropertyName = "name";
            this.FVName.HeaderText = "Имя нёчеткой переменной";
            this.FVName.Name = "FVName";
            this.FVName.ReadOnly = true;
            this.FVName.Width = 400;
            // 
            // rangeStart
            // 
            this.rangeStart.DataPropertyName = "rangeStart";
            this.rangeStart.HeaderText = "Диапазон ОТ";
            this.rangeStart.Name = "rangeStart";
            this.rangeStart.ReadOnly = true;
            this.rangeStart.Width = 75;
            // 
            // randeEnd
            // 
            this.randeEnd.DataPropertyName = "rangeEnd";
            this.randeEnd.HeaderText = "Диапазон ДО";
            this.randeEnd.Name = "randeEnd";
            this.randeEnd.ReadOnly = true;
            this.randeEnd.Width = 75;
            // 
            // VariablesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 572);
            this.Controls.Add(this.button_LVmap);
            this.Controls.Add(this.button_setupFV);
            this.Controls.Add(this.button_deleteFV);
            this.Controls.Add(this.button_editFV);
            this.Controls.Add(this.button_addFV);
            this.Controls.Add(this.button_deleteLV);
            this.Controls.Add(this.statusStrip1);
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewLV;
        private System.Windows.Forms.DataGridView dataGridViewFV;
        private System.Windows.Forms.Button getFVList;
        private System.Windows.Forms.Button button_addLV;
        private System.Windows.Forms.Button button_editLV;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripFVSelectStatus;
        private System.Windows.Forms.Button button_deleteLV;
        private System.Windows.Forms.Button button_addFV;
        private System.Windows.Forms.Button button_editFV;
        private System.Windows.Forms.Button button_deleteFV;
        private System.Windows.Forms.DataGridViewTextBoxColumn LVId;
        private System.Windows.Forms.DataGridViewTextBoxColumn LVName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LVDeleted;
        private System.Windows.Forms.Button button_setupFV;
        private System.Windows.Forms.Button button_LVmap;
        private System.Windows.Forms.DataGridViewTextBoxColumn FVId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FVdeleted;
        private System.Windows.Forms.DataGridViewTextBoxColumn linguisticVariableID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FVName;
        private System.Windows.Forms.DataGridViewTextBoxColumn rangeStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn randeEnd;
    }
}