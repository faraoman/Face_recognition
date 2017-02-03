namespace Recognizer.prj
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
			this.SuspendLayout();
			// 
			// _btnTestOpenCV
			// 
			this._btnTestOpenCV.Location = new System.Drawing.Point(140, 12);
			this._btnTestOpenCV.Name = "_btnTestOpenCV";
			this._btnTestOpenCV.Size = new System.Drawing.Size(132, 23);
			this._btnTestOpenCV.TabIndex = 0;
			this._btnTestOpenCV.Text = "Test OpenCV";
			this._btnTestOpenCV.UseVisualStyleBackColor = true;
			this._btnTestOpenCV.Click += new System.EventHandler(this.OnButtonTestOpenCV_Click);
			// 
			// _btnTestCamera
			// 
			this._btnTestCamera.Location = new System.Drawing.Point(140, 41);
			this._btnTestCamera.Name = "_btnTestCamera";
			this._btnTestCamera.Size = new System.Drawing.Size(132, 23);
			this._btnTestCamera.TabIndex = 1;
			this._btnTestCamera.Text = "Test Camera";
			this._btnTestCamera.UseVisualStyleBackColor = true;
			this._btnTestCamera.Click += new System.EventHandler(this.OnButtonTestCamera_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
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
	}
}

