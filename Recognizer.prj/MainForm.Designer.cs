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
			this._btnTestBtn = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.сервисToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.спискиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _btnTestOpenCV
			// 
			this._btnTestOpenCV.Location = new System.Drawing.Point(506, 440);
			this._btnTestOpenCV.Name = "_btnTestOpenCV";
			this._btnTestOpenCV.Size = new System.Drawing.Size(132, 23);
			this._btnTestOpenCV.TabIndex = 0;
			this._btnTestOpenCV.Text = "Test OpenCV";
			this._btnTestOpenCV.UseVisualStyleBackColor = true;
			this._btnTestOpenCV.Click += new System.EventHandler(this.OnButtonTestOpenCV_Click);
			// 
			// _btnTestCamera
			// 
			this._btnTestCamera.Location = new System.Drawing.Point(506, 469);
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
			this.logView1.Location = new System.Drawing.Point(2, 549);
			this.logView1.Name = "logView1";
			this.logView1.Size = new System.Drawing.Size(739, 155);
			this.logView1.TabIndex = 3;
			// 
			// videoImage1
			// 
			this.videoImage1.FooterFont = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.videoImage1.FPSFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.videoImage1.Location = new System.Drawing.Point(0, 27);
			this.videoImage1.Name = "videoImage1";
			this.videoImage1.Size = new System.Drawing.Size(500, 500);
			this.videoImage1.TabIndex = 4;
			// 
			// _btnTestBtn
			// 
			this._btnTestBtn.Location = new System.Drawing.Point(506, 499);
			this._btnTestBtn.Name = "_btnTestBtn";
			this._btnTestBtn.Size = new System.Drawing.Size(132, 23);
			this._btnTestBtn.TabIndex = 5;
			this._btnTestBtn.Text = "TestForm";
			this._btnTestBtn.UseVisualStyleBackColor = true;
			this._btnTestBtn.Click += new System.EventHandler(this._btnTestBtn_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сервисToolStripMenuItem,
            this.базаДанныхToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(747, 24);
			this.menuStrip1.TabIndex = 6;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
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
			// базаДанныхToolStripMenuItem
			// 
			this.базаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.спискиToolStripMenuItem});
			this.базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
			this.базаДанныхToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
			this.базаДанныхToolStripMenuItem.Text = "База данных";
			// 
			// настройкиToolStripMenuItem
			// 
			this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
			this.настройкиToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.настройкиToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.настройкиToolStripMenuItem.Text = "Настройка...";
			this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
			// 
			// выходToolStripMenuItem
			// 
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.выходToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.выходToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
			// 
			// спискиToolStripMenuItem
			// 
			this.спискиToolStripMenuItem.Name = "спискиToolStripMenuItem";
			this.спискиToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.спискиToolStripMenuItem.Text = "Списки";
			this.спискиToolStripMenuItem.Click += new System.EventHandler(this.спискиToolStripMenuItem_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 532);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(161, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Протокол работы программы:";
			// 
			// listView1
			// 
			this.listView1.Location = new System.Drawing.Point(506, 27);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(235, 407);
			this.listView1.TabIndex = 8;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(158, 6);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(747, 707);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._btnTestBtn);
			this.Controls.Add(this.videoImage1);
			this.Controls.Add(this.logView1);
			this.Controls.Add(this._btnTestCamera);
			this.Controls.Add(this._btnTestOpenCV);
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

		private System.Windows.Forms.Button _btnTestOpenCV;
		private System.Windows.Forms.Button _btnTestCamera;
		private Mallenom.Diagnostics.Logs.LogView logView1;
		private Mallenom.Imaging.VideoImage videoImage1;
		private System.Windows.Forms.Button _btnTestBtn;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem сервисToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem спискиToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
	}
}

