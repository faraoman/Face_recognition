namespace Recognizer
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this._btnTestOpenCV = new System.Windows.Forms.Button();
			this._btnTestCamera = new System.Windows.Forms.Button();
			this.logView1 = new Mallenom.Diagnostics.Logs.LogView();
			this.videoImage1 = new Mallenom.Imaging.VideoImage();
			this.SuspendLayout();
			// 
			// _btnTestOpenCV
			// 
			this._btnTestOpenCV.Location = new System.Drawing.Point(420, 12);
			this._btnTestOpenCV.Name = "_btnTestOpenCV";
			this._btnTestOpenCV.Size = new System.Drawing.Size(132, 23);
			this._btnTestOpenCV.TabIndex = 0;
			this._btnTestOpenCV.Text = "Test OpenCV";
			this._btnTestOpenCV.UseVisualStyleBackColor = true;
			this._btnTestOpenCV.Click += new System.EventHandler(this.OnButtonTestOpenCV_Click);
			// 
			// _btnTestCamera
			// 
			this._btnTestCamera.Location = new System.Drawing.Point(420, 41);
			this._btnTestCamera.Name = "_btnTestCamera";
			this._btnTestCamera.Size = new System.Drawing.Size(132, 23);
			this._btnTestCamera.TabIndex = 1;
			this._btnTestCamera.Text = "Test Camera";
			this._btnTestCamera.UseVisualStyleBackColor = true;
			this._btnTestCamera.Click += new System.EventHandler(this.OnButtonTestCamera_Click);
			// 
			// logView1
			// 
			this.logView1.AppenderName = "root";
			this.logView1.Location = new System.Drawing.Point(12, 320);
			this.logView1.Name = "logView1";
			this.logView1.Size = new System.Drawing.Size(384, 142);
			this.logView1.TabIndex = 3;
			// 
			// videoImage1
			// 
			this.videoImage1.FooterFont = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.videoImage1.FPSFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.videoImage1.Location = new System.Drawing.Point(12, 12);
			this.videoImage1.Name = "videoImage1";
			this.videoImage1.Size = new System.Drawing.Size(384, 288);
			this.videoImage1.TabIndex = 4;
			this.videoImage1.Text = "videoImage1";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 474);
			this.Controls.Add(this.videoImage1);
			this.Controls.Add(this.logView1);
			this.Controls.Add(this._btnTestCamera);
			this.Controls.Add(this._btnTestOpenCV);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainForm";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button _btnTestOpenCV;
		private System.Windows.Forms.Button _btnTestCamera;
		private Mallenom.Diagnostics.Logs.LogView logView1;
		private Mallenom.Imaging.VideoImage videoImage1;
	}
}

