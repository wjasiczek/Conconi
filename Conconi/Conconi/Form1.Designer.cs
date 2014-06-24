namespace Conconi
{
    partial class Form1
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
            if (disposing && (components != null))
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonAddData = new System.Windows.Forms.Button();
            this.labelHeartbeat = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.textBoxHeartbeat = new System.Windows.Forms.TextBox();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.labelAdded = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDeleteData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Location = new System.Drawing.Point(35, 112);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(817, 514);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // buttonAddData
            // 
            this.buttonAddData.Location = new System.Drawing.Point(15, 61);
            this.buttonAddData.Name = "buttonAddData";
            this.buttonAddData.Size = new System.Drawing.Size(100, 27);
            this.buttonAddData.TabIndex = 1;
            this.buttonAddData.TabStop = false;
            this.buttonAddData.Text = "Dodaj dane";
            this.buttonAddData.UseVisualStyleBackColor = true;
            this.buttonAddData.Click += new System.EventHandler(this.buttonAddData_Click);
            // 
            // labelHeartbeat
            // 
            this.labelHeartbeat.AutoSize = true;
            this.labelHeartbeat.Location = new System.Drawing.Point(12, 13);
            this.labelHeartbeat.Name = "labelHeartbeat";
            this.labelHeartbeat.Size = new System.Drawing.Size(45, 17);
            this.labelHeartbeat.TabIndex = 3;
            this.labelHeartbeat.Text = "Tętno";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Location = new System.Drawing.Point(118, 13);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(125, 17);
            this.labelTime.TabIndex = 3;
            this.labelTime.Text = "Czas w sekundach";
            // 
            // textBoxHeartbeat
            // 
            this.textBoxHeartbeat.Location = new System.Drawing.Point(15, 33);
            this.textBoxHeartbeat.Name = "textBoxHeartbeat";
            this.textBoxHeartbeat.Size = new System.Drawing.Size(100, 22);
            this.textBoxHeartbeat.TabIndex = 0;
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(121, 33);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(100, 22);
            this.textBoxTime.TabIndex = 1;
            // 
            // buttonDraw
            // 
            this.buttonDraw.Location = new System.Drawing.Point(249, 9);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(166, 44);
            this.buttonDraw.TabIndex = 6;
            this.buttonDraw.TabStop = false;
            this.buttonDraw.Text = "Rysuj wykres";
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // labelAdded
            // 
            this.labelAdded.AutoSize = true;
            this.labelAdded.Location = new System.Drawing.Point(122, 62);
            this.labelAdded.Name = "labelAdded";
            this.labelAdded.Size = new System.Drawing.Size(0, 17);
            this.labelAdded.TabIndex = 7;
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(32, 91);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(43, 17);
            this.labelY.TabIndex = 10;
            this.labelY.Text = "[bpm]";
            this.labelY.Visible = false;
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(858, 609);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(60, 17);
            this.labelX.TabIndex = 11;
            this.labelX.Text = "[min/km]";
            this.labelX.Visible = false;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(522, 9);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(97, 36);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "Wyczyść";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLog.Location = new System.Drawing.Point(861, 112);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(171, 489);
            this.textBoxLog.TabIndex = 13;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(421, 9);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(95, 36);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.TabStop = false;
            this.buttonSave.Text = "Zapisz";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonDeleteData
            // 
            this.buttonDeleteData.Location = new System.Drawing.Point(121, 62);
            this.buttonDeleteData.Name = "buttonDeleteData";
            this.buttonDeleteData.Size = new System.Drawing.Size(100, 26);
            this.buttonDeleteData.TabIndex = 15;
            this.buttonDeleteData.TabStop = false;
            this.buttonDeleteData.Text = "Usuń dane";
            this.buttonDeleteData.UseVisualStyleBackColor = true;
            this.buttonDeleteData.Click += new System.EventHandler(this.buttonDeleteData_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonAddData;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1035, 665);
            this.Controls.Add(this.buttonDeleteData);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelAdded);
            this.Controls.Add(this.buttonDraw);
            this.Controls.Add(this.textBoxTime);
            this.Controls.Add(this.textBoxHeartbeat);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelHeartbeat);
            this.Controls.Add(this.buttonAddData);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Conconi";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button buttonAddData;
        private System.Windows.Forms.Label labelHeartbeat;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.TextBox textBoxHeartbeat;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.Label labelAdded;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDeleteData;
    }
}

