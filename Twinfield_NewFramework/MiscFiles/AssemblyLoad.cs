using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrosoftServicesTestLabs.Monitor.VSTSUnitTest;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Twinfield_NewFramework
{
    [TestClass()]
    public partial class AssemblyLoad
    {
        #region Private Members

        public static string OneClick = string.Empty;
        public static string OneClickServices = string.Empty;
        public static string ClientManagementLink = string.Empty;
        public static string AzureLink = string.Empty;
        public static string Password = string.Empty;
        public static bool rtMonitorLog = false;
        public static string enviornmentName = string.Empty;
        public static string loadBalancer = string.Empty;
        public static string m_ConnectionString = string.Empty;
        public static string controllerIPAddress = string.Empty;
        public static string iUComponentVersion = string.Empty;
        public static string controllerName = string.Empty;
        public static string controllerBelongstoEnv = string.Empty;
        public static int userLoad;
        public static int stepUser;
        public static int StepUserSec;

        public static string DataSetToUse_CreateInvoice = string.Empty;
        public static string DataSetToUse_EditInvoice = string.Empty;
        public static string DataSetToUse_CompanySettings = string.Empty;
        public static string InputSetOrder_CompanySettings = string.Empty;
        public static string InputSetOrder_CreateInvoice = string.Empty;
        public static string InputSetOrder_EditInvoice = string.Empty;
        public static string DataSetToUse_ExtendedTBReport= string.Empty;
        public static string InputSetOrder_ExtendedTBReport= string.Empty;
        public static string DataSetToUse_NeoFixedAsset= string.Empty;
        public static string InputSetOrder_NeoFixedAsset= string.Empty;
        public static string DataSetToUse_NeoSalesInvoices= string.Empty;
        public static string InputSetOrder_NeoSalesInvoices= string.Empty;
        public static string DataSetToUse_ClassicSalesInvoices= string.Empty;
        public static string InputSetOrder_ClassicSalesInvoices= string.Empty;
        public static string DataSetToUse_CreateTransaction= string.Empty;
        public static string InputSetOrder_CreateTransaction= string.Empty;
        public static string DataSetToUse_ReadTransaction= string.Empty;
        public static string InputSetOrder_ReadTransaction= string.Empty;
        public static string DataSetToUse_UserAccessSettings= string.Empty;
        public static string InputSetOrder_UserAccessSettings= string.Empty;
        public static string DataSetToUse_DocumentManagement= string.Empty;
        public static string InputSetOrder_DocumentManagement= string.Empty;
        public static string DataSetToUse_PayAndCollectRun= string.Empty;
        public static string InputSetOrder_PayAndCollectRun= string.Empty;
        public static string DataSetToUse_ExportCustomers= string.Empty;
        public static string InputSetOrder_ExportCustomers= string.Empty;
        
        public static TableType objTableType = new TableType();

        public static Dictionary<string, string> appSettings = new Dictionary<string, string>();
        public static Dictionary<string, string> connectionStrings = new Dictionary<string, string>();
        public static Dictionary<string, string> ControllerSettings = new Dictionary<string, string>();
        public static List<ControllerSettings> objListControllerSettings = new List<ControllerSettings>();

        private TestContext _testContext;

        //private CCH.LoadTest.CreateNewReturn.CreateNewReturnTests createNewReturnTests;
        private Guid[] guidList = new Guid[3];

        private Guid firmGuid = new Guid();
        private Guid staffGuid = new Guid();
        private Guid userGuid = new Guid();
        private Guid clientGuid = new Guid();
        private Guid officeGuid = new Guid();
        private Guid businessUnitGuid = new Guid();
        private Guid returnGroupGuid = new Guid();
        private string clientId = string.Empty;
        private string clientSubId = string.Empty;
        private static int counterIndex = 0;
        private Dictionary<string, string> localDictionary;
        public static string sAgentId = "";

        public static Cookie cookie;
        public static Boolean isInitialized = false;
        private static HttpWebRequest request1;

        public static bool TrustAllCertificatesCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private static bool ipFlag;
        public static Stream fileStream; // = new byte[1048567];
        public static int BytesRead;
        public static int agentCount = 5;
        public static SqlConnection con;
        public static int assmCnt = 0;
        public static string Token = string.Empty;
        public static string prodstack1;
        public static string ApplicationInUseURL;
        public static string Flag;
        public static bool UseInputDBTable;
        public static string TwfcndURL;
        public static string AccountingURL;
        public static string LoginMain;
        

        #endregion Private Members

        public TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }

        #region Assembly Initialize

        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            try
            {
				//first get controller ip address
				//controllerIPAddress = CommonFunctions.GetControllerHostIPAddress(); // this is invalid logic to run on agent
				Logger.WriteGeneralLog("INit Here ...................Checking");

				controllerIPAddress = context.Properties["ControllerName"] as string;
                controllerIPAddress = controllerIPAddress.Split(':')[0].ToString().ToLower(); //Using this we can get controller IP - Fix Applied
				if (controllerIPAddress.ToLower().Equals("localhost"))
					//controllerIPAddress = CommonFunctions.GetControllerHostIPAddress(); //Using this code to get Controller IP while debug.
					controllerIPAddress = "172.18.100.5";
				//Read Table Names from XML
				//objTableType = CommonFunctions.GetInputTableNamesFromXML();
                //read load test setting config
                //CommonFunctions.ReadLoadTestSettingsConfig();
                //Get environment Name
                //enviornmentName = appSettings["EnviornmentToPoint"];

                //Get Load balancer Name
                //switch (enviornmentName)
                //{
                //    case "prodstack1":
                //        ApplicationInUseURL = appSettings["prodstack1"];
                //        break;
                //    case "preprodstack1":
                //        ApplicationInUseURL = appSettings["preprodstack1"];
                //        break;
                //    case "preprodstack2":
                //        ApplicationInUseURL = appSettings["preprodstack2"];
                //        break;
                //}
                              
               
                //Password = appSettings["Password"];
                // Token = appSettings["Token"];

                

                //csv or input table ?? flag below
                //UseInputDBTable = Convert.ToBoolean(appSettings["UseInputDBTable"].ToString());

                ////RTmonitor log
                //rtMonitorLog = Convert.ToBoolean(appSettings["RTMonitorLog"].ToLower());

                ////read controller config section
                //CommonFunctions.ReadControllerSettingsConfig();
                ////inside this function controller ipaddress is matched with machine name and then the controllerName variable is set along with its location on environment
                //CommonFunctions.GetControllerDestinationAndNameOnEnviornmentUsingIP(controllerIPAddress);
                ////Generate Connection string from controller ip
                //m_ConnectionString = CommonFunctions.InputDBConnectionString(controllerBelongstoEnv);

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //RTMonitor.SetHost(controllerIPAddress, 11000, RTMonitor.ConnectionMethod.UDP);

                ////Logger.WriteGeneralLog("controllerIPAddress: " + controllerIPAddress);

                //System.Threading.Interlocked.Increment(ref assmCnt);
                //if (Convert.ToInt32(context.Properties["TotalAgents"]) > 0)
                //{
                //    agentCount = Convert.ToInt32(context.Properties["TotalAgents"]);
                //}

                //Logger.WriteGeneralLog("Initialize input table start");

                ////to be added in case of wcf scenarios so kept commented to avoid confusion
                ////CustomDS.Instance.InitializeTWO(m_ConnectionString, context, UseInputDBTable);

                //Logger.WriteGeneralLog("Initialize input table end");

                //Thread.AllocateNamedDataSlot("Params");

                //AppDomain.CurrentDomain.SetData("APPBASE", Environment.CurrentDirectory);
            }
            catch (Exception ex)
            {
                Logger.WriteGeneralLog("Exception in AssemblyInit!!!!!!!!!!!!!!!!!!!!!!!!!!!!! : " + System.Environment.MachineName);
                Logger.WriteGeneralLog(ex.ToString());
                //throw ex;
            }
        }

        public static void AssemblyInitWeb(Microsoft.VisualStudio.TestTools.WebTesting.WebTestContext context)
        {
			Logger.WriteGeneralLog("INit Here ...................AssemblyInitWeb");
			WrapperToAseemblyInit(context);
        }

        public static void WrapperToAseemblyInit(Microsoft.VisualStudio.TestTools.WebTesting.WebTestContext context)
        {
            try
            {
				Logger.WriteGeneralLog("INit Here ...................WrapperToAseemblyInit");
				//first get controller ip address
				//controllerIPAddress = CommonFunctions.GetControllerHostIPAddress();

				controllerIPAddress = context.ControllerName;
                controllerIPAddress = controllerIPAddress.Split(':')[0].ToString().ToLower(); //Using this we can get controller IP - Fix Applied

               // if (controllerIPAddress.ToLower().Equals("localhost"))
                  
                //controllerIPAddress = CommonFunctions.GetControllerHostIPAddress(); //Using this code to get Controller IP while debug.
                
                //Read Table Names from XML
                objTableType = CommonFunctions.GetInputTableNamesFromXML();
                //read load test setting config
                CommonFunctions.ReadLoadTestSettingsConfig();
                //new System.Collections.Generic.Mscorlib_DictionaryDebugView<string, string>(appSettings).Items[1].Key;
               // environment= new System.Collections.Generic.Mscorlib_DictionaryDebugView<string, string>(appSettings).Items[1].Value
                //Get environment Name
                enviornmentName = appSettings["EnvironmentToPoint"];
                TwfcndURL = appSettings["TwfcndURL"];
                //Get Load balancer Name
                switch (enviornmentName)
                {
                    case "Perf":
                        LoginMain = appSettings["Perf_LoginMain"];
                        AccountingURL = appSettings["Perf_AccountingURL"];
                        break;
                    case "RC_Azure":
                        LoginMain = appSettings["RC_Azure_LoginMain"];
                        AccountingURL = appSettings["RC_Azure_AccountingURL"];
                        break;
                }
                Password = appSettings["Password"];
               
                //RTmonitor log
                rtMonitorLog = Convert.ToBoolean(appSettings["RTMonitorLog"].ToLower());
                //InputData Fetching Setting
                DataSetToUse_CreateInvoice = appSettings["DataSetToUse_CreateInvoice"];
                InputSetOrder_CreateInvoice = appSettings["InputSetOrder_CreateInvoice"];
                DataSetToUse_EditInvoice = appSettings["DataSetToUse_EditInvoice"];                
                InputSetOrder_EditInvoice = appSettings["InputSetOrder_EditInvoice"];
                DataSetToUse_CompanySettings = appSettings["DataSetToUse_CompanySettings"];
                InputSetOrder_CompanySettings = appSettings["InputSetOrder_CompanySettings"];
                DataSetToUse_ExtendedTBReport = appSettings["DataSetToUse_ExtendedTBReport"];
                InputSetOrder_ExtendedTBReport = appSettings["InputSetOrder_ExtendedTBReport"];
                DataSetToUse_NeoFixedAsset = appSettings["DataSetToUse_NeoFixedAsset"];
                InputSetOrder_NeoFixedAsset = appSettings["InputSetOrder_NeoFixedAsset"];
                DataSetToUse_NeoSalesInvoices = appSettings["DataSetToUse_NeoSalesInvoices"];
                InputSetOrder_NeoSalesInvoices = appSettings["InputSetOrder_NeoSalesInvoices"];
                DataSetToUse_ClassicSalesInvoices = appSettings["DataSetToUse_ClassicSalesInvoices"];
                InputSetOrder_ClassicSalesInvoices = appSettings["InputSetOrder_ClassicSalesInvoices"];
                DataSetToUse_CreateTransaction = appSettings["DataSetToUse_CreateTransaction"];
                InputSetOrder_CreateTransaction = appSettings["InputSetOrder_CreateTransaction"];
                DataSetToUse_ReadTransaction = appSettings["DataSetToUse_ReadTransaction"];
                InputSetOrder_ReadTransaction = appSettings["InputSetOrder_ReadTransaction"];
                DataSetToUse_UserAccessSettings = appSettings["DataSetToUse_UserAccessSettings"];
                InputSetOrder_UserAccessSettings = appSettings["InputSetOrder_UserAccessSettings"];
                DataSetToUse_DocumentManagement = appSettings["DataSetToUse_DocumentManagement"];
                InputSetOrder_DocumentManagement = appSettings["InputSetOrder_DocumentManagement"];
                DataSetToUse_PayAndCollectRun = appSettings["DataSetToUse_PayAndCollectRun"];
                InputSetOrder_PayAndCollectRun = appSettings["InputSetOrder_PayAndCollectRun"];
                DataSetToUse_ExportCustomers = appSettings["DataSetToUse_ExportCustomers"];
                InputSetOrder_ExportCustomers = appSettings["InputSetOrder_ExportCustomers"];

                //read controller config section
                CommonFunctions.ReadControllerSettingsConfig();
                //inside this function controller ipaddress is matched with machine name and then the controllerName variable is set along with its location on environment
                CommonFunctions.GetControllerDestinationAndNameOnEnviornmentUsingIP(controllerIPAddress);
                //Generate Connection string from controller ip
                m_ConnectionString = CommonFunctions.InputDBConnectionString(controllerBelongstoEnv);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RTMonitor.SetHost(controllerIPAddress, 11000, RTMonitor.ConnectionMethod.UDP);

                Logger.WriteGeneralLog("controllerIPAddress: " + controllerIPAddress);

                System.Threading.Interlocked.Increment(ref assmCnt);
                if (Convert.ToInt32(context.AgentCount) > 0)
                    agentCount = context.AgentCount;
                Logger.WriteGeneralLog("agentCount " + agentCount.ToString());

                Logger.WriteGeneralLog("Initialize input table start - InitializeTwinfieldWeb");

                CustomDS.Instance.InitializeWeb(m_ConnectionString, context);

                Logger.WriteGeneralLog("Initialize input table end - InitializeTwinfieldWeb"+context["ScenarioName"].ToString());
                Thread.AllocateNamedDataSlot("Params" + context["ScenarioName"].ToString());

                AppDomain.CurrentDomain.SetData("APPBASE", Environment.CurrentDirectory);
            }
            catch (Exception ex)
            {
                Logger.WriteGeneralLog("Exception in AssemblyInit!!!!!!!!!!!!!!!!!!!!!!!!!!!!! : " + System.Environment.MachineName);
                Logger.WriteGeneralLog(ex.ToString());
                throw ex;
            }
        }

        #endregion Assembly Initialize

        #region Assembly CleanUp

        [AssemblyCleanup()]
        public static void AssemblyCleanUp()
        {
            //SPExecution.DMVUtility.GetInstance.Stop();
            //Logger.WriteGeneralLog(Color.Black, "DMV data capturing is stopped....");
            //Code added to capture long running sql data from DMV
            //if (SPExecution.DMVUtility.GetInstance.IsDMVEnable)
            //{
            //    SPExecution.DMVUtility.GetInstance.CaptureDMVData();
            //}
        }

        #endregion Assembly CleanUp
    }
}