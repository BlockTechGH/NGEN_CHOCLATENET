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
        public List<Agent> Agents { get; set; }
        public List<Agent> Queues { get; set; }
        public List<Channel> ChannelList { get; set; }
        public string Duration { get; set; }


        public string AgentName { get; set; }
        public string InbConv { get; set; }
        public string OutConv { get; set; }
        public string Messages { get; set; }
        public string AvgRespTime { get; set; }
        public string InitRespTime { get; set; }
        public double SuprRating { get; set; }
        public string TotalRating { get;set; }
        public string ChatType { get; set; }
        public string Report { get; set; }
        public string[] AgentIds { get; set; }
        public string[] QueueIds { get; set; }
        public string CreatedOn { get; set; }
        public string Channel { get;  set; }
        public string QueueName { get;  set; }
    }
}