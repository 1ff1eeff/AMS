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
            this.tbName = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.lbServices = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbProtocol = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbStandard = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.tbMac = new System.Windows.Forms.TextBox();
            this.tbNameOnMap = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(152, 24);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(130, 20);
            this.tbName.TabIndex = 26;
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
            this.button11.Location = new System.Drawing.Point(406, 134);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(77, 20);
            this.button11.TabIndex = 23;
            this.button11.Text = "Добавить";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // lbServices
            // 
            this.lbServices.FormattingEnabled = true;
            this.lbServices.Location = new System.Drawing.Point(297, 72);
            this.lbServices.Name = "lbServices";
            this.lbServices.Size = new System.Drawing.Size(269, 56);
            this.lbServices.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(297, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 15);
            this.label12.TabIndex = 21;
            this.label12.Text = "Сервисы";
            // 
            // cbProtocol
            // 
            this.cbProtocol.FormattingEnabled = true;
            this.cbProtocol.Items.AddRange(new object[] {
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
            this.cbProtocol.Location = new System.Drawing.Point(297, 211);
            this.cbProtocol.Name = "cbProtocol";
            this.cbProtocol.Size = new System.Drawing.Size(270, 21);
            this.cbProtocol.TabIndex = 20;
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
            // cbStandard
            // 
            this.cbStandard.FormattingEnabled = true;
            this.cbStandard.Items.AddRange(new object[] {
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
            this.cbStandard.Location = new System.Drawing.Point(297, 173);
            this.cbStandard.Name = "cbStandard";
            this.cbStandard.Size = new System.Drawing.Size(270, 21);
            this.cbStandard.TabIndex = 18;
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
            this.label6.Location = new System.Drawing.Point(434, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "MAC-адрес";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(297, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "IP-адрес";
            // 
            // lbType
            // 
            this.lbType.FormattingEnabled = true;
            this.lbType.Items.AddRange(new object[] {
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
            this.lbType.Location = new System.Drawing.Point(12, 72);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(270, 160);
            this.lbType.TabIndex = 4;
            this.lbType.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_DoubleClick);
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
            this.label2.Location = new System.Drawing.Point(149, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "NetBIOS имя узла";
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(297, 24);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(129, 20);
            this.tbIp.TabIndex = 28;
            // 
            // tbMac
            // 
            this.tbMac.Location = new System.Drawing.Point(437, 24);
            this.tbMac.Name = "tbMac";
            this.tbMac.Size = new System.Drawing.Size(129, 20);
            this.tbMac.TabIndex = 29;
            // 
            // tbNameOnMap
            // 
            this.tbNameOnMap.Location = new System.Drawing.Point(12, 24);
            this.tbNameOnMap.Name = "tbNameOnMap";
            this.tbNameOnMap.Size = new System.Drawing.Size(130, 20);
            this.tbNameOnMap.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 9);
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
            this.Controls.Add(this.tbNameOnMap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbMac);
            this.Controls.Add(this.tbIp);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbType);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.lbServices);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbProtocol);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbStandard);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Name = "CreateNode";
            this.Text = "Создание новго узла";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ListBox lbType;
        private Label label3;
        private Label label2;
        private Label label5;
        private Label label6;
        private Button button7;
        private Button button8;
        private ComboBox cbStandard;
        private Label label8;
        private ComboBox cbProtocol;
        private Label label9;
        private Button button10;
        private Button button11;
        private ListBox lbServices;
        private Label label12;
        private TextBox tbName;
        private TextBox tbIp;
        private TextBox tbMac;
        private TextBox tbNameOnMap;
        private Label label1;
    }
}