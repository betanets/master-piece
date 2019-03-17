namespace master_piece.UI
{
    partial class FVValues
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
            this.dataGridView_FVValues = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FVId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.possibility = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_addValue = new System.Windows.Forms.Button();
            this.button_editValue = new System.Windows.Forms.Button();
            this.button_deleteValue = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_FVValues)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_FVValues
            // 
            this.dataGridView_FVValues.AllowUserToAddRows = false;
            this.dataGridView_FVValues.AllowUserToDeleteRows = false;
            this.dataGridView_FVValues.AllowUserToResizeRows = false;
            this.dataGridView_FVValues.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView_FVValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_FVValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.FVId,
            this.value,
            this.possibility,
            this.deleted});
            this.dataGridView_FVValues.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_FVValues.Location = new System.Drawing.Point(12, 12);
            this.dataGridView_FVValues.Name = "dataGridView_FVValues";
            this.dataGridView_FVValues.ReadOnly = true;
            this.dataGridView_FVValues.RowHeadersVisible = false;
            this.dataGridView_FVValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_FVValues.Size = new System.Drawing.Size(441, 377);
            this.dataGridView_FVValues.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // FVId
            // 
            this.FVId.DataPropertyName = "fuzzyVariableId";
            this.FVId.HeaderText = "FVId";
            this.FVId.Name = "FVId";
            this.FVId.ReadOnly = true;
            this.FVId.Visible = false;
            // 
            // value
            // 
            this.value.DataPropertyName = "value";
            this.value.HeaderText = "Значение";
            this.value.Name = "value";
            this.value.ReadOnly = true;
            this.value.Width = 200;
            // 
            // possibility
            // 
            this.possibility.DataPropertyName = "possibility";
            this.possibility.HeaderText = "Вероятность";
            this.possibility.Name = "possibility";
            this.possibility.ReadOnly = true;
            this.possibility.Width = 200;
            // 
            // deleted
            // 
            this.deleted.DataPropertyName = "deleted";
            this.deleted.HeaderText = "deleted";
            this.deleted.Name = "deleted";
            this.deleted.ReadOnly = true;
            this.deleted.Visible = false;
            // 
            // button_addValue
            // 
            this.button_addValue.Location = new System.Drawing.Point(12, 395);
            this.button_addValue.Name = "button_addValue";
            this.button_addValue.Size = new System.Drawing.Size(143, 23);
            this.button_addValue.TabIndex = 1;
            this.button_addValue.Text = "Добавить значение";
            this.button_addValue.UseVisualStyleBackColor = true;
            this.button_addValue.Click += new System.EventHandler(this.button_addValue_Click);
            // 
            // button_editValue
            // 
            this.button_editValue.Location = new System.Drawing.Point(161, 395);
            this.button_editValue.Name = "button_editValue";
            this.button_editValue.Size = new System.Drawing.Size(143, 23);
            this.button_editValue.TabIndex = 2;
            this.button_editValue.Text = "Редактировать значение";
            this.button_editValue.UseVisualStyleBackColor = true;
            this.button_editValue.Click += new System.EventHandler(this.button_editValue_Click);
            // 
            // button_deleteValue
            // 
            this.button_deleteValue.Location = new System.Drawing.Point(310, 395);
            this.button_deleteValue.Name = "button_deleteValue";
            this.button_deleteValue.Size = new System.Drawing.Size(143, 23);
            this.button_deleteValue.TabIndex = 3;
            this.button_deleteValue.Text = "Удалить значение";
            this.button_deleteValue.UseVisualStyleBackColor = true;
            this.button_deleteValue.Click += new System.EventHandler(this.button_deleteValue_Click);
            // 
            // FVValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 429);
            this.Controls.Add(this.button_deleteValue);
            this.Controls.Add(this.button_editValue);
            this.Controls.Add(this.button_addValue);
            this.Controls.Add(this.dataGridView_FVValues);
            this.Name = "FVValues";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Значения нечеткой переменной";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_FVValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_FVValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FVId;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.DataGridViewTextBoxColumn possibility;
        private System.Windows.Forms.DataGridViewTextBoxColumn deleted;
        private System.Windows.Forms.Button button_addValue;
        private System.Windows.Forms.Button button_editValue;
        private System.Windows.Forms.Button button_deleteValue;
    }
}