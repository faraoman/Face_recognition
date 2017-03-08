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
			this._logView = new Mallenom.Diagnostics.Logs.LogView();
			this._frameImage = new Mallenom.Imaging.FrameImage();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.сервисToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.спискиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._RecognizeByPhotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._lblExecutingProtocol = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _logView
			// 
			this._logView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this._logView.AppenderName = "root";
			this._logView.Location = new System.Drawing.Point(2, 542);
			this._logView.Name = "_logView";
			this._logView.Size = new System.Drawing.Size(802, 165);
			this._logView.TabIndex = 3;
			// 
			// _frameImage
			// 
			this._frameImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this._frameImage.CoordsFont = new System.Drawing.Font("Verdana", 8F);
			this._frameImage.FooterFont = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this._frameImage.FPSFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this._frameImage.Location = new System.Drawing.Point(0, 27);
			this._frameImage.Name = "_frameImage";
			this._frameImage.Size = new System.Drawing.Size(804, 496);
			this._frameImage.TabIndex = 4;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.сервисToolStripMenuItem,
			this.базаДанныхToolStripMenuItem,
			this._RecognizeByPhotoToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(804, 24);
			this.menuStrip1.TabIndex = 6;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// сервисToolStripMenuItem
			// 
			this.сервисToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.настройкиToolStripMenuItem,
			this.toolStripMenuItem1,
			this.выходToolStripMenuItem});
			this.сервисToolStripMenuItem.Name = "сервисToolStripMenuItem";
			this.сервисToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.сервисToolStripMenuItem.Text = "Сервис";
			// 
			// настройкиToolStripMenuItem
			// 
			this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
			this.настройкиToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.настройкиToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.настройкиToolStripMenuItem.Text = "Настройка...";
			this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.OnCameraSettingsToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(158, 6);
			// 
			// выходToolStripMenuItem
			// 
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.выходToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.выходToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			this.выходToolStripMenuItem.Click += new System.EventHandler(this.OnExitToolStripMenuItem_Click);
			// 
			// базаДанныхToolStripMenuItem
			// 
			this.базаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.спискиToolStripMenuItem});
			this.базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
			this.базаДанныхToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
			this.базаДанныхToolStripMenuItem.Text = "База данных";
			// 
			// спискиToolStripMenuItem
			// 
			this.спискиToolStripMenuItem.Name = "спискиToolStripMenuItem";
			this.спискиToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.спискиToolStripMenuItem.Text = "Списки";
			this.спискиToolStripMenuItem.Click += new System.EventHandler(this.OnListsToolStripMenuItem_Click);
			// 
			// _RecognizeByPhotoToolStripMenuItem
			// 
			this._RecognizeByPhotoToolStripMenuItem.Name = "_RecognizeByPhotoToolStripMenuItem";
			this._RecognizeByPhotoToolStripMenuItem.Size = new System.Drawing.Size(129, 20);
			this._RecognizeByPhotoToolStripMenuItem.Text = "Распознать по фото";
			this._RecognizeByPhotoToolStripMenuItem.Click += new System.EventHandler(this.OnRecognizeByPhotoToolStripMenuItem_Click);
			// 
			// _lblExecutingProtocol
			// 
			this._lblExecutingProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this._lblExecutingProtocol.AutoSize = true;
			this._lblExecutingProtocol.Location = new System.Drawing.Point(3, 526);
			this._lblExecutingProtocol.Name = "_lblExecutingProtocol";
			this._lblExecutingProtocol.Size = new System.Drawing.Size(161, 13);
			this._lblExecutingProtocol.TabIndex = 7;
			this._lblExecutingProtocol.Text = "Протокол работы программы:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(804, 711);
			this.Controls.Add(this._lblExecutingProtocol);
			this.Controls.Add(this._frameImage);
			this.Controls.Add(this._logView);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainForm";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Mallenom.Diagnostics.Logs.LogView _logView;
		private Mallenom.Imaging.FrameImage _frameImage;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem сервисToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem спискиToolStripMenuItem;
		private System.Windows.Forms.Label _lblExecutingProtocol;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem _RecognizeByPhotoToolStripMenuItem;
	}
}