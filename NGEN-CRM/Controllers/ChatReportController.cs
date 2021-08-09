using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class ChatReportController : Controller
    {
        public ActionResult Report()
        {
            Chat obj = new Chat();
            ChatDbLink objch = new ChatDbLink();
            ViewBag.ShowDiv = false;
            ViewBag.Message = "Your contact page.";
            DataTable dt = new DataTable();
            //obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd");
            //obj.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            //obj.CallList = HomeRepository.GetCallSummaryReport(obj);
            ViewBag.Agents = new SelectList(objch.getAllAgent(), "AgentName", "AgentName");
            ViewBag.Queue = new SelectList(objch.getAllQueues(), "AgentName", "AgentName");
            return View(obj);

        }
        public ActionResult Create(Chat obj, string btn)
        {
            ChatDbLink objCh = new ChatDbLink();
            ViewBag.SelectedYear = obj.AgentIds;
            List<Agent> selectedItems = new List<Agent>();
            List<Agent> selectedQItems = new List<Agent>();

            DataTable dt = new DataTable();
            if (obj.Duration == "1")
            {
                obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            }
            else if (obj.Duration == "2")
            {
                obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

            }
            else if (obj.Duration == "3")
            {
                obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = DateTime.Now.AddDays(-15).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

            }
            else if (obj.Duration == "4")
            {
                obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = DateTime.Now.AddDays(-30).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

            }
            else
            {
                obj.ToDate = Convert.ToDateTime(obj.ToDate).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = Convert.ToDateTime(obj.FromDate).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

            }
            if (obj.AgentIds != null)
            {

                obj.Agents = objCh.getAllAgent();
                selectedItems = obj.Agents.Where(p => obj.AgentIds.Contains((string)(p.AgentName))).ToList();
            }
            if (obj.QueueIds != null)
            {
                obj.Queues = objCh.getAllQueues();
                selectedQItems = obj.Queues.Where(p => obj.QueueIds.Contains((string)(p.AgentName))).ToList();
            }
            if (obj.Report == "1")
            {
                obj.ChatList = objCh.GetChatSummary(obj);
            }
            if (obj.Report == "2")
            {
                if (selectedItems.Count != 0)
                {
                    obj.ChatList = objCh.GetChatDetailsReport(obj);
                    obj.ChatList = obj.ChatList.Where(o => obj.AgentIds.Contains((string)(o.AgentName))).ToList();

                }
                if (selectedQItems.Count != 0)
                {
                    obj.ChatList = objCh.GetChatQueueDetailsReport(obj);
                    obj.ChatList = obj.ChatList.Where(o => obj.QueueIds.Contains((string)(o.QueueName))).ToList();
                }
                if (obj.ChatType == "1")
                {
                    obj.ChatList = obj.ChatList.Where(o => o.ChatType == "Inbound").ToList();
                }
                else if (obj.ChatType == "2")
                {
                    obj.ChatList = obj.ChatList.Where(o => o.ChatType == "Outbound").ToList();
                }
                else
                {
                    obj.ChatList = obj.ChatList;
                }



            }
           
            if (obj.Report == "3")
            {
                obj.ChatList = objCh.GetAgentData(obj);
                if (selectedItems.Count != 0)
                {
                    obj.ChatList = obj.ChatList.Where(o => obj.AgentIds.Contains((string)(o.AgentName))).ToList();
                }
            }
            if (obj.Report == "5")
            {
                obj.ChatList = objCh.GetQueueSummary(obj);
                if (selectedQItems.Count != 0)
                {
                    obj.ChatList = obj.ChatList.Where(o => obj.QueueIds.Contains((string)(o.QueueName))).ToList();
                }
            }
            //ViewBag.Agents = new MultiSelectList(objCh.getAllAgent(), "AgentName", "AgentName", obj.AgentIds);
            ViewBag.Queue = new MultiSelectList(objCh.getAllQueues(), "AgentName", "AgentName", obj.QueueIds);
            ViewBag.Agents = new SelectList(objCh.getAllAgent(), "AgentName", "AgentName");
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();
            if (obj.ChatList != null)
            {

                dt2.Columns.Add("Total Conversation", typeof(string));
                dt2.Columns.Add("Total Messages", typeof(string));
                dt2.Columns.Add("Avg.Response Time", typeof(string));
                dt2.Columns.Add("Avg.Initial Response Time ", typeof(string));
                foreach (var call in obj.ChatList)
                {
                    dt2.Rows.Add(new Object[]{
                            call.TotalConversation,
                            call.TotalMsg,
                            call.TotalAvgRespTime,
                            call.InitRespTime
                    });
                }
                dt3.Columns.Add("Employee", typeof(string));
                dt3.Columns.Add("Messages", typeof(string));
                dt3.Columns.Add("Type", typeof(string));
                dt3.Columns.Add("Date ", typeof(string));
                dt3.Columns.Add("Avg.Resp.Time", typeof(string));
                dt3.Columns.Add("Initial Resp.Time", typeof(string));
                dt3.Columns.Add("Sup.Rating", typeof(string));
                foreach (var call in obj.ChatList)
                {
                    dt3.Rows.Add(new Object[]{
                            call.Agent,
                            call.Messages,
                            call.ChatType,
                            call.CreatedOn,
                            call.AvgRespTime,
                            call.InitRespTime,
                             call.SuprRating
                    });
                }

                dt4.Columns.Add("Agent Name", typeof(string));
                dt4.Columns.Add("Total Conv.", typeof(string));
                dt4.Columns.Add("Total Messages", typeof(string));
                dt4.Columns.Add("Inbound", typeof(string));
                dt4.Columns.Add("Outbound", typeof(string));
                dt4.Columns.Add("Avg.ResTime", typeof(string));
                dt4.Columns.Add("Avg.Initial_ResTime", typeof(string));
                dt4.Columns.Add("Supervisr_Rating", typeof(string));
                foreach (var call in obj.ChatList)
                {
                    dt4.Rows.Add(new Object[]{
                            call.Agent,
                            call.TotalConversation,
                            call.Messages,
                            call.InbConv,
                            call.OutConv,
                            call.AvgRespTime,
                            call.InitRespTime,
                            call.SuprRating,
                    });
                }

                dt5.Columns.Add("Channel", typeof(string));
                dt5.Columns.Add("Inbound", typeof(string));
                dt5.Columns.Add("Outbound", typeof(string));
                dt5.Columns.Add("Messages", typeof(string));
                dt5.Columns.Add("Supervisor_Rating", typeof(string));
                foreach (var call in obj.ChatList)
                {
                    dt5.Rows.Add(new Object[]{
                            call.QueueName,
                            call.InbConv,
                            call.OutConv,
                            call.Messages,
                            call.SuprRating,
                    });
                }
               
            }
         
            if (btn == "Excel")
            {
                string fileName = "ChatSummary.xlsx";
                dt2.TableName = "ChatSummary";
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


                string fileName = "ChatDetailReport.xlsx";
                dt3.TableName = "ChatDetailReport";
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
           
            if (btn == "Excel3")
            {


                string fileName = "AgentChatReport.xlsx";
                dt4.TableName = "AgentChatReport";
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
            if (btn == "Excel4")
            {


                string fileName = "ChannelReport.xlsx";
                dt5.TableName = "ChannelCallReport";
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

            if (btn == "PDF")
            {
                ExportToPdf(dt2, "Chat Summary Report");
            }
            else if (btn == "PDF1")
            {
                ExportToPdf(dt3, "Chat Details Report");
            }
            else if (btn == "PDF3")
            {
                ExportToPdf(dt4, "Agent Chat Report");
            }
            else if (btn == "PDF4")
            {
                ExportToPdf(dt5, "Channel Report");
            }
            return View("Report", obj);


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
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=" + Title + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".pdf");
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
        //public DataTable ConvertToDataTable<T>(IList<T> data)
        //{
        //    PropertyDescriptorCollection properties =
        //       TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    foreach (PropertyDescriptor prop in properties)
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    foreach (T item in data)
        //    {
        //        DataRow row = table.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //        table.Rows.Add(row);
        //    }
        //    return table;

        //}
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