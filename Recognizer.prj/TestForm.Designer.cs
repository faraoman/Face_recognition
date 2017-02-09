namespace Recognizer
{
	partial class TestForm
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
			this._imgBefore = new System.Windows.Forms.PictureBox();
			this._imgAfter = new System.Windows.Forms.PictureBox();
			this._btnLoadImage = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._imgBefore)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._imgAfter)).BeginInit();
			this.SuspendLayout();
			// 
			// _imgBefore
			// 
			this._imgBefore.Location = new System.Drawing.Point(13, 13);
			this._imgBefore.Name = "_imgBefore";
			this._imgBefore.Size = new System.Drawing.Size(100, 50);
			this._imgBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this._imgBefore.TabIndex = 0;
			this._imgBefore.TabStop = false;
			// 
			// _imgAfter
			// 
			this._imgAfter.Location = new System.Drawing.Point(395, 12);
			this._imgAfter.Name = "_imgAfter";
			this._imgAfter.Size = new System.Drawing.Size(100, 50);
			this._imgAfter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this._imgAfter.TabIndex = 0;
			this._imgAfter.TabStop = false;
			// 
			// _btnLoadImage
			// 
			this._btnLoadImage.Location = new System.Drawing.Point(13, 354);
			this._btnLoadImage.Name = "_btnLoadImage";
			this._btnLoadImage.Size = new System.Drawing.Size(75, 23);
			this._btnLoadImage.TabIndex = 1;
			this._btnLoadImage.Text = "Load";
			this._btnLoadImage.UseVisualStyleBackColor = true;
			this._btnLoadImage.Click += new System.EventHandler(this._btnLoadImage_Click);
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(692, 394);
			this.Controls.Add(this._btnLoadImage);
			this.Controls.Add(this._imgAfter);
			this.Controls.Add(this._imgBefore);
			this.Name = "TestForm";
			this.Text = "TestForm";
			((System.ComponentModel.ISupportInitialize)(this._imgBefore)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._imgAfter)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox _imgBefore;
		private System.Windows.Forms.PictureBox _imgAfter;
		private System.Windows.Forms.Button _btnLoadImage;
	}
}