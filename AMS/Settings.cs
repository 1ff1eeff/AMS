using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AMS
{
    /// <summary>
    /// Форма "Настройки приложения".
    /// </summary>
    public partial class Settings : Form
    {
        // Объявляем и инициализируем объект класса AmsSettings для хранения настроек приложения.

        public AmsSettings amsSettings = new AmsSettings();

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public Settings()
        {
            // Инициализация компонентов.

            InitializeComponent();
        }

        /// <summary>
        /// Кнопка "..." – выбрать файл конфигурации приложения.
        /// </summary>
        private void btnOpenCfg_Click(object sender, EventArgs e)
        {
            // Открываемая по умолчанию папка – путь до исполняемого файла без имени файла.

            openFileDialog1.InitialDirectory = Application.StartupPath;

            // Открываем диалоговое окно выбора файла.

            DialogResult dr = openFileDialog1.ShowDialog();

            // Если получен ответ: "ОК".

            if (dr == DialogResult.OK)
            {
                // Передаём имя выбранного файла в TextBox "Файл конфигурации приложения".

                tbConfigFile.Text = openFileDialog1.FileName;
            }
        }

        /// <summary>
        /// Кнопка "..." – выбрать директорию хранения карт.
        /// </summary>
        private void btnOpenMapsFolder_Click(object sender, EventArgs e)
        {
            // Открываемая по умолчанию папка – текущая рабочая папка + "\Maps".

            fbdMapsFolder.SelectedPath = amsSettings.MapsFolder;

            // Открываем диалоговое окно выбора папки.

            DialogResult dr = fbdMapsFolder.ShowDialog();

            // Если получен ответ: "ОК".

            if (dr == DialogResult.OK)
            {
                // Передаём имя выбранной в TextBox "Директория хранения карт".

                tbMapsFolder.Text = fbdMapsFolder.SelectedPath;
            }
        }

        /// <summary>
        /// Кнопка "..." – выбрать директорию хранения журналов мониторинга.
        /// </summary>
        private void btnOpenLogsFolder_Click(object sender, EventArgs e)
        {
            // Открываемая по умолчанию папка – текущая рабочая папка + "\Logs".

            fbdLogsFolder.SelectedPath = amsSettings.LogsFolder;

            // Открываем диалоговое окно выбора папки.

            DialogResult dr = fbdLogsFolder.ShowDialog();

            // Если получен ответ: "ОК".

            if (fbdLogsFolder.ShowDialog() == DialogResult.OK)
            {
                // Передаём имя выбранной папки в TextBox "Директория хранения журналов мониторинга".

                tbLogsFolder.Text = fbdLogsFolder.SelectedPath;
            }
        }

        /// <summary>
        /// Кнопка "..." – указать путь до adb.exe (Android Debug Bridge).
        /// </summary>
        private void btnOpenAdbFile_Click(object sender, EventArgs e)
        {
            // Если задан путь до исполняемого файла Android Debug Bridge (adb.exe).

            if(!String.IsNullOrWhiteSpace(amsSettings.AdbFile))
            {
                // Объявляем и инициализируем объект класса FileInfo для хранения пути к файлу.

                FileInfo file = new FileInfo(amsSettings.AdbFile);

                // Открываемая по умолчанию папка – полный путь к каталогу, содержащему файл "adb.exe".

                openFileDialog2.InitialDirectory = file.DirectoryName;
            }

            // Открываем диалоговое окно выбора файла.

            DialogResult dr = openFileDialog2.ShowDialog();

            // Если получен ответ: "ОК".

            if (dr == DialogResult.OK)
            {
                // Передаём имя выбранного файла в TextBox "Путь до adb.exe (Android Debug Bridge)". 

                tbAdb.Text = openFileDialog2.FileName;
            }
        }

        /// <summary>
        /// Кнопка "Отправлять SMS через ADB".
        /// Событие "Состояние флаговой кнопки изменилось".
        /// </summary>
        private void rbtnSmsAdb_CheckedChanged(object sender, EventArgs e)
        {
            // Активируем панель "Отправлять SMS через ADB".
            
            pnlSmsAdb.Enabled = true;

            // Деактивируем панель "Отправлять SMS через E-mail (SMS-шлюз)".

            pnlSmsEmail.Enabled = false;
        }

        /// <summary>
        /// Кнопка "Отправлять SMS через E-mail (SMS-шлюз)".
        /// Событие "Состояние флаговой кнопки изменилось".
        /// </summary>
        private void rbtnSmsEmail_CheckedChanged(object sender, EventArgs e)
        {
            // Активируем панель "Отправлять SMS через E-mail (SMS-шлюз)".

            pnlSmsAdb.Enabled = false;

            // Деактивируем панель "Отправлять SMS через ADB".

            pnlSmsEmail.Enabled = true;
        }

        /// <summary>
        /// Кнопка "ОК".
        /// Сохраняем настройки приложения.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Заполняем поле "Почта пользователя, закреплённого за АСМ" значением поля для ввода "E-mail получателя".

            amsSettings.MailTo = textBox1.Text;

            // Заполняем поле "Имя отправителя" значением поля для ввода "Имя отправителя".

            amsSettings.SmtpSenderName = textBox6.Text;

            // Заполняем поле "Почта отправителя" значением поля для ввода "E-mail отправителя".

            amsSettings.SmtpSenderEmail = textBox9.Text;

            // Заполняем поле "Пароль почты/приложения" значением поля для ввода "Пароль".

            amsSettings.SmtpPassword = tbSmtpPassword.Text;

            // Заполняем поле "Почтовый сервер" значением поля для ввода "SMTP-сервер".

            amsSettings.SmtpHost = textBox7.Text;

            // Заполняем поле "Порт сервера" значением поля для ввода "Порт".

            amsSettings.SmtpPort = Int32.Parse(textBox8.Text);

            // Заполняем поле "Протокол SSL для шифрования соединения" значением флаговой кнопки "SSL".

            amsSettings.Ssl = ckSsl.Checked;

            // Заполняем поле "Уведомления посредством e-mail" значением флаговой кнопки "Оповещать посредтвом e-mail".

            amsSettings.EmailNotification = ckSmsEmail.Checked;

            // Заполняем поле "Номер получателя SMS" значением поля для ввода "Номер получателя СМС".

            amsSettings.PhoneToSMS = textBox12.Text;

            // Заполняем поле "E-mail шлюз для SMS" значением поля для ввода "SMS-шлюз оператора".

            amsSettings.EmailToSMS = textBox13.Text;

            // Заполняем поле "Требуется транслитерация email" значением флаговой кнопки "Транслитерировать сообщение".

            amsSettings.EmailNeedsTranslit = ckEmailTrans.Checked;

            // Заполняем поле "Требуется транслитерация SMS" значением флаговой кнопки "Транслитерировать сообщение".

            amsSettings.SmsNeedsTranslit = ckSmsTrans.Checked;

            // Заполняем поле "Уведомления посредством SMS" значением флаговой кнопки.

            amsSettings.SmsNotification = ckSmsNoti.Checked;

            // Если отмечена флаговая кнопка "Отправлять SMS через ADB".

            if (rbtnSmsAdb.Checked)
            {
                // Заполняем поле "Способ отправки SMS" значением "adb".

                amsSettings.WayToSendSms = "adb";
            }

            // Если отмечена флаговая кнопка "Отправлять SMS через E-mail (SMS-шлюз)". 

            if (rbtnSmsEmail.Checked)
            {
                // Заполняем поле "Способ отправки SMS" значением "email".

                amsSettings.WayToSendSms = "email";
            }

            // Заполняем поле "Путь до файла "adb.exe""
            // значением поля для ввода "Путь до adb.exe (Android Debug Bridge)".

            amsSettings.AdbFile = tbAdb.Text;

            // Получаем настройки приложения из XML файла.
            // Тип данных для сериализации – AmsSettings.

            XmlSerializer formatter = new XmlSerializer(typeof(AmsSettings));

            // Открываем файл хранящий настройки приложения.
            // Имя файла получено из значения поля для ввода "Файл конфигурации приложения".

            using (FileStream fs = new FileStream(tbConfigFile.Text, FileMode.Create))
            {
                // Сохраняем настройки приложения в XML формат.

                formatter.Serialize(fs, amsSettings);

                // Закрываем файл.

                fs.Close();
            }

            // Открываем основной файл хранящий настройки приложения.
            // Имя файла: путь до исполняемого файла без имени плюс "\config.xml".

            using (FileStream fs = new FileStream(Application.StartupPath + "\\config.xml", FileMode.Create))
            {
                // Сохраняем настройки приложения в XML формат.

                formatter.Serialize(fs, amsSettings);

                // Закрываем файл.

                fs.Close();
            }

            // Закрываем форму.

            Close();
        }

        /// <summary>
        /// Кнопка "Отмена".
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Закрываем форму.

            Close();
        }

        /// <summary>
        /// Кнопка "Показать лицензию".
        /// </summary>
        private void btnLicense_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр формы "Показать лицензионную информацию". 

            ShowLicense showLicense = new ShowLicense();

            // Открываем форму.

            showLicense.Show();
        }

        /// <summary>
        /// Кнопка "👁" – скрыть текст пароля.
        /// </summary>
        private void btnHidePassword_Click(object sender, EventArgs e)
        {
            // Переключает состояние видимости текста в поле ввода пароля.

            tbSmtpPassword.UseSystemPasswordChar = !tbSmtpPassword.UseSystemPasswordChar;
        }

        /// <summary>
        /// Событие "Форма загружена".
        /// Восстанавливаем настройки приложения.
        /// </summary>
        private void Settings_Load(object sender, EventArgs e)
        {
            // Получаем значение поля для ввода "E-mail получателя"
            // из поля параметров настроек приложения "". 

            textBox1.Text = amsSettings.MailTo;

            // Получаем значение поля для ввода "Имя отправителя"
            // из поля параметров настроек приложения "Имя отправителя". 

            textBox6.Text = amsSettings.SmtpSenderName;

            // Получаем значение поля для ввода "E-mail отправителя"
            // из поля параметров настроек приложения "Почта отправителя". 

            textBox9.Text = amsSettings.SmtpSenderEmail;

            // Получаем значение поля для ввода "Пароль"
            // из поля параметров настроек приложения "Пароль почты/приложения". 

            tbSmtpPassword.Text = amsSettings.SmtpPassword;

            // Получаем значение поля для ввода "SMTP-сервер"
            // из поля параметров настроек приложения "Почтовый сервер". 

            textBox7.Text = amsSettings.SmtpHost;

            // Получаем значение поля для ввода "Порт"
            // из поля параметров настроек приложения "Порт сервера". 

            textBox8.Text = amsSettings.SmtpPort.ToString();

            // Получаем значение поля для ввода "Номер получателя СМС"
            // из поля параметров настроек приложения "Номер получателя SMS". 

            textBox12.Text = amsSettings.PhoneToSMS;

            // Получаем значение поля для ввода "SMS-шлюз оператора"
            // из поля параметров настроек приложения "E-mail шлюз для SMS". 

            textBox13.Text = amsSettings.EmailToSMS;

            // Получаем значение поля для ввода "Путь до adb.exe (Android Debug Bridge)"
            // из поля параметров настроек приложения "Путь до файла "adb.exe"". 

            tbAdb.Text = amsSettings.AdbFile;

            // Анализируем выбранный способ отправки смс.

            switch (amsSettings.WayToSendSms)
            {
                // Если выбран вариант "adb".

                case "adb":

                    // Активируем флаговую кнопку "Отправлять SMS через ADB".

                    rbtnSmsAdb.Checked = true;

                    // Покидаем switch.

                    break;

                // Если выбран вариант "email".

                case "email":

                    // Активируем флаговую кнопку "Отправлять SMS через E-mail (SMS-шлюз)".

                    rbtnSmsEmail.Checked = true;

                    // Покидаем switch.

                    break;

                default:

                    // Покидаем switch.

                    break;
            }

            // Получаем значение флаговой кнопки "SSL"
            // из поля параметров настроек приложения "Протокол SSL для шифрования соединения".

            ckSsl.Checked = amsSettings.Ssl;

            // Получаем значение флаговой кнопки "Оповещать посредтвом e-mail"
            // из поля параметров настроек приложения "Уведомления посредством e-mail".

            ckSmsEmail.Checked = amsSettings.EmailNotification;

            // Получаем значение флаговой кнопки "Оповещать посредством SMS"
            // из поля параметров настроек приложения "Уведомления посредством SMS".

            ckSmsNoti.Checked = amsSettings.SmsNotification;

            // Получаем значение флаговой кнопки "Транслитерировать сообщение"
            // из поля параметров настроек приложения "Требуется транслитерация SMS".

            ckSmsTrans.Checked = amsSettings.SmsNeedsTranslit;

            // Получаем значение флаговой кнопки "Транслитерировать сообщение"
            // из поля параметров настроек приложения "Требуется транслитерация e-mail".

            ckEmailTrans.Checked = amsSettings.EmailNeedsTranslit;

            // Задаём значением поля для ввода "Файл конфигурации приложения"
            // путь до исполняемого файла без имени файла плюс "\config.xml".

            tbConfigFile.Text = Application.StartupPath + "\\config.xml";

            // Задаём значением поля для ввода "Директория хранения карт"
            // путь до исполняемого файла без имени файла плюс "\Maps".

            tbMapsFolder.Text = Application.StartupPath + "\\Maps";

            // Задаём значением поля для ввода "Директория хранения журналов мониторинга"
            // путь до исполняемого файла без имени файла плюс "\Logs"

            tbLogsFolder.Text = Application.StartupPath + "\\Logs";
        }

        
    }
}
