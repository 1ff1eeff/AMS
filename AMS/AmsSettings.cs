using System;

namespace AMS
{
    /// <summary>
    /// Класс представляющий настройки приложения.
    /// </summary>
    public class AmsSettings
    {
        //=========================================
        // Определяем свойства класса AmsSettings. 
        //=========================================

        /// <summary>
        /// Имя файла конфигурации приложения.
        /// </summary>

        private string _configFile = "config.xml";

        /// <summary>
        /// Директория хранения карт.
        /// </summary>

        private string _mapsFolder = Environment.CurrentDirectory + "\\Maps";

        /// <summary>
        /// Директория хранения журналов мониторинга.
        /// </summary>

        private string _logsFolder = Environment.CurrentDirectory + "\\Logs";

        /// <summary>
        /// Уведомления посредством e-mail.
        /// </summary>

        private bool _emailNotification = false;

        /// <summary>
        /// Уведомления посредством SMS.
        /// </summary>

        private bool _smsNotification = false;

        /// <summary>
        /// Способ отправки SMS (через "adb" или "email").
        /// </summary>

        private string _wayToSendSms = "email";

        /// <summary>
        /// Путь до файла "adb.exe".
        /// </summary>

        private string _adbFile = "";

        /// <summary>
        /// Номер получателя SMS.
        /// </summary>

        private string _phoneToSMS = "";

        /// <summary>
        /// Транслитерация SMS.
        /// </summary>

        private bool _smsNeedsTranslit = false;

        /// <summary>
        /// E-mail шлюз для SMS.
        /// </summary>

        private string _emailToSMS = "";

        /// <summary>
        /// Транслитерация SMS.
        /// </summary>

        private bool _emailNeedsTranslit = false;

        /// <summary>
        /// Почта отправителя.
        /// </summary>

        private string _smtpSenderEmail = "automaticmonitoringsystem@internet.ru";

        /// <summary>
        /// Имя отправителя.
        /// </summary>

        private string _smtpSenderName = "ACM";

        /// <summary>
        /// Пароль почты/приложения.
        /// </summary>

        private string _smtpPassword = "iihtsdh97tgwHFnfnC7g";

        /// <summary>
        /// Почтовый сервер. 
        /// </summary>

        private string _smtpHost = "smtp.mail.ru";

        /// <summary>
        /// Порт сервера.
        /// </summary>

        private int _smtpPort = 25;

        /// <summary>
        /// Время за которое должно отправиться письмо (мс).
        /// </summary>

        private int _smtpTimeout = 300000;

        /// <summary>
        /// Протокол SSL для шифрования соединения.
        /// </summary>

        private bool _ssl = true;

        /// <summary>
        /// Почта пользователя, закреплённого за АСМ.
        /// </summary>

        private string _mailTo = "amswatcher@internet.ru";

        //============================================================
        // Определяем аксессоры – методы доступа к свойствам класса. 
        //============================================================

        /// <summary>
        /// Определяет методы доступа к значению свойства "Имя файла конфигурации приложения".
        /// </summary>
        public string ConfigFile { get => _configFile; set => _configFile = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Директория хранения карт".
        /// </summary>
        public string MapsFolder { get => _mapsFolder; set => _mapsFolder = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Директория хранения журналов мониторинга".
        /// </summary>
        public string LogsFolder { get => _logsFolder; set => _logsFolder = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Уведомления посредством e-mail".
        /// </summary>
        public bool EmailNotification { get => _emailNotification; set => _emailNotification = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Уведомления посредством SMS".
        /// </summary>
        public bool SmsNotification { get => _smsNotification; set => _smsNotification = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Способ отправки SMS (через "adb" или "email")".
        /// </summary>
        public string WayToSendSms { get => _wayToSendSms; set => _wayToSendSms = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Путь до файла "adb.exe"".
        /// </summary>
        public string AdbFile { get => _adbFile; set => _adbFile = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Номер получателя SMS".
        /// </summary>
        public string PhoneToSMS { get => _phoneToSMS; set => _phoneToSMS = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Требуется транслитерация SMS".
        /// </summary>
        public bool SmsNeedsTranslit { get => _smsNeedsTranslit; set => _smsNeedsTranslit = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "E-mail шлюз для SMS".
        /// </summary>
        public string EmailToSMS { get => _emailToSMS; set => _emailToSMS = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Требуется транслитерация e-mail".
        /// </summary>
        public bool EmailNeedsTranslit { get => _emailNeedsTranslit; set => _emailNeedsTranslit = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Почта отправителя".
        /// </summary>
        public string SmtpSenderEmail { get => _smtpSenderEmail; set => _smtpSenderEmail = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Имя отправителя".
        /// </summary>
        public string SmtpSenderName { get => _smtpSenderName; set => _smtpSenderName = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Пароль почты/приложения".
        /// </summary>
        public string SmtpPassword { get => _smtpPassword; set => _smtpPassword = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Почтовый сервер".
        /// </summary>
        public string SmtpHost { get => _smtpHost; set => _smtpHost = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Порт сервера".
        /// </summary>
        public int SmtpPort { get => _smtpPort; set => _smtpPort = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Время за которое должно отправиться письмо (мс)".
        /// </summary>
        public int SmtpTimeout { get => _smtpTimeout; set => _smtpTimeout = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Протокол SSL для шифрования соединения".
        /// </summary>
        public bool Ssl { get => _ssl; set => _ssl = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Почта пользователя, закреплённого за АСМ".
        /// </summary>
        public string MailTo { get => _mailTo; set => _mailTo = value; }
    }
}
