using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class Agent_Queue
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string InitialCode { get;  set; }
        public bool isEdit { get; set; }
        public List<Agent_Queue> Agentlist { get; set; }
    }
}