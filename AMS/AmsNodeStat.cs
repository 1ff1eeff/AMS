using System;

namespace AMS
{
    /// <summary>
    /// Класс для хранения и управления информацией о работе узла сети.
    /// </summary>
    public class AmsNodeStat
    {
        //=========================================
        // Определяем свойства класса AmsNodeStat. 
        //=========================================

        /// <summary>
        /// Имя узла.
        /// </summary>

        private string _name;

        /// <summary>
        /// Имя узла на карте.
        /// </summary>

        private string _nameOnMap;

        /// <summary>
        /// IP-адрес узла.
        /// </summary>

        private string _ip;

        /// <summary>
        ///  Количество успешных ответов на запрос.
        /// </summary>

        private uint _succed;

        /// <summary>
        /// Количество запросов без ответа.
        /// </summary>

        private uint _failed;

        /// <summary>
        /// Общее время простоя.
        /// </summary>

        private double _offlineTime;

        /// <summary>
        /// Общее время в сети.
        /// </summary>

        private double _onlineTime;

        /// <summary>
        /// Общее время работы.
        /// </summary>

        private double _totalTime;

        /// <summary>
        /// Процент простоя.
        /// </summary>

        private double _offlinePercent;

        /// <summary>
        /// Время работы узла.
        /// </summary>

        private DateTime _time;

        /// <summary>
        /// Время финального опроса.
        /// </summary>

        private DateTime _finishTime;

        /// <summary>
        /// ID узла.
        /// </summary>

        private string _id;

        // Конструктор класса.
        // Инициализация свойств.

        public AmsNodeStat() 
        {

            // Устанавливаем значение свойства "Имя узла".

            Name = "";

            // Устанавливаем значение свойства "Имя узла на карте".

            NameOnMap = "";

            // Устанавливаем значение свойства "IP-адрес узла".

            Ip = "";

            // Устанавливаем значение свойства "Количество успешных ответов на запрос".

            Succed = 0;

            // Устанавливаем значение свойства "Количество запросов без ответа".

            Failed = 0;

            // Устанавливаем значение свойства "Общее время простоя".

            OfflineTime = 0;

            // Устанавливаем значение свойства "Общее время в сети".

            OnlineTime = 0;

            // Устанавливаем значение свойства "Обновляем общее время работы".

            TotalTime = 0;

            // Устанавливаем значение свойства "Процент простоя".

            OfflinePercent = 0;

            // Устанавливаем значение свойства "Время работы узла".

            Time = DateTime.Now;

            // Устанавливаем значение свойства "Время финального опроса".

            FinishTime = DateTime.Now;

            // Устанавливаем значение свойства "ID узла".

            Id = "";
        }

        //============================================================
        // Определяем аксессоры – методы доступа к свойствам класса. 
        //============================================================ 

        /// <summary>
        /// Определяет методы доступа к значению свойства "Имя узла на карте".
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Имя узла на карте".
        /// </summary>
        public string NameOnMap { get => _nameOnMap; set => _nameOnMap = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "IP-адрес узла".
        /// </summary>
        public string Ip { get => _ip; set => _ip = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Количество успешных ответов на запрос".
        /// </summary>
        public uint Succed { get => _succed; set => _succed = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Количество запросов без ответа".
        /// </summary>
        public uint Failed { get => _failed; set => _failed = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Общее время простоя".
        /// </summary>
        public double OfflineTime { get => _offlineTime; set => _offlineTime = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Общее время в сети".
        /// </summary>
        public double OnlineTime { get => _onlineTime; set => _onlineTime = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Процент простоя".
        /// </summary>
        public double OfflinePercent { get => _offlinePercent; set => _offlinePercent = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Время работы узла".
        /// </summary>
        public DateTime Time { get => _time; set => _time = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "ID узла".
        /// </summary>
        public string Id { get => _id; set => _id = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Общее время работы".
        /// </summary>
        public double TotalTime { get => _totalTime; set => _totalTime = value; }

        /// <summary>
        /// Определяет методы доступа к значению свойства "Время финального опроса".
        /// </summary>
        public DateTime FinishTime { get => _finishTime; set => _finishTime = value; }


    }
}
