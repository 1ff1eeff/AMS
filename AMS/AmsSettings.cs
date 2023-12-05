using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS
{
    /// <summary>
    /// Класс представляющий настройки приложения.
    /// </summary>
    public class AmsSettings
    {

        // Файл конфигурации приложения

        private string configFile = "config.xml";

        // Директория хранения карт

        private string mapsFolder = Environment.CurrentDirectory + "\\Maps";

        // Директория хранения журналов мониторинга

        private string logsFolder = Environment.CurrentDirectory + "\\Logs";

        // Уведомления посредством e-mail

        private bool emailNotification = false;

        // Уведомления посредством SMS 

        private bool smsNotification = false;

        // Способ отправки SMS - через "adb" или "email"

        private string wayToSendSms = "email";

        // Путь до файла adb.exe

        private string adbFile = "";

        // Номер получателя SMS

        private string phoneToSMS = "";

        // Транслитерация SMS

        private bool smsNeedsTranslit = false;

        // E-mail шлюз для SMS

        private string emailToSMS = "";

        // Транслитерация SMS

        private bool emailNeedsTranslit = false;

        // Почта отправителя

        private string smtpSenderEmail = "automaticmonitoringsystem@internet.ru";

        // Имя отправителя

        private string smtpSenderName = "ACM";

        // Пароль почты/приложения

        private string smtpPassword = "iihtsdh97tgwHFnfnC7g";

        // Почтовый сервер 

        private string smtpHost = "smtp.mail.ru";

        // Порт сервера

        private int smtpPort = 25;

        // Время за которое должно отправиться письмо (мс)

        private int smtpTimeout = 300000;

        // Протокол SSL для шифрования соединения

        private bool ssl = true;

        // Почта пользователя, закреплённого за АСМ

        private string mailTo = "amswatcher@internet.ru";


        public AmsSettings() { }

        public string SmtpSenderEmail { get => smtpSenderEmail; set => smtpSenderEmail = value; }
        public string SmtpPassword { get => smtpPassword; set => smtpPassword = value; }
        public string SmtpHost { get => smtpHost; set => smtpHost = value; }
        public int SmtpPort { get => smtpPort; set => smtpPort = value; }
        public bool Ssl { get => ssl; set => ssl = value; }
        public int SmtpTimeout { get => smtpTimeout; set => smtpTimeout = value; }
        public string MailTo { get => mailTo; set => mailTo = value; }
        public string SmtpSenderName { get => smtpSenderName; set => smtpSenderName = value; }
        public string ConfigFile { get => configFile; set => configFile = value; }
        public string MapsFolder { get => mapsFolder; set => mapsFolder = value; }
        public string LogsFolder { get => logsFolder; set => logsFolder = value; }
        public bool EmailNotification { get => emailNotification; set => emailNotification = value; }
        public bool SmsNotification { get => smsNotification; set => smsNotification = value; }
        public string EmailToSMS { get => emailToSMS; set => emailToSMS = value; }
        public string PhoneToSMS { get => phoneToSMS; set => phoneToSMS = value; }
        public bool SmsNeedsTranslit { get => smsNeedsTranslit; set => smsNeedsTranslit = value; }
        public bool EmailNeedsTranslit { get => emailNeedsTranslit; set => emailNeedsTranslit = value; }
        public string WayToSendSms { get => wayToSendSms; set => wayToSendSms = value; }
        public string AdbFile { get => adbFile; set => adbFile = value; }
    }
}
