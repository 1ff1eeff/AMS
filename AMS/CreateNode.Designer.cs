using System.Drawing;
using System.Windows.Forms;

namespace AMS
{
    partial class CreateNode
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button12 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(130, 20);
            this.textBox1.TabIndex = 26;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(408, 241);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(77, 20);
            this.button7.TabIndex = 1;
            this.button7.Text = "ОК";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(490, 241);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(77, 20);
            this.button8.TabIndex = 2;
            this.button8.Text = "Отмена";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(489, 134);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(77, 20);
            this.button10.TabIndex = 24;
            this.button10.Text = "Удалить";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(489, 111);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(77, 20);
            this.button11.TabIndex = 23;
            this.button11.Text = "Добавить";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.Location = new System.Drawing.Point(297, 111);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(187, 43);
            this.listBox4.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(297, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 15);
            this.label12.TabIndex = 21;
            this.label12.Text = "Сервисы";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "TCP/IP",
            "TCP/IPv6",
            "UDP",
            "DNS",
            "iSCSI",
            "RPC",
            "HTTP",
            "HTTPS",
            "FTP",
            "IMAP4",
            "POP3",
            "SMTP",
            "NTP",
            "TELNET",
            "TLS",
            "SSL",
            "RTSP",
            "SLP",
            "SNMP",
            "SOCKS",
            "Gopher",
            "SFTP",
            "FTPS",
            "TFTP",
            "AppleTalk",
            "ATM",
            "Bridge/Router",
            "CDMA2000",
            "Frame Relay",
            "GPRS",
            "NetBIOS",
            "SMB",
            "SNA",
            "SNMP",
            "SMI",
            "ISDN",
            "ISO-IP",
            "ISO over X.25",
            "ROSE",
            "RAS",
            "CIF",
            "DIS",
            "Ethernet",
            "FDDI",
            "GARP",
            "GMRP",
            "GVRP",
            "LLC",
            "SMT",
            "SNAP",
            "SRP",
            "Token Ring",
            "VLAN (802.1Q)",
            "Multiprotocol over ATM",
            "Novell",
            "BAP",
            "BSD",
            "CHAP",
            "DESE",
            "IPHC",
            "LCP",
            "LQR",
            "LZS",
            "MultiPPP",
            "MPPC",
            "PAP",
            "PPP",
            "PPP-BPDU",
            "EAP",
            "ECP",
            "MAPOS",
            "MLP",
            "PPPoE",
            "Sigtran",
            "SMDS",
            "SS7",
            "SUN",
            "ISL",
            "MPLS",
            "MPLS over ATM",
            "TDP",
            "VoDSL",
            "VoIP",
            "WAP",
            "X.25",
            "XNS"});
            this.comboBox2.Location = new System.Drawing.Point(297, 211);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(270, 21);
            this.comboBox2.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(297, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(171, 15);
            this.label9.TabIndex = 19;
            this.label9.Text = "Протокол передачи пакетов";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "10Base-2 Thin Ethernet 10 Mb/s",
            "10Base-5 Thick Ethernet 10 Mb/s",
            "10Base-F, 10Base-FL Ethernet 10 Mb/s",
            "10Base-Т Ethernet 10 Mb/s",
            "100Base-FX Fast Ethernet 100 Mb/s",
            "100Base-TX Fast Ethernet 100 Mb/s",
            "100Base-VG Quartet Coding Ethernet 25 Mb/s",
            "100Base-X FDDI TP-PMD",
            "100VG-AnyLAN Ethernet и Token Ring",
            "1000Base-LX Gigabit Ethernet 1000 Mb/s",
            "1000Base-SX Gigabit Ethernet 1000 Mb/s",
            "1000Base-T Gigabit Ethernet 1000 Mb/s"});
            this.comboBox1.Location = new System.Drawing.Point(297, 173);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(270, 21);
            this.comboBox1.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(297, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "Стандарт передачи данных";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(438, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "MAC-адрес";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(297, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "IP-адрес";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Сеть",
            "Компьютер",
            "Принтер сетевой",
            "Сервер",
            "Роутер",
            "Ноутбук",
            "Web-сервер",
            "Коммутатор",
            "Proxy-сервер",
            "Сервер баз данных",
            "Факс",
            "Брандмауэр",
            "Здание",
            "Почтовый сервер",
            "Смартфон",
            "Спутниковая антенна",
            "Файловый сервер",
            "Точка доступа",
            "ADSL-модем",
            "GPS/ГЛОНАСС",
            "Мобильный телефон",
            "Фотокамера",
            "Видеокамера",
            "Сканер",
            "Сервер времени",
            "Радиороутер",
            "Игровая приставка",
            "DVD-плеер",
            "Медиаплеер",
            "VoIP-шлюз",
            "IP-видеотелефон",
            "IP-телефон",
            "Хаб",
            "Принтер локальный",
            "Мэйнфрейм",
            "Кластер",
            "Планшет",
            "Видеопроектор",
            "IP-ATC",
            "Контроллер домена",
            "DNS-сервер",
            "Терминальный сервер",
            "Виртуальный сервер",
            "Виртуальный компьютер",
            "Спутник",
            "Оптический узел",
            "Сетевое хранилище",
            "Ленточная библиотека",
            "ИБП",
            "Плоттер"});
            this.listBox1.Location = new System.Drawing.Point(12, 72);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(270, 160);
            this.listBox1.TabIndex = 4;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(12, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Тип устройства";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "NetBIOS имя узла";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button12);
            this.panel1.Location = new System.Drawing.Point(300, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 45);
            this.panel1.TabIndex = 27;
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.SystemColors.Control;
            this.button12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.button12.Location = new System.Drawing.Point(0, 0);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(269, 45);
            this.button12.TabIndex = 0;
            this.button12.Text = "(Тестирование)\r\nЭтот компьютер";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(297, 73);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(129, 20);
            this.textBox2.TabIndex = 28;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(438, 73);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(129, 20);
            this.textBox3.TabIndex = 29;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(154, 24);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(130, 20);
            this.textBox4.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(154, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 30;
            this.label1.Text = "Имя узла на карте";
            // 
            // CreateNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 273);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Name = "CreateNode";
            this.Text = "Создание новго узла";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ListBox listBox1;
        private Label label3;
        private Label label2;
        private Label label5;
        private Label label6;
        private Button button7;
        private Button button8;
        private ComboBox comboBox1;
        private Label label8;
        private ComboBox comboBox2;
        private Label label9;
        private Button button10;
        private Button button11;
        private ListBox listBox4;
        private Label label12;
        private TextBox textBox1;
        private Panel panel1;
        private Button button12;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label1;
    }
}