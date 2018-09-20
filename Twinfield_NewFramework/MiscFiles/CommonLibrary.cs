using Microsoft.VisualStudio.TestTools.WebTesting;
using Microsoft.VisualStudio.TestTools.WebTesting.Rules;
using MicrosoftServicesTestLabs.Monitor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Twinfield_NewFramework
{
    public static class CommonDetails
    {
        public static string SecurityToken;
        public static string IPAddress;
        public static string IntegratorKey;
        public static string PROTOCOL;
        public static string AccountNo;
        public static string UserID;
    }

    public static class Logger
    {
        public static string AccountId;
        public static string UserId;
        public static System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        public static string methodName = method.Name;
        public static string className = method.ReflectedType.Name;
        public static string fullMethodName = string.Empty;

        public static Guid GetUserInformation(string userInfo, ref Guid userGuid)
        {
            Guid firmGuid = Guid.Empty;
            //runningFunctionName = "GetUserInformation";

            string userGuidTokenText = "<saml:Attribute AttributeName=\"userguid\" AttributeNamespace=\"http://www.cch.com\"><saml:AttributeValue>";
            string firmGuidTokenText = "<saml:Attribute AttributeName=\"firmguid\" AttributeNamespace=\"http://www.cch.com\"><saml:AttributeValue>";
            string identifierText = string.Empty;

            identifierText = userInfo.Substring(userInfo.IndexOf(userGuidTokenText) + userGuidTokenText.Length, 36);
            userGuid = new Guid(identifierText);

            identifierText = userInfo.Substring(userInfo.IndexOf(firmGuidTokenText) + firmGuidTokenText.Length, 36);
            firmGuid = new Guid(identifierText);

            return firmGuid;
        }

        #region RTMonitor - Write Statement

        public static void WriteExceptionErrorLog(string AccountId, string UserId, string scenarioName, Exception e)
        {
            string formatterLine = "\r\n------------------------------------------------------------------------\r\n ";
            string basicExceptionDetail = String.Format("Failure occurred for in Scenario " + scenarioName + "under Account=> {0} with LoginUser=> {1} on Machine=> {2}",
             AccountId.Trim(), UserId.Trim(), Environment.MachineName);
            string exceptionString = String.Format("\n ExceptionString: {0}", e.ToString());
            string exceptionStacktrace = String.Format("\n ExceptionStackTrace: {0}", e.StackTrace);
            string exceptionMessage = String.Format("\n ExceptionMessage: {0}", e.Message);
            string innerException = String.Empty;
            if (null != innerException)
            {
                innerException = String.Format("\n InnerException: {0}", e.InnerException);
            }

            if (AssemblyLoad.rtMonitorLog)
                RTMonitor.Write(System.Drawing.Color.Red, formatterLine + basicExceptionDetail + exceptionString + exceptionStacktrace + innerException + exceptionMessage);
        }

        public static void WriteInformationLog(string AccountId, string UserId, string scenarioName)
        {
            if (AssemblyLoad.rtMonitorLog)
                RTMonitor.Write(System.Drawing.Color.Green, String.Format("\nAccount\\LoginUser- {0}:{1} of {2}: Passed Successfully", AccountId.Trim(), UserId.Trim(), scenarioName));
        }

        public static void WriteGeneralLog(string message)
        {
            //fullMethodName = className + "." + methodName;
            if (AssemblyLoad.rtMonitorLog)
                RTMonitor.Write(System.Drawing.Color.Blue,
                          message);
        }

        public static void WriteGeneralLogUser(string message)
        {
            //fullMethodName = className + "." + methodName;

            RTMonitor.Write(System.Drawing.Color.Cyan,
                      message);
        }
        #endregion RTMonitor - Write Statement
    }

    public class ControllerSettings
    {
        public string ipAddress { get; set; }

        public string machineName { get; set; }

        public string description { get; set; }
    }

    public static class CommonFunctions
    {
        private static string InitializationAndTranslationExtractedText;

        public static string RandomString(int Size)
        {
            string input = "abcdefghijklmnopqrstuvwxyz0123456789QWERTYUIOPLKJHGFDSAZXCVBNM";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = input[new Random().Next(0, input.Length)]; Thread.Sleep(20);
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static string[] GetBetween(this string content, string startString, string endString, int optionCount)
        {
            int Start = 0, End = 0;
            string[] Companies = new string[optionCount];
            for (int i = 1; i <= optionCount; i++)
            {
                Start = content.IndexOf(startString, Start) + startString.Length;
                End = content.IndexOf(endString, Start);
                End = End - Start;
                Companies[i - 1] = content.Substring(Start, End);
                Start = Start + End;
                End = Start + End;
            }
            return Companies;
        }
        public static string GetSingleWordBetween(this string content, string startString, string endString)
        {
            int StartIndex = 0, EndIndex = 0;
            string extractedValue = string.Empty;
            StartIndex = content.IndexOf(startString) + startString.Length;
            EndIndex = content.IndexOf(endString, StartIndex);
            int substringLength = EndIndex - StartIndex;
            string temp = content.Substring(StartIndex, substringLength);            
            return temp;
        }
        public static List<string> GetBetweenList(this string inputString, string startString, string endString)
        {
            int StartIndex = 0, EndIndex = 0;
            List<string> extractedValues = new List<string>();
            while(inputString.Contains(startString) && inputString.Contains(endString))
            {
                StartIndex = inputString.IndexOf(startString, StartIndex) + startString.Length;
                EndIndex = inputString.IndexOf(endString, StartIndex);
                EndIndex = EndIndex - StartIndex;
                extractedValues.Add(inputString.Substring(StartIndex, EndIndex));
                StartIndex = StartIndex + EndIndex;
                EndIndex = StartIndex + EndIndex;
                inputString = inputString.Substring(EndIndex);
            }
            return extractedValues;
        }
        public static byte[] WriteMultipartForm(string boundary, Dictionary<string, string> data, string fileName, string fileContentType, byte[] fileData)
        {
            List<byte> requestByteList = new List<byte>();
            /// The first boundary
            byte[] boundarybytes = Encoding.UTF8.GetBytes("------" + boundary + "\r\n");
            /// the last boundary.
            byte[] trailer = Encoding.UTF8.GetBytes("\r\n------" + boundary + "--\r\n");
            /// the form data, properly formatted
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            /// the form-data file upload, properly formatted
            string fileheaderTemplate = "Content-Disposition: form-data; name=\"file\"; filename=\"blob\"\r\nContent-Type: {1}\r\n\r\n";

            /// Added to track if we need a CRLF or not.
            bool bNeedsCRLF = false;

            if (data != null)
            {
                foreach (string key in data.Keys)
                {
                    /// if we need to drop a CRLF, do that.
                    if (bNeedsCRLF) requestByteList.AddRange(Encoding.ASCII.GetBytes("\r\n"));

                    /// Write the boundary.
                    requestByteList.AddRange(boundarybytes);

                    /// Write the key.
                    requestByteList.AddRange(Encoding.ASCII.GetBytes(string.Format(formdataTemplate, key, data[key])));
                    bNeedsCRLF = true;
                }
            }

            /// If we don't have keys, we don't need a crlf.
            if (bNeedsCRLF)
                requestByteList.AddRange(Encoding.ASCII.GetBytes("\r\n"));

            requestByteList.AddRange(boundarybytes);
            requestByteList.AddRange(Encoding.ASCII.GetBytes(string.Format(fileheaderTemplate, fileName, fileContentType)));
            /// Write the file data to the stream.
            requestByteList.AddRange(fileData);
            requestByteList.AddRange(trailer);
            return requestByteList.ToArray();
        }

        public static string ReadFileContent(string filename)
        {
            try
            {
                string solutionpath = Path.Combine(Directory.GetCurrentDirectory(), "\\", filename);
                string readText = string.Empty;

                // This text is added only once to the file.
                if (File.Exists(filename))
                {
                    readText = File.ReadAllText(filename);
                }

                if (string.IsNullOrEmpty(readText))
                    throw new Exception("Not able to read the contents of txt file, Error in ReadFileContent Method");

                return readText;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string SerializeObject(ScenarioDataList objScenarioDataList)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(objScenarioDataList.GetType());

            using (StringWriter stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, objScenarioDataList);
                return stringWriter.ToString();
            }
        }

        public static ScenarioDataList GetScenarioDataListFromXML()
        {
            ScenarioDataList serializeObject;

            try
            {
                //Create an instance of BasicSerialization class.
                serializeObject = new ScenarioDataList();
                //Create an instance of new TextReader.
                TextReader txtReader = new StreamReader(Directory.GetCurrentDirectory() + @"\ScenarioDataList.xml");
                //Create and instance of XmlSerializer class.
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScenarioDataList));
                //Deserialize from the StreamReader.
                serializeObject = (ScenarioDataList)xmlSerializer.Deserialize(txtReader);
                // Close the stream reader
                txtReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting Scenario Data List From XML : " + ex.Message);
            }

            return serializeObject;
        }

        public static TableType GetInputTableNamesFromXML()
        {
            TableType serializeObject;

            try
            {
                //Create an instance of BasicSerialization class.
                serializeObject = new TableType();
                //Create an instance of new TextReader.
                TextReader txtReader = new StreamReader(Directory.GetCurrentDirectory() + @"\TableTypeSettings.xml");
                //Create and instance of XmlSerializer class.
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TableType));
                //Deserialize from the StreamReader.
                serializeObject = (TableType)xmlSerializer.Deserialize(txtReader);
                // Close the stream reader
                txtReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting Get Input Table Names From XML : " + ex.Message);
            }

            return serializeObject;
        }

        public static string GetControllerHostIPAddress()
        {
            string localIP = String.Empty;

            try
            {
                System.Net.IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ipAddress in host.AddressList)
                {
                    if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        localIP = ipAddress.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting controller IP address : " + ex.Message);
            }

            if (string.IsNullOrEmpty(localIP) || string.IsNullOrWhiteSpace(localIP))
            {
                throw new Exception("String is NullorEmpty, Error getting controller IP address : ");
            }

            return localIP;
        }

        public static void ReadLoadTestSettingsConfig()
        {
            string fullMethodName = string.Empty;
            string key = string.Empty;
            string value = string.Empty;
            try
            {


                //System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                //string methodName = method.Name;
                //string className = method.ReflectedType.Name;

                //fullMethodName = className + "." + methodName;

                XmlDocument doc = new XmlDocument();

                //original way to get from xml
                doc.Load(Directory.GetCurrentDirectory() + @"\LoadTestSettings.config");
                //doc.Load(Directory.GetCurrentDirectory() + "\\" + System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location) + ".config");
                XmlNodeList app = doc.SelectNodes("/configuration/appSettings/add");

                foreach (XmlNode node in app)
                {
                    key = node.Attributes["key"].Value;
                    value = node.Attributes["value"].Value;
                    //can not apply lower case here as elements sensitive to upper case or lower case will be in trouble for eg. password: Admin12!@ admin12!@
                    if (!AssemblyLoad.appSettings.ContainsKey(key))
                        AssemblyLoad.appSettings.Add(node.Attributes["key"].Value, node.Attributes["value"].Value);
                }

                if (AssemblyLoad.appSettings.Count == 0)
                    throw new Exception("Error Reading LoadTestSetting.config file section appsetting");

                //XmlNodeList connection = doc.SelectNodes("/configuration/connectionStrings/add");

                //foreach (XmlNode node in connection)
                //{
                //    connectionStrings.Add(node.Attributes["name"].Value, node.Attributes["connectionString"].Value);
                //}

                //if (connectionStrings.Count == 0)
                //    throw new Exception("Error Reading LoadTestSetting.config file section connectionStrings");
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ReadLoadTestSettingsConfig() methind in Reading LoadTestSetting.config file : Error : " + ex.Message + "key :" + key + "value :" + value);
            }
            finally
            {
            }
        }

        public static void ReadControllerSettingsConfig()
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                ControllerSettings objControllerSettings;

                //original way to get from xml
                doc.Load(Directory.GetCurrentDirectory() + @"\LoadTestSettings.config");
                //doc.Load(Directory.GetCurrentDirectory() + "\\" + System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location) + ".config");
                XmlNodeList app = doc.SelectNodes("/configuration/controllerSettings/add");

                foreach (XmlNode node in app)
                {
                    objControllerSettings = new ControllerSettings();
                    objControllerSettings.ipAddress = node.Attributes["key"].Value.ToLower();
                    objControllerSettings.machineName = node.Attributes["value"].Value.ToLower();
                    objControllerSettings.description = node.Attributes["description"].Value.ToLower();

                    AssemblyLoad.objListControllerSettings.Add(objControllerSettings);
                }

                if (AssemblyLoad.objListControllerSettings.Count == 0)
                    throw new Exception("Error Reading LoadTestSetting.config file section ControllerSettings");
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ReadControllerSettingsConfig() Reading LoadTestSetting.config file : Error : " + ex.Message);
            }
            finally
            {
            }
        }

        public static string InputDBConnectionString(string location)
        {
            try
            {
                switch (location.ToLower())
                {
                    case "twinfield":
                        //return "Data Source=sagarlt.database.windows.net;Initial Catalog=LTTransactionDB_InputDB; User ID=sagard@sagarlt;pwd=admin12!@;MultipleActiveResultSets=True;";//"Data Source=172.18.100.13;Initial Catalog=LoadTest;User ID=sa;Password=LT!@#123;"; //samiksh
                        return "Server = tcp:ltsql.database.windows.net,1433; Initial Catalog = AzureTestRigSQL; Persist Security Info = False; User ID = jredinger@ltsql; Password = Perfmon#1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                    default:
                        //return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=" + AssemblyLoad.appSettings["WKUKInputDBName"] + ";Integrated Security=True";
                        //return "Data Source = QAONECLICK\\CCH2008; Initial Catalog = " + AssemblyLoad.appSettings["WKUKInputDBName"] + "; User ID = sa; pwd = Afpftcb1td; MultipleActiveResultSets = True; ";
                        //return "Data Source = ocukpercentral\\CCH2008; Initial Catalog = " + AssemblyLoad.appSettings["WKUKInputDBName"] + "; User ID = vpmuser; pwd = BelfrY18; MultipleActiveResultSets = True; ";
                        //return "Data Source = TCHOPADE-LT\\TANVI;Initial Catalog =OneClickInputDB;Integrated Security = True";
                        //"Data Source = OCPERFCTRL; Initial Catalog = " + AssemblyLoad.appSettings["WKUKInputDBName"] + "; User ID = sa; pwd = P@ssw0rd123!; MultipleActiveResultSets = True; ";
                        return "Server = tcp:ltsql.database.windows.net,1433; Initial Catalog = AzureTestRigSQL; Persist Security Info = False; User ID = jredinger@ltsql; Password = Perfmon#1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                        //return "Data Source = .;Initial Catalog=OneClickInputDB;Integrated Security =True";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error In Method: InputDBConnectionString() , Variable Value Passed: " + location + "Exception Thrown : " + ex.Message);
            }
        }

        public static void GetControllerDestinationAndNameOnEnviornmentUsingIP(string controllerip)
        {
            try
            {
                foreach (var box in AssemblyLoad.objListControllerSettings)
                {
                    if (box.ipAddress.Equals(controllerip) || box.machineName.Trim().ToLower().Equals(controllerip)) //fix applied so that some of the upstream servers are registered with machine name so here code is breaking while running on agent
                    {
                        AssemblyLoad.controllerName = box.machineName.Trim().ToUpper();
                        AssemblyLoad.controllerBelongstoEnv = box.description.Trim().ToUpper();
                        break;
                    }
                }

                if (string.IsNullOrEmpty(AssemblyLoad.controllerName) && string.IsNullOrEmpty(AssemblyLoad.controllerBelongstoEnv))
                    throw new Exception("Check LoadTest Settings file IPaddress and not matching to controller name");
                else
                {
                    Logger.WriteGeneralLog(String.Format("Controller Machine Name: {0} Controller IP Address: {1} Controller Belongs to Env: {2} ", AssemblyLoad.controllerName, AssemblyLoad.controllerIPAddress, AssemblyLoad.controllerBelongstoEnv));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error In Method: GetControllerDestinationAndNameOnEnviornmentUsingIP(), Variable Value Passed: " + controllerip + "Exception Thrown : " + ex.Message);
            }
        }

        private static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
                RTMonitor.Write("Creating destination");
            }
            RTMonitor.Write("Preparing to copy ");
            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                if (File.Exists(Path.Combine(destination.FullName, file.Name)))
                {
                    continue;
                }
                RTMonitor.Write("Copying file " + file.Name);
                try
                {
                    file.CopyTo(Path.Combine(destination.FullName,
                        file.Name));
                }
                catch (Exception exp)
                {
                    RTMonitor.Write("EXCEPTION in FILECOPY " + exp.Message + " " + exp.StackTrace);
                }
            }

            RTMonitor.Write("Preparing to copy subdirectories");
            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                //if (Directory.Exists(Path.Combine(destination.FullName, dir.Name)))
                //{
                //    return;
                //}

                try
                {
                    string destinationDir = Path.Combine(destination.FullName, dir.Name);

                    CopyDirectory(dir, new DirectoryInfo(destinationDir));
                }
                catch (Exception exp)
                {
                    RTMonitor.Write("EXCEPTION in FILECOPY FOR SUBDIRECTOREIS " + exp.Message + " " + exp.StackTrace);
                }
            }
        }

        public static void GetFileStream()
        {
            Stream fileStream;
            string curentDIR = System.IO.Directory.GetCurrentDirectory();
            //curentDIR = curentDIR.Substring(0, curentDIR.LastIndexOf("SuiteWideLoadTesting\\"));
            //curentDIR = curentDIR.Substring(0, curentDIR.IndexOf("bin")) + @"Scenarios\Document";
            string path = curentDIR + @"\Scenarios\Document";//TestContext.TestDeploymentDir;
            FileInfo fi = new FileInfo(curentDIR + "\\TestTXTFile-C-2.txt");
            fileStream = fi.OpenRead();//.Read(inputFile, 0, 100048);
            fi = null;
        }

        //public static string GetScenarioPrefix(string scenarioName)
        //{
        //    switch(scenarioName)
        //    {
        //        case "CreateInvoice": return TwinfieldScenarioPrefix.CI_.ToString(); break;
        //        case "EditInvoice": return TwinfieldScenarioPrefix.EI_.ToString(); break;
        //        case "CompanySettings": return TwinfieldScenarioPrefix.CS_.ToString();break;
        //        case "ExtendedTBReport": return TwinfieldScenarioPrefix.ETBR_.ToString(); break;
        //        case "NeoFixedAsset": return TwinfieldScenarioPrefix.NFA_.ToString(); break;
        //        case "NeoSalesInvoices": return TwinfieldScenarioPrefix.NSI_.ToString(); break;
        //        case "ClassicSalesInvoices": return TwinfieldScenarioPrefix.CSI_.ToString(); break;
        //        case "CreateTransaction": return TwinfieldScenarioPrefix.CT_.ToString(); break;
        //        case "ReadTransaction": return TwinfieldScenarioPrefix.RT_.ToString(); break;
        //        case "UserAccessSettings": return TwinfieldScenarioPrefix.UAS_.ToString(); break;
        //        case "DocumentManagement": return TwinfieldScenarioPrefix.DM_.ToString(); break;
        //        case "PayAndCollectRun": return TwinfieldScenarioPrefix.PCR_.ToString(); break;
        //        case "ExportCustomers": return TwinfieldScenarioPrefix.EC_.ToString(); break;
        //        default: return ""; break;
        //    }
        //}
        //public class ScenarioPrefix
        //{
            
        //    public static string CreateInvoice = "CI";
            
        //    public static string EditInvoice = "EI";

        //    public static string CompanySettings = "CS";


        //}

        //Launch-Login Funcationality. 
        public static IEnumerable<WebTestRequest> LaunchLogin(this WebTest webTest, SharedThreadData threadData, WebTestRequestPlugin ObjRequest)
        {
            string WebBTPrefix = threadData.ScenarioPrefix;
            string WebRequestPrefix = "";

            #region HomePage
            WebRequestPrefix = "La_";
            webTest.BeginTransaction(WebBTPrefix + "Launch");

            webTest.BeginTransaction(WebRequestPrefix+"StartPage");
            WebTestRequest request1 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/"));
            request1.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            request1.ExpectedResponseUrl = (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue + "");
            WebTestRequest request1Dependent1 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/auth/content/font/fontawesome-webfont.eot"));
            request1Dependent1.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue + "")));
            request1Dependent1.QueryStringParameters.Add("", "", false, false);
            request1.DependentRequests.Add(request1Dependent1);
            yield return request1;
            request1 = null;
            webTest.EndTransaction(WebRequestPrefix + "StartPage");
            webTest.EndTransaction(WebBTPrefix + "Launch");
            #endregion
            Thread.Sleep(2000);

            #region Change Language to English
            webTest.BeginTransaction(WebBTPrefix + "LanguageEnglish");
            WebRequestPrefix = "LE_";

            webTest.BeginTransaction(WebRequestPrefix + "auth_authentication_login");
            WebTestRequest request2 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/auth/authentication/login"));
            request2.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue + "")));
            request2.QueryStringParameters.Add("signin", ObjRequest.SigninValue, false, false);
            request2.QueryStringParameters.Add("culture", "en-GB", false, false);
            WebTestRequest request2Dependent1 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/auth/content/font/fontawesome-webfont.eot"));
            request2Dependent1.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue + "&culture=en-GB")));
            request2Dependent1.QueryStringParameters.Add("", "", false, false);
            request2.DependentRequests.Add(request2Dependent1);
            ExtractText extractionRule1 = new ExtractText();
            extractionRule1.StartsWith = "idsrv.xsrf' value='";
            extractionRule1.EndsWith = "' />";
            extractionRule1.Index = 0;
            extractionRule1.IgnoreCase = false;
            extractionRule1.UseRegularExpression = false;
            extractionRule1.HtmlDecode = true;
            extractionRule1.Required = false;
            extractionRule1.ContextParameterName = "IdsrvValue";
            request2.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule1.Extract);
            yield return request2;
            request2 = null;
            webTest.EndTransaction(WebRequestPrefix + "auth_authentication_login");
            webTest.EndTransaction(WebBTPrefix + "LanguageEnglish");
            #endregion
            //RTMonitor.Write(Color.Green, "Login User: " + threadData.UserName + "idsrv value" + webTest.Context["IdsrvValue"].ToString() + "\n");
            Thread.Sleep(2000);

            #region Login
            webTest.BeginTransaction(WebBTPrefix + "Login");
            WebRequestPrefix = "LI_";

            webTest.BeginTransaction(WebRequestPrefix + "auth_authentication_login_1");
            WebTestRequest request3 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/auth/authentication/login"));
            request3.Method = "POST";
            //request3.ExpectedResponseUrl = (webTest.Context["LoginMain"].ToString() + @"/auth/authentication/connect/authorize?client_id=logonbox&redirect_uri=https%3A%2F%2Flogin.rc.dev.twinfield.com&response_mode=form_post&response_type=id_token&scope=" + ObjRequest.ScopeValue + "&state=" + ObjRequest.StateValue + "&nonce=" + ObjRequest.NonceValue + "&x-client-SKU=ID_NET&x-client-ver=1.0.40306.1554");
            // request3.ExpectedResponseUrl = (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login");
            request3.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue + "&culture=en-GB")));
            request3.QueryStringParameters.Add("signin", ObjRequest.SigninValue, false, false);
            FormPostHttpBody request3Body = new FormPostHttpBody();
            request3Body.FormPostParameters.Add("idsrv.xsrf", webTest.Context["IdsrvValue"].ToString());
            request3Body.FormPostParameters.Add("username", threadData.UserName);
            request3Body.FormPostParameters.Add("password", threadData.Password);
            request3Body.FormPostParameters.Add("txtCompanyID", threadData.Tenant);
            request3Body.FormPostParameters.Add("btnLogin", "");
            request3.Body = request3Body;
            // ExtractHiddenFields extractionRule2 = new ExtractHiddenFields();
            // extractionRule2.Required = true;
            // extractionRule2.HtmlDecode = true;
            // extractionRule2.ContextParameterName = "1";
            // request3.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule2.Extract);
            ExtractText extractionRule2 = new ExtractText();
            extractionRule2.StartsWith = "\"id_token\" value=\"";
            extractionRule2.EndsWith = "\" />";
            extractionRule2.Index = 0;
            extractionRule2.IgnoreCase = false;
            extractionRule2.UseRegularExpression = false;
            extractionRule2.HtmlDecode = true;
            extractionRule2.Required = false;
            extractionRule2.ContextParameterName = "ID_Token";
            request3.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule2.Extract);
            ExtractText extractionRule3 = new ExtractText();
            extractionRule3.StartsWith = "\"scope\" value=\"";
            extractionRule3.EndsWith = "\" />";
            extractionRule3.Index = 0;
            extractionRule3.IgnoreCase = false;
            extractionRule3.UseRegularExpression = false;
            extractionRule3.HtmlDecode = true;
            extractionRule3.Required = false;
            extractionRule3.ContextParameterName = "Scope";
            request3.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule3.Extract);
            ExtractText extractionRule4 = new ExtractText();
            extractionRule4.StartsWith = "\"state\" value=\"";
            extractionRule4.EndsWith = "\" />";
            extractionRule4.Index = 0;
            extractionRule4.IgnoreCase = false;
            extractionRule4.UseRegularExpression = false;
            extractionRule4.HtmlDecode = true;
            extractionRule4.Required = false;
            extractionRule4.ContextParameterName = "State";
            request3.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule4.Extract);
            ExtractText extractionRule5 = new ExtractText();
            extractionRule5.StartsWith = "\"session_state\" value=\"";
            extractionRule5.EndsWith = "\" />";
            extractionRule5.Index = 0;
            extractionRule5.IgnoreCase = false;
            extractionRule5.UseRegularExpression = false;
            extractionRule5.HtmlDecode = true;
            extractionRule5.Required = false;
            extractionRule5.ContextParameterName = "SessionState";
            request3.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule5.Extract);
            yield return request3;
            request3 = null;
            webTest.EndTransaction(WebRequestPrefix + "auth_authentication_login_1");



            webTest.BeginTransaction(WebRequestPrefix + "StartPage_1");
            WebTestRequest request4 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/"));
            request4.Method = "POST";
            request4.ExpectedResponseUrl = webTest.Context["AccountingURL"].ToString() + "/UI/";
            request4.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + @"/auth/authentication/connect/authorize?client_id=logonbox&redirect_uri=https%3A%2F%2Flogin.rc.dev.twinfield.com&response_mode=form_post&response_type=id_token&scope=" + ObjRequest.ScopeValue + "&state=" + ObjRequest.StateValue + "&nonce=" + ObjRequest.NonceValue + "&x-client-SKU=ID_NET&x-client-ver=1.0.40306.1554")));
            FormPostHttpBody request4Body = new FormPostHttpBody();
            request4Body.FormPostParameters.Add("id_token", webTest.Context["ID_Token"].ToString());
            request4Body.FormPostParameters.Add("scope", webTest.Context["Scope"].ToString());
            request4Body.FormPostParameters.Add("state", webTest.Context["State"].ToString());
            request4Body.FormPostParameters.Add("session_state", webTest.Context["SessionState"].ToString());
            request4.Body = request4Body;
            yield return request4;
            request4 = null;
            webTest.EndTransaction(WebRequestPrefix + "StartPage_1");
            
            webTest.BeginTransaction(WebRequestPrefix + "UI");
            WebTestRequest request5 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/UI/"));
            request5.ExpectedResponseUrl = (webTest.Context["AccountingURL"].ToString() + "/UI/#/");
            //var __CDNStaticRootFolder = '14481';
            ExtractText extractionRule6 = new ExtractText();
            extractionRule6.StartsWith = "__CDNStaticRootFolder=\"";
            extractionRule6.EndsWith = "\"";
            extractionRule6.Index = 0;
            extractionRule6.IgnoreCase = false;
            extractionRule6.UseRegularExpression = false;
            extractionRule6.HtmlDecode = true;
            extractionRule6.Required = false;
            extractionRule6.ContextParameterName = "BuildVersion";
            request5.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule6.Extract);
            yield return request5;
            threadData.BuildVersion = webTest.Context["BuildVersion"].ToString();
            request5 = null;
            webTest.EndTransaction(WebRequestPrefix + "UI");

            webTest.BeginTransaction(WebRequestPrefix + "api");
            WebTestRequest request6 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/"));
            request6.Method = "OPTIONS";
            request6.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request6.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request6Body = new StringHttpBody();
            request6Body.ContentType = "";
            request6Body.InsertByteOrderMark = false;
            request6Body.BodyString = "";
            request6.Body = request6Body;
            yield return request6;
            request6 = null;
            webTest.EndTransaction(WebRequestPrefix + "api");

            webTest.BeginTransaction(WebRequestPrefix + "api_notifications");
            WebTestRequest request7 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/notifications"));
            request7.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request7.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request7;
            request7 = null;
            var CreateDateList = JsonConvert.DeserializeObject<RootObject>(webTest.LastResponse.BodyString);
            var datelist = CreateDateList.items.Random();
            var CreatedDate = datelist.created.ToString();
            webTest.EndTransaction(WebRequestPrefix + "api_notifications");
            threadData.CreatedDate = CreatedDate;
            threadData.CreatedDate = DateTime.Now.ToString();

            webTest.BeginTransaction(WebRequestPrefix + "api_1");
            WebTestRequest request8 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/"));
            request8.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request8.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request8;
            request8 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_1");

            webTest.BeginTransaction(WebRequestPrefix + "api_companies");
            WebTestRequest request9 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/companies"));
            request9.Method = "OPTIONS";
            request9.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request9.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request9Body = new StringHttpBody();
            request9Body.ContentType = "";
            request9Body.InsertByteOrderMark = false;
            request9Body.BodyString = "";
            request9.Body = request9Body;
            yield return request9;
            request9 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_companies");

            webTest.BeginTransaction(WebRequestPrefix + "components__infrastructure_localisation_initializa");
            WebTestRequest request10 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/_infrastructure/localisation/initializations-and-transla" +
                    "tions.html"));
            request10.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request10.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            ExtractText extraction2 = new ExtractText();
            extraction2.StartsWith = "<twf:transfer-to-scope name=\"translations\" >";
            extraction2.EndsWith = "</twf:transfer-to-scope>";
            extraction2.IgnoreCase = false;
            extraction2.UseRegularExpression = false;
            extraction2.Required = true;
            extraction2.ExtractRandomMatch = false;
            extraction2.Index = 0;
            extraction2.HtmlDecode = true;
            extraction2.SearchInHeaders = false;
            extraction2.ContextParameterName = "InitializationAndTranslation";
            request10.ExtractValues += new EventHandler<ExtractionEventArgs>(extraction2.Extract);
            yield return request10;
            request10 = null;
            webTest.EndTransaction(WebRequestPrefix + "components__infrastructure_localisation_initializa");

            InitializationAndTranslationExtractedText = webTest.Context["InitializationAndTranslation"].ToString();

            webTest.BeginTransaction(WebRequestPrefix + "ui_timeline_internal_ui-timeline-common-titles.html");
            WebTestRequest request11 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/timeline/internal/ui-timeline-common-titles.html"));
            request11.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request11.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request11;
            request11 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_timeline_internal_ui-timeline-common-titles.html");

            //Added in Build #6.78.0-3
            webTest.BeginTransaction(WebRequestPrefix + "ui_timeline_internal_ui-timeline-common-titles");
            WebTestRequest request17 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/17217/en-GB/_ui/timeline/internal/ui-timeline-common-titles.html"));
            request17.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request17.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request17;
            request17 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_timeline_internal_ui-timeline-common-titles");

            webTest.BeginTransaction(WebRequestPrefix + "ui_timeline_internal_ui-timeline-common");
            WebTestRequest request12 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/timeline/internal/ui-timeline-common-translations.html"));
            request12.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request12.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request12;
            request12 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_timeline_internal_ui-timeline-common");

            webTest.BeginTransaction(WebRequestPrefix + "components__infrastructure_configuration_@config");
            WebTestRequest request13 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/_infrastructure/configuration/@configuration-localise.ht" +
                    "ml"));
            request13.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request13.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request13;
            request13 = null;
            webTest.EndTransaction(WebRequestPrefix + "components__infrastructure_configuration_@config");

            webTest.BeginTransaction(WebRequestPrefix + "company-import_@company-import-localise.html");
            WebTestRequest request14 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/company-import/@company-import-localise.html"));
            request14.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request14.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request14;
            request14 = null;
            webTest.EndTransaction(WebRequestPrefix + "company-import_@company-import-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "app-store_@app-store-localise.html");
            WebTestRequest request15 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/app-store/@app-store-localise.html"));
            request15.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request15.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request15;
            request15 = null;
            webTest.EndTransaction(WebRequestPrefix + "app-store_@app-store-localise.html");

            //Found in Build 15519-- Aditi
            webTest.BeginTransaction(WebRequestPrefix + "@feeds-localise.html");
            WebTestRequest request160 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/feeds/@feeds-localise.html"));
            request160.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request160.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request160;
            request160 = null;
            webTest.EndTransaction(WebRequestPrefix + "@feeds-localise.html");
            //

            webTest.BeginTransaction(WebRequestPrefix + "bank_@bank-localise.html");
            WebTestRequest request16 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/@bank-localise.html"));
            request16.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request16.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request16;
            request16 = null;
            webTest.EndTransaction(WebRequestPrefix + "bank_@bank-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "vat_@vat-localise.html");
            WebTestRequest reques17 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/vat/@vat-localise.html"));
            reques17.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            reques17.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return reques17;
            reques17 = null;
            webTest.EndTransaction(WebRequestPrefix + "vat_@vat-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "budgets_@budgets-localise.html");
            WebTestRequest request18 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/budgets/@budgets-localise.html"));
            request18.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request18.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request18;
            request18 = null;
            webTest.EndTransaction(WebRequestPrefix + "budgets_@budgets-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "companies_@companies-localise.html");
            WebTestRequest request19 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/companies/@companies-localise.html"));
            request19.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request19.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request19;
            request19 = null;
            webTest.EndTransaction(WebRequestPrefix + "companies_@companies-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "customers_@customers-localise.html");
            WebTestRequest request20 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/customers/@customers-localise.html"));
            request20.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request20.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request20;
            request20 = null;
            webTest.EndTransaction(WebRequestPrefix + "customers_@customers-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "dimensions_@dimensions-localise.html");
            WebTestRequest request21 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/dimensions/@dimensions-localise.html"));
            request21.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request21.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request21;
            request21 = null;
            webTest.EndTransaction(WebRequestPrefix + "dimensions_@dimensions-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "dockets_@dockets-localise.html");
            WebTestRequest request22 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/dockets/@dockets-localise.html"));
            request22.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request22.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request22;
            request22 = null;
            webTest.EndTransaction(WebRequestPrefix + "dockets_@dockets-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "document-share_@document-share-localise.html");
            WebTestRequest request23 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/document-share/@document-share-localise.html"));
            request23.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request23.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request23;
            request23 = null;
            webTest.EndTransaction(WebRequestPrefix + "document-share_@document-share-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "financial-professionals_@financial-professionals");
            WebTestRequest request24 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/financial-professionals/@financial-professionals-localise.html"));
            request24.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request24.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request24;
            request24 = null;
            webTest.EndTransaction(WebRequestPrefix + "financial-professionals_@financial-professionals");

            //Added in Build#6.77.0-5 - AnkitaG
            webTest.BeginTransaction(WebRequestPrefix + "diagnostic-contexts_@diagnostic-contexts-localise.html");
            WebTestRequest reques28 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/diagnostic-contexts/@diagnostic-contexts-localise.html"));
            reques28.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            reques28.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return reques28;
            reques28 = null;
            webTest.EndTransaction(WebRequestPrefix + "diagnostic-contexts_@diagnostic-contexts-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_classes_@classes-localise.html");
            WebTestRequest request25 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/classes/@classes-localise.html"));
            request25.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request25.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request25;
            request25 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_classes_@classes-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_assets_@assets-localise.html");
            WebTestRequest request26 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/assets/@assets-localise.html"));
            request26.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request26.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request26;
            request26 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_assets_@assets-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_central-maintenance_@config");
            WebTestRequest request27 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/central-maintenance/@configuration-localise.html"));
            request27.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request27.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request27;
            request27 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_central-maintenance_@config");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_settings_@asset-settings-localise");
            WebTestRequest request28 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/settings/@asset-settings-localise.html"));
            request28.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request28.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request28;
            request28 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_settings_@asset-settings-localise");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_settings_origins_@origins-localise.html");
            WebTestRequest request29 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/settings/origins/@origins-localise.html"));
            request29.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request29.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request29;
            request29 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_settings_origins_@origins-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_settings_reasons_@reasons-localise.html");
            WebTestRequest request30 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/settings/reasons/@reasons-localise.html"));
            request30.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request30.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request30;
            request30 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_settings_reasons_@reasons-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_reports_@reports-localise.html");
            WebTestRequest request31 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/reports/@reports-localise.html"));
            request31.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request31.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request31;
            request31 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_reports_@reports-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_property-tax_@property-tax-localise");
            WebTestRequest request32 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/property-tax/@property-tax-localise.html"));
            request32.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request32.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request32;
            request32 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_property-tax_@property-tax-localise");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_groups_@groups-localise.html");
            WebTestRequest request33 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/groups/@groups-localise.html"));
            request33.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request33.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request33;
            request33 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_groups_@groups-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_manual-conversion_@manual-conversion");
            WebTestRequest request34 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/manual-conversion/@manual-conversion-localise.html"));
            request34.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request34.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request34;
            request34 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_manual-conversion_@manual-conversion");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_historical-values_@historical-values");
            WebTestRequest request35 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/historical-values/@historical-values-localise.html"));
            request35.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request35.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request35;
            request35 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_historical-values_@historical-values");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets_@fixed-assets-localise.html");
            WebTestRequest request36 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/@fixed-assets-localise.html"));
            request36.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request36.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request36;
            request36 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets_@fixed-assets-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "user_@user-localise.html");
            WebTestRequest request37 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/user/@user-localise.html"));
            request37.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request37.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request37;
            request37 = null;
            webTest.EndTransaction(WebRequestPrefix + "user_@user-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "preaccounting_@preaccounting-localise.html");
            WebTestRequest request38 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/preaccounting/@preaccounting-localise.html"));
            request38.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request38.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request38;
            request38 = null;
            webTest.EndTransaction(WebRequestPrefix + "preaccounting_@preaccounting-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "projects_@projects-localise.html");
            WebTestRequest request39 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/projects/@projects-localise.html"));
            request39.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request39.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request39;
            request39 = null;
            webTest.EndTransaction(WebRequestPrefix + "projects_@projects-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "purchase_invoices_@purchase-localise.html");
            WebTestRequest request40 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/purchase/invoices/@purchase-localise.html"));
            request40.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request40.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request40;
            request40 = null;
            webTest.EndTransaction(WebRequestPrefix + "purchase_invoices_@purchase-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "purchase_expense-types_@expense-types");
            WebTestRequest request41 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/purchase/expense-types/@expense-types-localise.html"));
            request41.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request41.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request41;
            request41 = null;
            webTest.EndTransaction(WebRequestPrefix + "purchase_expense-types_@expense-types");

            webTest.BeginTransaction(WebRequestPrefix + "reports_@reports-localise.html");
            WebTestRequest request42 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/reports/@reports-localise.html"));
            request42.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request42.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request42;
            request42 = null;
            webTest.EndTransaction(WebRequestPrefix + "reports_@reports-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "sales_articles_@articles-localise.html");
            WebTestRequest request43 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/articles/@articles-localise.html"));
            request43.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request43.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request43;
            request43 = null;
            webTest.EndTransaction(WebRequestPrefix + "sales_articles_@articles-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "credit-management_@credit-management-localise");
            WebTestRequest request44 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/credit-management/@credit-management-localise.html"));
            request44.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request44.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request44;
            request44 = null;
            webTest.EndTransaction(WebRequestPrefix + "credit-management_@credit-management-localise");

            webTest.BeginTransaction(WebRequestPrefix + "credit-management_@credit-management-settings-local");
            WebTestRequest request45 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/credit-management/@credit-management-settings-localise.html"));
            request45.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request45.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request45;
            request45 = null;
            webTest.EndTransaction(WebRequestPrefix + "credit-management_@credit-management-settings-local");

            webTest.BeginTransaction(WebRequestPrefix + "sales_invoicing_@invoicing-localise");
            WebTestRequest request46 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/invoicing/@invoicing-localise.html"));
            request46.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request46.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request46;
            request46 = null;
            webTest.EndTransaction(WebRequestPrefix + "sales_invoicing_@invoicing-localise");

            webTest.BeginTransaction(WebRequestPrefix + "sales_timeframes_@timeframes-localise");
            WebTestRequest request47 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/timeframes/@timeframes-localise.html"));
            request47.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request47.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request47;
            request47 = null;
            webTest.EndTransaction(WebRequestPrefix + "sales_timeframes_@timeframes-localise");

            webTest.BeginTransaction(WebRequestPrefix + "sales_credit-note_@credit-note-localise.html");
            WebTestRequest request480 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/credit-note/@credit-note-localise.html"));
            request480.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request480.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request480;
            request480 = null;
            webTest.EndTransaction(WebRequestPrefix + "sales_credit-note_@credit-note-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "sales_quotes_@quotes-localise.html");
            WebTestRequest request48 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/quotes/@quotes-localise.html"));
            request48.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request48.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request48;
            request48 = null;
            webTest.EndTransaction(WebRequestPrefix + "sales_quotes_@quotes-localise.html");

            //Found in Build 15519 --Aditi
            webTest.BeginTransaction(WebRequestPrefix + "@revenue-types-localise.html");
            WebTestRequest request510 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/revenue-types/@revenue-types-localise.html"));
            request510.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request510.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request510;
            request510 = null;
            webTest.EndTransaction(WebRequestPrefix + "@revenue-types-localise.html");
            //

            webTest.BeginTransaction(WebRequestPrefix + "sales_@sales-localise.html");
            WebTestRequest request49 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/@sales-localise.html"));
            request49.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request49.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request49;
            request49 = null;
            webTest.EndTransaction(WebRequestPrefix + "sales_@sales-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "settings_company_@company-localise.html");
            WebTestRequest request50 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/settings/company/@company-localise.html"));
            request50.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request50.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request50;
            request50 = null;
            webTest.EndTransaction(WebRequestPrefix + "settings_company_@company-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "settings_organisation_@organisation-localise.html");
            WebTestRequest request51 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/settings/organisation/@organisation-localise.html"));
            request51.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request51.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request51;
            request51 = null;
            webTest.EndTransaction(WebRequestPrefix + "settings_organisation_@organisation-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "settings_access_@access-localise.html");
            WebTestRequest request52 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/settings/access/@access-localise.html"));
            request52.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request52.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request52;
            request52 = null;
            webTest.EndTransaction(WebRequestPrefix + "settings_access_@access-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "suppliers_@suppliers-localise.html");
            WebTestRequest request53 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/suppliers/@suppliers-localise.html"));
            request53.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request53.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request53;
            request53 = null;
            webTest.EndTransaction(WebRequestPrefix + "suppliers_@suppliers-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "tax_@tax-localise.html");
            WebTestRequest request54 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/tax/@tax-localise.html"));
            request54.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request54.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request54;
            request54 = null;
            webTest.EndTransaction(WebRequestPrefix + "tax_@tax-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "transactions_@transactions-localise.html");
            WebTestRequest request55 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/transactions/@transactions-localise.html"));
            request55.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request55.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request55;
            request55 = null;
            webTest.EndTransaction(WebRequestPrefix + "transactions_@transactions-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "year-end_@year-end-localise.html");
            WebTestRequest request56 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/year-end/@year-end-localise.html"));
            request56.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request56.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request56;
            request56 = null;
            webTest.EndTransaction(WebRequestPrefix + "year-end_@year-end-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "bank_feeds_bank-search_select2-choices.html");
            WebTestRequest request62 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/feeds/bank-search/templates/select2-choices.html"));
            request62.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request62.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request62;
            request62 = null;
            webTest.EndTransaction(WebRequestPrefix + "bank_feeds_bank-search_select2-choices.html");

            webTest.BeginTransaction(WebRequestPrefix + "bank_feeds_bank-search_select2-match.html");
            WebTestRequest request63 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/feeds/bank-search/templates/select2-match.html"));
            request63.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request63.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request63;
            request63 = null;
            webTest.EndTransaction(WebRequestPrefix + "bank_feeds_bank-search_select2-match.html");

            webTest.BeginTransaction(WebRequestPrefix + "bank_feeds_bank-search_select2-select.html");
            WebTestRequest request64 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/feeds/bank-search/templates/select2-select.html"));
            request64.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request64.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request64;
            request64 = null;
            webTest.EndTransaction(WebRequestPrefix + "bank_feeds_bank-search_select2-select.html");

            webTest.BeginTransaction(WebRequestPrefix + "bank_assignments_proposed-assignments-view.html");
            WebTestRequest request57 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/assignments/proposed-assignments-view.html"));
            request57.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request57.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request57;
            request57 = null;
            webTest.EndTransaction(WebRequestPrefix + "bank_assignments_proposed-assignments-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "bank_assignments_assigned-view.html");
            WebTestRequest request58 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/assignments/assigned-view.html"));
            request58.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request58.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request58;
            request58 = null;
            webTest.EndTransaction(WebRequestPrefix + "bank_assignments_assigned-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "bank_assignments_to-assign-view.html");
            WebTestRequest request59 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/assignments/to-assign-view.html"));
            request59.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request59.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request59;
            request59 = null;
            webTest.EndTransaction(WebRequestPrefix + "bank_assignments_to-assign-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "bank_assignments_other-view.html");
            WebTestRequest request60 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/assignments/other-view.html"));
            request60.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request60.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request60;
            request60 = null;
            webTest.EndTransaction(WebRequestPrefix + "bank_assignments_other-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "bank_listing-to-much-data.html");
            WebTestRequest request61 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/listing-to-much-data.html"));
            request61.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request61.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request61;
            request61 = null;
            webTest.EndTransaction(WebRequestPrefix + "bank_listing-to-much-data.html");

            webTest.BeginTransaction(WebRequestPrefix + "ui_form_fields_select_view_nf-select2-choices");
            WebTestRequest request65 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/form/fields/select/view/nf-select2-choices.html"));
            request65.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request65.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request65;
            request65 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_form_fields_select_view_nf-select2-choices");

            webTest.BeginTransaction(WebRequestPrefix + "ui_form_fields_select_view_nf-select2-select");
            WebTestRequest request66 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/form/fields/select/view/nf-select2-select.html"));
            request66.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request66.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request66;
            request66 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_form_fields_select_view_nf-select2-select");

            //Added in Build#66.7.0-5 - AnkitaG
            webTest.BeginTransaction(WebRequestPrefix + "view_replace-view_not-available-view.html");
            WebTestRequest reques73 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/replace-view/not-available-view.html"));
            reques73.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            reques73.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return reques73;
            reques73 = null;
            webTest.EndTransaction(WebRequestPrefix + "view_replace-view_not-available-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "components_framework_system_sitemap_select2-choice");
            WebTestRequest request67 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/system/sitemap-search/templates/select2-choice" +
                    "s.html"));
            request67.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request67.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request67;
            request67 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_framework_system_sitemap_select2-choice");

            webTest.BeginTransaction(WebRequestPrefix + "components_framework_system_sitemap_select2-match");
            WebTestRequest request68 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/system/sitemap-search/templates/select2-match." +
                    "html"));
            request68.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request68.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request68;
            request68 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_framework_system_sitemap_select2-match");

            webTest.BeginTransaction(WebRequestPrefix + "components_framework_system_sitemap_select2-select");
            WebTestRequest request69 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/system/sitemap-search/templates/select2-select" +
                    ".html"));
            request69.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request69.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request69;
            request69 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_framework_system_sitemap_select2-select");

            webTest.BeginTransaction(WebRequestPrefix + "components_top-bar_search_templates_select2-choices");
            WebTestRequest request70 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/search/templates/select2-choices.html"));
            request70.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request70.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request70;
            request70 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_top-bar_search_templates_select2-choices");

            webTest.BeginTransaction(WebRequestPrefix + "components_top-bar_search_templates_select2-match");
            WebTestRequest request71 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/search/templates/select2-match.html"));
            request71.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request71.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request71;
            request71 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_top-bar_search_templates_select2-match");

            webTest.BeginTransaction(WebRequestPrefix + "components_top-bar_search_templates_select2-select");
            WebTestRequest request72 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/search/templates/select2-select.html"));
            request72.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request72.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request72;
            request72 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_top-bar_search_templates_select2-select");

            webTest.BeginTransaction(WebRequestPrefix + "api_users");
            WebTestRequest request73 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users"));
            request73.Method = "OPTIONS";
            request73.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request73.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request73Body = new StringHttpBody();
            request73Body.ContentType = "";
            request73Body.InsertByteOrderMark = false;
            request73Body.BodyString = "";
            request73.Body = request73Body;
            yield return request73;
            request73 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users");

            webTest.BeginTransaction(WebRequestPrefix + "home_home.html");
            WebTestRequest request74 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/home/home.html"));
            request74.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request74.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request74;
            request74 = null;
            webTest.EndTransaction(WebRequestPrefix + "home_home.html");

            webTest.BeginTransaction(WebRequestPrefix + "components_top-bar_top-bar.html");
            WebTestRequest request75 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/top-bar.html"));
            request75.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request75.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request75;
            request75 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_top-bar_top-bar.html");

            webTest.BeginTransaction(WebRequestPrefix + "components_main-menu_main-menu.html");
            WebTestRequest request76 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/main-menu/main-menu.html"));
            request76.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request76.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request76;
            request76 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_main-menu_main-menu.html");

            webTest.BeginTransaction(WebRequestPrefix + "components_breadcrumb_breadcrumb.html");
            WebTestRequest request77 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/breadcrumb/breadcrumb.html"));
            request77.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request77.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request77;
            request77 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_breadcrumb_breadcrumb.html");

            //Removed in Build#66.7.0-5 - AnkitaG
            //webTest.BeginTransaction(WebRequestPrefix + "components_timeline_timeline.html");
            //WebTestRequest request78 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/timeline/timeline.html"));
            //request78.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            //request78.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //yield return request78;
            //request78 = null;
            //webTest.EndTransaction(WebRequestPrefix + "components_timeline_timeline.html");

            webTest.BeginTransaction(WebRequestPrefix + "components_actions_actions.html");
            WebTestRequest request79 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/actions/actions.html"));
            request79.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request79.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request79;
            request79 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_actions_actions.html");

            //Added in Build#66.7.0-5 - AnkitaG
            webTest.BeginTransaction(WebRequestPrefix + "_components_top-bar_system-feedback_system-feedback.html");
            WebTestRequest request84 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/system-feedback/system-feedback.html"));
            request84.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request84.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request84;
            request84 = null;
            webTest.EndTransaction(WebRequestPrefix + "_components_top-bar_system-feedback_system-feedback.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_officemanagement");
            WebTestRequest request85 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/officemanagement"));
            request85.Method = "OPTIONS";
            request85.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request85.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request85Body = new StringHttpBody();
            request85Body.ContentType = "";
            request85Body.InsertByteOrderMark = false;
            request85Body.BodyString = "";
            request85.Body = request85Body;
            ExtractText extractionRule7 = new ExtractText();
            extractionRule7.StartsWith = "api/officemanagement/offices/office/";
            extractionRule7.EndsWith = "\"},\"templateoffices";
            extractionRule7.Index = 0;
            extractionRule7.IgnoreCase = false;
            extractionRule7.UseRegularExpression = false;
            extractionRule7.HtmlDecode = true;
            extractionRule7.Required = false;
            extractionRule7.ContextParameterName = "OfficeMgtID";
            request85.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule7.Extract);
            yield return request85;
            request85 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_officemanagement");

            webTest.BeginTransaction(WebRequestPrefix + "api_organisation");
            WebTestRequest request86 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/organisation"));
            request86.Method = "OPTIONS";
            request86.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request86.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request86Body = new StringHttpBody();
            request86Body.ContentType = "";
            request86Body.InsertByteOrderMark = false;
            request86Body.BodyString = "";
            request86.Body = request86Body;
            yield return request86;
            request86 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_organisation");

            //webTest.BeginTransaction(WebRequestPrefix + "api_notifications_1");
            //WebTestRequest request90 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/notifications"));
            //request90.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //request90.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //request90.QueryStringParameters.Add("since", threadData.CreatedDate, false, false);
            //yield return request90;
            //request90 = null;
            //webTest.EndTransaction(WebRequestPrefix + "api_notifications_1");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_cabin-regular.woff");
            WebTestRequest request82 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/cabin-regular.woff"));
            request82.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request82;
            request82 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_cabin-regular.woff");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_cabin-bold.woff");
            WebTestRequest request83 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/cabin-bold.woff"));
            request83.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request83;
            request83 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_cabin-bold.woff");

            webTest.BeginTransaction(WebRequestPrefix + "components_top-bar_search_search.html");
            WebTestRequest request91 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/search/search.html"));
            request91.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request91.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request91;
            request91 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_top-bar_search_search.html");

            webTest.BeginTransaction(WebRequestPrefix + "components_framework_desktop_view_desktop-view");
            WebTestRequest request80 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/desktop-view.html"));
            request80.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request80.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request80;
            request80 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_framework_desktop_view_desktop-view");

            webTest.BeginTransaction(WebRequestPrefix + "api_sales");
            WebTestRequest request81 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sales"));
            request81.Method = "OPTIONS";
            request81.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request81.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request81Body = new StringHttpBody();
            request81Body.ContentType = "";
            request81Body.InsertByteOrderMark = false;
            request81Body.BodyString = "";
            request81.Body = request81Body;
            yield return request81;
            request81 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sales");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_fontawesome-webfont.eot");
            WebTestRequest request92 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/fontawesome-webfont.eot"));
            request92.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            request92.QueryStringParameters.Add("", "", false, false);
            yield return request92;
            request92 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_fontawesome-webfont.eot");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_twinfield-icomoon.eot");
            WebTestRequest request93 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/twinfield-icomoon.eot"));
            request93.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request93;
            request93 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_twinfield-icomoon.eot");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping");
            WebTestRequest request87 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping"));
            request87.Method = "OPTIONS";
            request87.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request87.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request87Body = new StringHttpBody();
            request87Body.ContentType = "";
            request87Body.InsertByteOrderMark = false;
            request87Body.BodyString = "";
            request87.Body = request87Body;
            yield return request87;
            request87 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping");

            webTest.BeginTransaction(WebRequestPrefix + "components_company-switch_company-switch.html");
            WebTestRequest request88 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/company-switch/company-switch.html"));
            request88.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request88.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request88;
            request88 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_company-switch_company-switch.html");

            webTest.BeginTransaction(WebRequestPrefix + "components_main-menu_view_main-menu-view.html");
            WebTestRequest reques84 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/main-menu/view/main-menu-view.html"));
            reques84.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            reques84.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return reques84;
            reques84 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_main-menu_view_main-menu-view.html");

            //webTest.BeginTransaction(WebRequestPrefix + "components_top-bar_system-feedback_system-feedback");
            //WebTestRequest request89 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/system-feedback/system-feedback.html"));
            //request89.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            //request89.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //yield return request89;
            //request89 = null;
            //webTest.EndTransaction(WebRequestPrefix + "components_top-bar_system-feedback_system-feedback");       


            webTest.BeginTransaction(WebRequestPrefix + "api_users_user");
            WebTestRequest request100 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + ""));
            request100.Method = "OPTIONS";
            request100.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request100.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request100Body = new StringHttpBody();
            request100Body.ContentType = "";
            request100Body.InsertByteOrderMark = false;
            request100Body.BodyString = "";
            request100.Body = request100Body;
            yield return request100;
            request100 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users_user");


            webTest.BeginTransaction(WebRequestPrefix + "api_organisation_logo");
            WebTestRequest request104 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/organisation/logo"));
            request104.Method = "OPTIONS";
            request104.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request104.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request104Body = new StringHttpBody();
            request104Body.ContentType = "";
            request104Body.InsertByteOrderMark = false;
            request104Body.BodyString = "";
            request104.Body = request104Body;
            yield return request104;
            request104 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_organisation_logo");

            webTest.BeginTransaction(WebRequestPrefix + "api_organisation_logo_1");
            WebTestRequest request105 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/organisation/logo"));
            request105.Method = "OPTIONS";
            request105.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request105.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request105Body = new StringHttpBody();
            request105Body.ContentType = "";
            request105Body.InsertByteOrderMark = false;
            request105Body.BodyString = "";
            request105.Body = request105Body;
            yield return request105;
            request105 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_organisation_logo_1");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping_dimensiontypes");
            WebTestRequest request107 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping/dimensiontypes"));
            request107.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request107.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request107;
            request107 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping_dimensiontypes");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_firasans-regular.woff");
            WebTestRequest request94 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/firasans-regular.woff"));
            request94.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request94;
            request94 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_firasans-regular.woff");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_firasans-italic.woff");
            WebTestRequest request95 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/firasans-italic.woff"));
            request95.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request95;
            request95 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_firasans-italic.woff");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_firasans-medium.woff");
            WebTestRequest request96 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/firasans-medium.woff"));
            request96.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request96;
            request96 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_firasans-medium.woff");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_firasans-mediumitalic.woff");
            WebTestRequest request97 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/firasans-mediumitalic.woff"));
            request97.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request97;
            request97 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_firasans-mediumitalic.woff");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-sitemap-favourite-section-view");
            WebTestRequest request98 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-sitemap-favourite-section-view" +
                    ".html"));
            request98.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request98.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request98;
            request98 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-sitemap-favourite-section-view");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-dashboard-section-group-view.h");
            WebTestRequest request99 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-dashboard-section-group-view.h" +
                    "tml"));
            request99.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request99.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request99;
            request99 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-dashboard-section-group-view.h");


            webTest.BeginTransaction(WebRequestPrefix + "api_officemanage_offices");
            WebTestRequest request108 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/officemanagement/offices/office/" + webTest.Context["OfficeMgtID"].ToString()));
            request108.Method = "OPTIONS";
            request108.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request108.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request108Body = new StringHttpBody();
            request108Body.ContentType = "";
            request108Body.InsertByteOrderMark = false;
            request108Body.BodyString = "";
            request108.Body = request108Body;
            yield return request108;
            request108 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_officemanage_offices");

            webTest.BeginTransaction(WebRequestPrefix + "api_sales_invoices_kpis");
            WebTestRequest request101 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sales/invoices/kpis"));
            request101.Method = "OPTIONS";
            request101.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request101.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request101Body = new StringHttpBody();
            request101Body.ContentType = "";
            request101Body.InsertByteOrderMark = false;
            request101Body.BodyString = "";
            request101.Body = request101Body;
            yield return request101;
            request101 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sales_invoices_kpis");

            //URL changed from /api/sales/tasks to /api/tasks
            webTest.BeginTransaction(WebRequestPrefix + "api_tasks");
            WebTestRequest request102 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/tasks"));
            request102.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request102.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request102;
            request102 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_tasks");

            webTest.BeginTransaction(WebRequestPrefix + "api_sitemapfavourites");
            WebTestRequest request103 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sitemapfavourites"));
            request103.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request103.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request103;
            request103 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sitemapfavourites");

            webTest.BeginTransaction(WebRequestPrefix + "api_sales_invoices_kpis_top10bestcustomers");
            WebTestRequest request122 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sales/invoices/kpis/top10bestcustomers"));
            request122.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request122.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request122;
            request122 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sales_invoices_kpis_top10bestcustomers");

            webTest.BeginTransaction(WebRequestPrefix + "api_users_user_1");
            WebTestRequest request109 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/displaysettings"));
            request109.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request109.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            request109.QueryStringParameters.Add("key", "HomeFavouritesExpanded", false, false);
            yield return request109;
            request109 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users_user_1");


            webTest.BeginTransaction(WebRequestPrefix + "api_users_user_4");
            WebTestRequest request112 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/viewedtours"));
            request112.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request112.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request112;
            request112 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users_user_4");

            webTest.BeginTransaction(WebRequestPrefix + "api_users_user_6");
            WebTestRequest request114 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/previouslogon"));
            request114.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request114.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request114;
            request114 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users_user_6");

            webTest.BeginTransaction(WebRequestPrefix + "api_users_user_5");
            WebTestRequest request113 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/preferreddesktop"));
            request113.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request113.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request113;
            request113 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users_user_5");

            webTest.BeginTransaction(WebRequestPrefix + "api_users_user_7");
            WebTestRequest request115 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/previouslogon"));
            request115.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request115.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request115;
            request115 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users_user_7");

            webTest.BeginTransaction(WebRequestPrefix + "api_home");
            WebTestRequest request123 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/home"));
            request123.Method = "OPTIONS";
            request123.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request123.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request123Body = new StringHttpBody();
            request123Body.ContentType = "";
            request123Body.InsertByteOrderMark = false;
            request123Body.BodyString = "";
            request123.Body = request123Body;
            yield return request123;
            request123 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_home");

            webTest.BeginTransaction(WebRequestPrefix + "api_users_user_2");
            WebTestRequest request110 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/displaysettings"));
            request110.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request110.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            request110.QueryStringParameters.Add("key", "HomeInsightsExpanded", false, false);
            yield return request110;
            request110 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users_user_2");

            webTest.BeginTransaction(WebRequestPrefix + "api_users_user_3");
            WebTestRequest request111 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/displaysettings"));
            request111.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request111.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            request111.QueryStringParameters.Add("key", "HomeHelpfulExpanded", false, false);
            yield return request111;
            request111 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users_user_3");

            webTest.BeginTransaction(WebRequestPrefix + "components_framework_desktop_view_cr-tile-element");
            WebTestRequest request106 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-tile-element.html"));
            request106.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request106.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request106;
            request106 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_framework_desktop_view_cr-tile-element");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-date-graph-tile.html");
            WebTestRequest request116 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-date-graph-tile.html"));
            request116.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request116.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request116;
            request116 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-date-graph-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-bar-charts-tile.html");
            WebTestRequest request117 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-bar-charts-tile.html"));
            request117.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request117.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request117;
            request117 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-bar-charts-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-customers-list-tile.html");
            WebTestRequest request118 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-customers-list-tile.html"));
            request118.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request118.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request118;
            request118 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-customers-list-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-raw-html-tile.html");
            WebTestRequest request119 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-raw-html-tile.html"));
            request119.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request119.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request119;
            request119 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-raw-html-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-basic-tile.html");
            WebTestRequest request120 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-basic-tile.html"));
            request120.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request120.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request120;
            request120 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-basic-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-switch-tile.html");
            WebTestRequest request121 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-switch-tile.html"));
            request121.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request121.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request121;
            request121 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-switch-tile.html");

            //////Need to check
            webTest.BeginTransaction(WebRequestPrefix + "api_sales_invoices_kpis_turnoverpast12months");
            WebTestRequest request124 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sales/invoices/kpis/turnoverpast12months"));
            request124.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request124.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request124;
            request124 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sales_invoices_kpis_turnoverpast12months");

            webTest.BeginTransaction(WebRequestPrefix + "api_sales_invoices_kpis_ageingoverdueinvoices");
            WebTestRequest request125 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sales/invoices/kpis/ageingoverdueinvoices"));
            request125.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request125.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request125;
            request125 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sales_invoices_kpis_ageingoverdueinvoices");
            /////////////////////

            webTest.BeginTransaction(WebRequestPrefix + "api_sales_invoices_kpis_top10bestcustomers_1");
            WebTestRequest request126 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sales/invoices/kpis/top10bestcustomers"));
            request126.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request126.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request126;
            request126 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sales_invoices_kpis_top10bestcustomers_1");

            //Not avaliable on 15463 build --Aditi
            // webTest.BeginTransaction(WebRequestPrefix + "tours_tours.json.html");
            // WebTestRequest request127 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_tours/tours.json.html"));
            // request127.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            // yield return request127;
            // request127 = null;
            // webTest.EndTransaction(WebRequestPrefix + "tours_tours.json.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_home_functions");
            WebTestRequest request129 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/home/functions"));
            request129.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request129.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request129;
            request129 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_home_functions");

            webTest.BeginTransaction(WebRequestPrefix + "api_home_reports");
            WebTestRequest request128 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/home/reports"));
            request128.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request128.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request128;
            request128 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_home_reports");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping_transactiontypes");
            WebTestRequest request130 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping/transactiontypes"));
            request130.Method = "OPTIONS";
            request130.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request130.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody request130Body = new StringHttpBody();
            request130Body.ContentType = "";
            request130Body.InsertByteOrderMark = false;
            request130Body.BodyString = "";
            request130.Body = request130Body;
            yield return request130;
            request130 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping_transactiontypes");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping_transactiontypes_1");
            WebTestRequest request131 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping/transactiontypes"));
            request131.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request131.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request131;
            request131 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping_transactiontypes_1");

            webTest.BeginTransaction(WebRequestPrefix + "components_main-menu_view_main-menu-item-view.html");
            WebTestRequest request132 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/main-menu/view/main-menu-item-view.html"));
            request132.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request132.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request132;
            request132 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_main-menu_view_main-menu-item-view.html");

            ////Added in Build#66.7.0 - AnkitaG
            //webTest.BeginTransaction(WebRequestPrefix + "v2_track");
            //WebTestRequest request138 = new WebTestRequest((webTest.Context["WebServer4"].ToString() + "/v2/track"));
            //request138.ThinkTime = 16;
            //request138.Method = "POST";
            //request138.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //StringHttpBody request138Body = new StringHttpBody();
            //request138Body.ContentType = "application/json";
            //request138Body.InsertByteOrderMark = false;
            //request138Body.BodyString = ""; //Need to check - AnkitaG
            //request138.Body = request138Body;
            //yield return request138;
            //request138 = null;
            //webTest.EndTransaction(WebRequestPrefix + "v2_track");

            //webTest.BeginTransaction(WebRequestPrefix + "api_notifications_2");
            //WebTestRequest request133 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/notifications"));
            //request133.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //request133.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //request133.QueryStringParameters.Add("since", threadData.CreatedDate, false, false);
            //yield return request133;
            //request133 = null;
            //webTest.EndTransaction(WebRequestPrefix + "api_notifications_2");
            webTest.EndTransaction(WebBTPrefix + "Login");
            #endregion
            
            Thread.Sleep(2000);
        }
        public static IEnumerable<WebTestRequest> LaunchLogin_FullAccess(this WebTest webTest, SharedThreadData threadData, WebTestRequestPlugin ObjRequest)
        {
            string WebBTPrefix = threadData.ScenarioPrefix;
            string WebRequestPrefix = string.Empty;

            #region HomePage
            webTest.BeginTransaction(WebBTPrefix + "Launch");
            WebRequestPrefix = "La_";

            webTest.BeginTransaction(WebRequestPrefix + "StartPage");
            WebTestRequest request1 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/"));
            request1.ExpectedResponseUrl = (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue);
            WebTestRequest request1Dependent1 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/auth/content/font/fontawesome-webfont.eot"));
            request1Dependent1.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue)));
            request1Dependent1.QueryStringParameters.Add("", "", false, false);
            request1.DependentRequests.Add(request1Dependent1);
            yield return request1;
            request1 = null;
            webTest.EndTransaction(WebRequestPrefix + "StartPage");
            webTest.EndTransaction(WebBTPrefix + "Launch");
            #endregion
            Thread.Sleep(2000);

            #region Change Language to English
            webTest.BeginTransaction(WebBTPrefix + "LanguageEnglish");
            WebRequestPrefix = "LE_";

            webTest.BeginTransaction(WebRequestPrefix + "auth_authentication_login");

            WebTestRequest request2 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/auth/authentication/login"));
            request2.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue)));
            request2.QueryStringParameters.Add("signin", ObjRequest.SigninValue, false, false);
            request2.QueryStringParameters.Add("culture", "en-GB", false, false);
            WebTestRequest request2Dependent1 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/auth/content/font/fontawesome-webfont.eot"));
            request2Dependent1.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue + "&culture=en-GB")));
            request2Dependent1.QueryStringParameters.Add("", "", false, false);
            request2.DependentRequests.Add(request2Dependent1);
            ExtractText extractionRule1 = new ExtractText();
            extractionRule1.StartsWith = "idsrv.xsrf' value='";
            extractionRule1.EndsWith = "' />";
            extractionRule1.Index = 0;
            extractionRule1.IgnoreCase = false;
            extractionRule1.UseRegularExpression = false;
            extractionRule1.HtmlDecode = true;
            extractionRule1.Required = false;
            extractionRule1.ContextParameterName = "IdsrvValue";
            request2.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule1.Extract);
            ExtractHiddenFields extractionRule17 = new ExtractHiddenFields();
            extractionRule17.Required = true;
            extractionRule17.HtmlDecode = true;
            extractionRule17.ContextParameterName = "1";
            request2.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule17.Extract);
            yield return request2;
            request2 = null;
            webTest.EndTransaction(WebRequestPrefix + "auth_authentication_login");

            webTest.EndTransaction(WebBTPrefix + "LanguageEnglish");
            #endregion
            //RTMonitor.Write(Color.Green, "Login User: " + threadData.UserName + "idsrv value" + webTest.Context["IdsrvValue"].ToString() + "\n");
            Thread.Sleep(2000);

            #region Login
            webTest.BeginTransaction(WebBTPrefix + "Login");
            WebRequestPrefix = "LI_";


            //WebTestRequest request3 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/auth/authentication/login"));
            //request3.ExpectedResponseUrl = (webTest.Context["LoginMain"].ToString() + @"/auth/authentication/connect/authorize?client_id=logonbox&redirect_uri=https%3A%2F%2Flogin.rc.dev.twinfield.com&response_mode=form_post&response_type=id_token%20token&scope="+ObjRequest.ScopeValue+"&state="+ObjRequest.StateValue+"&nonce="+ObjRequest.NonceValue+"&x-client-SKU=ID_NET&x-client-ver=1.0.40306.1554");
            //request3.Method = "POST";
            //request3.QueryStringParameters.Add("signin", ObjRequest.SigninValue, false, false);
            //FormPostHttpBody request3Body = new FormPostHttpBody();
            //request3Body.FormPostParameters.Add("idsrv.xsrf", webTest.Context["IdsrvValue"].ToString());
            //request3Body.FormPostParameters.Add("username", threadData.UserName);
            //request3Body.FormPostParameters.Add("password", threadData.Password);
            //request3Body.FormPostParameters.Add("txtCompanyID", threadData.CompanyID);
            //request3Body.FormPostParameters.Add("btnLogin", "");
            //request3.Body = request3Body;
            //yield return request3;
            //request3 = null;

            webTest.BeginTransaction(WebRequestPrefix + "auth_authentication_login_1");

            WebTestRequest request4 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/auth/authentication/login"));
            request4.ExpectedResponseUrl = (webTest.Context["LoginMain"].ToString() + @"/auth/authentication/connect/authorize?client_id=logonbox&redirect_uri=https%3A%2F%2Flogin.rc.dev.twinfield.com&response_mode=form_post&response_type=id_token%20token&scope=" + ObjRequest.ScopeValue + "&state=" + ObjRequest.StateValue + "&nonce=" + ObjRequest.NonceValue + "&x-client-SKU=ID_NET&x-client-ver=1.0.40306.1554");
            request4.Method = "POST";
            request4.QueryStringParameters.Add("signin", ObjRequest.SigninValue, false, false);
            FormPostHttpBody request4Body = new FormPostHttpBody();
            request4Body.FormPostParameters.Add("idsrv.xsrf", webTest.Context["IdsrvValue"].ToString());
            request4Body.FormPostParameters.Add("username", threadData.UserName);
            request4Body.FormPostParameters.Add("password", threadData.Password);
            request4Body.FormPostParameters.Add("txtCompanyID", threadData.Tenant);
            request4Body.FormPostParameters.Add("btnLogin", "");
            request4.Body = request4Body;
            ExtractHiddenFields extractionRule21 = new ExtractHiddenFields();
            extractionRule21.Required = true;
            extractionRule21.HtmlDecode = true;
            extractionRule21.ContextParameterName = "1";
            request4.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule21.Extract);
            ExtractText extractionRule2 = new ExtractText();
            extractionRule2.StartsWith = "<input type=\"hidden\" name=\"id_token\" value=\"";
            extractionRule2.EndsWith = "\" />";
            extractionRule2.Index = 0;
            extractionRule2.IgnoreCase = false;
            extractionRule2.UseRegularExpression = false;
            extractionRule2.HtmlDecode = true;
            extractionRule2.Required = false;
            extractionRule2.ContextParameterName = "ID_Token";
            request4.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule2.Extract);
            ExtractText extractionRule22 = new ExtractText();
            extractionRule22.StartsWith = "<input type=\"hidden\" name=\"access_token\" value=\"";
            extractionRule22.EndsWith = "\" />";
            extractionRule22.Index = 0;
            extractionRule22.IgnoreCase = false;
            extractionRule22.UseRegularExpression = false;
            extractionRule22.HtmlDecode = true;
            extractionRule22.Required = false;
            extractionRule22.ContextParameterName = "Access_Token";
            request4.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule22.Extract);
            ExtractText extractionRule3 = new ExtractText();
            extractionRule3.StartsWith = "<input type=\"hidden\" name=\"scope\" value=\"";
            extractionRule3.EndsWith = "\" />";
            extractionRule3.Index = 0;
            extractionRule3.IgnoreCase = false;
            extractionRule3.UseRegularExpression = false;
            extractionRule3.HtmlDecode = true;
            extractionRule3.Required = false;
            extractionRule3.ContextParameterName = "Scope";
            request4.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule3.Extract);
            ExtractText extractionRule4 = new ExtractText();
            extractionRule4.StartsWith = "<input type=\"hidden\" name=\"state\" value=\"";
            extractionRule4.EndsWith = "\" />";
            extractionRule4.Index = 0;
            extractionRule4.IgnoreCase = false;
            extractionRule4.UseRegularExpression = false;
            extractionRule4.HtmlDecode = true;
            extractionRule4.Required = false;
            extractionRule4.ContextParameterName = "State";
            request4.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule4.Extract);
            ExtractText extractionRule5 = new ExtractText();
            extractionRule5.StartsWith = "<input type=\"hidden\" name=\"session_state\" value=\"";
            extractionRule5.EndsWith = "\" />";
            extractionRule5.Index = 0;
            extractionRule5.IgnoreCase = false;
            extractionRule5.UseRegularExpression = false;
            extractionRule5.HtmlDecode = true;
            extractionRule5.Required = false;
            extractionRule5.ContextParameterName = "SessionState";
            request4.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule5.Extract);
            yield return request4;
            request4 = null;
            webTest.EndTransaction(WebRequestPrefix + "auth_authentication_login_1");
            
            webTest.BeginTransaction(WebRequestPrefix + "StartPage_1");
            
            WebTestRequest request89 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/"));
            request89.Method = "POST";
            request89.ExpectedResponseUrl = webTest.Context["AccountingURL"].ToString() + "/UI/#/Companies";
            request89.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + @"/auth/authentication/connect/authorize?client_id=logonbox&redirect_uri=https%3A%2F%2Flogin.rc.dev.twinfield.com&response_mode=form_post&response_type=id_token&scope=" + ObjRequest.ScopeValue + "&state=" + ObjRequest.StateValue + "&nonce=" + ObjRequest.NonceValue + "&x-client-SKU=ID_NET&x-client-ver=1.0.40306.1554")));
            FormPostHttpBody request89Body = new FormPostHttpBody();
            request89Body.FormPostParameters.Add("id_token", webTest.Context["ID_Token"].ToString());
            request89Body.FormPostParameters.Add("access_token", webTest.Context["Access_Token"].ToString());
            request89Body.FormPostParameters.Add("token_type", "Bearer");
            request89Body.FormPostParameters.Add("expires_in", "43200");
            request89Body.FormPostParameters.Add("scope", webTest.Context["Scope"].ToString());
            request89Body.FormPostParameters.Add("state", webTest.Context["State"].ToString());
            request89Body.FormPostParameters.Add("session_state", webTest.Context["SessionState"].ToString());
            request89.Body = request89Body;
            yield return request89;
            request89 = null;

            webTest.EndTransaction(WebRequestPrefix + "StartPage_1");

            webTest.BeginTransaction(WebRequestPrefix + "UI");
            WebTestRequest request7 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/UI/"));
            request7.ExpectedResponseUrl = (webTest.Context["AccountingURL"].ToString() + "/UI/#/Companies/All");
            ExtractText extractionRule6 = new ExtractText();
            extractionRule6.StartsWith = "__CDNStaticRootFolder=\"";
            extractionRule6.EndsWith = "\"";
            extractionRule6.Index = 0;
            extractionRule6.IgnoreCase = false;
            extractionRule6.UseRegularExpression = false;
            extractionRule6.HtmlDecode = true;
            extractionRule6.Required = false;
            extractionRule6.ContextParameterName = "BuildVersion";
            request7.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule6.Extract);
            yield return request7;
            threadData.BuildVersion = webTest.Context["BuildVersion"].ToString();
            request7 = null;
            webTest.EndTransaction(WebRequestPrefix + "UI");

            webTest.BeginTransaction(WebRequestPrefix + "api");
            WebTestRequest request8 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/"));
            request8.Method = "OPTIONS";
            request8.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request8.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request8;
            request8 = null;
            webTest.EndTransaction(WebRequestPrefix + "api");

            webTest.BeginTransaction(WebRequestPrefix + "fallback.json");
            WebTestRequest request9 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/UI/static_3.0.0." + threadData.BuildVersion + "/i18n/fallback.json"));
            request9.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*"));
            request9.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request9;
            request9 = null;
            webTest.EndTransaction(WebRequestPrefix + "fallback.json");

            webTest.BeginTransaction(WebRequestPrefix + "api");
            WebTestRequest request13 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/"));
            request13.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request13.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request13;
            request13 = null;
            webTest.EndTransaction(WebRequestPrefix + "api");

            webTest.BeginTransaction(WebRequestPrefix + "api");
            WebTestRequest request14 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/"));
            request14.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request14.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request14;
            request14 = null;
            webTest.EndTransaction(WebRequestPrefix + "api");

            webTest.BeginTransaction(WebRequestPrefix + "api_companies");
            WebTestRequest request15 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/companies"));
            request15.Method = "OPTIONS";
            request15.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request15.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request15;
            request15 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_companies");

            webTest.BeginTransaction(WebRequestPrefix + "initializations-and-translations.html");
            WebTestRequest request16 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/_infrastructure/localisation/initializations-and-transla" +
                    "tions.html"));
            request16.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request16.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            ExtractText extraction2 = new ExtractText();
            extraction2.StartsWith = "<twf:transfer-to-scope name=\"translations\" >";
            extraction2.EndsWith = "</twf:transfer-to-scope>";
            extraction2.IgnoreCase = false;
            extraction2.UseRegularExpression = false;
            extraction2.Required = true;
            extraction2.ExtractRandomMatch = false;
            extraction2.Index = 0;
            extraction2.HtmlDecode = true;
            extraction2.SearchInHeaders = false;
            extraction2.ContextParameterName = "InitializationAndTranslation";
            request16.ExtractValues += new EventHandler<ExtractionEventArgs>(extraction2.Extract);
            yield return request16;
            request16 = null;
            webTest.EndTransaction(WebRequestPrefix + "initializations-and-translations.html");

            webTest.BeginTransaction(WebRequestPrefix + "ui-timeline-common-titles.html");
            WebTestRequest request17 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/timeline/internal/ui-timeline-common-titles.html"));
            request17.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request17.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request17;
            request17 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui-timeline-common-titles.html");

            webTest.BeginTransaction(WebRequestPrefix + "ui-timeline-common-translations.html");
            WebTestRequest request18 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/timeline/internal/ui-timeline-common-translations.html"));
            request18.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request18.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request18;
            request18 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui-timeline-common-translations.html");

            webTest.BeginTransaction(WebRequestPrefix + "@configuration-localise.html");
            WebTestRequest request19 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/_infrastructure/configuration/@configuration-localise.ht" +
                    "ml"));
            request19.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request19.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request19;
            request19 = null;
            webTest.EndTransaction(WebRequestPrefix + "@configuration-localise.html");

            webTest.BeginTransaction(WebRequestPrefix + "company-import-localise");
            WebTestRequest request20 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/company-import/@company-import-localise.html"));
            request20.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request20.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request20;
            request20 = null;
            webTest.EndTransaction(WebRequestPrefix + "company-import-localise");

            webTest.BeginTransaction(WebRequestPrefix + "app-store-localise");
            WebTestRequest request21 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/app-store/@app-store-localise.html"));
            request21.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request21.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request21;
            request21 = null;
            webTest.EndTransaction(WebRequestPrefix + "app-store-localise");

            webTest.BeginTransaction(WebRequestPrefix + "feeds-localise");
            WebTestRequest request22 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/feeds/@feeds-localise.html"));
            request22.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request22.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request22;
            request22 = null;
            webTest.EndTransaction(WebRequestPrefix + "feeds-localise");

            webTest.BeginTransaction(WebRequestPrefix + "bank-localise");
            WebTestRequest request23 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/@bank-localise.html"));
            request23.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request23.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request23;
            request23 = null;
            webTest.EndTransaction(WebRequestPrefix + "bank-localise");

            webTest.BeginTransaction(WebRequestPrefix + "vat-localise");
            WebTestRequest request24 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/vat/@vat-localise.html"));
            request24.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request24.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request24;
            request24 = null;
            webTest.EndTransaction(WebRequestPrefix + "vat-localise");

            webTest.BeginTransaction(WebRequestPrefix + "budgets-localise");
            WebTestRequest request25 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/budgets/@budgets-localise.html"));
            request25.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request25.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request25;
            request25 = null;
            webTest.EndTransaction(WebRequestPrefix + "budgets-localise");

            webTest.BeginTransaction(WebRequestPrefix + "companies-localise");
            WebTestRequest request26 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/companies/@companies-localise.html"));
            request26.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request26.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request26;
            request26 = null;
            webTest.EndTransaction(WebRequestPrefix + "companies-localise");

            webTest.BeginTransaction(WebRequestPrefix + "customers-localise");
            WebTestRequest request27 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/customers/@customers-localise.html"));
            request27.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request27.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request27;
            request27 = null;
            webTest.EndTransaction(WebRequestPrefix + "customers-localise");

            webTest.BeginTransaction(WebRequestPrefix + "dimensions-localise");
            WebTestRequest request28 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/dimensions/@dimensions-localise.html"));
            request28.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request28.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request28;
            request28 = null;
            webTest.EndTransaction(WebRequestPrefix + "dimensions-localise");

            webTest.BeginTransaction(WebRequestPrefix + "dockets-localise");
            WebTestRequest request29 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/dockets/@dockets-localise.html"));
            request29.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request29.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request29;
            request29 = null;
            webTest.EndTransaction(WebRequestPrefix + "dockets-localise");

            webTest.BeginTransaction(WebRequestPrefix + "document-share-localise");
            WebTestRequest request30 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/document-share/@document-share-localise.html"));
            request30.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request30.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request30;
            request30 = null;
            webTest.EndTransaction(WebRequestPrefix + "document-share-localise");

            webTest.BeginTransaction(WebRequestPrefix + "financial-professionals-localise");
            WebTestRequest request31 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/financial-professionals/@financial-professionals-localise.html"));
            request31.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request31.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request31;
            request31 = null;
            webTest.EndTransaction(WebRequestPrefix + "financial-professionals-localise");

            webTest.BeginTransaction(WebRequestPrefix + "diagnostic-contexts-localise");
            WebTestRequest request32 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/diagnostic-contexts/@diagnostic-contexts-localise.html"));
            request32.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request32.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request32;
            request32 = null;
            webTest.EndTransaction(WebRequestPrefix + "diagnostic-contexts-localise");

            webTest.BeginTransaction(WebRequestPrefix + "classes-localise");
            WebTestRequest request33 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/classes/@classes-localise.html"));
            request33.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request33.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request33;
            request33 = null;
            webTest.EndTransaction(WebRequestPrefix + "classes-localise");

            webTest.BeginTransaction(WebRequestPrefix + "postings-localise");
            WebTestRequest request34 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/postings/@postings-localise.html"));
            request34.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request34.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request34;
            request34 = null;
            webTest.EndTransaction(WebRequestPrefix + "postings-localise");

            webTest.BeginTransaction(WebRequestPrefix + "assets-localise");
            WebTestRequest request35 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/assets/@assets-localise.html"));
            request35.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request35.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request35;
            request35 = null;
            webTest.EndTransaction(WebRequestPrefix + "assets-localise");

            webTest.BeginTransaction(WebRequestPrefix + "configuration-localise");
            WebTestRequest request36 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/central-maintenance/@configuration-localise.html"));
            request36.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request36.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request36;
            request36 = null;
            webTest.EndTransaction(WebRequestPrefix + "configuration-localise");

            webTest.BeginTransaction(WebRequestPrefix + "asset-settings-localise");
            WebTestRequest request37 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/settings/@asset-settings-localise.html"));
            request37.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request37.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request37;
            request37 = null;
            webTest.EndTransaction(WebRequestPrefix + "asset-settings-localise");

            webTest.BeginTransaction(WebRequestPrefix + "origins-localise");
            WebTestRequest request38 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/settings/origins/@origins-localise.html"));
            request38.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request38.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request38;
            request38 = null;
            webTest.EndTransaction(WebRequestPrefix + "origins-localise");

            webTest.BeginTransaction(WebRequestPrefix + "reasons-localise");
            WebTestRequest request39 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/settings/reasons/@reasons-localise.html"));
            request39.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request39.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request39;
            request39 = null;
            webTest.EndTransaction(WebRequestPrefix + "reasons-localise");

            webTest.BeginTransaction(WebRequestPrefix + "reports-localise");
            WebTestRequest request40 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/reports/@reports-localise.html"));
            request40.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request40.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request40;
            request40 = null;
            webTest.EndTransaction(WebRequestPrefix + "reports-localise");

            webTest.BeginTransaction(WebRequestPrefix + "property-tax-localise");
            WebTestRequest request41 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/property-tax/@property-tax-localise.html"));
            request41.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request41.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request41;
            request41 = null;
            webTest.EndTransaction(WebRequestPrefix + "property-tax-localise");

            webTest.BeginTransaction(WebRequestPrefix + "groups-localise");
            WebTestRequest request42 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/groups/@groups-localise.html"));
            request42.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request42.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request42;
            request42 = null;
            webTest.EndTransaction(WebRequestPrefix + "groups-localise");

            webTest.BeginTransaction(WebRequestPrefix + "manual-conversion-localise");
            WebTestRequest request43 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/manual-conversion/@manual-conversion-localise.html"));
            request43.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request43.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request43;
            request43 = null;
            webTest.EndTransaction(WebRequestPrefix + "manual-conversion-localise");

            webTest.BeginTransaction(WebRequestPrefix + "historical-values-localise");
            WebTestRequest request44 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/historical-values/@historical-values-localise.html"));
            request44.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request44.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request44;
            request44 = null;
            webTest.EndTransaction(WebRequestPrefix + "historical-values-localise");

            webTest.BeginTransaction(WebRequestPrefix + "fixed-assets-localise");
            WebTestRequest request45 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/fixed-assets/@fixed-assets-localise.html"));
            request45.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request45.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request45;
            request45 = null;
            webTest.EndTransaction(WebRequestPrefix + "fixed-assets-localise");

            webTest.BeginTransaction(WebRequestPrefix + "user-localise");
            WebTestRequest request46 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/user/@user-localise.html"));
            request46.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request46.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request46;
            request46 = null;
            webTest.EndTransaction(WebRequestPrefix + "user-localise");

            webTest.BeginTransaction(WebRequestPrefix + "preaccounting-localise");
            WebTestRequest request47 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/preaccounting/@preaccounting-localise.html"));
            request47.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request47.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request47;
            request47 = null;
            webTest.EndTransaction(WebRequestPrefix + "preaccounting-localise");

            webTest.BeginTransaction(WebRequestPrefix + "projects-localise");
            WebTestRequest request48 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/projects/@projects-localise.html"));
            request48.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request48.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request48;
            request48 = null;
            webTest.EndTransaction(WebRequestPrefix + "projects-localise");

            webTest.BeginTransaction(WebRequestPrefix + "purchase-localise");
            WebTestRequest request49 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/purchase/invoices/@purchase-localise.html"));
            request49.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request49.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request49;
            request49 = null;
            webTest.EndTransaction(WebRequestPrefix + "purchase-localise");

            webTest.BeginTransaction(WebRequestPrefix + "expense-types-localise");
            WebTestRequest request50 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/purchase/expense-types/@expense-types-localise.html"));
            request50.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request50.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request50;
            request50 = null;
            webTest.EndTransaction(WebRequestPrefix + "expense-types-localise");

            webTest.BeginTransaction(WebRequestPrefix + "reports-localise");
            WebTestRequest request51 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/reports/@reports-localise.html"));
            request51.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request51.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request51;
            request51 = null;
            webTest.EndTransaction(WebRequestPrefix + "reports-localise");

            webTest.BeginTransaction(WebRequestPrefix + "articles-localise");
            WebTestRequest request52 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/articles/@articles-localise.html"));
            request52.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request52.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request52;
            request52 = null;
            webTest.EndTransaction(WebRequestPrefix + "articles-localise");

            webTest.BeginTransaction(WebRequestPrefix + "credit-management-localise");
            WebTestRequest request53 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/credit-management/@credit-management-localise.html"));
            request53.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request53.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request53;
            request53 = null;
            webTest.EndTransaction(WebRequestPrefix + "credit-management-localise");

            webTest.BeginTransaction(WebRequestPrefix + "credit-management-settings-localise");
            WebTestRequest request54 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/credit-management/@credit-management-settings-localise.html"));
            request54.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request54.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request54;
            request54 = null;
            webTest.EndTransaction(WebRequestPrefix + "credit-management-settings-localise");

            webTest.BeginTransaction(WebRequestPrefix + "invoicing-localise");
            WebTestRequest request55 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/invoicing/@invoicing-localise.html"));
            request55.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request55.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request55;
            request55 = null;
            webTest.EndTransaction(WebRequestPrefix + "invoicing-localise");

            webTest.BeginTransaction(WebRequestPrefix + "timeframes-localise");
            WebTestRequest request56 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/timeframes/@timeframes-localise.html"));
            request56.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request56.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request56;
            request56 = null;
            webTest.EndTransaction(WebRequestPrefix + "timeframes-localise");

            webTest.BeginTransaction(WebRequestPrefix + "credit-note-localise");
            WebTestRequest request57 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/credit-note/@credit-note-localise.html"));
            request57.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request57.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request57;
            request57 = null;
            webTest.EndTransaction(WebRequestPrefix + "credit-note-localise");

            webTest.BeginTransaction(WebRequestPrefix + "quotes-localise");
            WebTestRequest request58 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/quotes/@quotes-localise.html"));
            request58.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request58.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request58;
            request58 = null;
            webTest.EndTransaction(WebRequestPrefix + "quotes-localise");

            webTest.BeginTransaction(WebRequestPrefix + "revenue-types-localise");
            WebTestRequest request59 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/revenue-types/@revenue-types-localise.html"));
            request59.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request59.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request59;
            request59 = null;
            webTest.EndTransaction(WebRequestPrefix + "revenue-types-localise");

            webTest.BeginTransaction(WebRequestPrefix + "sales-localise");
            WebTestRequest request60 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/sales/@sales-localise.html"));
            request60.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request60.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request60;
            request60 = null;
            webTest.EndTransaction(WebRequestPrefix + "sales-localise");

            webTest.BeginTransaction(WebRequestPrefix + "company-localise");
            WebTestRequest request61 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/settings/company/@company-localise.html"));
            request61.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request61.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request61;
            request61 = null;
            webTest.EndTransaction(WebRequestPrefix + "company-localise");

            webTest.BeginTransaction(WebRequestPrefix + "organisation-localise");
            WebTestRequest request62 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/settings/organisation/@organisation-localise.html"));
            request62.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request62.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request62;
            request62 = null;
            webTest.EndTransaction(WebRequestPrefix + "organisation-localise");

            webTest.BeginTransaction(WebRequestPrefix + "access-localise");
            WebTestRequest request63 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/settings/access/@access-localise.html"));
            request63.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request63.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request63;
            request63 = null;
            webTest.EndTransaction(WebRequestPrefix + "access-localise");

            webTest.BeginTransaction(WebRequestPrefix + "suppliers-localise");
            WebTestRequest request64 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/suppliers/@suppliers-localise.html"));
            request64.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request64.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request64;
            request64 = null;
            webTest.EndTransaction(WebRequestPrefix + "suppliers-localise");

            webTest.BeginTransaction(WebRequestPrefix + "tax-localise");
            WebTestRequest request65 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/tax/@tax-localise.html"));
            request65.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request65.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request65;
            request65 = null;
            webTest.EndTransaction(WebRequestPrefix + "tax-localise");

            webTest.BeginTransaction(WebRequestPrefix + "transactions-localise");
            WebTestRequest request66 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/transactions/@transactions-localise.html"));
            request66.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request66.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request66;
            request66 = null;
            webTest.EndTransaction(WebRequestPrefix + "transactions-localise");

            webTest.BeginTransaction(WebRequestPrefix + "year-end-localise");
            WebTestRequest request67 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/year-end/@year-end-localise.html"));
            request67.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request67.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request67;
            request67 = null;
            webTest.EndTransaction(WebRequestPrefix + "year-end-localise");

            webTest.BeginTransaction(WebRequestPrefix + "select2-choices");
            WebTestRequest request68 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/feeds/bank-search/templates/select2-choices.html"));
            request68.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request68.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request68;
            request68 = null;
            webTest.EndTransaction(WebRequestPrefix + "select2-choices");

            webTest.BeginTransaction(WebRequestPrefix + "select2-match");
            WebTestRequest request69 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/feeds/bank-search/templates/select2-match.html"));
            request69.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request69.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request69;
            request69 = null;
            webTest.EndTransaction(WebRequestPrefix + "select2-match");

            webTest.BeginTransaction(WebRequestPrefix + "select2-select");
            WebTestRequest request70 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/feeds/bank-search/templates/select2-select.html"));
            request70.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request70.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request70;
            request70 = null;
            webTest.EndTransaction(WebRequestPrefix + "select2-select");

            webTest.BeginTransaction(WebRequestPrefix + "proposed-assignments-view");
            WebTestRequest request71 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/assignments/proposed-assignments-view.html"));
            request71.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request71.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request71;
            request71 = null;
            webTest.EndTransaction(WebRequestPrefix + "proposed-assignments-view");

            webTest.BeginTransaction(WebRequestPrefix + "assigned-view");
            WebTestRequest request72 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/assignments/assigned-view.html"));
            request72.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request72.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request72;
            request72 = null;
            webTest.EndTransaction(WebRequestPrefix + "assigned-view");
            
            webTest.BeginTransaction(WebRequestPrefix + "en-GB_bank_assignments_to-assign-view.html");
            WebTestRequest request73 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/assignments/to-assign-view.html"));
            request73.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request73.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request73;
            request73 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB_bank_assignments_to-assign-view.html");

            //Commented for RC-Dev Enviornmnet
            //webTest.BeginTransaction(WebRequestPrefix + "en-GB_bank_assignments_other-view.html");
            //WebTestRequest request74 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/assignments/other-view.html"));
            //request74.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            //request74.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //yield return request74;
            //request74 = null;
            //webTest.EndTransaction(WebRequestPrefix + "en-GB_bank_assignments_other-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "en-GB_bank_listing-to-much-data.html");
            WebTestRequest request75 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/bank/listing-to-much-data.html"));
            request75.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request75.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request75;
            request75 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB_bank_listing-to-much-data.html");

            webTest.BeginTransaction(WebRequestPrefix + "form_fields_select_view_nf-select2-choices.html");
            WebTestRequest request76 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/form/fields/select/view/nf-select2-choices.html"));
            request76.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request76.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request76;
            request76 = null;
            webTest.EndTransaction(WebRequestPrefix + "form_fields_select_view_nf-select2-choices.html");

            webTest.BeginTransaction(WebRequestPrefix + "form_fields_select_view_nf-select2-select.html");
            WebTestRequest request77 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/form/fields/select/view/nf-select2-select.html"));
            request77.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request77.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request77;
            request77 = null;
            webTest.EndTransaction(WebRequestPrefix + "form_fields_select_view_nf-select2-select.html");

            webTest.BeginTransaction(WebRequestPrefix + "list_view_replace-view_not-available-view.html");
            WebTestRequest request78 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/replace-view/not-available-view.html"));
            request78.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request78.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request78;
            request78 = null;
            webTest.EndTransaction(WebRequestPrefix + "list_view_replace-view_not-available-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "system_sitemap-search_templates_select2-choice");
            WebTestRequest request79 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/system/sitemap-search/templates/select2-choice" +
                    "s.html"));
            request79.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request79.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request79;
            request79 = null;
            webTest.EndTransaction(WebRequestPrefix + "system_sitemap-search_templates_select2-choice");

            webTest.BeginTransaction(WebRequestPrefix + "system_sitemap-search_templates_select2-match.");
            WebTestRequest request80 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/system/sitemap-search/templates/select2-match." +
                    "html"));
            request80.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request80.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request80;
            request80 = null;
            webTest.EndTransaction(WebRequestPrefix + "system_sitemap-search_templates_select2-match.");

            webTest.BeginTransaction(WebRequestPrefix + "system_sitemap-search_templates_select2-select");
            WebTestRequest request81 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/system/sitemap-search/templates/select2-select" +
                    ".html"));
            request81.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request81.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request81;
            request81 = null;
            webTest.EndTransaction(WebRequestPrefix + "system_sitemap-search_templates_select2-select");

            webTest.BeginTransaction(WebRequestPrefix + "top-bar_search_templates_select2-choices.html");
            WebTestRequest request82 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/search/templates/select2-choices.html"));
            request82.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request82.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request82;
            request82 = null;
            webTest.EndTransaction(WebRequestPrefix + "top-bar_search_templates_select2-choices.html");

            webTest.BeginTransaction(WebRequestPrefix + "top-bar_search_templates_select2-match.html");
            WebTestRequest request83 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/search/templates/select2-match.html"));
            request83.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request83.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request83;
            request83 = null;
            webTest.EndTransaction(WebRequestPrefix + "top-bar_search_templates_select2-match.html");

            webTest.BeginTransaction(WebRequestPrefix + "top-bar_search_templates_select2-select.html");
            WebTestRequest request84 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/search/templates/select2-select.html"));
            request84.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request84.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request84;
            request84 = null;
            webTest.EndTransaction(WebRequestPrefix + "top-bar_search_templates_select2-select.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_companies_summary");
            WebTestRequest request85 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/companies/summary"));
            request85.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request85.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request85;
            request85 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_companies_summary");

            webTest.BeginTransaction(WebRequestPrefix + "api_users");
            WebTestRequest request86 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users"));
            request86.Method = "OPTIONS";
            request86.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request86.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request86;
            request86 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users");

            webTest.BeginTransaction(WebRequestPrefix + "i18n_en-GB.json");
            WebTestRequest request87 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/UI/static_3.0.0." + threadData.BuildVersion + "/i18n/en-GB.json"));
            request87.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*"));
            request87.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request87;
            request87 = null;
            webTest.EndTransaction(WebRequestPrefix + "i18n_en-GB.json");

            webTest.BeginTransaction(WebRequestPrefix + "i18n_en-GB.json_1");
            WebTestRequest request88 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/UI/static_3.0.0." + threadData.BuildVersion + "/i18n/en-GB.json"));
            request88.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*"));
            request88.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request88;
            request88 = null;
            webTest.EndTransaction(WebRequestPrefix + "i18n_en-GB.json_1");

            webTest.BeginTransaction(WebRequestPrefix + "en-GB_companies_companies.html");
            WebTestRequest request890 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/companies/companies.html"));
            request890.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request890.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request890;
            request890 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB_companies_companies.html");

            webTest.BeginTransaction(WebRequestPrefix + "en-GB__components_top-bar_top-bar.html");
            WebTestRequest request90 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/top-bar.html"));
            request90.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request90.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request90;
            request90 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB__components_top-bar_top-bar.html");

            webTest.BeginTransaction(WebRequestPrefix + "en-GB__components_main-menu_main-menu.html");
            WebTestRequest request91 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/main-menu/main-menu.html"));
            request91.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request91.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request91;
            request91 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB__components_main-menu_main-menu.html");

            webTest.BeginTransaction(WebRequestPrefix + "en-GB__components_breadcrumb_breadcrumb.html");
            WebTestRequest request92 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/breadcrumb/breadcrumb.html"));
            request92.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request92.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request92;
            request92 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB__components_breadcrumb_breadcrumb.html");

            webTest.BeginTransaction(WebRequestPrefix + "activity-title_view_ui-activity-title-view.html");
            WebTestRequest request93 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/activity/activity-title/view/ui-activity-title-view.html"));
            request93.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request93.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request93;
            request93 = null;
            webTest.EndTransaction(WebRequestPrefix + "activity-title_view_ui-activity-title-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "ui_buckets_view_ui-activity-buckets-view.html");
            WebTestRequest request94 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/buckets/view/ui-activity-buckets-view.html"));
            request94.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request94.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request94;
            request94 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_buckets_view_ui-activity-buckets-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "ui_related_view_ui-activity-related-view.html");
            WebTestRequest request95 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/related/view/ui-activity-related-view.html"));
            request95.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request95.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request95;
            request95 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_related_view_ui-activity-related-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_officemanagement");
            WebTestRequest request96 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/officemanagement"));
            request96.Method = "OPTIONS";
            request96.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request96.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            request96 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_officemanagement");

            webTest.BeginTransaction(WebRequestPrefix + "api_organisation");
            WebTestRequest request97 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/organisation"));
            request97.Method = "OPTIONS";
            request97.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request97.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request97;
            request97 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_organisation");

            webTest.BeginTransaction(WebRequestPrefix + "en-GB__ui_list_view_ui-list-view.html");
            WebTestRequest request98 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/ui-list-view.html"));
            request98.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request98.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request98;
            request98 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB__ui_list_view_ui-list-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "top-bar_system-feedback_system-feedback.html");
            WebTestRequest request99 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/system-feedback/system-feedback.html"));
            request99.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request99.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request99;
            request99 = null;
            webTest.EndTransaction(WebRequestPrefix + "top-bar_system-feedback_system-feedback.html");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_cabin-regular.woff");
            WebTestRequest request101 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/cabin-regular.woff"));
            request101.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request101;
            request101 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_cabin-regular.woff");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_cabin-bold.woff");
            WebTestRequest request102 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/cabin-bold.woff"));
            request102.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request102;
            request102 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_cabin-bold.woff");

            webTest.BeginTransaction(WebRequestPrefix + "components_main-menu_view_main-menu-view.html");
            WebTestRequest request103 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/main-menu/view/main-menu-view.html"));
            request103.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request103.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request103;
            request103 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_main-menu_view_main-menu-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping");
            WebTestRequest request104 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping"));
            request104.Method = "OPTIONS";
            request104.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request104.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request104;
            request104 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_fontawesome-webfont.eot");
            WebTestRequest request105 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/fontawesome-webfont.eot"));
            request105.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            request105.QueryStringParameters.Add("", "", false, false);
            yield return request105;
            request105 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_fontawesome-webfont.eot");

            webTest.BeginTransaction(WebRequestPrefix + "api_companies_summary_1");
            WebTestRequest request106 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/companies/summary"));
            request106.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request106.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request106;
            request106 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_companies_summary_1");

            webTest.BeginTransaction(WebRequestPrefix + "en-GB__ui_buckets_view_ui-buckets-carousel.html");
            WebTestRequest request107 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/buckets/view/ui-buckets-carousel.html"));
            request107.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request107.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request107;
            request107 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB__ui_buckets_view_ui-buckets-carousel.html");

            webTest.BeginTransaction(WebRequestPrefix + "view_flex-columns_ui-list-view-flex-headers.html");
            WebTestRequest request108 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/flex-columns/ui-list-view-flex-headers.html"));
            request108.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request108.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request108;
            request108 = null;
            webTest.EndTransaction(WebRequestPrefix + "view_flex-columns_ui-list-view-flex-headers.html");

            webTest.BeginTransaction(WebRequestPrefix + "ui_activity_actions_view_ui-actions-view.html");
            WebTestRequest request109 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/activity/actions/view/ui-actions-view.html"));
            request109.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request109.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request109;
            request109 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_activity_actions_view_ui-actions-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_firasans-regular.woff");
            WebTestRequest request110 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/firasans-regular.woff"));
            request110.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request110;
            request110 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_firasans-regular.woff");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_firasans-italic.woff");
            WebTestRequest request111 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/firasans-italic.woff"));
            request111.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request111;
            request111 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_firasans-italic.woff");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_firasans-medium.woff");
            WebTestRequest request112 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/firasans-medium.woff"));
            request112.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request112;
            request112 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_firasans-medium.woff");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_firasans-mediumitalic.woff");
            WebTestRequest request113 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/firasans-mediumitalic.woff"));
            request113.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request113;
            request113 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_firasans-mediumitalic.woff");

            webTest.BeginTransaction(WebRequestPrefix + "ui_list_view_partials_ui-list-view-addnew.html");
            WebTestRequest request114 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/partials/ui-list-view-addnew.html"));
            request114.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request114.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request114;
            request114 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_list_view_partials_ui-list-view-addnew.html");

            webTest.BeginTransaction(WebRequestPrefix + "ui_list_view_partials_ui-list-view-query.html");
            WebTestRequest request115 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/partials/ui-list-view-query.html"));
            request115.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request115.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request115;
            request115 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_list_view_partials_ui-list-view-query.html");

            webTest.BeginTransaction(WebRequestPrefix + "list_view_partials_ui-list-view-navigation.html");
            WebTestRequest request116 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/partials/ui-list-view-navigation.html"));
            request116.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request116.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request116;
            request116 = null;
            webTest.EndTransaction(WebRequestPrefix + "list_view_partials_ui-list-view-navigation.html");

            webTest.BeginTransaction(WebRequestPrefix + "ui_list_view_partials_ui-list-view-filters.html");
            WebTestRequest request117 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/partials/ui-list-view-filters.html"));
            request117.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request117.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request117;
            request117 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_list_view_partials_ui-list-view-filters.html");

            webTest.BeginTransaction(WebRequestPrefix + "list_view_partials_ui-list-view-pagination.html");
            WebTestRequest request118 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/partials/ui-list-view-pagination.html"));
            request118.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request118.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request118;
            request118 = null;
            webTest.EndTransaction(WebRequestPrefix + "list_view_partials_ui-list-view-pagination.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_companies");
            WebTestRequest request119 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/companies"));
            request119.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request119.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            request119.QueryStringParameters.Add("asc", "true", false, false);
            request119.QueryStringParameters.Add("limit", "100", false, false);
            request119.QueryStringParameters.Add("offset", "0", false, false);
            request119.QueryStringParameters.Add("orderBy", "name", false, false);
            yield return request119;
            request119 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_companies");


            var CompaniesList = JsonConvert.DeserializeObject<CompaniesDetails>(webTest.LastResponse.BodyString);
            var FilteredCompaniesList=CompaniesList.items.Where(s => s.name.Contains("TESTCOMPANY01")).ToList<Item5>();
            var CompaniesList2 = FilteredCompaniesList.Random<Item5>();
            string officeManagementID = CompaniesList2.id;

            webTest.Context.Add("OfficeMgtID", officeManagementID);

            webTest.BeginTransaction(WebRequestPrefix + "api_users_user");
            WebTestRequest request120 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName));
            request120.Method = "OPTIONS";
            request120.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request120.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request120;
            request120 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users_user");

            webTest.BeginTransaction(WebRequestPrefix + "ui_buckets_view_ui-activity-bucket-view.html");
            WebTestRequest request121 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/buckets/view/ui-activity-bucket-view.html"));
            request121.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request121.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request121;
            request121 = null;
            webTest.EndTransaction(WebRequestPrefix + "ui_buckets_view_ui-activity-bucket-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "actions_view_ui-actions-notifications-view.html");
            WebTestRequest request122 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/activity/actions/view/ui-actions-notifications-view.html"));
            request122.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request122.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request122;
            request122 = null;
            webTest.EndTransaction(WebRequestPrefix + "actions_view_ui-actions-notifications-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "view_partials_ui-actions-view-primaries.html");
            WebTestRequest request123 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/activity/actions/view/partials/ui-actions-view-primaries.html"));
            request123.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, *;version=latest"));
            request123.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request123;
            request123 = null;
            webTest.EndTransaction(WebRequestPrefix + "view_partials_ui-actions-view-primaries.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping_dimensiontypes");
            WebTestRequest request124 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping/dimensiontypes"));
            request124.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request124.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request124;
            request124 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping_dimensiontypes");

            webTest.BeginTransaction(WebRequestPrefix + "api_home");
            WebTestRequest request125 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/home"));
            request125.Method = "OPTIONS";
            request125.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request125.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request125;
            request125 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_home");

            webTest.BeginTransaction(WebRequestPrefix + "api_organisation_logo");
            WebTestRequest request126 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/organisation/logo"));
            request126.Method = "OPTIONS";
            request126.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request126.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request126;
            request126 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_organisation_logo");

            webTest.BeginTransaction(WebRequestPrefix + "api_organisation_logo_1");
            WebTestRequest request127 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/organisation/logo"));
            request127.Method = "OPTIONS";
            request127.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request127.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request127;
            request127 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_organisation_logo_1");

            webTest.BeginTransaction(WebRequestPrefix + "api_home_functions");
            WebTestRequest request128 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/home/functions"));
            request128.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request128.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request128;
            request128 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_home_functions");


            ////////403 - Forbidden
            //WebTestRequest request129 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/viewedtours"));
            //request129.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //request129.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            ////ExtractText extractionRule7 = new ExtractText();
            ////extractionRule7.StartsWith = "api/officemanagement/offices/office/";
            ////extractionRule7.EndsWith = "\"},\"templateoffices";
            ////extractionRule7.Index = 0;
            ////extractionRule7.IgnoreCase = false;
            ////extractionRule7.UseRegularExpression = false;
            ////extractionRule7.HtmlDecode = true;
            ////extractionRule7.Required = false;
            ////extractionRule7.ContextParameterName = "OfficeMgtID";
            ////request129.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule7.Extract);
            //yield return request129;
            //request129 = null;

            //WebTestRequest request130 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/previouslogon"));
            //request130.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //request130.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //yield return request130;
            //request130 = null;

            webTest.BeginTransaction(WebRequestPrefix + "view_flex-columns_ui-list-view-flex-record.html");
            WebTestRequest request131 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/flex-columns/ui-list-view-flex-record.html"));
            request131.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, *;version=latest"));
            request131.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request131;
            request131 = null;
            webTest.EndTransaction(WebRequestPrefix + "view_flex-columns_ui-list-view-flex-record.html");

            webTest.BeginTransaction(WebRequestPrefix + "view_partials_ui-list-view-record-actions.html");
            WebTestRequest request132 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/partials/ui-list-view-record-actions.html"));
            request132.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request132.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request132;
            request132 = null;
            webTest.EndTransaction(WebRequestPrefix + "view_partials_ui-list-view-record-actions.html");

            webTest.BeginTransaction(WebRequestPrefix + "view_flex-columns_ui-list-view-flex-column.html");
            WebTestRequest request133 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/flex-columns/ui-list-view-flex-column.html"));
            request133.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request133.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request133;
            request133 = null;
            webTest.EndTransaction(WebRequestPrefix + "view_flex-columns_ui-list-view-flex-column.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping_transactiontypes");
            WebTestRequest request134 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping/transactiontypes"));
            request134.Method = "OPTIONS";
            request134.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            request134.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request134;
            request134 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping_transactiontypes");

            webTest.BeginTransaction(WebRequestPrefix + "view_partials_ui-list-view-record-indicator.html");
            WebTestRequest request135 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_ui/list/view/partials/ui-list-view-record-indicator.html"));
            request135.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request135.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request135;
            request135 = null;
            webTest.EndTransaction(WebRequestPrefix + "view_partials_ui-list-view-record-indicator.html");

            webTest.BeginTransaction(WebRequestPrefix + "main-menu_view_main-menu-item-view.html");
            WebTestRequest request136 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/main-menu/view/main-menu-item-view.html"));
            request136.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            request136.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request136;
            request136 = null;
            webTest.EndTransaction(WebRequestPrefix + "main-menu_view_main-menu-item-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "bundles_fonts_twinfield-icomoon.eot");
            WebTestRequest request137 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/bundles/fonts/twinfield-icomoon.eot"));
            request137.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request137;
            request137 = null;
            webTest.EndTransaction(WebRequestPrefix + "bundles_fonts_twinfield-icomoon.eot");
            
            webTest.EndTransaction(WebBTPrefix + "Login");
            #endregion

            Thread.Sleep(2000);
            
            #region DM_SelectCompany
            webTest.BeginTransaction(WebBTPrefix + "SelectCompany");
            WebRequestPrefix = "SC_";

            webTest.BeginTransaction(WebRequestPrefix + "api_officemanagement_offices_office");
            WebTestRequest requestSelectCompany134 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/officemanagement/offices/office/" + officeManagementID + "/switch"));
            requestSelectCompany134.Method = "POST";
            requestSelectCompany134.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany134.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            StringHttpBody requestSelectCompany134Body = new StringHttpBody();
            requestSelectCompany134Body.ContentType = "application/json;charset=utf-8";
            requestSelectCompany134Body.InsertByteOrderMark = false;
            requestSelectCompany134Body.BodyString = "{}";
            requestSelectCompany134.Body = requestSelectCompany134Body;
            yield return requestSelectCompany134;
            requestSelectCompany134 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_officemanagement_offices_office");

            webTest.BeginTransaction(WebRequestPrefix + "api");
            WebTestRequest requestSelectCompany135 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/"));
            requestSelectCompany135.Method = "OPTIONS";
            requestSelectCompany135.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany135.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany135;
            requestSelectCompany135 = null;
            webTest.EndTransaction(WebRequestPrefix + "api");

            webTest.BeginTransaction(WebRequestPrefix + "api_1");
            WebTestRequest requestSelectCompany136 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/"));
            requestSelectCompany136.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany136.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany136;
            requestSelectCompany136 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_1");

            webTest.BeginTransaction(WebRequestPrefix + "api_users");
            WebTestRequest requestSelectCompany137 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users"));
            requestSelectCompany137.Method = "OPTIONS";
            requestSelectCompany137.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany137.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany137;
            requestSelectCompany137 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users");

            webTest.BeginTransaction(WebRequestPrefix + "api_officemanagement");
            WebTestRequest requestSelectCompany138 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/officemanagement"));
            requestSelectCompany138.Method = "OPTIONS";
            requestSelectCompany138.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany138.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));

            yield return requestSelectCompany138;
            requestSelectCompany138 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_officemanagement");



            webTest.BeginTransaction(WebRequestPrefix + "api_organisation");
            WebTestRequest requestSelectCompany139 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/organisation"));
            requestSelectCompany139.Method = "OPTIONS";
            requestSelectCompany139.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany139.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany139;
            requestSelectCompany139 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_organisation");


            webTest.BeginTransaction(WebRequestPrefix + "api_companies");
            WebTestRequest requestSelectCompany141 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/companies"));
            requestSelectCompany141.Method = "OPTIONS";
            requestSelectCompany141.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany141.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany141;
            requestSelectCompany141 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_companies");

            webTest.BeginTransaction(WebRequestPrefix + "en-GB__components_actions_actions.html");
            WebTestRequest requestSelectCompany142 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/actions/actions.html"));
            requestSelectCompany142.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany142.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany142;
            requestSelectCompany142 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB__components_actions_actions.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping");
            WebTestRequest requestSelectCompany143 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping"));
            requestSelectCompany143.Method = "OPTIONS";
            requestSelectCompany143.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany143.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany143;
            requestSelectCompany143 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping");

            webTest.BeginTransaction(WebRequestPrefix + "en-GB__components_top-bar_search_search.html");
            WebTestRequest requestSelectCompany145 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/top-bar/search/search.html"));
            requestSelectCompany145.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany145.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany145;
            requestSelectCompany145 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB__components_top-bar_search_search.html");

            webTest.BeginTransaction(WebRequestPrefix + "components_company-switch_company-switch.html");
            WebTestRequest requestSelectCompany146 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/company-switch/company-switch.html"));
            requestSelectCompany146.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany146.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany146;
            requestSelectCompany146 = null;
            webTest.EndTransaction(WebRequestPrefix + "components_company-switch_company-switch.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_tasks");
            WebTestRequest requestSelectCompany147 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/tasks"));
            requestSelectCompany147.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany147.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany147;
            requestSelectCompany147 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_tasks");

            webTest.BeginTransaction(WebRequestPrefix + "api_users_user");
            WebTestRequest requestSelectCompany148 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName));
            requestSelectCompany148.Method = "OPTIONS";
            requestSelectCompany148.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany148.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany148;
            requestSelectCompany148 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_users_user");

            webTest.BeginTransaction(WebRequestPrefix + "en-GB_home_home.html");
            WebTestRequest requestSelectCompany149 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/home/home.html"));
            requestSelectCompany149.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany149.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany149;
            requestSelectCompany149 = null;
            webTest.EndTransaction(WebRequestPrefix + "en-GB_home_home.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_organisation_logo");
            WebTestRequest requestSelectCompany150 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/organisation/logo"));
            requestSelectCompany150.Method = "OPTIONS";
            requestSelectCompany150.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany150.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany150;
            requestSelectCompany150 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_organisation_logo");

            webTest.BeginTransaction(WebRequestPrefix + "api_organisation_logo_1");
            WebTestRequest requestSelectCompany151 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/organisation/logo"));
            requestSelectCompany151.Method = "OPTIONS";
            requestSelectCompany151.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany151.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany151;
            requestSelectCompany151 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_organisation_logo_1");

            webTest.BeginTransaction(WebRequestPrefix + "api_officemanagement_offices_office_1");
            WebTestRequest requestSelectCompany152 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/officemanagement/offices/office/" + officeManagementID));
            requestSelectCompany152.Method = "OPTIONS";
            requestSelectCompany152.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany152.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany152;
            requestSelectCompany152 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_officemanagement_offices_office_1");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping_dimensiontypes");
            WebTestRequest requestSelectCompany153 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping/dimensiontypes"));
            requestSelectCompany153.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany153.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany153;
            requestSelectCompany153 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping_dimensiontypes");

            webTest.BeginTransaction(WebRequestPrefix + "framework_desktop_view_desktop-view.html");
            WebTestRequest requestSelectCompany154 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/desktop-view.html"));
            requestSelectCompany154.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany154.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany154;
            requestSelectCompany154 = null;
            webTest.EndTransaction(WebRequestPrefix + "framework_desktop_view_desktop-view.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_sales");
            WebTestRequest requestSelectCompany155 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sales"));
            requestSelectCompany155.Method = "OPTIONS";
            requestSelectCompany155.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany155.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany155;
            requestSelectCompany155 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sales");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-sitemap-favourite-section-view");
            WebTestRequest requestSelectCompany156 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-sitemap-favourite-section-view" +
                    ".html"));
            requestSelectCompany156.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany156.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany156;
            requestSelectCompany156 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-sitemap-favourite-section-view");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-dashboard-section-group-view.h");
            WebTestRequest requestSelectCompany157 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-dashboard-section-group-view.h" +
                    "tml"));
            requestSelectCompany157.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany157.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany157;
            requestSelectCompany157 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-dashboard-section-group-view.h");

            webTest.BeginTransaction(WebRequestPrefix + "framework_desktop_view_cr-tile-element.html");
            WebTestRequest requestSelectCompany158 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-tile-element.html"));
            requestSelectCompany158.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany158.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany158;
            requestSelectCompany158 = null;
            webTest.EndTransaction(WebRequestPrefix + "framework_desktop_view_cr-tile-element.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_sitemapfavourites");
            WebTestRequest requestSelectCompany159 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sitemapfavourites"));
            requestSelectCompany159.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany159.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany159;
            requestSelectCompany159 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sitemapfavourites");

            webTest.BeginTransaction(WebRequestPrefix + "api_home");
            WebTestRequest requestSelectCompany160 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/home"));
            requestSelectCompany160.Method = "OPTIONS";
            requestSelectCompany160.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany160.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany160;
            requestSelectCompany160 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_home");

            //WebTestRequest requestSelectCompany162 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/"+threadData.UserName+"/preferreddesktop"));
            //requestSelectCompany162.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //requestSelectCompany162.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //yield return requestSelectCompany162;
            //requestSelectCompany162 = null;

            //WebTestRequest requestSelectCompany163 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/displaysettings"));
            //requestSelectCompany163.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //requestSelectCompany163.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //requestSelectCompany163.QueryStringParameters.Add("key", "HomeInsightsExpanded", false, false);
            //yield return requestSelectCompany163;
            //requestSelectCompany163 = null;

            //WebTestRequest requestSelectCompany164 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/displaysettings"));
            //requestSelectCompany164.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //requestSelectCompany164.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //requestSelectCompany164.QueryStringParameters.Add("key", "HomeHelpfulExpanded", false, false);
            //yield return requestSelectCompany164;
            //requestSelectCompany164 = null;

            //WebTestRequest requestSelectCompany165 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/displaysettings"));
            //requestSelectCompany165.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //requestSelectCompany165.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //requestSelectCompany165.QueryStringParameters.Add("key", "HomeFavouritesExpanded", false, false);
            //yield return requestSelectCompany165;
            //requestSelectCompany165 = null;

            //WebTestRequest requestSelectCompany166 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/viewedtours"));
            //requestSelectCompany166.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //requestSelectCompany166.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //yield return requestSelectCompany166;
            //requestSelectCompany166 = null;

            //WebTestRequest requestSelectCompany167 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/previouslogon"));
            //requestSelectCompany167.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //requestSelectCompany167.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //yield return requestSelectCompany167;
            //requestSelectCompany167 = null;

            //WebTestRequest requestSelectCompany168 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/users/user/" + threadData.UserName + "/previouslogon"));
            //requestSelectCompany168.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            //requestSelectCompany168.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            //yield return requestSelectCompany168;
            //requestSelectCompany168 = null;

            webTest.BeginTransaction(WebRequestPrefix + "framework_desktop_view_cr-date-graph-tile.html");
            WebTestRequest requestSelectCompany169 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-date-graph-tile.html"));
            requestSelectCompany169.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany169.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany169;
            requestSelectCompany169 = null;
            webTest.EndTransaction(WebRequestPrefix + "framework_desktop_view_cr-date-graph-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "framework_desktop_view_cr-bar-charts-tile.html");
            WebTestRequest requestSelectCompany170 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-bar-charts-tile.html"));
            requestSelectCompany170.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany170.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany170;
            requestSelectCompany170 = null;
            webTest.EndTransaction(WebRequestPrefix + "framework_desktop_view_cr-bar-charts-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "desktop_view_cr-customers-list-tile.html");
            WebTestRequest requestSelectCompany171 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-customers-list-tile.html"));
            requestSelectCompany171.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany171.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany171;
            requestSelectCompany171 = null;
            webTest.EndTransaction(WebRequestPrefix + "desktop_view_cr-customers-list-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "framework_desktop_view_cr-raw-html-tile.html");
            WebTestRequest requestSelectCompany172 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-raw-html-tile.html"));
            requestSelectCompany172.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany172.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany172;
            requestSelectCompany172 = null;
            webTest.EndTransaction(WebRequestPrefix + "framework_desktop_view_cr-raw-html-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "framework_desktop_view_cr-basic-tile.html");
            WebTestRequest requestSelectCompany173 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-basic-tile.html"));
            requestSelectCompany173.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany173.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany173;
            requestSelectCompany173 = null;
            webTest.EndTransaction(WebRequestPrefix + "framework_desktop_view_cr-basic-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "framework_desktop_view_cr-switch-tile.html");
            WebTestRequest requestSelectCompany174 = new WebTestRequest((webTest.Context["TwfcndURL"].ToString() + "/" + threadData.BuildVersion + "/en-GB/_components/framework/desktop/view/cr-switch-tile.html"));
            requestSelectCompany174.Headers.Add(new WebTestRequestHeader("Accept", "application/json, text/plain, */*;version=latest"));
            requestSelectCompany174.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany174;
            requestSelectCompany174 = null;
            webTest.EndTransaction(WebRequestPrefix + "framework_desktop_view_cr-switch-tile.html");

            webTest.BeginTransaction(WebRequestPrefix + "api_home_functions");
            WebTestRequest requestSelectCompany175 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/home/functions"));
            requestSelectCompany175.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany175.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany175;
            requestSelectCompany175 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_home_functions");

            webTest.BeginTransaction(WebRequestPrefix + "api_home_reports");
            WebTestRequest requestSelectCompany176 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/home/reports"));
            requestSelectCompany176.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany176.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany176;
            requestSelectCompany176 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_home_reports");

            webTest.BeginTransaction(WebRequestPrefix + "api_sales_invoices_kpis");
            WebTestRequest requestSelectCompany177 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sales/invoices/kpis"));
            requestSelectCompany177.Method = "OPTIONS";
            requestSelectCompany177.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany177.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany177;
            requestSelectCompany177 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sales_invoices_kpis");

            webTest.BeginTransaction(WebRequestPrefix + "api_sales_invoices_kpis_top10bestcustomers");
            WebTestRequest requestSelectCompany178 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/sales/invoices/kpis/top10bestcustomers"));
            requestSelectCompany178.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany178.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany178;
            requestSelectCompany178 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_sales_invoices_kpis_top10bestcustomers");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping_transactiontypes");
            WebTestRequest requestSelectCompany179 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping/transactiontypes"));
            requestSelectCompany179.Method = "OPTIONS";
            requestSelectCompany179.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany179.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany179;
            requestSelectCompany179 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping_transactiontypes");

            webTest.BeginTransaction(WebRequestPrefix + "api_bookkeeping_transactiontypes_1");
            WebTestRequest requestSelectCompany180 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/api/bookkeeping/transactiontypes"));
            requestSelectCompany180.ThinkTime = 8;
            requestSelectCompany180.Headers.Add(new WebTestRequestHeader("Accept", "application/vnd.twinfield+json;version=latest"));
            requestSelectCompany180.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return requestSelectCompany180;
            requestSelectCompany180 = null;
            webTest.EndTransaction(WebRequestPrefix + "api_bookkeeping_transactiontypes_1");

            webTest.EndTransaction(WebBTPrefix + "SelectCompany");
            #endregion

            Thread.Sleep(2000);
        }

        public static IEnumerable<WebTestRequest> Login_WebAPI(this WebTest webTest, SharedThreadData threadData, WebTestRequestPlugin ObjRequest)
        {
            string WebBTPrefix = threadData.ScenarioPrefix;
            string WebRequestPrefix = string.Empty;

            #region RT_Login
            webTest.BeginTransaction(WebBTPrefix + "RT_Login");
            WebRequestPrefix = "LI_";

            webTest.BeginTransaction(WebRequestPrefix + "webservices_session");
            WebTestRequest request1 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/webservices/session.asmx"));
            request1.Timeout = 120;
            request1.Method = "POST";
            request1.Headers.Add(new WebTestRequestHeader("Content-Type", "text/xml; charset=utf-8"));
            request1.Headers.Add(new WebTestRequestHeader("SOAPAction", "\"http://www.twinfield.com/Logon\""));
            StringHttpBody request1Body = new StringHttpBody();
            request1Body.ContentType = "text/xml; charset=utf-8";
            request1Body.InsertByteOrderMark = false;
            request1Body.BodyString = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Body><Logon xmlns=""http://www.twinfield.com/""><user>" + threadData.UserName + "</user><password>" + threadData.Password + "</password><organisation>" + threadData.Tenant + "</organisation></Logon></soap:Body></soap:Envelope>";
            request1.Body = request1Body;
            ExtractText extraction2 = new ExtractText();
            extraction2.StartsWith = "<SessionID>";
            extraction2.EndsWith = "</SessionID>";
            extraction2.IgnoreCase = false;
            extraction2.UseRegularExpression = false;
            extraction2.Required = true;
            extraction2.ExtractRandomMatch = false;
            extraction2.Index = 0;
            extraction2.HtmlDecode = true;
            extraction2.SearchInHeaders = false;
            extraction2.ContextParameterName = "SessionID";
            request1.ExtractValues += new EventHandler<ExtractionEventArgs>(extraction2.Extract);
            yield return request1;
            request1 = null;
            webTest.EndTransaction(WebRequestPrefix + "webservices_session");

            webTest.EndTransaction(WebBTPrefix + "RT_Login");
            #endregion

        }
        public static IEnumerable<WebTestRequest> Logout_WebAPI(this WebTest webTest, SharedThreadData threadData, WebTestRequestPlugin ObjRequest)
        {
            string WebBTPrefix = threadData.ScenarioPrefix;
            string WebRequestPrefix = string.Empty;
            string SessionID = webTest.Context["SessionID"].ToString();

            #region RT_Logout
            webTest.BeginTransaction(WebBTPrefix + "RT_Logout");

            WebRequestPrefix = "Lo_";
            webTest.BeginTransaction(WebRequestPrefix + "webservices_session");
            WebTestRequest request4 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/webservices/session.asmx"));
            request4.Timeout = 60;
            request4.Method = "POST";
            request4.Headers.Add(new WebTestRequestHeader("Content-Type", "text/xml; charset=utf-8"));
            request4.Headers.Add(new WebTestRequestHeader("SOAPAction", "\"http://www.twinfield.com/Abandon\""));
            StringHttpBody request4Body = new StringHttpBody();
            request4Body.ContentType = "text/xml; charset=utf-8";
            request4Body.InsertByteOrderMark = false;
            request4Body.BodyString = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Header><Header xmlns=""http://www.twinfield.com/""><SessionID>" + SessionID + @"</SessionID><CompanyId xsi:nil=""true"" /></Header></soap:Header><soap:Body><Abandon xmlns=""http://www.twinfield.com/"" /></soap:Body></soap:Envelope>";
            request4.Body = request4Body;
            yield return request4;
            request4 = null;
            webTest.EndTransaction(WebRequestPrefix + "webservices_session");

            webTest.EndTransaction(WebBTPrefix + "RT_Logout");

            #endregion

        }

        //Logout Funcationality
        public static IEnumerable<WebTestRequest> Logout(this WebTest webTest, SharedThreadData threadData, WebTestRequestPlugin ObjRequest)
        {
            string WebBTPrefix = threadData.ScenarioPrefix;
            string WebRequestPrefix = string.Empty;
            #region Logout
            webTest.BeginTransaction(WebBTPrefix + "Logout");
            WebRequestPrefix = "Lo_";

            webTest.BeginTransaction(WebRequestPrefix + "logoff");
            WebTestRequest request236 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/logoff/"));
            // request236.ExpectedResponseUrl = "https://login.rc.dev.twinfield.com/auth/authentication/logout?id=c4fad85299acf4de" +"9d7a61c4e0f0b31d";
            request236.ExpectedResponseUrl = webTest.Context["LoginMain"].ToString() + "/auth/authentication/logout";
            request236.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["AccountingURL"].ToString() + "/UI/")));
            yield return request236;
            request236 = null;
            webTest.EndTransaction(WebRequestPrefix + "logoff");

            webTest.BeginTransaction(WebRequestPrefix + "Launch");
            WebTestRequest request237 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/"));
            request237.ExpectedResponseUrl = (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue);
            //request237.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + "/auth/authentication/logout?id=c4fad85299acf4de9d7a61c4e0f0b31d")));
            request237.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + "/auth/authentication/logout")));
            WebTestRequest request237Dependent1 = new WebTestRequest((webTest.Context["LoginMain"].ToString() + "/auth/content/font/fontawesome-webfont.eot"));
            request237Dependent1.Headers.Add(new WebTestRequestHeader("Referer", (webTest.Context["LoginMain"].ToString() + "/auth/authentication/login?signin=" + ObjRequest.SigninValue)));
            request237Dependent1.QueryStringParameters.Add("", "", false, false);
            request237.DependentRequests.Add(request237Dependent1);
            yield return request237;
            request237 = null;
            webTest.EndTransaction(WebRequestPrefix + "Launch");

            webTest.EndTransaction(WebBTPrefix + "Logout");
            #endregion
        }

        public static SharedThreadData getThreadDataByScenarioName(int WebTestUserId,string scenarioName)
        {
            SharedThreadData threadData = null;
            switch ((SpecificScenarioName)Enum.Parse(typeof(SpecificScenarioName), scenarioName))
            {
                case SpecificScenarioName.CreateInvoice_A:
                    threadData = CustomDS.Instance.Get_CreateInvoice_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.CreateInvoice_B:
                    threadData = CustomDS.Instance.Get_CreateInvoice_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.CreateInvoice_C:
                    threadData = CustomDS.Instance.Get_CreateInvoice_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.CreateInvoice_D:
                    threadData = CustomDS.Instance.Get_CreateInvoice_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.CreateInvoice_E:
                    threadData = CustomDS.Instance.Get_CreateInvoice_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.EditInvoice:
                    threadData = CustomDS.Instance.GetEditInvoiceUserData_Web(WebTestUserId);
                    break;
                case SpecificScenarioName.CompanySettings_A:
                    threadData = CustomDS.Instance.Get_CompanySettings_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.CompanySettings_B:
                    threadData = CustomDS.Instance.Get_CompanySettings_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.CompanySettings_C:
                    threadData = CustomDS.Instance.Get_CompanySettings_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.CompanySettings_D:
                    threadData = CustomDS.Instance.Get_CompanySettings_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.CompanySettings_E:
                    threadData = CustomDS.Instance.Get_CompanySettings_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.ExtendedTBReport_A:
                    threadData = CustomDS.Instance.Get_ExtendedTBReport_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.ExtendedTBReport_B:
                    threadData = CustomDS.Instance.Get_ExtendedTBReport_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.ExtendedTBReport_C:
                    threadData = CustomDS.Instance.Get_ExtendedTBReport_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.ExtendedTBReport_D:
                    threadData = CustomDS.Instance.Get_ExtendedTBReport_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.ExtendedTBReport_E:
                    threadData = CustomDS.Instance.Get_ExtendedTBReport_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.NeoFixedAsset_A:
                    threadData = CustomDS.Instance.Get_NeoFixedAsset_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.NeoFixedAsset_B:
                    threadData = CustomDS.Instance.Get_NeoFixedAsset_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.NeoFixedAsset_C:
                    threadData = CustomDS.Instance.Get_NeoFixedAsset_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.NeoFixedAsset_D:
                    threadData = CustomDS.Instance.Get_NeoFixedAsset_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.NeoFixedAsset_E:
                    threadData = CustomDS.Instance.Get_NeoFixedAsset_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.NeoSalesInvoices_A:
                    threadData = CustomDS.Instance.Get_NeoSalesInvoices_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.NeoSalesInvoices_B:
                    threadData = CustomDS.Instance.Get_NeoSalesInvoices_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.NeoSalesInvoices_C:
                    threadData = CustomDS.Instance.Get_NeoSalesInvoices_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.NeoSalesInvoices_D:
                    threadData = CustomDS.Instance.Get_NeoSalesInvoices_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.NeoSalesInvoices_E:
                    threadData = CustomDS.Instance.Get_NeoSalesInvoices_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.ClassicSalesInvoices_A:
                    threadData = CustomDS.Instance.Get_ClassicSalesInvoices_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.ClassicSalesInvoices_B:
                    threadData = CustomDS.Instance.Get_ClassicSalesInvoices_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.ClassicSalesInvoices_C:
                    threadData = CustomDS.Instance.Get_ClassicSalesInvoices_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.ClassicSalesInvoices_D:
                    threadData = CustomDS.Instance.Get_ClassicSalesInvoices_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.ClassicSalesInvoices_E:
                    threadData = CustomDS.Instance.Get_ClassicSalesInvoices_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.CreateTransaction_A:
                    threadData = CustomDS.Instance.Get_CreateTransaction_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.CreateTransaction_B:
                    threadData = CustomDS.Instance.Get_CreateTransaction_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.CreateTransaction_C:
                    threadData = CustomDS.Instance.Get_CreateTransaction_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.CreateTransaction_D:
                    threadData = CustomDS.Instance.Get_CreateTransaction_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.CreateTransaction_E:
                    threadData = CustomDS.Instance.Get_CreateTransaction_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.ReadTransaction_A:
                    threadData = CustomDS.Instance.Get_ReadTransaction_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.ReadTransaction_B:
                    threadData = CustomDS.Instance.Get_ReadTransaction_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.ReadTransaction_C:
                    threadData = CustomDS.Instance.Get_ReadTransaction_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.ReadTransaction_D:
                    threadData = CustomDS.Instance.Get_ReadTransaction_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.ReadTransaction_E:
                    threadData = CustomDS.Instance.Get_ReadTransaction_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.UserAccessSettings_A:
                    threadData = CustomDS.Instance.Get_UserAccessSettings_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.UserAccessSettings_B:
                    threadData = CustomDS.Instance.Get_UserAccessSettings_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.UserAccessSettings_C:
                    threadData = CustomDS.Instance.Get_UserAccessSettings_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.UserAccessSettings_D:
                    threadData = CustomDS.Instance.Get_UserAccessSettings_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.UserAccessSettings_E:
                    threadData = CustomDS.Instance.Get_UserAccessSettings_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.DocumentManagement_A:
                    threadData = CustomDS.Instance.Get_DocumentManagement_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.DocumentManagement_B:
                    threadData = CustomDS.Instance.Get_DocumentManagement_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.DocumentManagement_C:
                    threadData = CustomDS.Instance.Get_DocumentManagement_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.DocumentManagement_D:
                    threadData = CustomDS.Instance.Get_DocumentManagement_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.DocumentManagement_E:
                    threadData = CustomDS.Instance.Get_DocumentManagement_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.PayAndCollectRun_A:
                    threadData = CustomDS.Instance.Get_PayAndCollectRun_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.PayAndCollectRun_B:
                    threadData = CustomDS.Instance.Get_PayAndCollectRun_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.PayAndCollectRun_C:
                    threadData = CustomDS.Instance.Get_PayAndCollectRun_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.PayAndCollectRun_D:
                    threadData = CustomDS.Instance.Get_PayAndCollectRun_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.PayAndCollectRun_E:
                    threadData = CustomDS.Instance.Get_PayAndCollectRun_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                case SpecificScenarioName.ExportCustomers_A:
                    threadData = CustomDS.Instance.Get_ExportCustomers_UserData(WebTestUserId, TwinfieldDBTenant.A);
                    break;
                case SpecificScenarioName.ExportCustomers_B:
                    threadData = CustomDS.Instance.Get_ExportCustomers_UserData(WebTestUserId, TwinfieldDBTenant.B);
                    break;
                case SpecificScenarioName.ExportCustomers_C:
                    threadData = CustomDS.Instance.Get_ExportCustomers_UserData(WebTestUserId, TwinfieldDBTenant.C);
                    break;
                case SpecificScenarioName.ExportCustomers_D:
                    threadData = CustomDS.Instance.Get_ExportCustomers_UserData(WebTestUserId, TwinfieldDBTenant.D);
                    break;
                case SpecificScenarioName.ExportCustomers_E:
                    threadData = CustomDS.Instance.Get_ExportCustomers_UserData(WebTestUserId, TwinfieldDBTenant.E);
                    break;
                default: break;
            }
            return threadData;
        }
    }
}