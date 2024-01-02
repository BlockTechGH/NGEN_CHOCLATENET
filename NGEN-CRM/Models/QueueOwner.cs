using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class QueueOwner
    {
        public int Id { get; set; }
        public string AgentIds { get; set; }
        public string[] QueueIds { get; set; }
        //public List<string> QueueNos { get; set; }
        public string[] QueueNos { get; set; }
        public string ExtensionNo { get; set; }
        public List<QueueOwnerList> queueownerList { get; set; }

    }

    public class QueueOwnerList
    {
        public int QueueID { get; set; }
        public string AgentName { get; set; }
        public string Extension { get; set; }
        public string QueueNumber { get; set; }
    }
}