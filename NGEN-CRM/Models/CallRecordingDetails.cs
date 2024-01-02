using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class CallRecordingDetails
    {
        public string SearchDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<CallRecordingsList> CallRecordingList { get; set; }
        public string[] QueueIds { get; set; }
        public string[] AgentIds { get; set; }
    }

    public class CallRecordingsList
    {
        public string RecordingDate { get; set; }//common

        public string CustomerNo { get; set; }//callernumber
        public string AgentName { get; set; }//internal
        
        public string QueueName { get; set; }//external
        public string QueueOwner { get; set; }
        public string FileName { get; set; }//common
        public string RecordingSequence { get; set; }//common
        public string AgentExtension { get; set; }//common
    }
}