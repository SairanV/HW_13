using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ServiceType { get; set; }
        public bool IsVip { get; set; }
        public int Priority { get; set; }

        public Client(int id, string name, string serviceType, bool isVip, int priority)
        {
            Id = id;
            Name = name;
            ServiceType = serviceType;
            IsVip = isVip;
            Priority = priority;
        }
    }
}
