using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class ExtensionACD_IBOB_Log
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<ExtensionACD_IBOB_LogList> modelList { get; set; }
    }

    public class ExtensionACD_IBOB_LogList
    {
        public int ID { get; set; }

        public string ExtensionNumber { get; set; }

        public string InboundACD_Calls { get; set; }

        public string InboundExtension_Calls { get; set; }

        public string TotalInboundCalls { get; set; }

        public string ACDAvgTalktime { get; set; }

        public string ExtensionAvgTalktime { get; set; }

        public string AvgTalktime { get; set; }

        public string TotalTalktime { get; set; }

        public string OutboundExternalCalls { get; set; }

        public string OutboundExtensionCalls { get; set; }

        public string TotalOutboundCalls { get; set; }

        public string OutboundAvgTalktime { get; set; }

        public string TotalOutboundTalktime { get; set; }

        public string TotalCalls { get; set; }

        public string Date { get; set; }
    }
}