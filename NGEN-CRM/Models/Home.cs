using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Models
{
    public class Home
    {
        [AllowHtml]
        public string GridHtml { get; set; }
        public string PhoneNo { get; set; }
        public string Report { get; set; }
        public string SLA { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Duration { get; set; }
        public string Inbound { get; set; }
        public string InboundAns { get; set; }
        public string Outbound { get; set; }
        public string Agent { get; set; }
        public string QueueName { get; set; }
        public string Missed { get; set; }
        public string QMissed { get; set; }
        public string IMissed { get; set; }
        public string Total { get; set; }
        public string FinalType { get; set; }

        public List<Home> CallList { get; set; }
        public List<QCall> QCallList { get; set; }
        public string CallDate { get;  set; }
        public string CallTime { get; set; }
        public string CallType { get;  set; }
        public List<object> Data { get; set; }
        public string Agentstring { get; set; }
        public string Qstring { get; set; }
        public string[] AgentIds { get; set; }
        public string[] QueueIds { get; set; }
        public List<Agent> Agents { get; set; }
        public List<Agent> Queues { get; set; }
        public string TotalCall { get;  set; }
    }
}