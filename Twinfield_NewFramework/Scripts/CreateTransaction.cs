﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Twinfield_NewFramework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.WebTesting;
    using Microsoft.VisualStudio.TestTools.WebTesting.Rules;
    using Newtonsoft.Json;
    using Microsoft.VisualStudio.TestTools.LoadTesting;
    //using MicrosoftServicesTestLabs.Monitor.VSTSUnitTest;
    using System.Drawing;
    using System.Threading;
    using System.Data;
    using System.Reflection;
    using System.Web;
    using System.Linq;

    public class CreateTransaction
    {
        WebTest webTest;
        private Dictionary<string, string> localDictionary;
        WebTestRequestPlugin objPlugin = new WebTestRequestPlugin();    //object of webTesRequestPlugin class to access the variables uniquely in each thread.
        private InputTableRecord testRecord;
        public Object assemblyinitializeLock = new Object();
        private WebTestContext _textContext;
        private object virtualuserId;
        private object doLogin;
        string OfficeManagementID = string.Empty;
        private string companyTypeDescription;
        string TransactionStatus = string.Empty;
        string Code = string.Empty;

        public CreateTransaction(WebTest webTest)
        {
            this.webTest = webTest;
            
        }
        public WebTestContext TestContext
        {
            get { return _textContext; }

            set { _textContext = value; }
        }
        
        public IEnumerator<WebTestRequest> GetRequestEnumerator(TwinfieldDBTenant DBTenant,string userDictionaryKey, WebTestRequestPlugin objPlugin)
        {
            var WebBTPrefix = TwinfieldScenarioPrefix.CT_ + DBTenant.ToString() + "_";
            _textContext = webTest.Context;
            _textContext.TryGetValue("doLogin", out doLogin);
            _textContext.TryGetValue("UserId", out virtualuserId);

            SharedThreadData threadData;

            #region DoLogin
            if (Convert.ToBoolean(doLogin))
            {
                webTest.Context.CookieContainer = new System.Net.CookieContainer();

                try
                {
                    localDictionary = CustomDS.Instance.GetNextRowTwinfield(DBTenant, TwinfieldScenarioName.CreateTransaction);
                    
                    threadData = new SharedThreadData(Convert.ToInt32(virtualuserId),this.TestContext);
                    threadData.UserName = localDictionary["UserName"];
                    threadData.Tenant = localDictionary["DBTenant"];
                    threadData.CustomerName = localDictionary["CustomerName"];
                    threadData.hasFullAccess = localDictionary["hasFullAccess"];
                    threadData.Password = AssemblyLoad.Password;
                    threadData.ScenarioPrefix = WebBTPrefix;
                    CustomDS.Instance.Save_CreateTransaction_UserData(Convert.ToInt32(virtualuserId), threadData, DBTenant);
                    Logger.WriteGeneralLogUser(TwinfieldScenarioName.CreateTransaction + " : " + DBTenant + " : LoggedIn : " + virtualuserId); 
                }
                catch (Exception ex)
                {

                    Logger.WriteGeneralLog("Input Data not fetched for:" + TwinfieldScenarioName.CreateTransaction);
                    throw new Exception("Error in reading data for " + TwinfieldScenarioName.CreateTransaction + " : " + ex.Message);

                }
                if(threadData.hasFullAccess=="0")
                {
                    foreach (var request in webTest.Login_WebAPI(threadData, objPlugin)) yield return request;
                }
                else
                {
                    foreach (var request in webTest.Login_WebAPI(threadData, objPlugin)) yield return request;
                }
                //RTMonitor.Write(Color.Green, "Create Invoice Login User: " + threadData.UserName + "with iteration no :" + threadData.iterationCount + " LoginTime: " + DateTime.Now + "\r\n");
                CustomDS.Instance.Save_CreateTransaction_UserData(Convert.ToInt32(virtualuserId), threadData, DBTenant);
                //Logger.WriteGeneralLog("Create Invoice : Vuser - " + Convert.ToInt32(virtualuserId).ToString() + " Logged In");
            }
            else
            {
                try
                {
                    threadData = CustomDS.Instance.Get_CreateTransaction_UserData(Convert.ToInt32(virtualuserId), DBTenant);
                    Logger.WriteGeneralLogUser(TwinfieldScenarioName.CreateTransaction + " : " + DBTenant + " : NotLoggedIn : " + virtualuserId);

                }
                catch (Exception ex)
                {

                    Logger.WriteGeneralLog("Input Data not saved for :" + TwinfieldScenarioName.CreateTransaction);
                    throw new Exception("Error in getting data for " + TwinfieldScenarioName.CreateTransaction + " : " + ex.Message);

                }
            }
            #endregion DoLogin
            
            Thread.Sleep(2000);
            string SessionID = webTest.Context["SessionID"].ToString();
            WebBTPrefix = threadData.ScenarioPrefix;
            string WebRequestPrefix = "";

            string UserDetailsFromFile = System.IO.File.ReadAllText("CreateTransactionData.txt");
            string encUserDetailsFromFile = HttpUtility.HtmlEncode(UserDetailsFromFile);

            //Will be used in case need to create both types of transaction
            if (this.GetType().Name == "CreateTransactionXML")
            {
                TransactionStatus = "final";
                Code = "VRK";

            }
            else
            {
                //Selecting Random transaction type
                string[] TTypes = { "final", "temporary" };
                Random rtemp = new Random();
                int index = rtemp.Next(2);
                string TType = TTypes[index];
                TransactionStatus = TType;

                //Selecting Random Code
                string[] codes = { "INK", "VRK" };
                Random rInt = new Random();
                int r = rInt.Next(2);
                string CodeTemp = codes[r];
                Code = CodeTemp;

            }

            //Creating Random Invoice Number
            Random rand = new Random();
            string CharacterString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string appendName = new string(Enumerable.Repeat(CharacterString, 4)
                  .Select(s => s[rand.Next(s.Length)]).ToArray());
            string InvoiceNumber = "SALES-INV-" + appendName;

            //Office fetched from CSV 
            string Office = threadData.CompanyName;

            string TranDate = DateTime.Now.ToString("yyyy/MM/MM");
            TranDate = TranDate.Replace("/", "");

            //Replacing the XML file contents to have the dynamic data 
            encUserDetailsFromFile = encUserDetailsFromFile.Replace("{transactionStatus}", TransactionStatus);
            encUserDetailsFromFile = encUserDetailsFromFile.Replace("{Code}", Code);
            encUserDetailsFromFile = encUserDetailsFromFile.Replace("{InvoiceNumber}", InvoiceNumber);
            encUserDetailsFromFile = encUserDetailsFromFile.Replace("{Office}", Office);
            encUserDetailsFromFile = encUserDetailsFromFile.Replace("{TranDate}", TranDate);

            #region CT_ChangeOffice

            webTest.BeginTransaction(WebBTPrefix + "ChangeOffice");
            WebRequestPrefix = "CO_";

            webTest.BeginTransaction(WebRequestPrefix + "webservices_session");
            WebTestRequest request2 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/webservices/session.asmx"));
            request2.Timeout = 60;
            request2.Method = "POST";
            request2.Headers.Add(new WebTestRequestHeader("Content-Type", "text/xml; charset=utf-8"));
            request2.Headers.Add(new WebTestRequestHeader("SOAPAction", "\"http://www.twinfield.com/SelectCompany\""));
            StringHttpBody request2Body = new StringHttpBody();
            request2Body.ContentType = "text/xml; charset=utf-8";
            request2Body.InsertByteOrderMark = false;
            request2Body.BodyString = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Header><Header xmlns=""http://www.twinfield.com/""><SessionID>" + SessionID + @"</SessionID><CompanyId xsi:nil=""true"" /></Header></soap:Header><soap:Body><SelectCompany xmlns=""http://www.twinfield.com/""><company>" + threadData.CompanyName + "</company></SelectCompany></soap:Body></soap:Envelope>";
            request2.Body = request2Body;
            yield return request2;
            request2 = null;
            webTest.EndTransaction(WebRequestPrefix + "webservices_session");

            webTest.EndTransaction(WebBTPrefix + "ChangeOffice");
            #endregion

            Thread.Sleep(2000);

            #region CT_CreateTransactionXML
            webTest.BeginTransaction(WebBTPrefix + "CreateTransactionXML");
            WebRequestPrefix = "CTX_";

            webTest.BeginTransaction(WebRequestPrefix + "webservices_processxml");
            WebTestRequest request3 = new WebTestRequest((webTest.Context["AccountingURL"].ToString() + "/webservices/processxml.asmx"));
            request3.Timeout = 60;
            request3.Method = "POST";
            request3.Headers.Add(new WebTestRequestHeader("Content-Type", "text/xml; charset=utf-8"));
            request3.Headers.Add(new WebTestRequestHeader("SOAPAction", "\"http://www.twinfield.com/ProcessXmlString\""));
            StringHttpBody request3Body = new StringHttpBody();
            request3Body.ContentType = "text/xml; charset=utf-8";
            request3Body.InsertByteOrderMark = false;
            request3Body.BodyString = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Header><Header xmlns=""http://www.twinfield.com/""><SessionID>" + SessionID + @"</SessionID><CompanyId xsi:nil=""true""/></Header></soap:Header><soap:Body><ProcessXmlString xmlns=""http://www.twinfield.com/""><xmlRequest>" + encUserDetailsFromFile + "</xmlRequest></ProcessXmlString></soap:Body></soap:Envelope>";
            request3.Body = request3Body;
            yield return request3;
            request3 = null;
            webTest.EndTransaction(WebRequestPrefix + "webservices_processxml");

            webTest.EndTransaction(WebBTPrefix + "CreateTransactionXML");
            #endregion

            Thread.Sleep(2000);

            if ((Convert.ToInt32(webTest.Context["iterationNo"])% Convert.ToInt32(webTest.Context["maxiterationNo"])) ==0)
            {
                foreach (var request in webTest.Logout_WebAPI(threadData, objPlugin)) yield return request;
                //RTMonitor.Write(Color.Green, "User Logout: " + threadData.UserName + " LogOutTime: " + DateTime.Now + "\r\n");
                Thread.Sleep(2000);
            }
        }
        
    }
    public class CreateTransaction_A : WebTest
    {
        CreateTransaction _CreateTransaction = null;
        WebTestRequestPlugin objPlugin = new WebTestRequestPlugin();
        private WebTestContext _textContext;
        public CreateTransaction_A()
        {
            if (_CreateTransaction == null)
                _CreateTransaction = new CreateTransaction(this);


            this.Context.Add("LoginMain", AssemblyLoad.LoginMain);
            this.Context.Add("AccountingURL", AssemblyLoad.AccountingURL);
            this.Context.Add("TwfcndURL", AssemblyLoad.TwfcndURL);
            this.PreAuthenticate = true;
            this.Proxy = "default";
            this.StopOnError = true;
            _textContext = this.Context;

            this.PreWebTest += TwinfieldWebLoadTestPlugin.PreWebTestEvent;
            this.PreRequest += objPlugin.Test_PreRequest;
            this.PostRequest += objPlugin.Test_PostRequest;
        }

        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            _textContext= this._textContext;
            return _CreateTransaction.GetRequestEnumerator(TwinfieldDBTenant.A, Context.WebTestUserId.ToString(), objPlugin);
        }
    }

    public class CreateTransaction_B : WebTest
    {
        CreateTransaction _CreateTransaction = null;
        WebTestRequestPlugin objPlugin = new WebTestRequestPlugin();
        private WebTestContext _textContext;
        public CreateTransaction_B()
        {
            if (_CreateTransaction == null)
                _CreateTransaction = new CreateTransaction(this);


            this.Context.Add("LoginMain", AssemblyLoad.LoginMain);
            this.Context.Add("AccountingURL", AssemblyLoad.AccountingURL);
            this.Context.Add("TwfcndURL", AssemblyLoad.TwfcndURL);
            this.PreAuthenticate = true;
            this.Proxy = "default";
            this.StopOnError = true;
            _textContext = this.Context;

            this.PreWebTest += TwinfieldWebLoadTestPlugin.PreWebTestEvent;
            this.PreRequest += objPlugin.Test_PreRequest;
            this.PostRequest += objPlugin.Test_PostRequest;
        }

        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            _textContext = this._textContext;
            return _CreateTransaction.GetRequestEnumerator(TwinfieldDBTenant.B, Context.WebTestUserId.ToString(), objPlugin);
        }
    }

    public class CreateTransaction_C : WebTest
    {
        CreateTransaction _CreateTransaction = null;
        WebTestRequestPlugin objPlugin = new WebTestRequestPlugin();
        private WebTestContext _textContext;
        public CreateTransaction_C()
        {
            if (_CreateTransaction == null)
                _CreateTransaction = new CreateTransaction(this);


            this.Context.Add("LoginMain", AssemblyLoad.LoginMain);
            this.Context.Add("AccountingURL", AssemblyLoad.AccountingURL);
            this.Context.Add("TwfcndURL", AssemblyLoad.TwfcndURL);
            this.PreAuthenticate = true;
            this.Proxy = "default";
            this.StopOnError = true;
            _textContext = this.Context;

            this.PreWebTest += TwinfieldWebLoadTestPlugin.PreWebTestEvent;
            this.PreRequest += objPlugin.Test_PreRequest;
            this.PostRequest += objPlugin.Test_PostRequest;
        }

        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            _textContext = this._textContext;
            return _CreateTransaction.GetRequestEnumerator(TwinfieldDBTenant.C, Context.WebTestUserId.ToString(), objPlugin);
        }
    }

    public class CreateTransaction_D : WebTest
    {
        CreateTransaction _CreateTransaction = null;
        WebTestRequestPlugin objPlugin = new WebTestRequestPlugin();
        private WebTestContext _textContext;
        public CreateTransaction_D()
        {
            if (_CreateTransaction == null)
                _CreateTransaction = new CreateTransaction(this);


            this.Context.Add("LoginMain", AssemblyLoad.LoginMain);
            this.Context.Add("AccountingURL", AssemblyLoad.AccountingURL);
            this.Context.Add("TwfcndURL", AssemblyLoad.TwfcndURL);
            this.PreAuthenticate = true;
            this.Proxy = "default";
            this.StopOnError = true;
            _textContext = this.Context;

            this.PreWebTest += TwinfieldWebLoadTestPlugin.PreWebTestEvent;
            this.PreRequest += objPlugin.Test_PreRequest;
            this.PostRequest += objPlugin.Test_PostRequest;
        }

        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            _textContext = this._textContext;
            return _CreateTransaction.GetRequestEnumerator(TwinfieldDBTenant.D, Context.WebTestUserId.ToString(), objPlugin);
        }
    }

    public class CreateTransaction_E : WebTest
    {
        CreateTransaction _CreateTransaction = null;
        WebTestRequestPlugin objPlugin = new WebTestRequestPlugin();
        private WebTestContext _textContext;
        public CreateTransaction_E()
        {
            if (_CreateTransaction == null)
                _CreateTransaction = new CreateTransaction(this);


            this.Context.Add("LoginMain", AssemblyLoad.LoginMain);
            this.Context.Add("AccountingURL", AssemblyLoad.AccountingURL);
            this.Context.Add("TwfcndURL", AssemblyLoad.TwfcndURL);
            this.PreAuthenticate = true;
            this.Proxy = "default";
            this.StopOnError = true;
            _textContext = this.Context;

            this.PreWebTest += TwinfieldWebLoadTestPlugin.PreWebTestEvent;
            this.PreRequest += objPlugin.Test_PreRequest;
            this.PostRequest += objPlugin.Test_PostRequest;
        }

        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            _textContext = this._textContext;
            return _CreateTransaction.GetRequestEnumerator(TwinfieldDBTenant.E, Context.WebTestUserId.ToString(), objPlugin);
        }
    }
}