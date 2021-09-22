using Newtonsoft.Json;
using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
//using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Timers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace NGEN_CRM.Controllers
{
  
    public class HomeController : Controller
    {
    
        public  ActionResult Contact()
        {
           
            //Main(string[] args);
            Home obj = new Home();
            ViewBag.ShowDiv = false;
            ViewBag.Message = "Your contact page.";
            obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj.FromDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj = HomeRepository.GetData(obj);
            obj.CallList = HomeRepository.GetAgentData(obj);
            obj.QCallList = HomeRepository.GetAgentQReportDashboard(obj);
            int TotalINBOUND = Convert.ToInt32(obj.Inbound);
            int TotalOUTBOUND = Convert.ToInt32(obj.Outbound);
            int TotalMissed = Convert.ToInt32(obj.Missed);
            int Total = TotalINBOUND + TotalOUTBOUND;
            ViewBag.Missed = TotalMissed;
            ViewBag.Inbound = TotalINBOUND-TotalMissed;
            ViewBag.Outbound = TotalOUTBOUND;
            ViewBag.Total = Total;
            return View(obj);
        }
      
        public ActionResult Create(Home obj,string btn)
        {
           
                DataTable dt = new DataTable();
                if (obj.Duration == null)
                {
                obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                }
                if (obj.Duration == "1")
                {
                    obj.ToDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                    obj.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                }
                else if (obj.Duration == "2")
                {
                    obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                    obj.FromDate = DateTime.Now.AddDays(-6).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

                }
                else if (obj.Duration == "3")
                {
                    obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                    obj.FromDate = DateTime.Now.AddDays(-14).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

                }
                else if (obj.Duration == "4")
                {
                    obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                    obj.FromDate = DateTime.Now.AddDays(-29).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

                }
                else if (obj.Duration == "5")
                {
                    obj.ToDate = Convert.ToDateTime(obj.ToDate).AddDays(-1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                    obj.FromDate = Convert.ToDateTime(obj.FromDate).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

                }
                obj = HomeRepository.GetData(obj);
                int TotalINBOUND = Convert.ToInt32(obj.Inbound);
                int TotalOUTBOUND = Convert.ToInt32(obj.Outbound);
                int Missed = Convert.ToInt32(obj.Missed);
                ViewBag.Inbound = TotalINBOUND - Missed;
                ViewBag.Outbound = TotalOUTBOUND;
                ViewBag.Missed = Missed;
                ViewBag.Total = TotalINBOUND + TotalOUTBOUND;

            if (btn == "submit")
            {

                obj.CallList = HomeRepository.GetAgentData(obj);

                obj.QCallList = HomeRepository.GetAgentQReportDashboard(obj);
                return View("contact", obj);
            }
            else if(btn== "CallDetailsIn")
            {
                obj.CallList = HomeRepository.GetCallDetailsReport(obj).Where(o => o.CallType == "INBOUND").ToList(); 
                //obj.CallList = obj.CallList.Where(o => o.CallType == "INBOUND").ToList();
                DataTable dt3 = new DataTable();
                dt3.Columns.Add("Phone Number", typeof(string));
                dt3.Columns.Add("Call Date", typeof(string));
                dt3.Columns.Add("Call Time", typeof(string));
                dt3.Columns.Add("Duration ", typeof(string));
                dt3.Columns.Add("CallType", typeof(string));
                dt3.Columns.Add("AgentName", typeof(string));
                dt3.Columns.Add("QueName", typeof(string));
                foreach (var call in obj.CallList)
                {
                    dt3.Rows.Add(new Object[]{
                            call.PhoneNo,
                            call.CallDate,
                            call.CallTime,
                            call.Duration,
                            call.CallType,
                            call.Agent,
                            call.QueueName
                    });
                }
                ExportToPdf(dt3, "Inbound Call Details");
                return View("contact", obj);
            }
            else if (btn == "CallDetailsOut")
            {
                obj.CallList = HomeRepository.GetCallDetailsReport(obj).Where(o => o.CallType == "OUTBOUND").ToList();
                //obj.CallList = obj.CallList.Where(o => o.CallType == "INBOUND").ToList();
                DataTable dt3 = new DataTable();
                dt3.Columns.Add("Phone Number", typeof(string));
                dt3.Columns.Add("Call Date", typeof(string));
                dt3.Columns.Add("Call Time", typeof(string));
                dt3.Columns.Add("Duration ", typeof(string));
                dt3.Columns.Add("CallType", typeof(string));
                dt3.Columns.Add("AgentName", typeof(string));
                foreach (var call in obj.CallList)
                {
                    dt3.Rows.Add(new Object[]{
                            call.PhoneNo,
                            call.CallDate,
                            call.CallTime,
                            call.Duration,
                            call.CallType,
                            call.Agent,
                    });
                }
                ExportToPdf(dt3, "Outbound Call Details");
                return View("contact", obj);
            }
            else if (btn == "CallDetailsMissed")
            {
                DataTable dt7 = new DataTable();
                obj.CallList = HomeRepository.GetMissedCallReport(obj).Where(o => o.Agent != "").ToList();
                dt7.Columns.Add("Phone Number", typeof(string));
                dt7.Columns.Add("Call Date", typeof(string));
                dt7.Columns.Add("Call Time", typeof(string));
                dt7.Columns.Add("Agent", typeof(string));
                foreach (var call in obj.CallList)
                {
                    dt7.Rows.Add(new Object[]{
                            call.PhoneNo,
                            call.CallDate,
                            call.CallTime,
                            call.Agent
                    });
                }
                ExportToPdf(dt7, "Missed Call Details");
                return View("contact", obj);
            }
            else if (btn == "CallDetailsTot")
            {
                obj.CallList = HomeRepository.GetCallDetailsReport(obj).Where(o => o.CallType == "OUTBOUND" ||o.CallType== "INBOUND").ToList();
               
                DataTable dt3 = new DataTable();
                dt3.Columns.Add("Phone Number", typeof(string));
                dt3.Columns.Add("Call Date", typeof(string));
                dt3.Columns.Add("Call Time", typeof(string));
                dt3.Columns.Add("Duration ", typeof(string));
                dt3.Columns.Add("CallType", typeof(string));
                dt3.Columns.Add("AgentName", typeof(string));
                dt3.Columns.Add("QueName", typeof(string));
                foreach (var call in obj.CallList)
                {
                    dt3.Rows.Add(new Object[]{
                            call.PhoneNo,
                            call.CallDate,
                            call.CallTime,
                            call.Duration,
                            call.CallType,
                            call.Agent,
                            call.QueueName
                    });
                }
                ExportToPdf(dt3, "Total Call");
                return View("contact", obj);
            }
            else { return View("contact", obj); }
        }
        public void ExportToPdf(DataTable myDataTable, string Title)
        {
            DataTable dt = myDataTable;
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            Font font13 = FontFactory.GetFont("ARIAL", 10);
            Font font18 = FontFactory.GetFont("ARIAL", 12);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                if (dt.Rows.Count > 0)
                {
                    PdfPTable PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = 200f;
                    PdfTable.LockedWidth = true;

                    PdfPCell PdfPCell = new PdfPCell(new Phrase(new Chunk(Title, font18)));
                    PdfPCell.Border = Rectangle.NO_BORDER;
                    PdfTable.AddCell(PdfPCell);
                    DrawLine(writer, 25f, pdfDoc.Top - 30f, pdfDoc.PageSize.Width - 25f, pdfDoc.Top - 30f, new BaseColor(System.Drawing.Color.Red));
                    pdfDoc.Add(PdfTable);

                    PdfTable = new PdfPTable(dt.Columns.Count);
                    PdfTable.SpacingBefore = 20f;
                    for (int columns = 0; columns <= dt.Columns.Count - 1; columns++)
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Columns[columns].ColumnName, font13)));
                        PdfTable.AddCell(PdfPCell);
                    }

                    for (int rows = 0; rows <= dt.Rows.Count - 1; rows++)
                    {
                        for (int column = 0; column <= dt.Columns.Count - 1; column++)
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), font13)));
                            PdfTable.AddCell(PdfPCell);
                        }
                    }
                    pdfDoc.Add(PdfTable);
                }
                pdfDoc.Close();
                //return pdfDoc;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline; filename=" + Title + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc);
                Response.Flush();
                Response.End();
            }
            catch (DocumentException de)
            {
            }
            // System.Web.HttpContext.Current.Response.Write(de.Message)
            catch (IOException ioEx)
            {
            }
            // System.Web.HttpContext.Current.Response.Write(ioEx.Message)
            catch (Exception ex)
            {
            }
        }
        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
        public FileResult GetReport()
        {
            string ReportURL = @"C: \Users\sulusulu\Downloads\Call Details Report22920210000.pdf";
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application /pdf");
        }
    }
}