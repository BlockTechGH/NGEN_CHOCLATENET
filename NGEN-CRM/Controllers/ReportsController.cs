using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace NGEN_CRM.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Report()
        {
            Home obj = new Home();
            ViewBag.ShowDiv = false;
            ViewBag.Message = "Your contact page.";
            DataTable dt = new DataTable();
            //obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd");
            //obj.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            //obj.CallList = HomeRepository.GetCallSummaryReport(obj);
            ViewBag.Agents = new SelectList(HomeRepository.getAllAgent(), "AgentName", "AgentName");
            ViewBag.Queue = new SelectList(HomeRepository.getAllQueueName(), "AgentName", "AgentName");
            return View(obj);
           
        }
        public ActionResult Create(Home obj,string btn)
        {
            ViewBag.SelectedYear = obj.AgentIds;
            List<Agent> selectedItems = new List<Agent>();
            List<Agent> selectedQItems = new List<Agent>();
            
            DataTable dt = new DataTable();
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
            else
            {
                obj.ToDate = Convert.ToDateTime(obj.ToDate).AddDays(-1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = Convert.ToDateTime(obj.FromDate).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

            }
            if (obj.AgentIds != null)
            {

                obj.Agents = HomeRepository.getAllAgent();
                selectedItems = obj.Agents.Where(p => obj.AgentIds.Contains((string)(p.AgentName))).ToList();
            }
            if (obj.QueueIds != null)
            {
                if (obj.QueueIds.Contains("IVR MissedCall"))
                {
                    obj.CallList = HomeRepository.GetIVRMissedCallReport(obj);
                    //obj.CallList = obj.CallList.Where(o => obj.QueueIds.Contains((string)(o.Agent))).ToList();
                }
                obj.Queues = HomeRepository.getAllQueueName();
                selectedQItems = obj.Queues.Where(p => obj.QueueIds.Contains((string)(p.AgentName))).ToList();
            }
            if (obj.Report=="1")
            {
                obj.CallList = HomeRepository.GetCallSummaryReport(obj);
            }
            if (obj.Report == "2")
            {
                obj.CallList = HomeRepository.GetCallDetailsReport(obj);
                if (selectedItems.Count != 0)
                {
                    obj.CallList = HomeRepository.GetCallDetailsReport(obj);
                    obj.CallList = obj.CallList.Where(o => obj.AgentIds.Contains((string)(o.Agent))).ToList();

                }
                if (selectedQItems.Count != 0)
                {
                    obj.CallList = HomeRepository.GetQueueCallDetailsReport(obj);
                    obj.CallList = obj.CallList.Where(o => obj.QueueIds.Contains((string)(o.QueueName))).ToList();
                }
                if (obj.CallType == "1")
                {
                    obj.CallList = obj.CallList.Where(o => o.CallType == "INBOUND").ToList();
                }
                else if(obj.CallType == "2")
                {
                    obj.CallList = obj.CallList.Where(o => o.CallType == "OUTBOUND").ToList();
                }
                else
                {
                    obj.CallList = obj.CallList;
                }

               
               
            }
            if (obj.Report == "4" )
            {
               
                
                if (selectedItems.Count != 0)
                {
                    obj.CallList = HomeRepository.GetMissedCallReport(obj);
                    obj.CallList = obj.CallList.Where(o => obj.AgentIds.Contains((string)(o.Agent))).ToList();
                }
                if (selectedQItems.Count != 0)
                {
                    if (obj.QueueIds.Contains("IVR MissedCall"))
                    {
                        obj.CallList  = HomeRepository.GetIVRMissedCallReport(obj);
                    }
                    else
                    {
                        obj.CallList = HomeRepository.GetMissedCallReport(obj).Where(o => obj.QueueIds.Contains((string)(o.QueueName))).ToList();

                    }
                }
            }
            if (obj.Report == "3")
            {
                //obj.CallList = HomeRepository.GetAgentReport(obj);
                if (selectedItems.Count != 0)
                {
                    obj.CallList = HomeRepository.GetAgentReport(obj);
                    obj.CallList = obj.CallList.Where(o => obj.AgentIds.Contains((string)(o.Agent))).ToList();
                    obj.TotalCall = obj.CallList.Sum(item => Convert.ToInt64(item.Total)).ToString();
                }
              

            }
            if (obj.Report == "5")
            {
                //obj.QCallList = HomeRepository.GetAgentQReportDashboard(obj);

                if (selectedQItems.Count != 0)
                {
                    obj.QCallList = HomeRepository.GetAgentQReportDashboard(obj);
                    obj.QCallList = obj.QCallList.Where(o => obj.QueueIds.Contains((string)(o.Agent))).ToList();
                    obj.TotalCall = obj.QCallList.Sum(item => Convert.ToInt64(item.QTotal)).ToString();

                    string totalAnswe= obj.QCallList.Sum(item => Convert.ToInt64(item.QAns)).ToString();
                    if (obj.TotalCall != "0")
                    {
                        obj.TotalSLA = ((Convert.ToDecimal(totalAnswe)) / (Convert.ToDecimal(obj.TotalCall))).ToString("0.00%");
                    }
                    //obj.TotalSLA= obj.QCallList.Average(item => Convert.ToDecimal(item.QSLA.Replace(@"%", ""))).ToString();                    
                    //obj.QCallList = obj.QCallList.Where(o => obj.QueueIds.Contains((string)(o.Agent))).ToList();
                }
              
            }
            //ViewBag.Agents = new MultiSelectList(HomeRepository.getAllAgent(), "AgentName", "AgentName", obj.AgentIds);
            ViewBag.Queue = new MultiSelectList(HomeRepository.getAllQueueName(), "AgentName", "AgentName", obj.QueueIds);


            ViewBag.Agents = new SelectList(HomeRepository.getAllAgent(), "AgentName", "AgentName");
            //var selected = new[] { 1, 3, 7 };
            //var categories = HomeRepository.getAllAgent().AsEnumerable()
            //                .Select(c => new SelectListItem
            //                {
            //                    Value = c.AgentName.ToString(),
            //                    Text = c.AgentName.ToString(),
            //                    Selected = obj.AgentIds.Contains((string)(c.AgentName))
            //                }).ToList();
            //ViewBag.Agents = new SelectList(categories, "AgentName", "AgentName", ViewBag.SelectedYear);
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();
            if (obj.CallList!=null)
            {
             
                dt2.Columns.Add("Inbound Answered", typeof(string));
                dt2.Columns.Add("Inbound Missed ", typeof(string));
               
                dt2.Columns.Add("Total Inbound", typeof(string));
                dt2.Columns.Add("SLA% ", typeof(string));
                dt2.Columns.Add("Outbound", typeof(string));
                dt2.Columns.Add("Total ", typeof(string));
                foreach (var call in obj.CallList)
                {
                    dt2.Rows.Add(new Object[]{
                            call.InboundAns,
                            call.Missed,
                          
                            call.Inbound,
                            call.SLA,
                            call.Outbound,
                            call.Total
                    });
                }
               
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
              
                dt4.Columns.Add("Phone Number", typeof(string));
                dt4.Columns.Add("Call Date", typeof(string));
                dt4.Columns.Add("Call Time", typeof(string));
                dt4.Columns.Add("AgentName", typeof(string));
                dt4.Columns.Add("QueueName", typeof(string));
                foreach (var call in obj.CallList)
                {
                    dt4.Rows.Add(new Object[]{
                            call.PhoneNo,
                            call.CallDate,
                            call.CallTime,
                            call.Agent,
                            call.QueueName
                    });
                }
                
                dt5.Columns.Add("AgentName", typeof(string));
                dt5.Columns.Add("Inbound Answered", typeof(string));
                dt5.Columns.Add("Inbound Missed", typeof(string));
                dt5.Columns.Add("Total Inbound", typeof(string));
                dt5.Columns.Add("SLA%", typeof(string));
                dt5.Columns.Add("Outbound", typeof(string));
                dt5.Columns.Add("Total", typeof(string));
                foreach (var call in obj.CallList)
                {
                    dt5.Rows.Add(new Object[]{
                            call.Agent,
                            call.InboundAns,
                            call.Missed,
                            call.Inbound,
                            call.SLA,
                            call.Outbound,
                            call.Total
                    });
                }
                dt7.Columns.Add("Phone Number", typeof(string));
                dt7.Columns.Add("Call Date", typeof(string));
                dt7.Columns.Add("Call Time", typeof(string));
                foreach (var call in obj.CallList)
                {
                    dt7.Rows.Add(new Object[]{
                            call.PhoneNo,
                            call.CallDate,
                            call.CallTime,
                            
                    });
                }
                
            }
            if(obj.QCallList!=null)
            {
                dt6.Columns.Add("QueueName", typeof(string));
                dt6.Columns.Add("Inbound", typeof(string));
                dt6.Columns.Add("Queue Missed ", typeof(string));
                dt6.Columns.Add("Queue Answered", typeof(string));
                dt6.Columns.Add("SLA%", typeof(string));
                foreach (var call in obj.QCallList)
                {
                    dt6.Rows.Add(new Object[]{
                            call.Agent,
                            call.QTotal,
                            call.QMissed,
                            call.QAns,
                            call.QSLA
                    });
                }
            }


           
            if (btn == "Excel")
            {               
                string fileName = "CallSummary.xlsx";
                dt2.TableName = "CallSummary";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt2);
                   
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.ms-excel", fileName);
                        
                    }

                }
            }
            if (btn == "Excel1")
            {
              
               
                string fileName = "CallDetailReport.xlsx";
                dt3.TableName = "CallDetailReport";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt3);
                   
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.ms-excel", fileName);

                    }

                }
            }
            if (btn == "Excel2")
            {
               
               
                string fileName = "MissedCallReport.xlsx";
                dt4.TableName = "MissedCallReport";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt4);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.ms-excel", fileName);

                    }

                }
            }
            if (btn == "Excel3")
            {
               
               
                string fileName = "AgentCallReport.xlsx";
                dt5.TableName = "AgentCallReport";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt5);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.ms-excel", fileName);

                    }

                }
            }
            if (btn == "Excel4")
            {


                string fileName = "QueueCallReport.xlsx";
                dt6.TableName = "QueueCallReport";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt6);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.ms-excel", fileName);

                    }

                }
            }
            if (btn == "Excel5")
            {


                string fileName = "IVRMissedCallReport.xlsx";
                dt7.TableName = "IVRMissedCallReport";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt7);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.ms-excel", fileName);

                    }

                }
            }
            if (btn=="PDF")
            {
                ExportToPdf(dt2,"Call Summary Report");
            }
            else if(btn == "PDF1")
            {
                ExportToPdf(dt3, "Call Details Report");
            }
            else if (btn == "PDF2")
            {
                ExportToPdf(dt4, "Missed Call Report");
            }
            else if (btn == "PDF3")
            {
                ExportToPdf(dt5, "Agent Call Report");
            }
            else if (btn == "PDF4")
            {
                ExportToPdf(dt6, "Queue Call Report");
            }
            else if (btn == "PDF5")
            {
                ExportToPdf(dt7, "IVR Missed Call Report");
            }
            return View("Report", obj);


        }
        public void ExportToPdf(DataTable myDataTable,string Title)
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
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename="+ Title+ DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".pdf");
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
        public ActionResult WriteDataToExcel(DataTable dt)
        {
            //DataTable dt = ConvertToDataTable();
            //Name of File  
            string fileName = "Sample.xlsx";
            dt.TableName = "abc";
            using (XLWorkbook wb = new XLWorkbook())
            {

                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    return File(stream.ToArray(), "application/vnd.ms-excel", fileName);
                    //Response.AddHeader("content-disposition", "attachment; filename=" + myName);
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // or "application/vnd.ms-excel"
                }
            }
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        public bool WriteDataTableToExcel(System.Data.DataTable dataTable, string worksheetName, string saveAsLocation, string ReporType)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            try
            {
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = true;
                excel.DisplayAlerts = true;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;


                excelSheet.Cells[1, 1] = ReporType;
                excelSheet.Cells[1, 2] = "Date : " + DateTime.Now.ToShortDateString();

                // loop through each row and add values to our sheet
                int rowcount = 2;

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= dataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 3)
                        {
                            excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
                            excelSheet.Cells.Font.Color = System.Drawing.Color.Black;

                        }

                        excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

                        //for alternate rows
                        if (rowcount > 3)
                        {
                            if (i == dataTable.Columns.Count)
                            {
                                if (rowcount % 2 == 0)
                                {
                                    excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                    FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                                }

                            }
                        }

                    }

                }

                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;


                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[2, dataTable.Columns.Count]];
                FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);


                //now save the workbook and exit Excel


                excelworkBook.SaveAs(saveAsLocation); ;
                //excelworkBook.Close();
                //excel.Quit();
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }

        }
        public void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }
    }
}