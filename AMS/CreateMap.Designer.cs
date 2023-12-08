using System.Drawing;
using System.Windows.Forms;

namespace AMS
{
    partial class CreateMap
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.tbFirstIp = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.panScanRange = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button8 = new System.Windows.Forms.Button();
            this.lvScanResult = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnScan = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.lvScanRange = new System.Windows.Forms.ListView();
            this.FirstIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Separator = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemove = new System.Windows.Forms.Button();
            this.tbLastIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMapName = new System.Windows.Forms.TextBox();
            this.rbNewMap = new System.Windows.Forms.RadioButton();
            this.rbMapOnScan = new System.Windows.Forms.RadioButton();
            this.gbCms = new System.Windows.Forms.GroupBox();
            this.cbNewMap = new System.Windows.Forms.CheckBox();
            this.panScanRange.SuspendLayout();
            this.gbCms.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Начальный адрес";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, -2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Финальный адрес";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(102, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 21);
            this.button1.TabIndex = 5;
            this.button1.Text = ">";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(229, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 22);
            this.button2.TabIndex = 6;
            this.button2.Text = "Добавить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Диапазоны сканирования";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(655, 445);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "Отмена";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tbFirstIp
            // 
            this.tbFirstIp.Location = new System.Drawing.Point(6, 15);
            this.tbFirstIp.MaxLength = 15;
            this.tbFirstIp.Name = "tbFirstIp";
            this.tbFirstIp.Size = new System.Drawing.Size(90, 20);
            this.tbFirstIp.TabIndex = 27;
            this.tbFirstIp.Text = "192.168.0.102";
            this.tbFirstIp.Leave += new System.EventHandler(this.tbFirstIp_Leave);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(574, 445);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 36;
            this.button6.Text = "ОК";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // panScanRange
            // 
            this.panScanRange.Controls.Add(this.progressBar1);
            this.panScanRange.Controls.Add(this.button8);
            this.panScanRange.Controls.Add(this.lvScanResult);
            this.panScanRange.Controls.Add(this.btnScan);
            this.panScanRange.Controls.Add(this.label2);
            this.panScanRange.Controls.Add(this.button3);
            this.panScanRange.Controls.Add(this.lvScanRange);
            this.panScanRange.Controls.Add(this.btnRemove);
            this.panScanRange.Controls.Add(this.tbLastIp);
            this.panScanRange.Controls.Add(this.tbFirstIp);
            this.panScanRange.Controls.Add(this.label5);
            this.panScanRange.Controls.Add(this.button2);
            this.panScanRange.Controls.Add(this.button1);
            this.panScanRange.Controls.Add(this.label4);
            this.panScanRange.Controls.Add(this.label3);
            this.panScanRange.Enabled = false;
            this.panScanRange.Location = new System.Drawing.Point(10, 49);
            this.panScanRange.Name = "panScanRange";
            this.panScanRange.Size = new System.Drawing.Size(723, 390);
            this.panScanRange.TabIndex = 37;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 366);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(717, 20);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 45;
            this.progressBar1.Visible = false;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(303, 337);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(101, 23);
            this.button8.TabIndex = 36;
            this.button8.Text = "Редактировать";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // lvScanResult
            // 
            this.lvScanResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lvScanResult.HideSelection = false;
            this.lvScanResult.Location = new System.Drawing.Point(300, 58);
            this.lvScanResult.Name = "lvScanResult";
            this.lvScanResult.Size = new System.Drawing.Size(420, 273);
            this.lvScanResult.TabIndex = 35;
            this.lvScanResult.UseCompatibleStateImageBehavior = false;
            this.lvScanResult.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP-адрес";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "MAC-адрес";
            this.columnHeader2.Width = 101;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Имя узла";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Сервисы";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Тип узла";
            this.columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Стандарт передачи данных";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Протокол передачи данных";
            this.columnHeader7.Width = 120;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Имя узла на карте";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(211, 337);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(83, 23);
            this.btnScan.TabIndex = 34;
            this.btnScan.Text = "Сканировать";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Активные IP-адреса";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(624, 337);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 23);
            this.button3.TabIndex = 32;
            this.button3.Text = "Удалить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lvScanRange
            // 
            this.lvScanRange.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FirstIP,
            this.Separator,
            this.LastIP});
            this.lvScanRange.HideSelection = false;
            this.lvScanRange.Location = new System.Drawing.Point(3, 58);
            this.lvScanRange.Name = "lvScanRange";
            this.lvScanRange.Size = new System.Drawing.Size(291, 273);
            this.lvScanRange.TabIndex = 30;
            this.lvScanRange.UseCompatibleStateImageBehavior = false;
            this.lvScanRange.View = System.Windows.Forms.View.Details;
            // 
            // FirstIP
            // 
            this.FirstIP.Text = "Начальный IP";
            this.FirstIP.Width = 130;
            // 
            // Separator
            // 
            this.Separator.Text = "";
            this.Separator.Width = 20;
            // 
            // LastIP
            // 
            this.LastIP.Text = "Финальный IP";
            this.LastIP.Width = 130;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(3, 337);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(83, 23);
            this.btnRemove.TabIndex = 29;
            this.btnRemove.Text = "Удалить";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // tbLastIp
            // 
            this.tbLastIp.Location = new System.Drawing.Point(133, 15);
            this.tbLastIp.MaxLength = 15;
            this.tbLastIp.Name = "tbLastIp";
            this.tbLastIp.Size = new System.Drawing.Size(90, 20);
            this.tbLastIp.TabIndex = 28;
            this.tbLastIp.Leave += new System.EventHandler(this.tbLastIp_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Имя новой карты";
            // 
            // tbMapName
            // 
            this.tbMapName.Location = new System.Drawing.Point(10, 23);
            this.tbMapName.Name = "tbMapName";
            this.tbMapName.Size = new System.Drawing.Size(211, 20);
            this.tbMapName.TabIndex = 40;
            this.tbMapName.Text = "Новая карта";
            // 
            // rbNewMap
            // 
            this.rbNewMap.AutoSize = true;
            this.rbNewMap.Checked = true;
            this.rbNewMap.Location = new System.Drawing.Point(5, 14);
            this.rbNewMap.Name = "rbNewMap";
            this.rbNewMap.Size = new System.Drawing.Size(136, 17);
            this.rbNewMap.TabIndex = 42;
            this.rbNewMap.TabStop = true;
            this.rbNewMap.Text = "Создать пустую карту";
            this.rbNewMap.UseVisualStyleBackColor = true;
            // 
            // rbMapOnScan
            // 
            this.rbMapOnScan.AutoSize = true;
            this.rbMapOnScan.Location = new System.Drawing.Point(147, 14);
            this.rbMapOnScan.Name = "rbMapOnScan";
            this.rbMapOnScan.Size = new System.Drawing.Size(184, 17);
            this.rbMapOnScan.TabIndex = 43;
            this.rbMapOnScan.Text = "Карта на основе сканирования";
            this.rbMapOnScan.UseVisualStyleBackColor = true;
            this.rbMapOnScan.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // gbCms
            // 
            this.gbCms.Controls.Add(this.cbNewMap);
            this.gbCms.Controls.Add(this.rbMapOnScan);
            this.gbCms.Controls.Add(this.rbNewMap);
            this.gbCms.Location = new System.Drawing.Point(226, 9);
            this.gbCms.Name = "gbCms";
            this.gbCms.Size = new System.Drawing.Size(507, 41);
            this.gbCms.TabIndex = 44;
            this.gbCms.TabStop = false;
            this.gbCms.Text = "Опции создания карты";
            // 
            // cbNewMap
            // 
            this.cbNewMap.AutoSize = true;
            this.cbNewMap.Checked = true;
            this.cbNewMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNewMap.Enabled = false;
            this.cbNewMap.Location = new System.Drawing.Point(336, 14);
            this.cbNewMap.Name = "cbNewMap";
            this.cbNewMap.Size = new System.Drawing.Size(149, 17);
            this.cbNewMap.TabIndex = 44;
            this.cbNewMap.Text = "Не создать новую карту";
            this.cbNewMap.UseVisualStyleBackColor = true;
            // 
            // CreateMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 477);
            this.Controls.Add(this.gbCms);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tbMapName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panScanRange);
            this.Name = "CreateMap";
            this.Text = "Создание новой карты";
            this.panScanRange.ResumeLayout(false);
            this.panScanRange.PerformLayout();
            this.gbCms.ResumeLayout(false);
            this.gbCms.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label3;
        private Label label4;
        private Button button1;
        private Button button2;
        private Label label5;
        private Button button4;
        private TextBox tbFirstIp;
        private Button button6;
        private Panel panScanRange;
        private Label label1;
        private TextBox tbMapName;
        private TextBox tbLastIp;
        private Button btnRemove;
        private ListView lvScanRange;
        private ColumnHeader FirstIP;
        private ColumnHeader LastIP;
        private ColumnHeader Separator;
        private Button button3;
        private Button btnScan;
        private Label label2;
        private ListView lvScanResult;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private RadioButton rbNewMap;
        private RadioButton rbMapOnScan;
        private GroupBox gbCms;
        private CheckBox cbNewMap;
        private ProgressBar progressBar1;
        private ColumnHeader columnHeader4;
        private Button button8;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
    }
}