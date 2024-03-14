using Dapper;
using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class AgentDetailReportController : Controller
    {
        private Connection sqlConnectionString = new Connection();
        CLSCommen cLSCommen = new CLSCommen();

        // GET: AgentDetailReport
        public ActionResult Report()
        {
            List<AgentDetailReport> model = new List<AgentDetailReport>();
            try
            {
                using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    conn.Open();
                    model=conn.Query<AgentDetailReport>($@"select final_Dn Extension,time_Start CallStartTime,time_End CallEndTime,Duration,dial_No CalledNumber,final_Dispname CallRoutedCSQs,duration AS TalkTime,
                                                            case when LEN(from_Dn) = 5 then 'Inbound' when LEN(from_Dn) <> 5 then 'Outbound' end CallType
                                                            from cdr 
                                                            where final_Dn<>'' and LEN(final_Dn)=3 and time_Answered <> ''").ToList();
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                var msg= ex.Message;
            }

            ViewBag.UserMenu = cLSCommen.GetUserMenu(Convert.ToInt16(Session["UsmID"]));

            return View(model);
        }
    }
}