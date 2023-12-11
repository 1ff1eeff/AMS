using System.Drawing;
using System.Windows.Forms;

namespace AMS
{
    partial class Settings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.label5 = new System.Windows.Forms.Label();
            this.btnOpenLogsFolder = new System.Windows.Forms.Button();
            this.btnOpenMapsFolder = new System.Windows.Forms.Button();
            this.btnOpenCfg = new System.Windows.Forms.Button();
            this.tbLogsFolder = new System.Windows.Forms.TextBox();
            this.tbMapsFolder = new System.Windows.Forms.TextBox();
            this.tbConfigFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tbSmtpPassword = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.pnlSmsEmail = new System.Windows.Forms.Panel();
            this.ckEmailTrans = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.pnlSmsAdb = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ckSmsTrans = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOpenAdbFile = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.tbAdb = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.rbtnSmsEmail = new System.Windows.Forms.RadioButton();
            this.rbtnSmsAdb = new System.Windows.Forms.RadioButton();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ckSmsNoti = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ckSsl = new System.Windows.Forms.CheckBox();
            this.ckSmsEmail = new System.Windows.Forms.CheckBox();
            this.btnLicense = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.fbdLogsFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.fbdMapsFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.pnlSmsEmail.SuspendLayout();
            this.pnlSmsAdb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 347);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(243, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Директория хранения журналов мониторинга:";
            // 
            // btnOpenLogsFolder
            // 
            this.btnOpenLogsFolder.Location = new System.Drawing.Point(283, 363);
            this.btnOpenLogsFolder.Name = "btnOpenLogsFolder";
            this.btnOpenLogsFolder.Size = new System.Drawing.Size(27, 21);
            this.btnOpenLogsFolder.TabIndex = 17;
            this.btnOpenLogsFolder.Text = "...";
            this.btnOpenLogsFolder.UseVisualStyleBackColor = true;
            this.btnOpenLogsFolder.Click += new System.EventHandler(this.btnOpenLogsFolder_Click);
            // 
            // btnOpenMapsFolder
            // 
            this.btnOpenMapsFolder.Location = new System.Drawing.Point(283, 311);
            this.btnOpenMapsFolder.Name = "btnOpenMapsFolder";
            this.btnOpenMapsFolder.Size = new System.Drawing.Size(27, 21);
            this.btnOpenMapsFolder.TabIndex = 16;
            this.btnOpenMapsFolder.Text = "...";
            this.btnOpenMapsFolder.UseVisualStyleBackColor = true;
            this.btnOpenMapsFolder.Click += new System.EventHandler(this.btnOpenMapsFolder_Click);
            // 
            // btnOpenCfg
            // 
            this.btnOpenCfg.Location = new System.Drawing.Point(283, 258);
            this.btnOpenCfg.Name = "btnOpenCfg";
            this.btnOpenCfg.Size = new System.Drawing.Size(27, 21);
            this.btnOpenCfg.TabIndex = 15;
            this.btnOpenCfg.Text = "...";
            this.btnOpenCfg.UseVisualStyleBackColor = true;
            this.btnOpenCfg.Click += new System.EventHandler(this.btnOpenCfg_Click);
            // 
            // tbLogsFolder
            // 
            this.tbLogsFolder.Location = new System.Drawing.Point(7, 363);
            this.tbLogsFolder.Name = "tbLogsFolder";
            this.tbLogsFolder.Size = new System.Drawing.Size(270, 20);
            this.tbLogsFolder.TabIndex = 14;
            // 
            // tbMapsFolder
            // 
            this.tbMapsFolder.Location = new System.Drawing.Point(7, 311);
            this.tbMapsFolder.Name = "tbMapsFolder";
            this.tbMapsFolder.Size = new System.Drawing.Size(270, 20);
            this.tbMapsFolder.TabIndex = 12;
            // 
            // tbConfigFile
            // 
            this.tbConfigFile.Location = new System.Drawing.Point(7, 259);
            this.tbConfigFile.Name = "tbConfigFile";
            this.tbConfigFile.Size = new System.Drawing.Size(270, 20);
            this.tbConfigFile.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Директория хранения карт:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Файл конфигурации приложения:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(4, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Файлы и каталоги";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(3, 83);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 13);
            this.label24.TabIndex = 33;
            this.label24.Text = "Пароль:";
            // 
            // tbSmtpPassword
            // 
            this.tbSmtpPassword.Location = new System.Drawing.Point(116, 80);
            this.tbSmtpPassword.Name = "tbSmtpPassword";
            this.tbSmtpPassword.Size = new System.Drawing.Size(159, 20);
            this.tbSmtpPassword.TabIndex = 32;
            this.tbSmtpPassword.UseSystemPasswordChar = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 57);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(105, 13);
            this.label23.TabIndex = 31;
            this.label23.Text = "E-mail отправителя:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(116, 54);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(192, 20);
            this.textBox9.TabIndex = 28;
            // 
            // pnlSmsEmail
            // 
            this.pnlSmsEmail.Controls.Add(this.ckEmailTrans);
            this.pnlSmsEmail.Controls.Add(this.label30);
            this.pnlSmsEmail.Controls.Add(this.textBox13);
            this.pnlSmsEmail.Enabled = false;
            this.pnlSmsEmail.Location = new System.Drawing.Point(6, 263);
            this.pnlSmsEmail.Name = "pnlSmsEmail";
            this.pnlSmsEmail.Size = new System.Drawing.Size(307, 63);
            this.pnlSmsEmail.TabIndex = 55;
            // 
            // checkBox4
            // 
            this.ckEmailTrans.AutoSize = true;
            this.ckEmailTrans.Location = new System.Drawing.Point(3, 42);
            this.ckEmailTrans.Name = "checkBox4";
            this.ckEmailTrans.Size = new System.Drawing.Size(187, 17);
            this.ckEmailTrans.TabIndex = 52;
            this.ckEmailTrans.Text = "Транслитерировать сообщение";
            this.ckEmailTrans.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(0, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(117, 13);
            this.label30.TabIndex = 50;
            this.label30.Text = "SMS-шлюз оператора";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(2, 16);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(301, 20);
            this.textBox13.TabIndex = 51;
            // 
            // pnlSmsAdb
            // 
            this.pnlSmsAdb.Controls.Add(this.richTextBox1);
            this.pnlSmsAdb.Controls.Add(this.ckSmsTrans);
            this.pnlSmsAdb.Controls.Add(this.label6);
            this.pnlSmsAdb.Controls.Add(this.btnOpenAdbFile);
            this.pnlSmsAdb.Controls.Add(this.label29);
            this.pnlSmsAdb.Controls.Add(this.tbAdb);
            this.pnlSmsAdb.Controls.Add(this.textBox12);
            this.pnlSmsAdb.Location = new System.Drawing.Point(3, 24);
            this.pnlSmsAdb.Name = "pnlSmsAdb";
            this.pnlSmsAdb.Size = new System.Drawing.Size(307, 204);
            this.pnlSmsAdb.TabIndex = 54;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1, 115);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(301, 89);
            this.richTextBox1.TabIndex = 68;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // checkBox5
            // 
            this.ckSmsTrans.AutoSize = true;
            this.ckSmsTrans.Location = new System.Drawing.Point(1, 42);
            this.ckSmsTrans.Name = "checkBox5";
            this.ckSmsTrans.Size = new System.Drawing.Size(187, 17);
            this.ckSmsTrans.TabIndex = 53;
            this.ckSmsTrans.Text = "Транслитерировать сообщение";
            this.ckSmsTrans.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "Путь до adb.exe (Android Debug Bridge)";
            // 
            // btnOpenAdbFile
            // 
            this.btnOpenAdbFile.Location = new System.Drawing.Point(276, 78);
            this.btnOpenAdbFile.Name = "btnOpenAdbFile";
            this.btnOpenAdbFile.Size = new System.Drawing.Size(27, 21);
            this.btnOpenAdbFile.TabIndex = 65;
            this.btnOpenAdbFile.Text = "...";
            this.btnOpenAdbFile.UseVisualStyleBackColor = true;
            this.btnOpenAdbFile.Click += new System.EventHandler(this.btnOpenAdbFile_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(-2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(127, 13);
            this.label29.TabIndex = 46;
            this.label29.Text = "Номер получателя СМС";
            // 
            // tbAdb
            // 
            this.tbAdb.Location = new System.Drawing.Point(1, 79);
            this.tbAdb.Name = "tbAdb";
            this.tbAdb.Size = new System.Drawing.Size(270, 20);
            this.tbAdb.TabIndex = 64;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(1, 16);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(301, 20);
            this.textBox12.TabIndex = 47;
            // 
            // rbtnSmsEmail
            // 
            this.rbtnSmsEmail.AutoSize = true;
            this.rbtnSmsEmail.Location = new System.Drawing.Point(6, 240);
            this.rbtnSmsEmail.Name = "rbtnSmsEmail";
            this.rbtnSmsEmail.Size = new System.Drawing.Size(237, 17);
            this.rbtnSmsEmail.TabIndex = 49;
            this.rbtnSmsEmail.Text = "Отправлять SMS через E-mail (SMS-шлюз)";
            this.rbtnSmsEmail.UseVisualStyleBackColor = true;
            this.rbtnSmsEmail.CheckedChanged += new System.EventHandler(this.rbtnSmsEmail_CheckedChanged);
            // 
            // rbtnSmsAdb
            // 
            this.rbtnSmsAdb.AutoSize = true;
            this.rbtnSmsAdb.Checked = true;
            this.rbtnSmsAdb.Location = new System.Drawing.Point(3, 3);
            this.rbtnSmsAdb.Name = "rbtnSmsAdb";
            this.rbtnSmsAdb.Size = new System.Drawing.Size(168, 17);
            this.rbtnSmsAdb.TabIndex = 41;
            this.rbtnSmsAdb.TabStop = true;
            this.rbtnSmsAdb.Text = "Отправлять SMS через ADB";
            this.rbtnSmsAdb.UseVisualStyleBackColor = true;
            this.rbtnSmsAdb.CheckedChanged += new System.EventHandler(this.rbtnSmsAdb_CheckedChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label27.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label27.Location = new System.Drawing.Point(326, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(292, 25);
            this.label27.TabIndex = 40;
            this.label27.Text = "Параметры отправления SMS";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(197, 109);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(35, 13);
            this.label26.TabIndex = 39;
            this.label26.Text = "Порт:";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(198, 125);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(58, 20);
            this.textBox8.TabIndex = 27;
            this.textBox8.Text = "465";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(4, 125);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(188, 20);
            this.textBox7.TabIndex = 26;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(1, 109);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(79, 13);
            this.label21.TabIndex = 25;
            this.label21.Text = "SMTP-сервер:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(116, 28);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(192, 20);
            this.textBox6.TabIndex = 24;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(99, 13);
            this.label20.TabIndex = 23;
            this.label20.Text = "Имя отправителя:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label19.Location = new System.Drawing.Point(3, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(307, 25);
            this.label19.TabIndex = 22;
            this.label19.Text = "Параметры отправления E-mail";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(573, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(498, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(66, 23);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.ckSmsNoti);
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.ckSmsEmail);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.btnOpenLogsFolder);
            this.splitContainer1.Panel1.Controls.Add(this.label27);
            this.splitContainer1.Panel1.Controls.Add(this.btnOpenMapsFolder);
            this.splitContainer1.Panel1.Controls.Add(this.btnOpenCfg);
            this.splitContainer1.Panel1.Controls.Add(this.tbLogsFolder);
            this.splitContainer1.Panel1.Controls.Add(this.tbMapsFolder);
            this.splitContainer1.Panel1.Controls.Add(this.tbConfigFile);
            this.splitContainer1.Panel1.Controls.Add(this.label19);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnLicense);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btnOk);
            this.splitContainer1.Size = new System.Drawing.Size(646, 435);
            this.splitContainer1.SplitterDistance = 401;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 20;
            // 
            // checkBox3
            // 
            this.ckSmsNoti.AutoSize = true;
            this.ckSmsNoti.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckSmsNoti.Location = new System.Drawing.Point(327, 28);
            this.ckSmsNoti.Name = "checkBox3";
            this.ckSmsNoti.Size = new System.Drawing.Size(205, 17);
            this.ckSmsNoti.TabIndex = 63;
            this.ckSmsNoti.Text = "Оповещать посредством SMS";
            this.ckSmsNoti.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rbtnSmsAdb);
            this.panel4.Controls.Add(this.rbtnSmsEmail);
            this.panel4.Controls.Add(this.pnlSmsEmail);
            this.panel4.Controls.Add(this.pnlSmsAdb);
            this.panel4.Location = new System.Drawing.Point(327, 51);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(313, 333);
            this.panel4.TabIndex = 62;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.textBox8);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.textBox7);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.ckSsl);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.textBox9);
            this.panel3.Controls.Add(this.textBox6);
            this.panel3.Controls.Add(this.tbSmtpPassword);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Location = new System.Drawing.Point(3, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(313, 152);
            this.panel3.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "E-mail получателя:";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(281, 80);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(27, 21);
            this.button7.TabIndex = 59;
            this.button7.Text = "👁";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.btnHidePassword_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(116, 1);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(192, 20);
            this.textBox1.TabIndex = 58;
            // 
            // checkBox1
            // 
            this.ckSsl.AutoSize = true;
            this.ckSsl.Checked = true;
            this.ckSsl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckSsl.Location = new System.Drawing.Point(262, 128);
            this.ckSsl.Name = "checkBox1";
            this.ckSsl.Size = new System.Drawing.Size(46, 17);
            this.ckSsl.TabIndex = 56;
            this.ckSsl.Text = "SSL";
            this.ckSsl.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.ckSmsEmail.AutoSize = true;
            this.ckSmsEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckSmsEmail.Location = new System.Drawing.Point(7, 28);
            this.ckSmsEmail.Name = "checkBox2";
            this.ckSmsEmail.Size = new System.Drawing.Size(205, 17);
            this.ckSmsEmail.TabIndex = 60;
            this.ckSmsEmail.Text = "Оповещать посредтвом e-mail";
            this.ckSmsEmail.UseVisualStyleBackColor = true;
            // 
            // btnLicense
            // 
            this.btnLicense.Location = new System.Drawing.Point(7, 3);
            this.btnLicense.Name = "btnLicense";
            this.btnLicense.Size = new System.Drawing.Size(140, 23);
            this.btnLicense.TabIndex = 56;
            this.btnLicense.Text = "Показать лицензию";
            this.btnLicense.UseVisualStyleBackColor = true;
            this.btnLicense.Click += new System.EventHandler(this.btnLicense_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "config.xml";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "adb.exe";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 435);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Settings";
            this.Text = "Настройки приложения";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.pnlSmsEmail.ResumeLayout(false);
            this.pnlSmsEmail.PerformLayout();
            this.pnlSmsAdb.ResumeLayout(false);
            this.pnlSmsAdb.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Label label2;
        private TextBox tbLogsFolder;
        private TextBox tbMapsFolder;
        private TextBox tbConfigFile;
        private Label label4;
        private Label label3;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btnOpenLogsFolder;
        private Button btnOpenMapsFolder;
        private Button btnOpenCfg;
        private Button btnCancel;
        private Button btnOk;
        private Label label5;
        private SplitContainer splitContainer1;
        private ToolTip toolTip1;
        private TextBox textBox9;
        private TextBox textBox8;
        private TextBox textBox7;
        private Label label21;
        private TextBox textBox6;
        private Label label20;
        private Label label19;
        private Label label24;
        private Label label23;
        private Label label26;
        private RadioButton rbtnSmsAdb;
        private Label label27;
        private TextBox textBox12;
        private Label label29;
        private TextBox textBox13;
        private Label label30;
        private RadioButton rbtnSmsEmail;
        private TextBox tbSmtpPassword;
        private Panel pnlSmsAdb;
        private Panel pnlSmsEmail;
        private FolderBrowserDialog fbdLogsFolder;
        private FolderBrowserDialog fbdMapsFolder;
        private Button btnLicense;
        private OpenFileDialog openFileDialog1;
        private CheckBox ckSsl;
        private TextBox textBox1;
        private Label label1;
        private Button button7;
        private Panel panel3;
        private CheckBox ckSmsEmail;
        private Panel panel4;
        private CheckBox ckSmsNoti;
        private CheckBox ckEmailTrans;
        private CheckBox ckSmsTrans;
        private Label label6;
        private Button btnOpenAdbFile;
        private TextBox tbAdb;
        private RichTextBox richTextBox1;
        private OpenFileDialog openFileDialog2;
    }
}