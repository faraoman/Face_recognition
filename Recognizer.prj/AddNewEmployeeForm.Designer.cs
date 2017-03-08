namespace Recognizer
{
	partial class AddNewEmployeeForm
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
			this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
			this._frameImage = new Mallenom.Imaging.FrameImage();
			this._textBoxFirstname = new System.Windows.Forms.TextBox();
			this._textBoxLastname = new System.Windows.Forms.TextBox();
			this._textBoxPatronymic = new System.Windows.Forms.TextBox();
			this._btnAddNewEmployee = new System.Windows.Forms.Button();
			this._labelFirstname = new System.Windows.Forms.Label();
			this._labelLastname = new System.Windows.Forms.Label();
			this._labelPatronymic = new System.Windows.Forms.Label();
			this._btnTakePicture = new System.Windows.Forms.Button();
			this._btnDropPicture = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
			this.SuspendLayout();
			// 
			// fileSystemWatcher1
			// 
			this.fileSystemWatcher1.EnableRaisingEvents = true;
			this.fileSystemWatcher1.SynchronizingObject = this;
			// 
			// _frameImage
			// 
			this._frameImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._frameImage.CoordsFont = new System.Drawing.Font("Verdana", 8F);
			this._frameImage.FooterFont = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this._frameImage.FPSFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this._frameImage.Location = new System.Drawing.Point(0, 0);
			this._frameImage.Name = "_frameImage";
			this._frameImage.Size = new System.Drawing.Size(494, 325);
			this._frameImage.TabIndex = 5;
			// 
			// _textBoxFirstname
			// 
			this._textBoxFirstname.Location = new System.Drawing.Point(500, 117);
			this._textBoxFirstname.Name = "_textBoxFirstname";
			this._textBoxFirstname.Size = new System.Drawing.Size(123, 20);
			this._textBoxFirstname.TabIndex = 2;
			// 
			// _textBoxLastname
			// 
			this._textBoxLastname.Location = new System.Drawing.Point(500, 165);
			this._textBoxLastname.Name = "_textBoxLastname";
			this._textBoxLastname.Size = new System.Drawing.Size(123, 20);
			this._textBoxLastname.TabIndex = 3;
			// 
			// _textBoxPatronymic
			// 
			this._textBoxPatronymic.Location = new System.Drawing.Point(500, 213);
			this._textBoxPatronymic.Name = "_textBoxPatronymic";
			this._textBoxPatronymic.Size = new System.Drawing.Size(123, 20);
			this._textBoxPatronymic.TabIndex = 4;
			// 
			// _btnAddNewEmployee
			// 
			this._btnAddNewEmployee.Location = new System.Drawing.Point(500, 286);
			this._btnAddNewEmployee.Name = "_btnAddNewEmployee";
			this._btnAddNewEmployee.Size = new System.Drawing.Size(123, 23);
			this._btnAddNewEmployee.TabIndex = 5;
			this._btnAddNewEmployee.Text = "Добавить";
			this._btnAddNewEmployee.UseVisualStyleBackColor = true;
			// 
			// _labelFirstname
			// 
			this._labelFirstname.AutoSize = true;
			this._labelFirstname.Location = new System.Drawing.Point(500, 101);
			this._labelFirstname.Name = "_labelFirstname";
			this._labelFirstname.Size = new System.Drawing.Size(29, 13);
			this._labelFirstname.TabIndex = 6;
			this._labelFirstname.Text = "Имя";
			// 
			// _labelLastname
			// 
			this._labelLastname.AutoSize = true;
			this._labelLastname.Location = new System.Drawing.Point(500, 149);
			this._labelLastname.Name = "_labelLastname";
			this._labelLastname.Size = new System.Drawing.Size(56, 13);
			this._labelLastname.TabIndex = 7;
			this._labelLastname.Text = "Фамилия";
			// 
			// _labelPatronymic
			// 
			this._labelPatronymic.AutoSize = true;
			this._labelPatronymic.Location = new System.Drawing.Point(497, 197);
			this._labelPatronymic.Name = "_labelPatronymic";
			this._labelPatronymic.Size = new System.Drawing.Size(54, 13);
			this._labelPatronymic.TabIndex = 8;
			this._labelPatronymic.Text = "Отчество";
			// 
			// _btnTakePicture
			// 
			this._btnTakePicture.Location = new System.Drawing.Point(500, 12);
			this._btnTakePicture.Name = "_btnTakePicture";
			this._btnTakePicture.Size = new System.Drawing.Size(123, 23);
			this._btnTakePicture.TabIndex = 0;
			this._btnTakePicture.Text = "Фото";
			this._btnTakePicture.UseVisualStyleBackColor = true;
			// 
			// _btnDropPicture
			// 
			this._btnDropPicture.Location = new System.Drawing.Point(500, 41);
			this._btnDropPicture.Name = "_btnDropPicture";
			this._btnDropPicture.Size = new System.Drawing.Size(123, 23);
			this._btnDropPicture.TabIndex = 1;
			this._btnDropPicture.Text = "Сбросить";
			this._btnDropPicture.UseVisualStyleBackColor = true;
			// 
			// AddNewEmployeeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(628, 323);
			this.Controls.Add(this._btnDropPicture);
			this.Controls.Add(this._btnTakePicture);
			this.Controls.Add(this._labelPatronymic);
			this.Controls.Add(this._labelLastname);
			this.Controls.Add(this._labelFirstname);
			this.Controls.Add(this._btnAddNewEmployee);
			this.Controls.Add(this._textBoxPatronymic);
			this.Controls.Add(this._textBoxLastname);
			this.Controls.Add(this._textBoxFirstname);
			this.Controls.Add(this._frameImage);
			this.Name = "AddNewEmployeeForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Добавление сотрудника";
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.IO.FileSystemWatcher fileSystemWatcher1;
		private Mallenom.Imaging.FrameImage _frameImage;
		private System.Windows.Forms.Button _btnAddNewEmployee;
		private System.Windows.Forms.TextBox _textBoxPatronymic;
		private System.Windows.Forms.TextBox _textBoxLastname;
		private System.Windows.Forms.TextBox _textBoxFirstname;
		private System.Windows.Forms.Label _labelPatronymic;
		private System.Windows.Forms.Label _labelLastname;
		private System.Windows.Forms.Label _labelFirstname;
		private System.Windows.Forms.Button _btnDropPicture;
		private System.Windows.Forms.Button _btnTakePicture;
	}
}