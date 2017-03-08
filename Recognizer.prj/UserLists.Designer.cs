using System.Windows.Forms;

namespace Recognizer
{
	partial class UserLists
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
			if(disposing && (components != null))
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
			this._btnShowEmployees = new System.Windows.Forms.Button();
			this._btnAddEmployee = new System.Windows.Forms.Button();
			this._dataGridView = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// _btnShowEmployees
			// 
			this._btnShowEmployees.Location = new System.Drawing.Point(383, 12);
			this._btnShowEmployees.Name = "_btnShowEmployees";
			this._btnShowEmployees.Size = new System.Drawing.Size(179, 23);
			this._btnShowEmployees.TabIndex = 1;
			this._btnShowEmployees.Text = "Показать сотрудников";
			this._btnShowEmployees.UseVisualStyleBackColor = true;
			this._btnShowEmployees.Click += new System.EventHandler(this._btnShowEmployees_Click);
			// 
			// _btnAddEmployee
			// 
			this._btnAddEmployee.Location = new System.Drawing.Point(383, 42);
			this._btnAddEmployee.Name = "_btnAddEmployee";
			this._btnAddEmployee.Size = new System.Drawing.Size(179, 23);
			this._btnAddEmployee.TabIndex = 2;
			this._btnAddEmployee.Text = "Добавить сотрудника";
			this._btnAddEmployee.UseVisualStyleBackColor = true;
			this._btnAddEmployee.Click += new System.EventHandler(this._btnAddEmployee_Click);
			// 
			// _dataGridView
			// 
			this._dataGridView.AllowUserToAddRows = false;
			this._dataGridView.AllowUserToDeleteRows = false;
			this._dataGridView.AllowUserToResizeRows = false;
			this._dataGridView.Location = new System.Drawing.Point(12, 12);
			this._dataGridView.Name = "_dataGridView";
			this._dataGridView.ReadOnly = true;
			this._dataGridView.RowHeadersVisible = false;
			this._dataGridView.Size = new System.Drawing.Size(365, 487);
			//this._dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			this._dataGridView.TabIndex = 9;
			// 
			// UserLists
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
			this.ClientSize = new System.Drawing.Size(574, 511);
			this.Controls.Add(this._dataGridView);
			this.Controls.Add(this._btnAddEmployee);
			this.Controls.Add(this._btnShowEmployees);
			this.Name = "UserLists";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Списки";
			((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button _btnShowEmployees;
		private System.Windows.Forms.Button _btnAddEmployee;
		private System.Windows.Forms.DataGridView _dataGridView;
	}
}