using System;

namespace AMS
{
    /// <summary>
    /// Класс для хранения и управления информацией об узлах сети.
    /// </summary>
    public class DNodeInfo
    {
        private string name = "";
        private string type = "";
        private string standart = "";
        private string protocol = "";
        private string ip = "";
        private string mac = "";
        private string[] services = Array.Empty<string>();
        private bool isSelected = false;

        /// <summary>
        /// Имя узла
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Тип узла
        /// </summary>
        public string Type { get => type; set => type = value; }

        /// <summary>
        /// Стандарт передачи данных
        /// </summary>
        public string Standart { get => standart; set => standart = value; }

        /// <summary>
        /// Протокол передачи данных
        /// </summary>
        public string Protocol { get => protocol; set => protocol = value; }

        /// <summary>
        /// IP-адрес узла
        /// </summary>
        public string Ip { get => ip; set => ip = value; }

        /// <summary>
        /// MAC-адрес узла
        /// </summary>
        public string Mac { get => mac; set => mac = value; }

        /// <summary>
        /// Отслеживаемые сервисы узла
        /// </summary>
        public string[] Services { get => services; set => services = value; }

        /// <summary>
        /// Узел выделен на карте
        /// </summary>
        public bool IsSelected { get => isSelected; set => isSelected = value; }
    }
}