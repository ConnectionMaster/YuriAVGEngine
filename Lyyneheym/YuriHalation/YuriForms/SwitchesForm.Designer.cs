﻿namespace Yuri.YuriForms
{
    partial class SwitchesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.switchDataGridView = new System.Windows.Forms.DataGridView();
            this.开关序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.描述 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.switchDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // switchDataGridView
            // 
            this.switchDataGridView.AllowUserToAddRows = false;
            this.switchDataGridView.AllowUserToDeleteRows = false;
            this.switchDataGridView.AllowUserToResizeRows = false;
            this.switchDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.switchDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.开关序号,
            this.描述});
            this.switchDataGridView.Location = new System.Drawing.Point(12, 12);
            this.switchDataGridView.MultiSelect = false;
            this.switchDataGridView.Name = "switchDataGridView";
            this.switchDataGridView.RowHeadersVisible = false;
            this.switchDataGridView.RowTemplate.Height = 23;
            this.switchDataGridView.Size = new System.Drawing.Size(351, 297);
            this.switchDataGridView.TabIndex = 2;
            // 
            // 开关序号
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.开关序号.DefaultCellStyle = dataGridViewCellStyle2;
            this.开关序号.Frozen = true;
            this.开关序号.HeaderText = "序号";
            this.开关序号.Name = "开关序号";
            this.开关序号.ReadOnly = true;
            this.开关序号.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.开关序号.Width = 40;
            // 
            // 描述
            // 
            this.描述.HeaderText = "描述";
            this.描述.Name = "描述";
            this.描述.ReadOnly = true;
            this.描述.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.描述.Width = 290;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(414, 274);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 35);
            this.button1.TabIndex = 7;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "on",
            "off"});
            this.comboBox1.Location = new System.Drawing.Point(369, 241);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(136, 20);
            this.comboBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(367, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "将选中的开关设定为：";
            // 
            // SwitchesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 322);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.switchDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SwitchesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "开关操作";
            ((System.ComponentModel.ISupportInitialize)(this.switchDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView switchDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn 开关序号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 描述;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}