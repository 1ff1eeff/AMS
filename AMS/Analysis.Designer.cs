namespace AMS
{
    partial class Analysis
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.lvStatistics = new System.Windows.Forms.ListView();
            this.Succeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Failed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Downtime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TotalTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TimeoutPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PingStarted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PingFinished = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NodeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(910, 450);
            this.splitContainer1.SplitterDistance = 421;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lvStatistics);
            this.splitContainer2.Size = new System.Drawing.Size(910, 421);
            this.splitContainer2.SplitterDistance = 26;
            this.splitContainer2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(910, 26);
            this.label4.TabIndex = 5;
            this.label4.Text = "Общая статистика работы";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lvStatistics
            // 
            this.lvStatistics.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvStatistics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NodeName,
            this.IP,
            this.Succeed,
            this.Failed,
            this.Downtime,
            this.TotalTime,
            this.TimeoutPercent,
            this.PingStarted,
            this.PingFinished});
            this.lvStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvStatistics.HideSelection = false;
            this.lvStatistics.Location = new System.Drawing.Point(0, 0);
            this.lvStatistics.Name = "lvStatistics";
            this.lvStatistics.Size = new System.Drawing.Size(910, 391);
            this.lvStatistics.TabIndex = 29;
            this.lvStatistics.UseCompatibleStateImageBehavior = false;
            this.lvStatistics.View = System.Windows.Forms.View.Details;
            // 
            // Succeed
            // 
            this.Succeed.Text = "Успешно";
            this.Succeed.Width = 100;
            // 
            // Failed
            // 
            this.Failed.Text = "Отказов";
            this.Failed.Width = 100;
            // 
            // Downtime
            // 
            this.Downtime.Text = "Время простоя";
            this.Downtime.Width = 100;
            // 
            // TotalTime
            // 
            this.TotalTime.Text = "Время работы";
            this.TotalTime.Width = 100;
            // 
            // TimeoutPercent
            // 
            this.TimeoutPercent.Text = "Процент простоя";
            this.TimeoutPercent.Width = 100;
            // 
            // PingStarted
            // 
            this.PingStarted.Text = "Начало опроса";
            this.PingStarted.Width = 100;
            // 
            // PingFinished
            // 
            this.PingFinished.Text = "Финал опроса";
            this.PingFinished.Width = 100;
            // 
            // IP
            // 
            this.IP.Text = "Адрес узла";
            this.IP.Width = 100;
            // 
            // NodeName
            // 
            this.NodeName.Text = "Имя узла";
            this.NodeName.Width = 100;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(776, -1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Analysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Analysis";
            this.Text = "Analysis";
            this.Load += new System.EventHandler(this.Analysis_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvStatistics;
        private System.Windows.Forms.ColumnHeader Succeed;
        private System.Windows.Forms.ColumnHeader Failed;
        private System.Windows.Forms.ColumnHeader Downtime;
        private System.Windows.Forms.ColumnHeader TotalTime;
        private System.Windows.Forms.ColumnHeader TimeoutPercent;
        private System.Windows.Forms.ColumnHeader PingStarted;
        private System.Windows.Forms.ColumnHeader PingFinished;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader NodeName;
    }
}