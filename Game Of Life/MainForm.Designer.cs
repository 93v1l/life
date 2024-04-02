namespace Game_Of_Life
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
            if (disposing && (components != null))
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
            this.RunButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.RunOneStepButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.seedpercent = new System.Windows.Forms.Label();
            this.FillingPercentileTracker = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.newborn = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nmax = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nmin = new System.Windows.Forms.NumericUpDown();
            this.ResetButton = new System.Windows.Forms.Button();
            this.movelabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bornlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.deadlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.alivelabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ColorButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FillingPercentileTracker)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newborn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmin)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RunButton
            // 
            this.RunButton.Enabled = false;
            this.RunButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RunButton.Location = new System.Drawing.Point(807, 40);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(115, 52);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "Запустить";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Enabled = false;
            this.PauseButton.Location = new System.Drawing.Point(928, 40);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(69, 52);
            this.PauseButton.TabIndex = 4;
            this.PauseButton.Text = "Пауза";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // RunOneStepButton
            // 
            this.RunOneStepButton.Enabled = false;
            this.RunOneStepButton.Location = new System.Drawing.Point(928, 12);
            this.RunOneStepButton.Name = "RunOneStepButton";
            this.RunOneStepButton.Size = new System.Drawing.Size(144, 23);
            this.RunOneStepButton.TabIndex = 7;
            this.RunOneStepButton.Text = "Шаг вперёд";
            this.RunOneStepButton.UseVisualStyleBackColor = true;
            this.RunOneStepButton.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.seedpercent);
            this.groupBox1.Controls.Add(this.FillingPercentileTracker);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(10, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 70);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Начальное кол-во бактерий %";
            // 
            // seedpercent
            // 
            this.seedpercent.AutoSize = true;
            this.seedpercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.seedpercent.Location = new System.Drawing.Point(181, 30);
            this.seedpercent.Name = "seedpercent";
            this.seedpercent.Size = new System.Drawing.Size(14, 13);
            this.seedpercent.TabIndex = 10;
            this.seedpercent.Text = "0";
            // 
            // FillingPercentileTracker
            // 
            this.FillingPercentileTracker.Location = new System.Drawing.Point(12, 19);
            this.FillingPercentileTracker.Maximum = 100;
            this.FillingPercentileTracker.Name = "FillingPercentileTracker";
            this.FillingPercentileTracker.Size = new System.Drawing.Size(163, 45);
            this.FillingPercentileTracker.TabIndex = 7;
            this.FillingPercentileTracker.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1087, 565);
            this.panel1.TabIndex = 16;
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(3, -6);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(1084, 565);
            this.pic.TabIndex = 1;
            this.pic.TabStop = false;
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.newborn);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.nmax);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.nmin);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(292, 34);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 64);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Соседи";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Родители";
            // 
            // newborn
            // 
            this.newborn.Location = new System.Drawing.Point(240, 24);
            this.newborn.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.newborn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.newborn.Name = "newborn";
            this.newborn.ReadOnly = true;
            this.newborn.Size = new System.Drawing.Size(42, 20);
            this.newborn.TabIndex = 4;
            this.newborn.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.newborn.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Макс";
            // 
            // nmax
            // 
            this.nmax.Location = new System.Drawing.Point(122, 24);
            this.nmax.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nmax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmax.Name = "nmax";
            this.nmax.ReadOnly = true;
            this.nmax.Size = new System.Drawing.Size(42, 20);
            this.nmax.TabIndex = 2;
            this.nmax.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmax.ValueChanged += new System.EventHandler(this.nmax_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Мин";
            // 
            // nmin
            // 
            this.nmin.Location = new System.Drawing.Point(39, 24);
            this.nmin.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nmin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmin.Name = "nmin";
            this.nmin.ReadOnly = true;
            this.nmin.Size = new System.Drawing.Size(42, 20);
            this.nmin.TabIndex = 0;
            this.nmin.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nmin.ValueChanged += new System.EventHandler(this.nmin_ValueChanged);
            // 
            // ResetButton
            // 
            this.ResetButton.Enabled = false;
            this.ResetButton.Location = new System.Drawing.Point(1003, 40);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(69, 52);
            this.ResetButton.TabIndex = 21;
            this.ResetButton.Text = "Сброс";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // movelabel
            // 
            this.movelabel.Name = "movelabel";
            this.movelabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatusLabel3.Text = " | ";
            // 
            // bornlabel
            // 
            this.bornlabel.ForeColor = System.Drawing.Color.Blue;
            this.bornlabel.Name = "bornlabel";
            this.bornlabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatusLabel1.Text = " | ";
            // 
            // deadlabel
            // 
            this.deadlabel.ForeColor = System.Drawing.Color.Red;
            this.deadlabel.Name = "deadlabel";
            this.deadlabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatusLabel2.Text = " | ";
            // 
            // alivelabel
            // 
            this.alivelabel.ForeColor = System.Drawing.Color.Green;
            this.alivelabel.Name = "alivelabel";
            this.alivelabel.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.movelabel,
            this.toolStripStatusLabel3,
            this.bornlabel,
            this.toolStripStatusLabel1,
            this.deadlabel,
            this.toolStripStatusLabel2,
            this.alivelabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1087, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ColorButton
            // 
            this.ColorButton.Location = new System.Drawing.Point(807, 11);
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(115, 23);
            this.ColorButton.TabIndex = 22;
            this.ColorButton.Text = "Синий";
            this.ColorButton.UseVisualStyleBackColor = true;
            this.ColorButton.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1087, 675);
            this.Controls.Add(this.ColorButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RunOneStepButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.RunButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Жизнь";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FillingPercentileTracker)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newborn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmin)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button RunOneStepButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label seedpercent;
        private System.Windows.Forms.TrackBar FillingPercentileTracker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nmin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown newborn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.ToolStripStatusLabel movelabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel bornlabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel deadlabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel alivelabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button ColorButton;
    }
}

