using Microsoft.VisualStudio.TestTools.LoadTesting;
using Microsoft.VisualStudio.TestTools.WebTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;

using System.Threading;
using System.Net;

namespace Twinfield_NewFramework
{
    public class UserBasedLoadTestPlugin : ILoadTestPlugin
    {
        private ScenarioDataList objScenarioDataList = null;
        private Dictionary<string, GenericScenario> GenericScenarioNameList = new Dictionary<string, GenericScenario>();
        public GenericScenario objGenericScenario = null;
        public static Thread _autoSavedataTimer = new Thread(new ThreadStart(DequeuRecords));
        public UserBasedLoadTestPlugin()
        {
        }

        private void M_loadTest_LoadTestFinished(object sender, EventArgs e)
        {
            //while (vsoTransactionContext._testTransactiondetails.IsEmpty)
            //{
            //    continue;
            //}


        }

        public void Initialize(Microsoft.VisualStudio.TestTools.LoadTesting.LoadTest loadTest)
        {
            try
            {
                m_loadTest = loadTest;
                m_loadTest.TestStarting += new EventHandler<TestStartingEventArgs>(TestStarting);
                m_loadTest.TestSelected += new EventHandler<TestSelectedEventArgs>(TestSelected);
                m_loadTest.TestFinished += new EventHandler<TestFinishedEventArgs>(TestFinished);
                m_loadTest.LoadTestFinished += M_loadTest_LoadTestFinished;


                //Deserialize the Scenario Data List Xml into the C# class
                objScenarioDataList = CommonFunctions.GetScenarioDataListFromXML();

                if (vsoTransactionContext.IsSaveTransactionInAzure)
                {
                    //Fill the dictionary for maintaining the logins
                    foreach (ScenarioDataListScenario scenario in objScenarioDataList.Scenarios)
                    {
                        objGenericScenario = new GenericScenario();
                        objGenericScenario.scenarioName = scenario.Name;
                        objGenericScenario.scenarioUserMaxIterationCount = Convert.ToInt32(scenario.IterationCountPerLogin);
                        objGenericScenario.throughputValue = Convert.ToDecimal(scenario.Throughput);

                        GenericScenarioNameList.Add(scenario.Name, objGenericScenario);
                    }
                    vsoTransactionContext._agentID = m_loadTest.Context.AgentId;
                    if (vsoTransactionContext._isInserted)
                    {
                        DBConnector dbconnect = new DBConnector();
                        LoadTestRun loadTestRun = new LoadTestRun();
                        loadTestRun.ControllerName = m_loadTest.Context.ControllerName;
                        loadTestRun.CooldownTime = m_loadTest.RunSettings.WarmupTime;
                        loadTestRun.Description = m_loadTest.RunSettings.Description;
                        loadTestRun.IsLocalRun = m_loadTest.Context.AgentCount <= 0 ? true : false;
                        loadTestRun.loadtestrunID = 0;
                        loadTestRun.RunDuration = m_loadTest.RunSettings.RunDuration;
                        loadTestRun.RunSettingUsed = "";
                        loadTestRun.StartTime = DateTime.Now;
                        loadTestRun.WarmupTime = m_loadTest.RunSettings.WarmupTime;
                        loadTestRun.LoadTestName = m_loadTest.RunSettings.Name;

                        DBConnector dbconnctor = new DBConnector();
                        //vsoTransactionContext._isLoadtestTestFinished = false;

                        TimeSpan TS = new TimeSpan(0, 1, 0);
                        string query = "select top 1 LoadTestRunID from Twinfield.tbl_WKLoadTestRun with(tablockx) WHERE Description='" + m_loadTest.RunSettings.Description + "' AND StartTime BETWEEN  '" + DateTime.Now.Subtract(new TimeSpan(0, 5, 0)) + "' AND '" + DateTime.Now.Add(new TimeSpan(0, 5, 0)) + "' ";

                        for (int i = 0; i < m_loadTest.Scenarios.Count; i++)
                        {
                            LoadtestTestCase testcase = new LoadtestTestCase();
                            testcase.LoadTestRunId = vsoTransactionContext._loadtestRunID;
                            testcase.TestCaseId = i;
                            testcase.ScenarioId = i;
                            testcase.TestType = "Loadtest";
                            testcase.TestCaseName = m_loadTest.Scenarios[i].TestNames.First().ToString();
                            vsoTransactionContext._loadtestTestCase.Add(m_loadTest.Scenarios[i].TestNames.First().ToString(), testcase);
                        }
                        var testcaselist = (from l in vsoTransactionContext._loadtestTestCase
                                            select l.Value).ToList();

                        dbconnctor.InsertLoadtestrunNtestCse(loadTestRun, testcaselist, query);

                        vsoTransactionContext._isInserted = false;
                        _autoSavedataTimer.Start();
                        //_autoSavedataTimer.Join();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error In UserBasedPlugin in Initialize() during reading ScenarioDataList.xml, Exception : " + ex.Message);
            }
        }

        private void TestFinished(object sender, TestFinishedEventArgs e)
        {

            //if first login fails re-try login
            if (!e.IsInitializeTest && !e.Result.Passed)
            {
                if (GenericScenarioNameList[e.TestName].scenarioUserCount.ContainsKey(e.UserContext.UserId) && GenericScenarioNameList[e.TestName].scenarioUserCount[e.UserContext.UserId] == 1)
                    GenericScenarioNameList[e.TestName].scenarioUserCount[e.UserContext.UserId] = 0;

            }
            if (vsoTransactionContext.IsSaveTransactionInAzure)
            {
                //VSOnline Logging
                if (!e.IsInitializeTest && !e.Result.Passed)
                {
                    //if (GenericScenarioNameList[e.TestName].scenarioUserCount.ContainsKey(e.UserContext.UserId) && GenericScenarioNameList[e.TestName].scenarioUserCount[e.UserContext.UserId] == 1)
                    //	GenericScenarioNameList[e.TestName].scenarioUserCount[e.UserContext.UserId] = 0;
                    //vsoTransactionContext._loadtestTestCase[e.TestName].TestCaseId + "_" + e.UserContext.UserId.ToString()
                    //vsoTransactionContext._testTransactiondetails.Enqueue((DataTable)e.UserContext[e.UserContext.UserId.ToString()]);
                    //vsoTransactionContext._testTransactiondetails.Enqueue((DataTable)e.UserContext[vsoTransactionContext._loadtestTestCase[e.TestName].TestCaseId + "_" + e.UserContext.UserId.ToString()]);
                }
                if (vsoTransactionContext.IsSaveTransactionInAzure)
                {
                    try
                    {
                        vsoTransactionContext._testTransactiondetails.Enqueue((DataTable)e.UserContext[vsoTransactionContext._loadtestTestCase[e.TestName].TestCaseId + "_" + e.UserContext.UserId.ToString()]);
                        vsoTransactionContext._testdetails.Enqueue(e.Result.StartTime.ToString() + "|" + e.Result.EndTime.ToString() + "|" + e.Result.Duration.ToString() + "|" + e.Result.Passed.ToString() + "|" + e.UserContext[vsoTransactionContext._loadtestTestCase[e.TestName].TestCaseId + "_T_" + e.UserContext.UserId.ToString()].ToString());

                    }
                    catch (Exception x)
                    {

                        // throw x;
                    }
                }
            }
        }

        private void TestStarting(object source, TestStartingEventArgs testStartingEventArgs)
        {
            if (vsoTransactionContext.IsSaveTransactionInAzure)
            {
                lock (this)
                {
                    Boolean flag = false;
                    testStartingEventArgs.TestContextProperties.Add("ScenarioName", testStartingEventArgs.TestName);

                    //A generic iterative process where it will manage maxiterationcount and data by dynamically taking the scenario/test method (for New LT framework)
                    for (int i = 0; i < GenericScenarioNameList.Count; i++)
                    {
                        if (GenericScenarioNameList.ContainsKey(testStartingEventArgs.TestName))
                        {
                            if (GenericScenarioNameList[testStartingEventArgs.TestName].scenarioUserCount.ContainsKey(testStartingEventArgs.UserContext.UserId) &&
                                GenericScenarioNameList[testStartingEventArgs.TestName].scenarioUserCount[testStartingEventArgs.UserContext.UserId] != 0)
                            {
                                flag = false;

                                if (GenericScenarioNameList[testStartingEventArgs.TestName].scenarioUserCount[testStartingEventArgs.UserContext.UserId] == GenericScenarioNameList[testStartingEventArgs.TestName].scenarioUserMaxIterationCount)
                                {
                                    flag = true;
                                    GenericScenarioNameList[testStartingEventArgs.TestName].scenarioUserCount[testStartingEventArgs.UserContext.UserId] = 0;
                                }

                                GenericScenarioNameList[testStartingEventArgs.TestName].scenarioUserCount[testStartingEventArgs.UserContext.UserId] += 1;
                            }
                            else
                            {
                                GenericScenarioNameList[testStartingEventArgs.TestName].scenarioUserCount.Remove(testStartingEventArgs.UserContext.UserId);
                                GenericScenarioNameList[testStartingEventArgs.TestName].scenarioUserCount.Add(testStartingEventArgs.UserContext.UserId, 1);
                                flag = true;
                            }

                            testStartingEventArgs.TestContextProperties.Add("iterationNo", GenericScenarioNameList[testStartingEventArgs.TestName].scenarioUserCount[testStartingEventArgs.UserContext.UserId]);
                            testStartingEventArgs.TestContextProperties.Add("maxiterationNo", GenericScenarioNameList[testStartingEventArgs.TestName].scenarioUserMaxIterationCount);
                            //testStartingEventArgs.TestContextProperties.Add("flagCount", GenericScenarioNameList[testStartingEventArgs.TestName].flagCount);
                            break;
                        }
                    }

                    foreach (KeyValuePair<string, object> keyValuePair in m_loadTest.Context)
                    {
                        testStartingEventArgs.TestContextProperties.Add(keyValuePair.Key, keyValuePair.Value);
                    }

                    testStartingEventArgs.TestContextProperties.Add("doLogin", flag);
                    testStartingEventArgs.TestContextProperties.Add("UserId", testStartingEventArgs.UserContext.UserId);
                    int testdetailID = vsoTransactionContext.CntLoadTestDetail;
                    testStartingEventArgs.TestContextProperties.Add("TestDetailID", testdetailID);
                    testStartingEventArgs.TestContextProperties.Add("LoadTestrunID", vsoTransactionContext._loadtestRunID);
                    testStartingEventArgs.TestContextProperties.Add("AgentID", vsoTransactionContext._agentID);
                    vsoTransactionContext.CntLoadTestDetail = testdetailID + 1;
                    testStartingEventArgs.TestContextProperties.Add("TestCaseId", vsoTransactionContext._loadtestTestCase[testStartingEventArgs.TestName].TestCaseId);

                }
            }
        }

        private void TestSelected(object source, TestSelectedEventArgs testSelectedEventArgs)
        {
        }

        private Microsoft.VisualStudio.TestTools.LoadTesting.LoadTest m_loadTest;
        static void DequeuRecords()
        {
            try
            {
                DBConnector dbConnector = new DBConnector();
                dbConnector.openConnection();
                DataTable DT;
                string outData;
                while (!vsoTransactionContext._isLoadtestTestFinished)
                {
                    Thread.Sleep(6000);
                    while (vsoTransactionContext._testTransactiondetails.Count > 0)
                    {
                        if (vsoTransactionContext._testTransactiondetails.TryDequeue(out DT))
                        {
                            dbConnector.BulkInsertdata(DT);
                        }
                        DT = null;
                    }
                    while (vsoTransactionContext._testdetails.Count > 0)
                    {
                        if (vsoTransactionContext._testdetails.TryDequeue(out outData))
                        {
                            dbConnector.insertData(outData, vsoTransactionContext._loadtestRunID);
                        }
                    }

                }
                dbConnector.closeConnection();
            }
            catch (Exception ex)
            {
                // RTMonitor.Write("DequeuRecords " + ex.Message);
                throw;
            }
        }
        //static void DequeuRecords()
        //{
        //	try
        //	{
        //		DBConnector dbConnector = new DBConnector();
        //		dbConnector.openConnection();
        //		DataTable DT;
        //		string outData;
        //              //!vsoTransactionContext.isLoadtestTestFinished && vsoTransactionContext.testTransactiondetails.Count!= 0 && vsoTransactionContext._testdetails.Count !=0
        //              while (!vsoTransactionContext._isLoadtestTestFinished && vsoTransactionContext._testTransactiondetails.Count!=0 & vsoTransactionContext._testdetails.Count !=0)
        //		{
        //			Thread.Sleep(60000);
        //			while (vsoTransactionContext._testTransactiondetails.Count > 0)
        //			{
        //				if (vsoTransactionContext._testTransactiondetails.TryDequeue(out DT))
        //				{
        //					dbConnector.BulkInsertdata(DT);
        //				}
        //				DT = null;
        //			}
        //			while (vsoTransactionContext._testdetails.Count > 0)
        //			{
        //				if (vsoTransactionContext._testdetails.TryDequeue(out outData))
        //				{
        //					dbConnector.insertData(outData, vsoTransactionContext._loadtestRunID);
        //				}
        //			}

        //		}
        //		dbConnector.closeConnection();
        //	}
        //	catch (Exception ex)
        //	{
        //		// RTMonitor.Write("DequeuRecords " + ex.Message);
        //		throw;
        //	}
        //}

    }

    //VSOnline Logging
    public static class vsoTransactionContext
    {
        public static bool IsSaveTransactionInAzure = true;
        public static bool _istestCaseSaved;
        public static bool _isthreadrunnin;
        public static bool _isLoadtestTestFinished;
        public static int _loadtestRunID;
        public static string _webTransactionTbleName;
        public static string _testDetailTblName;
        public static string _transactionTbleName;
        public static bool _isInserted = true;//{ get; set; } 
        public static DataTable DT = new DataTable();
        public static int CntLoadTestTransactionDetail = 0;
        public static int CntLoadTestDetail = 0;
        public static int _agentID = 0;
        public static Dictionary<string, LoadtestTestCase> _loadtestTestCase = new Dictionary<string, LoadtestTestCase>();
        public static ConcurrentQueue<DataTable> _testTransactiondetails = new ConcurrentQueue<DataTable>();
        public static ConcurrentQueue<string> _testdetails = new ConcurrentQueue<string>();

    }
    public class GenericScenario
    {
        public string scenarioName = string.Empty;
        public string testcasewithprefix = string.Empty;
        public int scenarioUserMaxIterationCount = 0;
        public decimal throughputValue = 0;
        public int flagCount = 0;
        public Dictionary<int, int> scenarioUserCount = new Dictionary<int, int>();
    }

    public class TwinfieldWebLoadTestPlugin
    {
        public static Object assemblyinitializeLock = new Object();
        public static void PostTransactionEvent(object sender, PostTransactionEventArgs e, DataTable _DTTransactionDetails)
        {
            if (vsoTransactionContext.IsSaveTransactionInAzure)
            {
                try
                {
                    if (_DTTransactionDetails.Columns.Count <= 0)
                    {
                        _DTTransactionDetails.Columns.Add("LoadTestRunId", typeof(int));
                        _DTTransactionDetails.Columns.Add("TestcaseID", typeof(int));
                        _DTTransactionDetails.Columns.Add("AgentID", typeof(int));
                        _DTTransactionDetails.Columns.Add("VuserID", typeof(int));
                        _DTTransactionDetails.Columns.Add("TestdetailId", typeof(int));
                        _DTTransactionDetails.Columns.Add("TimeStamp", typeof(DateTime));
                        _DTTransactionDetails.Columns.Add("ElapsedTime", typeof(double));
                        _DTTransactionDetails.Columns.Add("Endtime", typeof(DateTime));
                        _DTTransactionDetails.Columns.Add("TransactionName", typeof(string));
                        _DTTransactionDetails.AcceptChanges();
                    }

                    DataRow _NewRow = _DTTransactionDetails.NewRow();
                    _NewRow["testcaseID"] = (int)e.WebTest.Context["TestCaseId"];
                    _NewRow["LoadTestRunId"] = (int)e.WebTest.Context["LoadTestrunID"];
                    _NewRow["AgentID"] = (int)e.WebTest.Context["AgentID"];
                    _NewRow["testdetailId"] = (int)e.WebTest.Context["TestDetailID"]; ;
                    _NewRow["transactionName"] = e.TransactionName;
                    _NewRow["vuserID"] = (int)e.WebTest.Context.WebTestUserId;
                    _NewRow["TimeStamp"] = DateTime.Now - e.Duration;
                    _NewRow["ElapsedTime"] = e.Duration.TotalSeconds;
                    _NewRow["endtime"] = DateTime.Now;
                    _DTTransactionDetails.Rows.Add(_NewRow);
                    _DTTransactionDetails.AcceptChanges();

                }
                catch (Exception)
                {

                    //throw;
                }
            }
        }

        public static void PostWebTestEvent(object sender, PostWebTestEventArgs e, DataTable _DTTransactionDetails)
        {
            if (vsoTransactionContext.IsSaveTransactionInAzure)
            {
                string testdetailID = e.WebTest.Context["TestDetailID"].ToString();
                string testcaseID = e.WebTest.Context["TestCaseId"].ToString();
                string agentId = e.WebTest.Context["AgentID"].ToString();
                string loadtestrunID = e.WebTest.Context["LoadTestrunID"].ToString();
                Microsoft.VisualStudio.TestTools.LoadTesting.LoadTestUserContext loadtestusercontext = (Microsoft.VisualStudio.TestTools.LoadTesting.LoadTestUserContext)e.WebTest.Context["$LoadTestUserContext"];
                if (!loadtestusercontext.ContainsKey(testcaseID.ToString() + "_" + e.WebTest.Context.WebTestUserId.ToString()))
                {
                    loadtestusercontext.Add(testcaseID.ToString() + "_" + e.WebTest.Context.WebTestUserId.ToString(), _DTTransactionDetails.Copy());
                }
                else { loadtestusercontext[testcaseID.ToString() + "_" + e.WebTest.Context.WebTestUserId.ToString()] = _DTTransactionDetails.Copy(); }

                string basicTestData = testdetailID.ToString() + "|" + testcaseID.ToString() + "|" + e.WebTest.Context.WebTestUserId.ToString() + "|" + agentId.ToString() + "|" + loadtestrunID.ToString();
                if (!loadtestusercontext.ContainsKey(testcaseID.ToString() + "_T_" + e.WebTest.Context.WebTestUserId.ToString()))
                {
                    loadtestusercontext.Add(testcaseID.ToString() + "_T_" + e.WebTest.Context.WebTestUserId.ToString(), basicTestData);
                }
                else { loadtestusercontext[testcaseID.ToString() + "_T_" + e.WebTest.Context.WebTestUserId.ToString()] = basicTestData; }

            }

        }

        public static void PreWebTestEvent(object sender, PreWebTestEventArgs e)
        {
            if (!AssemblyLoad.isInitialized)
            {
                lock (assemblyinitializeLock)
                {
                    if (!AssemblyLoad.isInitialized)
                    {
                        //comment while debugging thats safe
                        AssemblyLoad.AssemblyInitWeb(e.WebTest.Context);
                        e.WebTest.Context.Add("LoginMain", AssemblyLoad.LoginMain);
                        e.WebTest.Context.Add("AccountingURL", AssemblyLoad.AccountingURL);
                        e.WebTest.Context.Add("TwfcndURL", AssemblyLoad.TwfcndURL);
                    }

                    AssemblyLoad.isInitialized = true;
                }
            }
        }
    }


    public class WebTestRequestPlugin
    {
        public string SigninValue = "";
        public string ScopeValue = "";
        public string StateValue = "";
        public string NonceValue = "";
        public string SessionIdValue = "";
        public string BNES_SID = "";
        public string OrgIdValue = "";

        #region PreRequest Event
        public void Test_PreRequest(object sender, PreRequestEventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            SharedThreadData threadData = CommonFunctions.getThreadDataByScenarioName(Convert.ToInt32(e.WebTest.Context.WebTestUserId), e.WebTest.Name);

            if (threadData.SessionId != null && threadData.OrganizationId != null)
            {
                if(threadData.BNES_SID != null)
                    e.Request.Headers.Add(new WebTestRequestHeader("Cookie", "SID=SessionId=" + threadData.SessionId + "&OrganisationId=" + threadData.OrganizationId + "&BNES_SID="+threadData.BNES_SID));
                else
                    e.Request.Headers.Add(new WebTestRequestHeader("Cookie", "SID=SessionId=" + threadData.SessionId + "&OrganisationId=" + threadData.OrganizationId));
            }
        }
        #endregion

        #region PostRequest Event
        public void Test_PostRequest(object sender, PostRequestEventArgs e)
        {
            //var sharedThreadData = CustomDS.Instance.GetCreateInvoiceUserData_Web(Convert.ToInt32(e.WebTest.Context.WebTestUserId));

            SharedThreadData threadData = CommonFunctions.getThreadDataByScenarioName(Convert.ToInt32(e.WebTest.Context.WebTestUserId), e.WebTest.Name);

            if (e.Request.Url == e.WebTest.Context["LoginMain"].ToString() + "/")
            {
                List<string> list = new List<string>(e.Response.Headers.AllKeys);
                if (ScopeValue == "" && StateValue == "" && NonceValue == "")
                {
                    string location = e.Response.Headers["Location"].ToString();
                    if (!location.Contains("msg=loggedoff"))
                    {


                        string[] words = location.Split('&');

                        //Extracting Scope Value
                        string Scopeword = words[4].ToString();
                        string[] ScopeVariable = Scopeword.Split('=');
                        string correctscope = ScopeVariable[1].ToString();

                        ScopeValue = correctscope.Replace('+', ' ');

                        //Extracting State Value
                        string Stateword = words[5].ToString();
                        string[] stateVariable = Stateword.Split('=');
                        StateValue = stateVariable[1].ToString();

                        //Extracting Nonce Value
                        string Nonceword = words[6].ToString();
                        string[] NonceVariable = Nonceword.Split('=');
                        NonceValue = NonceVariable[1].ToString();
                    }
                }
                if (list.Contains("Set-Cookie"))
                {
                    string setcookie = e.Response.Headers["Set-Cookie"].ToString();

                    if (setcookie.Contains("SessionId"))
                    {
                        string[] varcookie = setcookie.Split(';');
                        string cookiesplit = string.Empty;
                        if(AssemblyLoad.enviornmentName.Equals("Perf"))
                        {
                            if (threadData.hasFullAccess == "0")
                                cookiesplit = varcookie[5].ToString();//For Perf Env
                            else
                                cookiesplit = varcookie[8].ToString();
                        }
                        else if(AssemblyLoad.enviornmentName.Equals("RC_Azure"))
                        {
                            if (threadData.hasFullAccess == "0")
                                cookiesplit = varcookie[0].ToString();//string cookiesplit = varcookie[0].ToString(); //For RC-Azure Env
                            else
                                cookiesplit = varcookie[0].ToString();
                        }

                        string[] cookiesplit1 = cookiesplit.Split('&');

                        string SID = cookiesplit1[0].ToString();
                        string OrgID = cookiesplit1[1].ToString();

                        //Extracting Session ID value
                        string[] SIDSplit = SID.Split('=');
                        SessionIdValue = SIDSplit[2].ToString();

                        threadData.SessionId = SessionIdValue;

                        //Extracting Organisation ID value
                        string[] OrgIDSplit = OrgID.Split('=');
                        OrgIdValue = OrgIDSplit[1].ToString();

                        threadData.OrganizationId = OrgIdValue;

                        string BNES_SIDCookie = string.Empty;
                        if (AssemblyLoad.enviornmentName.Equals("Perf"))
                        {
                            if (threadData.hasFullAccess == "0")
                                BNES_SIDCookie = varcookie[14].ToString();//For Perf Env
                            else
                                BNES_SIDCookie = varcookie[20].ToString();

                            string[] BNES_SIDArray = BNES_SIDCookie.Split('=');
                            string BNES_SID = BNES_SIDArray[1].ToString() + "=";

                            threadData.BNES_SID = BNES_SID;
                        }
                        else if (AssemblyLoad.enviornmentName.Equals("RC_Azure"))
                        {
                            /*if (threadData.hasFullAccess == "0")
                                BNES_SIDCookie = varcookie[0].ToString();//For RC-Azure Env
                            else
                                BNES_SIDCookie = varcookie[3].ToString();

                            string[] BNES_SIDArray = BNES_SIDCookie.Split('=');
                            string BNES_SID = BNES_SIDArray[1].ToString()+"=";

                            threadData.BNES_SID = BNES_SID;*/
                        }
                        

                    }
                }
            }

            if (e.Request.Url == e.WebTest.Context["LoginMain"].ToString() + "/auth/authentication/connect/authorize")
            {
                List<string> list = new List<string>(e.Response.Headers.AllKeys);
                if (list.Contains("Location"))
                {
                    string signin = e.Response.Headers["Location"].ToString();
                    if (signin.Contains("signin"))
                    {
                        string[] words = signin.Split('?');
                        string wordsign = words[1].ToString();
                        string[] Variable = wordsign.Split('=');
                        SigninValue = Variable[1].ToString();
                    }
                }
            }
            if(e.Request.Url==e.WebTest.Context["AccountingURL"].ToString() + "/pay/default.aspx" && !e.Request.UrlWithQueryString.Contains("?"))
            {
                if(e.Response.BodyString.Contains("tempXml"))
                {
                    List<string> extractedValues = CommonFunctions.GetBetweenList(e.Response.BodyString, "tempXml\" ID=\"tempXml\" VALUE=\"", "\"><BR>");
                    if (e.WebTest.Context.Keys.Contains("tempXmlId"))
                        e.WebTest.Context["tempXmlId"] = extractedValues.Last();
                    else
                        e.WebTest.Context.Add("tempXmlId", extractedValues.Last());

                    if (e.WebTest.Context.Keys.Contains("hdnRowCount"))
                        e.WebTest.Context["hdnRowCount"] = CommonFunctions.GetSingleWordBetween(e.Response.BodyString, "ID=\"hdnRowCount\" VALUE=\"", "\">");
                    else
                        e.WebTest.Context.Add("hdnRowCount", CommonFunctions.GetSingleWordBetween(e.Response.BodyString, "ID=\"hdnRowCount\" VALUE=\"", "\">"));

                    if (e.WebTest.Context.Keys.Contains("hdnFromForm"))
                        e.WebTest.Context["hdnFromForm"] = CommonFunctions.GetSingleWordBetween(e.Response.BodyString, "ID=\"hdnFromForm\" VALUE=\"", "\">");
                    else
                        e.WebTest.Context.Add("hdnFromForm", CommonFunctions.GetSingleWordBetween(e.Response.BodyString, "ID=\"hdnFromForm\" VALUE=\"", "\">"));

                    if (e.WebTest.Context.Keys.Contains("txtButtonPushedId"))
                        e.WebTest.Context["txtButtonPushedId"] = CommonFunctions.GetSingleWordBetween(e.Response.BodyString, "ID=\"txtButtonPushedId\" VALUE=\"", "\">");
                    else
                        e.WebTest.Context.Add("txtButtonPushedId", CommonFunctions.GetSingleWordBetween(e.Response.BodyString, "ID=\"txtButtonPushedId\" VALUE=\"", "\">"));                    
                }

            }
        }
        #endregion PostRequest Event
    }

}