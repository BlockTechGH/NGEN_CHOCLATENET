using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class AgentDetailReport
    {
        //public string AgentName { get; set; }
        //public string AgentID { get; set; }
        public string Extension { get; set; }
        public string CallStartTime { get; set; }
        public string CallEndTime { get; set; }
        public string Duration { get; set; }
        public string CalledNumber { get; set; }
        
        public string CallRoutedCSQs { get; set; }
        
        
        //public string TalkTime { get; set; }
        
        public string CallType { get; set; }
    }
}