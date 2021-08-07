using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class Chat
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TotalConversation{ get; set; }
        public string TotalMsg { get; set; }
        public string TotalAvgRespTime { get; set; }
        public string Agent { get; set; }
        public List<Chat> ChatList { get; set; }
        public string Duration { get; set; }


        public string AgentName { get; set; }
        public string InbConv { get; set; }
        public string OutConv { get; set; }
        public string Messages { get; set; }
        public string AvgRespTime { get; set; }
        public string InitRespTime { get; set; }
        public double SuprRating { get; set; }
        public string TotalRating { get;set; }
    }
}