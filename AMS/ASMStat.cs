using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS
{  
    public class ASMStat
    {        
        private string name;
        private string ip;
        private string checkType;
        private uint succed;
        private uint failed;
        private uint offlineTime;
        private uint onlineTime;
        private uint totalTime;
        private uint offlinePercent;
        private DateTime time;
        private DateTime finishTime;
        private string id;

        public ASMStat() 
        {
            Name = "";
            Ip = "";
            CheckType = "ICMP";
            Succed = 0;
            Failed = 0;
            OfflineTime = 0;
            OnlineTime = 0;
            TotalTime = 0;
            OfflinePercent = 0;
            Time = DateTime.Now;            
            FinishTime = DateTime.Now;
            Id = "";
        }

        public string Name { get => name; set => name = value; }
        public string Ip { get => ip; set => ip = value; }
        public string CheckType { get => checkType; set => checkType = value; }
        public uint Succed { get => succed; set => succed = value; }
        public uint Failed { get => failed; set => failed = value; }
        public uint OfflineTime { get => offlineTime; set => offlineTime = value; }
        public uint OnlineTime { get => onlineTime; set => onlineTime = value; }
        public uint OfflinePercent { get => offlinePercent; set => offlinePercent = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Id { get => id; set => id = value; }
        public uint TotalTime { get => totalTime; set => totalTime = value; }
        public DateTime FinishTime { get => finishTime; set => finishTime = value; }
    }
}
