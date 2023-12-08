using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS
{
    //[Serializable]
    public class AmsLog
    {
        private string name;
        private string ip;
        private string status;
        private string rtt;
        private string pl;
        private string services;
        private string date;

        public AmsLog() { }

        /// <summary>
        /// Имя узла.
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// IP-адрес узла.
        /// </summary>
        public string Ip { get => ip; set => ip = value; }

        /// <summary>
        /// Статус доступности узла.
        /// </summary>
        public string Status { get => status; set => status = value; }

        /// <summary>
        /// 
        /// </summary>
        public string Rtt { get => rtt; set => rtt = value; }



        public string Pl { get => pl; set => pl = value; }
        


        public string Services { get => services; set => services = value; }
        public string Date { get => date; set => date = value; }
    }
}
