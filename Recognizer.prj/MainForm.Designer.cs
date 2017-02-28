﻿namespace Recognizer
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
			this._logView = new Mallenom.Diagnostics.Logs.LogView();
			this._frameImage = new Mallenom.Imaging.FrameImage();
			this._btnTestBtn = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.сервисToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.спискиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._lblExecutingProtocol = new System.Windows.Forms.Label();
			this._listView = new System.Windows.Forms.ListView();
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
			// _logView
			// 
			this._logView.AppenderName = "root";
			this._logView.Location = new System.Drawing.Point(2, 549);
			this._logView.Name = "_logView";
			this._logView.Size = new System.Drawing.Size(739, 155);
			this._logView.TabIndex = 3;
			// 
			// _videoImage
			// 
			this._frameImage.FooterFont = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this._frameImage.FPSFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this._frameImage.Location = new System.Drawing.Point(0, 27);
			this._frameImage.Name = "_videoImage";
			this._frameImage.Size = new System.Drawing.Size(500, 500);
			this._frameImage.TabIndex = 4;
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
			// настройкиToolStripMenuItem
			// 
			this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
			this.настройкиToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.настройкиToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.настройкиToolStripMenuItem.Text = "Настройка...";
			this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
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
			this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
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
			this.спискиToolStripMenuItem.Click += new System.EventHandler(this.спискиToolStripMenuItem_Click);
			// 
			// _lblExecutingProtocol
			// 
			this._lblExecutingProtocol.AutoSize = true;
			this._lblExecutingProtocol.Location = new System.Drawing.Point(3, 532);
			this._lblExecutingProtocol.Name = "_lblExecutingProtocol";
			this._lblExecutingProtocol.Size = new System.Drawing.Size(161, 13);
			this._lblExecutingProtocol.TabIndex = 7;
			this._lblExecutingProtocol.Text = "Протокол работы программы:";
			// 
			// _listView
			// 
			this._listView.Location = new System.Drawing.Point(506, 27);
			this._listView.Name = "_listView";
			this._listView.Size = new System.Drawing.Size(235, 407);
			this._listView.TabIndex = 8;
			this._listView.UseCompatibleStateImageBehavior = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(747, 707);
			this.Controls.Add(this._listView);
			this.Controls.Add(this._lblExecutingProtocol);
			this.Controls.Add(this._btnTestBtn);
			this.Controls.Add(this._frameImage);
			this.Controls.Add(this._logView);
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
		private Mallenom.Diagnostics.Logs.LogView _logView;
		private Mallenom.Imaging.FrameImage _frameImage;
		private System.Windows.Forms.Button _btnTestBtn;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem сервисToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem спискиToolStripMenuItem;
		private System.Windows.Forms.Label _lblExecutingProtocol;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		public System.Windows.Forms.ListView _listView;
	}
}