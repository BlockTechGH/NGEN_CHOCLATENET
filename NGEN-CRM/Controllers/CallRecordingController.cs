using Dapper;
using NGEN_CRM.Models;
using NLog;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace NGEN_CRM.Controllers
{
    public class CallRecordingController : Controller
    {
        private Connection sqlConnectionString = new Connection();
        CLSCommen cLSCommen = new CLSCommen();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // GET: CallRecording
        public ActionResult Report()
        {
            //var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            logger.Info("started");
            CallRecordingDetails model = new CallRecordingDetails();
            using (var conn=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                conn.Open();
                try
                {
                    logger.Info("before fetching rec files");
                    var filenames = HomeRepository.GetCallRecordingDetails();
                    logger.Info("after fetching rec files");
                    foreach(var filename in filenames)
                    {
                        CallRecordingsList model1 = new CallRecordingsList();
                        model1.FileName = filename;
                        var split = filename.Split(']');
                        if(split[0].Contains("%3A"))
                        {
                            var pattern = "%3A";
                            var firstsplit1 = Regex.Replace(split[0], pattern, " ");

                            var firstsplit2 = firstsplit1.Split(' ');
                            //var queuename = firstsplit2[2];
                            model1.QueueName = firstsplit2[2];
                            //var externalnumber = firstsplit2[1];//customer
                            model1.CustomerNo = firstsplit2[1];//customer

                            var d = split[1].Split('_');
                            var d1 = d[1].Split('-');
                            //var agentno = d1[1];
                            model1.AgentExtension = d1[1];

                            var d2 = d[2].Split('(');
                            DateTime RecordingDate = DateTime.ParseExact(d2[0], "yyyyMMddHHmmss", null);
                            //model1.RecordingDate = dateTime.ToString();
                            

                            //DateTime myDateTime = DateTime.ParseExact(d2[0], "yyyyMMddHHmmss", null);
                            //model1.RecordingDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                            var d3 = d2[1].Split(')');
                            //var recordsequence = d3[0];
                            model1.RecordingSequence = d3[0];

                            //model.CallRecordingList.Add(model1);
                            var exist = conn.Query<int>($"select COUNT(*) from CallRecording where RecordingSequence = '{model1.RecordingSequence}' and RecordingDate='{RecordingDate}'").FirstOrDefault();
                            if (exist == 0)
                            {
                                var insert = conn.Execute($@"INSERT INTO [dbo].[CallRecording]
                                                       ([RecordingDate]
                                                       ,[AgentExtension]
                                                       ,[CustomerNo]
                                                       ,[AgentName]
                                                       ,[QueueName]
                                                       ,[QueueOwner]
                                                       ,[FileName]
                                                       ,[RecordingSequence])
                                                 VALUES
                                                       ('{RecordingDate}'
                                                       ,'{model1.AgentExtension}'
                                                       ,'{model1.CustomerNo}'
                                                       ,'-'
                                                       ,'{model1.QueueName}'
                                                       ,'-'
                                                       ,'{model1.FileName}'
                                                       ,'{model1.RecordingSequence}')");
                            }

                            
                        }
                        else
                        {
                            var firstsplit = split[0].Trim('[');
                            //var agentname = firstsplit;
                            model1.AgentName = firstsplit;
                            var d = split[1].Split('_');
                            var d1 = d[1].Split('-');
                            //var agentno = d1[1];
                            model1.AgentExtension= d1[0];
                            //var callernumber = d1[0];
                            model1.CustomerNo = d1[1];

                            var d2 = d[2].Split('(');
                            //DateTime dateTime = DateTime.ParseExact(d2[0], "yyyyMMddHHmmss", null);
                            //model1.RecordingDate = DateTime.ParseExact(d2[0], "yyyyMMddHHmmss", null).ToString();
                            DateTime RecordingDate = DateTime.ParseExact(d2[0], "yyyyMMddHHmmss", null);
                            var d3 = d2[1].Split(')');
                            //var recordsequence = d3[0];
                            model1.RecordingSequence = d3[0];

                            //model.CallRecordingList.Add(model1);

                            var exist = conn.Query<int>($"select COUNT(*) from CallRecording where RecordingSequence = '{model1.RecordingSequence}' and RecordingDate='{RecordingDate}'").FirstOrDefault();
                            if (exist == 0)
                            {
                                var insert = conn.Execute($@"INSERT INTO [dbo].[CallRecording]
                                                       ([RecordingDate]
                                                       ,[AgentExtension]
                                                       ,[CustomerNo]
                                                       ,[AgentName]
                                                       ,[QueueName]
                                                       ,[QueueOwner]
                                                       ,[FileName]
                                                       ,[RecordingSequence])
                                                 VALUES
                                                       ('{RecordingDate}'
                                                       ,'{model1.AgentExtension}'
                                                       ,'{model1.CustomerNo}'
                                                       ,'{model1.AgentName}'
                                                       ,'-'
                                                       ,'-'
                                                       ,'{model1.FileName}'
                                                       ,'{model1.RecordingSequence}')");
                            }

                            
                        }
                    }

                    model.CallRecordingList = new List<CallRecordingsList>();
                    if(Session["Usmname"].ToString() == "Admin")
                    {
                        model.CallRecordingList = conn.Query<CallRecordingsList>($"select * from CallRecording order by RecordingDate desc").ToList();
                    }
                    else
                    {
                        string agentextension = conn.Query<string>($"select Code from agent_queue where Name='{Session["Usmname"]}'").FirstOrDefault();
                        var queueNos = conn.Query<string>($"select QueueNumber from QueueTable where Extension='{agentextension.Replace("Ext.", "")}'").FirstOrDefault();
                        model.CallRecordingList = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension where cr.AgentExtension= '{agentextension.Replace("Ext.", "")}' order by cr.RecordingDate desc").ToList();
                        if (queueNos.Contains(','))
                        {
                            var qNo = queueNos.Split(',').ToList();
                            foreach( var q in qNo )
                            {
                                var qName = conn.Query<string>($"select Name from agent_queue where Code like '%{q}%'").FirstOrDefault();
                                List<CallRecordingsList> obj = new List<CallRecordingsList>();
                                obj = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension where cr.QueueName='{qName}' order by cr.RecordingDate desc").ToList();
                                model.CallRecordingList.AddRange(obj);
                            }
                        }
                        else
                        {
                            var qName = conn.Query<string>($"select Name from agent_queue where Code like '%{queueNos}%'").FirstOrDefault();
                            List<CallRecordingsList> obj = new List<CallRecordingsList>();
                            obj = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension where cr.QueueName='{qName}' order by cr.RecordingDate desc").ToList();
                            model.CallRecordingList.AddRange(obj);

                        }
                        //var queuenames = conn.Query<string>($"select distinct(QueueName) from CallRecording where AgentExtension='{agentextension.Replace("Ext.", "")}' and QueueName<>'-'").ToList();
                        
                        //model.CallRecordingList = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension inner join agent_queue aq on REPLACE(aq.Code, 'Ext.', '')=cr.AgentExtension where cr.AgentExtension= '{agentextension.Replace("Ext.", "")}' and aq.Type='A' order by cr.RecordingDate desc").ToList();
                        //foreach (var qname in queuenames)
                        //{
                        //    List<CallRecordingsList> obj = new List<CallRecordingsList>();
                        //    var QueueCode = conn.Query<string>($"select Code from agent_queue where Name='{qname}'").FirstOrDefault();
                        //    obj = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension inner join agent_queue aq on REPLACE(aq.Code, 'Ext.', '')=cr.AgentExtension where CHARINDEX('{QueueCode}', qt.QueueNumber)>0 and aq.Type='Q' order by cr.RecordingDate desc").ToList();
                        //    model.CallRecordingList.AddRange(obj);
                        //}


                        //model.CallRecordingList = conn.Query<CallRecordingsList>($"select * from CallRecording where AgentName='{Session["Usmname"]}' order by RecordingDate asc").ToList();
                    }
                    
                    ViewBag.Agents = new SelectList(HomeRepository.getAllAgent(), "AgentName", "AgentName");
                    ViewBag.Queue = new SelectList(HomeRepository.getAllQueueName(), "AgentName", "AgentName");
                }
                catch(Exception e)
                {
                    ViewBag.Message(e);
                    logger.Info(e.Message);
                }
                conn.Close();
            }

            //SideMenuName
            //using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            //{
            //    conn.Open();
            //    var menuIds = conn.Query<string>($"select MenuIDs from UserMenuAccessNgen where UserID={Session["UsmID"]}").FirstOrDefault();
            //    ViewBag.UserMenu = conn.Query<SideMenuNgen>($"select * from SideMenuNgen where MenuID in ({menuIds})").ToList();
            //    conn.Close();
            //}
            ViewBag.UserMenu = cLSCommen.GetUserMenu(Convert.ToInt16(Session["UsmID"]));
            return View(model);
        }

        public JsonResult GetCallRecordingsByFromDateToDate(DateTime fromdate,DateTime todate)
        {
            List<CallRecordingsList> model = new List<CallRecordingsList>();
            using (var conn=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                conn.Open();
                if (Session["Usmname"].ToString() == "Admin")
                {
                    model = conn.Query<CallRecordingsList>($"select * from CallRecording where RecordingDate>='{Convert.ToDateTime(fromdate).ToShortDateString()}' and RecordingDate<='{Convert.ToDateTime(todate).ToShortDateString()}' order by RecordingDate desc").ToList();
                }
                else
                {
                    string agentextension = conn.Query<string>($"select Code from agent_queue where Name='{Session["Usmname"]}'").FirstOrDefault();
                    var queueNos = conn.Query<string>($"select QueueNumber from QueueTable where Extension='{agentextension.Replace("Ext.", "")}'").FirstOrDefault();
                    model = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension where cr.AgentExtension= '{agentextension.Replace("Ext.", "")}' and RecordingDate>='{Convert.ToDateTime(fromdate).ToString("dd-MM-yyyy")}' and RecordingDate<='{Convert.ToDateTime(todate).ToString("dd-MM-yyyy")}' order by cr.RecordingDate desc").ToList();
                    if (queueNos.Contains(','))
                    {
                        var qNo = queueNos.Split(',').ToList();
                        foreach (var q in qNo)
                        {
                            var qName = conn.Query<string>($"select Name from agent_queue where Code like '%{q}%'").FirstOrDefault();
                            List<CallRecordingsList> obj = new List<CallRecordingsList>();
                            obj = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension where cr.QueueName='{qName}' and RecordingDate>='{Convert.ToDateTime(fromdate).ToString("dd-MM-yyyy")}' and RecordingDate<='{Convert.ToDateTime(todate).ToString("dd-MM-yyyy")}' order by cr.RecordingDate desc").ToList();
                            model.AddRange(obj);
                        }
                    }
                    else
                    {
                        var qName = conn.Query<string>($"select Name from agent_queue where Code like '%{queueNos}%'").FirstOrDefault();
                        List<CallRecordingsList> obj = new List<CallRecordingsList>();
                        obj = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension where cr.QueueName='{qName}' and RecordingDate>='{Convert.ToDateTime(fromdate).ToString("dd-MM-yyyy")}' and RecordingDate<='{Convert.ToDateTime(todate).ToString("dd-MM-yyyy")}' order by cr.RecordingDate desc").ToList();
                        model.AddRange(obj);

                    }
                }
                    
                conn.Close();
            }
            return Json(model);
        }

        //public static async Task DownloadAudioFile(string filename)
        //{
        //    string remoteUrl = "http://192.168.1.112:8080//path/to/your/audiofile.mp3";
        //    string localPath = "C:\\path\\to\\save\\audiofile.mp3";

        //    await DownloadFileAsync(remoteUrl, localPath);

        //    Console.WriteLine("File downloaded successfully.");
        //}

        //public static async Task DownloadFileAsync(string remoteUrl, string localPath)
        //{
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        try
        //        {
        //            byte[] fileData = await httpClient.GetByteArrayAsync(remoteUrl);
        //            System.IO.File.WriteAllBytes(localPath, fileData);
        //        }
        //        catch (HttpRequestException e)
        //        {
        //            Console.WriteLine($"Error downloading file: {e.Message}");
        //        }
        //    }
        //}

        public ActionResult DownloadAudio(string filename)
        {
            // Replace "path_to_your_audio_file.wav" with the actual path to your WAV audio file on the server.
            string folderPath = "";

            #if DEBUG
                folderPath = @"C:\Recordings"; // Path of the test source file
            #else
                folderPath = @"C:\ProgramData\3CX\Instance1\Data\Recordings"; // Path of the real source file
            #endif
            string[] subdirectories = Directory.GetDirectories(folderPath);

            string filePath = "";
            string fileName = "";

            //List<string> filenames = new List<string>();
            //Display details for each subdirectory
            foreach (string subdirectory in subdirectories)
            {
                List<string> audioFiles = new List<string>();
                if (Directory.Exists(subdirectory))
                {
                    // Retrieve audio files (change the search pattern if needed)
                    //string[] audioFiles = Directory.GetFiles(folderPath, "*.mp3");
                    //string[] audioExtensions = { "*.mp3", "*.wav", "*.ogg", "*.flac", "*.aac", "*.wma" };
                    string[] audioExtensions = { "*.wav" };

                    foreach (string extension in audioExtensions)
                    {
                        audioFiles.AddRange(Directory.GetFiles(subdirectory, extension));
                    }
                    foreach (string audioFile in audioFiles)
                    {
                        string FileName = Path.GetFileName(audioFile);
                        if(FileName==filename)
                        {
                            // Replace "path_to_your_audio_file.wav" with the actual path to your WAV audio file on the server.
                            filePath = audioFile;

                            // Replace "desired_audio_filename.wav" with the desired filename that the client will see.
                            fileName = filename;
                        }
                        //filenames.Add(fileName);
                    }
                }
            }

            //string filePath = Server.MapPath($"~/Content/Audio/your_audio_file.wav");

            // Replace "desired_audio_filename.wav" with the desired filename that the client will see.
            //string fileName = "desired_audio_filename.wav";

            return File(filePath, "audio/wav", fileName);
        }

        public ActionResult PlayAudio(string filename)
        {
            // Replace "path_to_your_audio_file.wav" with the actual path to your WAV audio file on the server.
            string folderPath = "";

            #if DEBUG
                        folderPath = @"C:\Recordings"; // Path of the test source file
            #else
                        folderPath = @"C:\ProgramData\3CX\Instance1\Data\Recordings"; // Path of the real source file
            #endif
            string[] subdirectories = Directory.GetDirectories(folderPath);

            string filePath = "";
            string fileName = "";

            //List<string> filenames = new List<string>();
            //Display details for each subdirectory
            foreach (string subdirectory in subdirectories)
            {
                List<string> audioFiles = new List<string>();
                if (Directory.Exists(subdirectory))
                {
                    // Retrieve audio files (change the search pattern if needed)
                    //string[] audioFiles = Directory.GetFiles(folderPath, "*.mp3");
                    //string[] audioExtensions = { "*.mp3", "*.wav", "*.ogg", "*.flac", "*.aac", "*.wma" };
                    string[] audioExtensions = { "*.wav" };

                    foreach (string extension in audioExtensions)
                    {
                        audioFiles.AddRange(Directory.GetFiles(subdirectory, extension));
                    }
                    foreach (string audioFile in audioFiles)
                    {
                        string FileName = Path.GetFileName(audioFile);
                        if (FileName == filename)
                        {
                            // Replace "path_to_your_audio_file.wav" with the actual path to your WAV audio file on the server.
                            filePath = audioFile;

                            // Replace "desired_audio_filename.wav" with the desired filename that the client will see.
                            fileName = filename;
                        }
                        //filenames.Add(fileName);
                    }
                }
            }

            //return filePath;
            return File(filePath, "audio/wav");
            //var file= Convert.ToBase64String(filePath.ToString());
            //var file = Base64Encode(filePath);
            //return file;

            //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            //// Convert the byte array to a base64 string
            //string base64String = Convert.ToBase64String(fileBytes);
            //return base64String;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}