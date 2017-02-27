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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this._btnShowEmployees = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left)));
			this.listBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.IntegralHeight = false;
			this.listBox1.ItemHeight = 100;
			this.listBox1.Location = new System.Drawing.Point(5, 5);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(360, 459);
			this.listBox1.TabIndex = 0;
			// 
			// _btnShowEmployees
			// 
			this._btnShowEmployees.Location = new System.Drawing.Point(383, 5);
			this._btnShowEmployees.Name = "_btnShowEmployees";
			this._btnShowEmployees.Size = new System.Drawing.Size(179, 23);
			this._btnShowEmployees.TabIndex = 1;
			this._btnShowEmployees.Text = "Показать сотрудников";
			this._btnShowEmployees.UseVisualStyleBackColor = true;
			this._btnShowEmployees.Click += new System.EventHandler(this._btnShowEmployees_Click);
			// 
			// UserLists
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
			this.ClientSize = new System.Drawing.Size(574, 511);
			this.Controls.Add(this._btnShowEmployees);
			this.Controls.Add(this.listBox1);
			this.Name = "UserLists";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Списки";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button _btnShowEmployees;
	}
}