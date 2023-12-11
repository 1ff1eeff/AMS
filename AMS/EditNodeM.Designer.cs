namespace AMS
{
    partial class EditNodeM
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
            this.btnDetect = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbMac = new System.Windows.Forms.TextBox();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbServices = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbProtocol = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbStandard = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDeviceType = new System.Windows.Forms.ComboBox();
            this.tbNameOnMap = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(204, 171);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(77, 23);
            this.btnDetect.TabIndex = 70;
            this.btnDetect.Text = "Обнаружить";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(123, 322);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 23);
            this.btnOk.TabIndex = 68;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(205, 322);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 23);
            this.btnCancel.TabIndex = 69;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbMac
            // 
            this.tbMac.Location = new System.Drawing.Point(153, 66);
            this.tbMac.Name = "tbMac";
            this.tbMac.Size = new System.Drawing.Size(129, 20);
            this.tbMac.TabIndex = 67;
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(12, 66);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(129, 20);
            this.tbIp.TabIndex = 66;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(153, 26);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(129, 20);
            this.tbName.TabIndex = 65;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(153, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 15);
            this.label2.TabIndex = 52;
            this.label2.Text = "NetBIOS имя узла";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(12, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 53;
            this.label3.Text = "Тип устройства";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(205, 141);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(77, 23);
            this.btnRemove.TabIndex = 64;
            this.btnRemove.Text = "Удалить";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(204, 112);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(77, 23);
            this.btnAdd.TabIndex = 63;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lbServices
            // 
            this.lbServices.FormattingEnabled = true;
            this.lbServices.Location = new System.Drawing.Point(12, 112);
            this.lbServices.Name = "lbServices";
            this.lbServices.Size = new System.Drawing.Size(187, 82);
            this.lbServices.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(12, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 55;
            this.label5.Text = "IP-адрес";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(12, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 15);
            this.label12.TabIndex = 61;
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
            this.cbProtocol.Location = new System.Drawing.Point(12, 293);
            this.cbProtocol.Name = "cbProtocol";
            this.cbProtocol.Size = new System.Drawing.Size(270, 21);
            this.cbProtocol.TabIndex = 60;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(12, 277);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(171, 15);
            this.label9.TabIndex = 59;
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
            this.cbStandard.Location = new System.Drawing.Point(12, 255);
            this.cbStandard.Name = "cbStandard";
            this.cbStandard.Size = new System.Drawing.Size(270, 21);
            this.cbStandard.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(12, 239);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 15);
            this.label8.TabIndex = 57;
            this.label8.Text = "Стандарт передачи данных";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(153, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 56;
            this.label6.Text = "MAC-адрес";
            // 
            // cbDeviceType
            // 
            this.cbDeviceType.FormattingEnabled = true;
            this.cbDeviceType.Items.AddRange(new object[] {
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
            this.cbDeviceType.Location = new System.Drawing.Point(11, 215);
            this.cbDeviceType.Name = "cbDeviceType";
            this.cbDeviceType.Size = new System.Drawing.Size(270, 21);
            this.cbDeviceType.TabIndex = 71;
            // 
            // tbNameOnMap
            // 
            this.tbNameOnMap.Location = new System.Drawing.Point(12, 26);
            this.tbNameOnMap.Name = "tbNameOnMap";
            this.tbNameOnMap.Size = new System.Drawing.Size(129, 20);
            this.tbNameOnMap.TabIndex = 73;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 72;
            this.label1.Text = "Имя узла на карте";
            // 
            // EditNodeM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 357);
            this.Controls.Add(this.tbNameOnMap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDeviceType);
            this.Controls.Add(this.btnDetect);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbMac);
            this.Controls.Add(this.tbIp);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbServices);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbProtocol);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbStandard);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Name = "EditNodeM";
            this.Text = "Редактирование узла";
            this.Load += new System.EventHandler(this.EditNodeM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbMac;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lbServices;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbProtocol;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbStandard;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbDeviceType;
        private System.Windows.Forms.TextBox tbNameOnMap;
        private System.Windows.Forms.Label label1;
    }
}