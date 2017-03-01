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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Estimate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// _btnTestOpenCV
			// 
			this._btnTestOpenCV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnTestOpenCV.Location = new System.Drawing.Point(680, 428);
			this._btnTestOpenCV.Name = "_btnTestOpenCV";
			this._btnTestOpenCV.Size = new System.Drawing.Size(132, 23);
			this._btnTestOpenCV.TabIndex = 0;
			this._btnTestOpenCV.Text = "Test OpenCV";
			this._btnTestOpenCV.UseVisualStyleBackColor = true;
			this._btnTestOpenCV.Click += new System.EventHandler(this.OnButtonTestOpenCV_Click);
			// 
			// _btnTestCamera
			// 
			this._btnTestCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnTestCamera.Location = new System.Drawing.Point(680, 457);
			this._btnTestCamera.Name = "_btnTestCamera";
			this._btnTestCamera.Size = new System.Drawing.Size(132, 23);
			this._btnTestCamera.TabIndex = 1;
			this._btnTestCamera.Text = "Test Camera";
			this._btnTestCamera.UseVisualStyleBackColor = true;
			this._btnTestCamera.Click += new System.EventHandler(this.OnButtonTestCamera_Click);
			// 
			// _logView
			// 
			this._logView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._logView.AppenderName = "root";
			this._logView.Location = new System.Drawing.Point(2, 549);
			this._logView.Name = "_logView";
			this._logView.Size = new System.Drawing.Size(810, 155);
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
			this._frameImage.Size = new System.Drawing.Size(500, 500);
			this._frameImage.TabIndex = 4;
			// 
			// _btnTestBtn
			// 
			this._btnTestBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnTestBtn.Location = new System.Drawing.Point(680, 487);
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
			this.menuStrip1.Size = new System.Drawing.Size(817, 24);
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
			this._lblExecutingProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._lblExecutingProtocol.AutoSize = true;
			this._lblExecutingProtocol.Location = new System.Drawing.Point(3, 532);
			this._lblExecutingProtocol.Name = "_lblExecutingProtocol";
			this._lblExecutingProtocol.Size = new System.Drawing.Size(161, 13);
			this._lblExecutingProtocol.TabIndex = 7;
			this._lblExecutingProtocol.Text = "Протокол работы программы:";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this._colName,
            this.Estimate});
			this.dataGridView1.Location = new System.Drawing.Point(506, 27);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(306, 395);
			this.dataGridView1.TabIndex = 8;
			// 
			// Time
			// 
			this.Time.HeaderText = "Дата/время";
			this.Time.Name = "Time";
			this.Time.ReadOnly = true;
			// 
			// _colName
			// 
			this._colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this._colName.DataPropertyName = "Record";
			this._colName.HeaderText = "ФИО";
			this._colName.Name = "_colName";
			this._colName.ReadOnly = true;
			// 
			// Estimate
			// 
			this.Estimate.HeaderText = "Уверенность";
			this.Estimate.Name = "Estimate";
			this.Estimate.ReadOnly = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(817, 707);
			this.Controls.Add(this.dataGridView1);
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
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn _colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Time;
		private System.Windows.Forms.DataGridViewTextBoxColumn Estimate;
	}
}