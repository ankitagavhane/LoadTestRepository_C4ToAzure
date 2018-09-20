using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Reflection;
using System.Data.OleDb;


namespace Twinfield_NewFramework
{
    public class SharedThreadData
    {
        public Microsoft.VisualStudio.TestTools.WebTesting.WebTestContext _textContext { get; set; }
        public Dictionary<string, string> threadInputData { get; set; }
        
		public int virtualId { get; set; }
        
        public bool FirstTimeLogin;
        public string BuildVersion { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Tenant { get; set; }
        public string CreatedDate { get; set; }
        public string hasFullAccess { get; set; }
        public string CustomerName { get; set; }
        public string SessionId { get; set; }
        public string OrganizationId { get; set; }

        public string BNES_SID { get; set; }

        public string CompanyName { get; set; }

        public string ScenarioPrefix { get; set; }

        //TWO Specific
        public SharedThreadData(int virtualId,Microsoft.VisualStudio.TestTools.WebTesting.WebTestContext objWebTestContext)
        {
            this.virtualId = virtualId;
            
            this._textContext = objWebTestContext;
        }
              
	}

    public class CustomDS
    {
        #region Variable Declaration
        private static readonly CustomDS m_instance = new CustomDS();

        private static readonly object m_Padlock_AllTenantInputData = new object();
        private DataSet m_datasource_AllTenantInputData = null;
        private bool m_initialized_AllTenantInputData = false;
        private DataTable m_dataTable_AllTenantInputData;

        private static readonly object m_Padlock_CreateInvoice = new object();
        private DataSet m_datasource_CreateInvoice = null;
        private bool m_initialized_CreateInvoice = false;
        private DataTable m_dataTable_CreateInvoice;
        private int m_nextPosition_CreateInvoice = 0;
        private DataTable m_dataTableCreateInvoicetemp = null;
        public bool Initialized_CreateInvoice
        {
            get { return m_initialized_CreateInvoice; }
        }

        private static readonly object m_Padlock_ExtendedTBReport = new object();
        private DataSet m_datasource_ExtendedTBReport = null;
        private bool m_initialized_ExtendedTBReport = false;
        private DataTable m_dataTable_ExtendedTBReport;
        private int m_nextPosition_ExtendedTBReport = 0;
        private DataTable m_dataTableExtendedTBReporttemp = null;
        public bool Initialized_ExtendedTBReport
        {
            get { return m_initialized_ExtendedTBReport; }
        }

        private static readonly object m_Padlock_NeoFixedAsset = new object();
        private DataSet m_datasource_NeoFixedAsset = null;
        private bool m_initialized_NeoFixedAsset = false;
        private DataTable m_dataTable_NeoFixedAsset;
        private int m_nextPosition_NeoFixedAsset = 0;
        private DataTable m_dataTableNeoFixedAssettemp = null;
        public bool Initialized_NeoFixedAsset
        {
            get { return m_initialized_NeoFixedAsset; }
        }

        private static readonly object m_Padlock_NeoSalesInvoices = new object();
        private DataSet m_datasource_NeoSalesInvoices = null;
        private bool m_initialized_NeoSalesInvoices = false;
        private DataTable m_dataTable_NeoSalesInvoices;
        private int m_nextPosition_NeoSalesInvoices = 0;
        private DataTable m_dataTableNeoSalesInvoicestemp = null;
        public bool Initialized_NeoSalesInvoices
        {
            get { return m_initialized_NeoSalesInvoices; }
        }

        private static readonly object m_Padlock_ClassicSalesInvoices = new object();
        private DataSet m_datasource_ClassicSalesInvoices = null;
        private bool m_initialized_ClassicSalesInvoices = false;
        private DataTable m_dataTable_ClassicSalesInvoices;
        private int m_nextPosition_ClassicSalesInvoices = 0;
        private DataTable m_dataTableClassicSalesInvoicestemp = null;
        public bool Initialized_ClassicSalesInvoices
        {
            get { return m_initialized_ClassicSalesInvoices; }
        }

        private static readonly object m_Padlock_CreateTransaction = new object();
        private DataSet m_datasource_CreateTransaction = null;
        private bool m_initialized_CreateTransaction = false;
        private DataTable m_dataTable_CreateTransaction;
        private int m_nextPosition_CreateTransaction = 0;
        private DataTable m_dataTableCreateTransactiontemp = null;
        public bool Initialized_CreateTransaction
        {
            get { return m_initialized_CreateTransaction; }
        }

        private static readonly object m_Padlock_ReadTransaction = new object();
        private DataSet m_datasource_ReadTransaction = null;
        private bool m_initialized_ReadTransaction = false;
        private DataTable m_dataTable_ReadTransaction;
        private int m_nextPosition_ReadTransaction = 0;
        private DataTable m_dataTableReadTransactiontemp = null;
        public bool Initialized_ReadTransaction
        {
            get { return m_initialized_ReadTransaction; }
        }

        private static readonly object m_Padlock_CompanySettings = new object();
        private DataSet m_datasource_CompanySettings = null;
        private bool m_initialized_CompanySettings = false;
        private DataTable m_dataTable_CompanySettings;
        private int m_nextPosition_CompanySettings = 0;
        private DataTable m_dataTableCompanySettingstemp = null;
        public bool Initialized_CompanySettings
        {
            get { return m_initialized_CompanySettings; }
        }

        private static readonly object m_Padlock_UserAccessSettings = new object();
        private DataSet m_datasource_UserAccessSettings = null;
        private bool m_initialized_UserAccessSettings = false;
        private DataTable m_dataTable_UserAccessSettings;
        private int m_nextPosition_UserAccessSettings = 0;
        private DataTable m_dataTableUserAccessSettingstemp = null;
        public bool Initialized_UserAccessSettings
        {
            get { return m_initialized_UserAccessSettings; }
        }

        private static readonly object m_Padlock_DocumentManagement = new object();
        private DataSet m_datasource_DocumentManagement = null;
        private bool m_initialized_DocumentManagement = false;
        private DataTable m_dataTable_DocumentManagement;
        private int m_nextPosition_DocumentManagement = 0;
        private DataTable m_dataTableDocumentManagementtemp = null;
        public bool Initialized_DocumentManagement
        {
            get { return m_initialized_DocumentManagement; }
        }

        private static readonly object m_Padlock_PayAndCollectRun = new object();
        private DataSet m_datasource_PayAndCollectRun = null;
        private bool m_initialized_PayAndCollectRun = false;
        private DataTable m_dataTable_PayAndCollectRun;
        private int m_nextPosition_PayAndCollectRun = 0;
        private DataTable m_dataTablePayAndCollectRuntemp = null;
        public bool Initialized_PayAndCollectRun
        {
            get { return m_initialized_PayAndCollectRun; }
        }

        private static readonly object m_Padlock_ExportCustomers = new object();
        private DataSet m_datasource_ExportCustomers = null;
        private bool m_initialized_ExportCustomers = false;
        private DataTable m_dataTable_ExportCustomers;
        private int m_nextPosition_ExportCustomers = 0;
        private DataTable m_dataTableExportCustomerstemp = null;
        public bool Initialized_ExportCustomers
        {
            get { return m_initialized_ExportCustomers; }
        }

        private static readonly object m_Padlock_EditInvoice = new object();
        private DataSet m_datasource_EditInvoice = null;
        private bool m_initialized_EditInvoice = false;
        private DataTable m_dataTable_EditInvoice;
        private int m_nextPosition_EditInvoice = 0;
        private DataTable m_dataTableEditInvoicetemp = null;
        public bool Initialized_EditInvoice
        {
            get { return m_initialized_EditInvoice; }
        }

        //private static readonly object m_Padlock_CompanySettings = new object();
        //private DataSet m_datasource_CompanySettings = null;
        //private bool m_initialized_CompanySettings = false;
        //private DataTable m_dataTable_CompanySettings;
        //private int m_nextPosition_CompanySettings = 0;
        //private DataTable m_dataTableCompanySettingstemp = null;
        //public bool Initialized_CompanySettings
        //{
        //    get { return m_initialized_CompanySettings; }
        //}

        public bool Initialized_AllTenantInputData
        {
            get { return m_initialized_AllTenantInputData; }
        }

        public static CustomDS Instance
        {
            get { return m_instance; }
        } 
        #endregion

        #region InitializeWeb
        public void InitializeWeb(string connectionString, Microsoft.VisualStudio.TestTools.WebTesting.WebTestContext utc)
        {
            double maxUserCount = 200000;
            
				try
				{
					
					//Single State Create Return with UserData Input Data
					SqlDataAdapter sqlDataAdapter = null;
                lock (m_Padlock_AllTenantInputData)
                {
                    if (m_datasource_AllTenantInputData == null && Initialized_AllTenantInputData == false)
                    {
                        string AllTenantInputData = GetsqlSelectStatement(utc, AssemblyLoad.objTableType.AllTenantInputTable, maxUserCount);
                        Logger.WriteGeneralLog("Sql statement " + AllTenantInputData);
                        //load the data and create adapter
                        sqlDataAdapter = new SqlDataAdapter(AllTenantInputData, connectionString);
                        //create the dataset
                        m_datasource_AllTenantInputData = new DataSet();
                        m_datasource_AllTenantInputData.Locale = CultureInfo.CurrentCulture;
                        //load the data
                        sqlDataAdapter.Fill(m_datasource_AllTenantInputData);
                        m_dataTable_AllTenantInputData = m_datasource_AllTenantInputData.Tables[0];
                    }
                    m_initialized_AllTenantInputData = true;
                }

                #region Commented
                //           lock (m_Padlock_CreateInvoice)
                //{
                //	if (m_datasource_CreateInvoice == null && Initialized_CreateInvoice == false)
                //	{
                //		string CreateInvoice = GetsqlSelectStatement(utc, AssemblyLoad.objTableType.AllTenantInputTable, maxUserCount);
                //		Logger.WriteGeneralLog("Sql statement " + CreateInvoice);
                //		//load the data and create adapter
                //		sqlDataAdapter = new SqlDataAdapter(CreateInvoice, connectionString);
                //		//create the dataset
                //		m_datasource_CreateInvoice = new DataSet();
                //		m_datasource_CreateInvoice.Locale = CultureInfo.CurrentCulture;
                //		//load the data
                //		sqlDataAdapter.Fill(m_datasource_CreateInvoice);
                //		m_dataTable_CreateInvoice = m_datasource_CreateInvoice.Tables[0];
                //	}
                //	m_initialized_CreateInvoice = true;
                //}
                //           #endregion CreateInvoice User Data

                //               #region EditInvoice User Data

                //           sqlDataAdapter = null;
                //           lock (m_Padlock_EditInvoice)
                //           {
                //               if (m_datasource_EditInvoice == null && Initialized_EditInvoice == false)
                //               {
                //                   string EditInvoice = GetsqlSelectStatement(utc, AssemblyLoad.objTableType.AllTenantInputTable, maxUserCount);
                //                   Logger.WriteGeneralLog("Sql statement " + EditInvoice);
                //                   //load the data and create adapter
                //                   sqlDataAdapter = new SqlDataAdapter(EditInvoice, connectionString);
                //                   //create the dataset
                //                   m_datasource_EditInvoice = new DataSet();
                //                   m_datasource_EditInvoice.Locale = CultureInfo.CurrentCulture;
                //                   //load the data
                //                   sqlDataAdapter.Fill(m_datasource_EditInvoice);
                //                   m_dataTable_EditInvoice = m_datasource_EditInvoice.Tables[0];
                //               }
                //               m_initialized_EditInvoice = true;
                //           }
                //           #endregion EditInvoice User Data 
                #endregion
                DataView DVTemp = m_dataTable_AllTenantInputData.DefaultView;

                DVTemp.RowFilter = "ScenarioName = 'CreateInvoice' and DataSet in (" + AssemblyLoad.DataSetToUse_CreateInvoice + ")";
                DVTemp.Sort= "Thread " + AssemblyLoad.InputSetOrder_CreateInvoice;
                m_dataTable_CreateInvoice = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'EditInvoice' and DataSet in (" + AssemblyLoad.DataSetToUse_EditInvoice + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_EditInvoice;
                m_dataTable_EditInvoice = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'CompanySettings' and DataSet in (" + AssemblyLoad.DataSetToUse_CompanySettings + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_CompanySettings;
                m_dataTable_CompanySettings = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'ExtendedTBReport' and DataSet in (" + AssemblyLoad.DataSetToUse_ExtendedTBReport + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_ExtendedTBReport;
                m_dataTable_ExtendedTBReport = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'NeoFixedAsset' and DataSet in (" + AssemblyLoad.DataSetToUse_NeoFixedAsset + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_NeoFixedAsset;
                m_dataTable_NeoFixedAsset = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'NeoSalesInvoices' and DataSet in (" + AssemblyLoad.DataSetToUse_NeoSalesInvoices + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_NeoSalesInvoices;
                m_dataTable_NeoSalesInvoices = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'ClassicSalesInvoices' and DataSet in (" + AssemblyLoad.DataSetToUse_ClassicSalesInvoices + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_ClassicSalesInvoices;
                m_dataTable_ClassicSalesInvoices = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'CreateTransaction' and DataSet in (" + AssemblyLoad.DataSetToUse_CreateTransaction + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_CreateTransaction;
                m_dataTable_CreateTransaction = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'ReadTransaction' and DataSet in (" + AssemblyLoad.DataSetToUse_ReadTransaction + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_ReadTransaction;
                m_dataTable_ReadTransaction = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'UserAccessSettings' and DataSet in (" + AssemblyLoad.DataSetToUse_UserAccessSettings + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_UserAccessSettings;
                m_dataTable_UserAccessSettings = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'DocumentManagement' and DataSet in (" + AssemblyLoad.DataSetToUse_DocumentManagement + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_DocumentManagement;
                m_dataTable_DocumentManagement = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'PayAndCollectRun' and DataSet in (" + AssemblyLoad.DataSetToUse_PayAndCollectRun + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_PayAndCollectRun;
                m_dataTable_PayAndCollectRun = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";

                DVTemp.RowFilter = "ScenarioName = 'ExportCustomers' and DataSet in (" + AssemblyLoad.DataSetToUse_ExportCustomers + ")";
                DVTemp.Sort = "Thread " + AssemblyLoad.InputSetOrder_ExportCustomers;
                m_dataTable_ExportCustomers = DVTemp.ToTable();
                DVTemp.RowFilter = "1=1";
                

            }
                catch (Exception ex)
				{

					Logger.WriteGeneralLog("Error in Getting data from IPO input table : " + ex.Message);
					throw new Exception("Error in Getting data from IPO input table : " + ex.Message);
				}
        }
        
        private string GetsqlSelectStatement(Microsoft.VisualStudio.TestTools.WebTesting.WebTestContext utc, string tablename, double maxUserCount)
        {
            double tempCount = Math.Round(maxUserCount);
            if (AssemblyLoad.agentCount > 1)
            {
                
                AssemblyLoad.sAgentId = "Agent " + Convert.ToString(1 + (utc.AgentId));
                Logger.WriteGeneralLog("Debug : " + AssemblyLoad.controllerName.ToString() + " env name" + Environment.MachineName.ToString() + "Scenario : "+ utc["ScenarioName"].ToString());
                //return "select top " + tempCount + " * from " + tablename + " with(nolock) WHERE [" + AssemblyLoad.controllerName + "] = '" + Environment.MachineName + "' and IsActive = 1"
                string SqlQuery = "";
				if (tablename.Contains("_InputData"))
				{
					SqlQuery = "select top " + tempCount + " * from " + tablename + " with(nolock) Where Used = 0 and [" + AssemblyLoad.controllerName + "] = '" + Environment.MachineName + "'";


                    SqlQuery="select * from " + tablename + " with(nolock) WHERE [" + /*theController*/AssemblyLoad.controllerName + "] = '" + Convert.ToInt32(utc.AgentId) + "'";
                }
				else
				{
					SqlQuery = "select top " + tempCount + " * from " + tablename + " with(nolock) Where [" + AssemblyLoad.controllerName + "] = '" + Environment.MachineName + "'";
				}
				
				return SqlQuery;
            }

            AssemblyLoad.sAgentId = "Agent - Local";
            if(tablename.Contains("_InputData") )
                return "select top " + tempCount + " * from " + tablename + " with(nolock) Where Used = 0";
            else
                return "select top " + tempCount + " * from " + tablename;
        }

        #endregion
        
        public Dictionary<String, String> GetNextRowTwinfield(TwinfieldDBTenant DBTenant, TwinfieldScenarioName scenarioName)
        {
            DataView dvt=new DataView();
            switch (scenarioName)
            {
                #region CreateInvoice
                //start
                case TwinfieldScenarioName.CreateInvoice:

                    if (m_dataTable_CreateInvoice != null)
                    {
                        //lock the thread
                        lock (m_Padlock_CreateInvoice)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch(DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_CreateInvoice.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableCreateInvoicetemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_CreateInvoice.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableCreateInvoicetemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_CreateInvoice.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableCreateInvoicetemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_CreateInvoice.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableCreateInvoicetemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_CreateInvoice.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableCreateInvoicetemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_CreateInvoice == m_dataTableCreateInvoicetemp.Rows.Count)
                            {
                                m_nextPosition_CreateInvoice = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableCreateInvoicetemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableCreateInvoicetemp.Rows[m_nextPosition_CreateInvoice][c].ToString());
                            }
                            m_nextPosition_CreateInvoice++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableCreateInvoicetemp = m_dataTable_CreateInvoice;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_CreateInvoice == m_dataTableCreateInvoicetemp.Rows.Count)
                            //    {
                            //        m_nextPosition_CreateInvoice = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableCreateInvoicetemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableCreateInvoicetemp.Rows[m_nextPosition_CreateInvoice][c].ToString());
                            //    }
                            //    m_nextPosition_CreateInvoice++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion CreateInvoice

                #region CompanySettings
                //start
                case TwinfieldScenarioName.CompanySettings:

                    if (m_dataTable_CompanySettings != null)
                    {
                        //lock the thread
                        lock (m_Padlock_CompanySettings)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_CompanySettings.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableCompanySettingstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_CompanySettings.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableCompanySettingstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_CompanySettings.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableCompanySettingstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_CompanySettings.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableCompanySettingstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_CompanySettings.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableCompanySettingstemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_CompanySettings == m_dataTableCompanySettingstemp.Rows.Count)
                            {
                                m_nextPosition_CompanySettings = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableCompanySettingstemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableCompanySettingstemp.Rows[m_nextPosition_CompanySettings][c].ToString());
                            }
                            m_nextPosition_CompanySettings++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableCompanySettingstemp = m_dataTable_CompanySettings;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_CompanySettings == m_dataTableCompanySettingstemp.Rows.Count)
                            //    {
                            //        m_nextPosition_CompanySettings = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableCompanySettingstemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableCompanySettingstemp.Rows[m_nextPosition_CompanySettings][c].ToString());
                            //    }
                            //    m_nextPosition_CompanySettings++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion CompanySettings

                #region EditInvoice
                //start
                case TwinfieldScenarioName.EditInvoice:

                    if (m_dataTable_EditInvoice != null)
                    {
                        //lock the thread
                        lock (m_Padlock_EditInvoice)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_EditInvoice.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'A'";
                                    m_dataTableEditInvoicetemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_EditInvoice.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableEditInvoicetemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_EditInvoice.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableEditInvoicetemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_EditInvoice.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableEditInvoicetemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_EditInvoice.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableEditInvoicetemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_EditInvoice == m_dataTableEditInvoicetemp.Rows.Count)
                            {
                                m_nextPosition_EditInvoice = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableEditInvoicetemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableEditInvoicetemp.Rows[m_nextPosition_EditInvoice][c].ToString());
                            }
                            m_nextPosition_EditInvoice++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableEditInvoicetemp = m_dataTable_EditInvoice;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_EditInvoice == m_dataTableEditInvoicetemp.Rows.Count)
                            //    {
                            //        m_nextPosition_EditInvoice = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableEditInvoicetemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableEditInvoicetemp.Rows[m_nextPosition_EditInvoice][c].ToString());
                            //    }
                            //    m_nextPosition_EditInvoice++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion EditInvoice

                #region ExtendedTBReport
                //start
                case TwinfieldScenarioName.ExtendedTBReport:

                    if (m_dataTable_ExtendedTBReport != null)
                    {
                        //lock the thread
                        lock (m_Padlock_ExtendedTBReport)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_ExtendedTBReport.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableExtendedTBReporttemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_ExtendedTBReport.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableExtendedTBReporttemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_ExtendedTBReport.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableExtendedTBReporttemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_ExtendedTBReport.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableExtendedTBReporttemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_ExtendedTBReport.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableExtendedTBReporttemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_ExtendedTBReport == m_dataTableExtendedTBReporttemp.Rows.Count)
                            {
                                m_nextPosition_ExtendedTBReport = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableExtendedTBReporttemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableExtendedTBReporttemp.Rows[m_nextPosition_ExtendedTBReport][c].ToString());
                            }
                            m_nextPosition_ExtendedTBReport++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableExtendedTBReporttemp = m_dataTable_ExtendedTBReport;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_ExtendedTBReport == m_dataTableExtendedTBReporttemp.Rows.Count)
                            //    {
                            //        m_nextPosition_ExtendedTBReport = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableExtendedTBReporttemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableExtendedTBReporttemp.Rows[m_nextPosition_ExtendedTBReport][c].ToString());
                            //    }
                            //    m_nextPosition_ExtendedTBReport++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion ExtendedTBReport

                #region NeoFixedAsset
                //start
                case TwinfieldScenarioName.NeoFixedAsset:

                    if (m_dataTable_NeoFixedAsset != null)
                    {
                        //lock the thread
                        lock (m_Padlock_NeoFixedAsset)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_NeoFixedAsset.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableNeoFixedAssettemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_NeoFixedAsset.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableNeoFixedAssettemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_NeoFixedAsset.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableNeoFixedAssettemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_NeoFixedAsset.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableNeoFixedAssettemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_NeoFixedAsset.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableNeoFixedAssettemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_NeoFixedAsset == m_dataTableNeoFixedAssettemp.Rows.Count)
                            {
                                m_nextPosition_NeoFixedAsset = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableNeoFixedAssettemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableNeoFixedAssettemp.Rows[m_nextPosition_NeoFixedAsset][c].ToString());
                            }
                            m_nextPosition_NeoFixedAsset++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableNeoFixedAssettemp = m_dataTable_NeoFixedAsset;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_NeoFixedAsset == m_dataTableNeoFixedAssettemp.Rows.Count)
                            //    {
                            //        m_nextPosition_NeoFixedAsset = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableNeoFixedAssettemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableNeoFixedAssettemp.Rows[m_nextPosition_NeoFixedAsset][c].ToString());
                            //    }
                            //    m_nextPosition_NeoFixedAsset++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion NeoFixedAsset
                    
                #region NeoSalesInvoices
                //start
                case TwinfieldScenarioName.NeoSalesInvoices:

                    if (m_dataTable_NeoSalesInvoices != null)
                    {
                        //lock the thread
                        lock (m_Padlock_NeoSalesInvoices)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_NeoSalesInvoices.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableNeoSalesInvoicestemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_NeoSalesInvoices.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableNeoSalesInvoicestemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_NeoSalesInvoices.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableNeoSalesInvoicestemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_NeoSalesInvoices.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableNeoSalesInvoicestemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_NeoSalesInvoices.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableNeoSalesInvoicestemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_NeoSalesInvoices == m_dataTableNeoSalesInvoicestemp.Rows.Count)
                            {
                                m_nextPosition_NeoSalesInvoices = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableNeoSalesInvoicestemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableNeoSalesInvoicestemp.Rows[m_nextPosition_NeoSalesInvoices][c].ToString());
                            }
                            m_nextPosition_NeoSalesInvoices++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableNeoSalesInvoicestemp = m_dataTable_NeoSalesInvoices;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_NeoSalesInvoices == m_dataTableNeoSalesInvoicestemp.Rows.Count)
                            //    {
                            //        m_nextPosition_NeoSalesInvoices = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableNeoSalesInvoicestemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableNeoSalesInvoicestemp.Rows[m_nextPosition_NeoSalesInvoices][c].ToString());
                            //    }
                            //    m_nextPosition_NeoSalesInvoices++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion NeoSalesInvoices

                #region ClassicSalesInvoices
                //start
                case TwinfieldScenarioName.ClassicSalesInvoices:

                    if (m_dataTable_ClassicSalesInvoices != null)
                    {
                        //lock the thread
                        lock (m_Padlock_ClassicSalesInvoices)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_ClassicSalesInvoices.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableClassicSalesInvoicestemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_ClassicSalesInvoices.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableClassicSalesInvoicestemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_ClassicSalesInvoices.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableClassicSalesInvoicestemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_ClassicSalesInvoices.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableClassicSalesInvoicestemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_ClassicSalesInvoices.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableClassicSalesInvoicestemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_ClassicSalesInvoices == m_dataTableClassicSalesInvoicestemp.Rows.Count)
                            {
                                m_nextPosition_ClassicSalesInvoices = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableClassicSalesInvoicestemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableClassicSalesInvoicestemp.Rows[m_nextPosition_ClassicSalesInvoices][c].ToString());
                            }
                            m_nextPosition_ClassicSalesInvoices++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableClassicSalesInvoicestemp = m_dataTable_ClassicSalesInvoices;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_ClassicSalesInvoices == m_dataTableClassicSalesInvoicestemp.Rows.Count)
                            //    {
                            //        m_nextPosition_ClassicSalesInvoices = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableClassicSalesInvoicestemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableClassicSalesInvoicestemp.Rows[m_nextPosition_ClassicSalesInvoices][c].ToString());
                            //    }
                            //    m_nextPosition_ClassicSalesInvoices++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion ClassicSalesInvoices

                #region CreateTransaction
                //start
                case TwinfieldScenarioName.CreateTransaction:

                    if (m_dataTable_CreateTransaction != null)
                    {
                        //lock the thread
                        lock (m_Padlock_CreateTransaction)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_CreateTransaction.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableCreateTransactiontemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_CreateTransaction.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableCreateTransactiontemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_CreateTransaction.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableCreateTransactiontemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_CreateTransaction.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableCreateTransactiontemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_CreateTransaction.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableCreateTransactiontemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_CreateTransaction == m_dataTableCreateTransactiontemp.Rows.Count)
                            {
                                m_nextPosition_CreateTransaction = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableCreateTransactiontemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableCreateTransactiontemp.Rows[m_nextPosition_CreateTransaction][c].ToString());
                            }
                            m_nextPosition_CreateTransaction++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableCreateTransactiontemp = m_dataTable_CreateTransaction;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_CreateTransaction == m_dataTableCreateTransactiontemp.Rows.Count)
                            //    {
                            //        m_nextPosition_CreateTransaction = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableCreateTransactiontemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableCreateTransactiontemp.Rows[m_nextPosition_CreateTransaction][c].ToString());
                            //    }
                            //    m_nextPosition_CreateTransaction++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion CreateTransaction

                #region ReadTransaction
                //start
                case TwinfieldScenarioName.ReadTransaction:

                    if (m_dataTable_ReadTransaction != null)
                    {
                        //lock the thread
                        lock (m_Padlock_ReadTransaction)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_ReadTransaction.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableReadTransactiontemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_ReadTransaction.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableReadTransactiontemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_ReadTransaction.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableReadTransactiontemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_ReadTransaction.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableReadTransactiontemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_ReadTransaction.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableReadTransactiontemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_ReadTransaction == m_dataTableReadTransactiontemp.Rows.Count)
                            {
                                m_nextPosition_ReadTransaction = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableReadTransactiontemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableReadTransactiontemp.Rows[m_nextPosition_ReadTransaction][c].ToString());
                            }
                            m_nextPosition_ReadTransaction++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableReadTransactiontemp = m_dataTable_ReadTransaction;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_ReadTransaction == m_dataTableReadTransactiontemp.Rows.Count)
                            //    {
                            //        m_nextPosition_ReadTransaction = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableReadTransactiontemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableReadTransactiontemp.Rows[m_nextPosition_ReadTransaction][c].ToString());
                            //    }
                            //    m_nextPosition_ReadTransaction++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion ReadTransaction
                    
                #region UserAccessSettings
                //start
                case TwinfieldScenarioName.UserAccessSettings:

                    if (m_dataTable_UserAccessSettings != null)
                    {
                        //lock the thread
                        lock (m_Padlock_UserAccessSettings)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_UserAccessSettings.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableUserAccessSettingstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_UserAccessSettings.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableUserAccessSettingstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_UserAccessSettings.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableUserAccessSettingstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_UserAccessSettings.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableUserAccessSettingstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_UserAccessSettings.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableUserAccessSettingstemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_UserAccessSettings == m_dataTableUserAccessSettingstemp.Rows.Count)
                            {
                                m_nextPosition_UserAccessSettings = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableUserAccessSettingstemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableUserAccessSettingstemp.Rows[m_nextPosition_UserAccessSettings][c].ToString());
                            }
                            m_nextPosition_UserAccessSettings++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableUserAccessSettingstemp = m_dataTable_UserAccessSettings;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_UserAccessSettings == m_dataTableUserAccessSettingstemp.Rows.Count)
                            //    {
                            //        m_nextPosition_UserAccessSettings = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableUserAccessSettingstemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableUserAccessSettingstemp.Rows[m_nextPosition_UserAccessSettings][c].ToString());
                            //    }
                            //    m_nextPosition_UserAccessSettings++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion UserAccessSettings
                    
                #region DocumentManagement
                //start
                case TwinfieldScenarioName.DocumentManagement:

                    if (m_dataTable_DocumentManagement != null)
                    {
                        //lock the thread
                        lock (m_Padlock_DocumentManagement)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_DocumentManagement.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableDocumentManagementtemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_DocumentManagement.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableDocumentManagementtemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_DocumentManagement.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableDocumentManagementtemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_DocumentManagement.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableDocumentManagementtemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_DocumentManagement.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableDocumentManagementtemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_DocumentManagement == m_dataTableDocumentManagementtemp.Rows.Count)
                            {
                                m_nextPosition_DocumentManagement = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableDocumentManagementtemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableDocumentManagementtemp.Rows[m_nextPosition_DocumentManagement][c].ToString());
                            }
                            m_nextPosition_DocumentManagement++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableDocumentManagementtemp = m_dataTable_DocumentManagement;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_DocumentManagement == m_dataTableDocumentManagementtemp.Rows.Count)
                            //    {
                            //        m_nextPosition_DocumentManagement = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableDocumentManagementtemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableDocumentManagementtemp.Rows[m_nextPosition_DocumentManagement][c].ToString());
                            //    }
                            //    m_nextPosition_DocumentManagement++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion DocumentManagement
                    
                #region PayAndCollectRun
                //start
                case TwinfieldScenarioName.PayAndCollectRun:

                    if (m_dataTable_PayAndCollectRun != null)
                    {
                        //lock the thread
                        lock (m_Padlock_PayAndCollectRun)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_PayAndCollectRun.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-nl'";
                                    m_dataTablePayAndCollectRuntemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_PayAndCollectRun.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTablePayAndCollectRuntemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_PayAndCollectRun.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTablePayAndCollectRuntemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_PayAndCollectRun.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTablePayAndCollectRuntemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_PayAndCollectRun.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTablePayAndCollectRuntemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_PayAndCollectRun == m_dataTablePayAndCollectRuntemp.Rows.Count)
                            {
                                m_nextPosition_PayAndCollectRun = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTablePayAndCollectRuntemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTablePayAndCollectRuntemp.Rows[m_nextPosition_PayAndCollectRun][c].ToString());
                            }
                            m_nextPosition_PayAndCollectRun++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTablePayAndCollectRuntemp = m_dataTable_PayAndCollectRun;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_PayAndCollectRun == m_dataTablePayAndCollectRuntemp.Rows.Count)
                            //    {
                            //        m_nextPosition_PayAndCollectRun = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTablePayAndCollectRuntemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTablePayAndCollectRuntemp.Rows[m_nextPosition_PayAndCollectRun][c].ToString());
                            //    }
                            //    m_nextPosition_PayAndCollectRun++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion PayAndCollectRun
                    
                #region ExportCustomers
                //start
                case TwinfieldScenarioName.ExportCustomers:

                    if (m_dataTable_ExportCustomers != null)
                    {
                        //lock the thread
                        lock (m_Padlock_ExportCustomers)
                        {
                            //create an object to hold the name value pairs
                            Dictionary<String, String> dictionary = new Dictionary<string, string>();
                            switch (DBTenant)
                            {
                                case TwinfieldDBTenant.A:
                                    dvt = m_dataTable_ExportCustomers.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'azure-test-perf'";
                                    m_dataTableExportCustomerstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.B:
                                    dvt = m_dataTable_ExportCustomers.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'B'";
                                    m_dataTableExportCustomerstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.C:
                                    dvt = m_dataTable_ExportCustomers.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'C'";
                                    m_dataTableExportCustomerstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.D:
                                    dvt = m_dataTable_ExportCustomers.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'D'";
                                    m_dataTableExportCustomerstemp = dvt.ToTable();
                                    break;
                                case TwinfieldDBTenant.E:
                                    dvt = m_dataTable_ExportCustomers.DefaultView;
                                    dvt.RowFilter = "DBTenant = 'E'";
                                    m_dataTableExportCustomerstemp = dvt.ToTable();
                                    break;

                            }
                            //if you have reached the end of the cursor, loop back around to the beginning
                            if (m_nextPosition_ExportCustomers == m_dataTableExportCustomerstemp.Rows.Count)
                            {
                                m_nextPosition_ExportCustomers = 0;
                            }
                            //add each column to the dictionary
                            foreach (DataColumn c in m_dataTableExportCustomerstemp.Columns)
                            {
                                dictionary.Add(c.ColumnName, m_dataTableExportCustomerstemp.Rows[m_nextPosition_ExportCustomers][c].ToString());
                            }
                            m_nextPosition_ExportCustomers++;
                            ////switch (scenarioName)
                            //{
                            //    //case TWOScenarioName.FilterReturnGridView:

                            //    m_dataTableExportCustomerstemp = m_dataTable_ExportCustomers;
                            //    //if you have reached the end of the cursor, loop back around to the beginning
                            //    if (m_nextPosition_ExportCustomers == m_dataTableExportCustomerstemp.Rows.Count)
                            //    {
                            //        m_nextPosition_ExportCustomers = 0;
                            //    }
                            //    //add each column to the dictionary
                            //    foreach (DataColumn c in m_dataTableExportCustomerstemp.Columns)
                            //    {
                            //        dictionary.Add(c.ColumnName, m_dataTableExportCustomerstemp.Rows[m_nextPosition_ExportCustomers][c].ToString());
                            //    }
                            //    m_nextPosition_ExportCustomers++;
                            //    // break;
                            //}
                            return dictionary;
                        }
                    }
                    break;
                #endregion ExportCustomers

            }

            return null;
        }

        #region ReducedLogin

        #region Dictionary and Lock Declaration for each scenario
        private static readonly object CreateInvoice_A_Lock = new object();
        private static readonly object CreateInvoice_B_Lock = new object();
        private static readonly object CreateInvoice_C_Lock = new object();
        private static readonly object CreateInvoice_D_Lock = new object();
        private static readonly object CreateInvoice_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> CreateInvoice_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CreateInvoice_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CreateInvoice_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CreateInvoice_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CreateInvoice_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object CompanySettings_A_Lock = new object();
        private static readonly object CompanySettings_B_Lock = new object();
        private static readonly object CompanySettings_C_Lock = new object();
        private static readonly object CompanySettings_D_Lock = new object();
        private static readonly object CompanySettings_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> CompanySettings_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CompanySettings_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CompanySettings_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CompanySettings_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CompanySettings_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object ExtendedTBReport_A_Lock = new object();
        private static readonly object ExtendedTBReport_B_Lock = new object();
        private static readonly object ExtendedTBReport_C_Lock = new object();
        private static readonly object ExtendedTBReport_D_Lock = new object();
        private static readonly object ExtendedTBReport_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> ExtendedTBReport_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ExtendedTBReport_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ExtendedTBReport_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ExtendedTBReport_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ExtendedTBReport_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object NeoFixedAsset_A_Lock = new object();
        private static readonly object NeoFixedAsset_B_Lock = new object();
        private static readonly object NeoFixedAsset_C_Lock = new object();
        private static readonly object NeoFixedAsset_D_Lock = new object();
        private static readonly object NeoFixedAsset_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> NeoFixedAsset_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> NeoFixedAsset_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> NeoFixedAsset_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> NeoFixedAsset_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> NeoFixedAsset_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object NeoSalesInvoices_A_Lock = new object();
        private static readonly object NeoSalesInvoices_B_Lock = new object();
        private static readonly object NeoSalesInvoices_C_Lock = new object();
        private static readonly object NeoSalesInvoices_D_Lock = new object();
        private static readonly object NeoSalesInvoices_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> NeoSalesInvoices_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> NeoSalesInvoices_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> NeoSalesInvoices_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> NeoSalesInvoices_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> NeoSalesInvoices_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object ClassicSalesInvoices_A_Lock = new object();
        private static readonly object ClassicSalesInvoices_B_Lock = new object();
        private static readonly object ClassicSalesInvoices_C_Lock = new object();
        private static readonly object ClassicSalesInvoices_D_Lock = new object();
        private static readonly object ClassicSalesInvoices_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> ClassicSalesInvoices_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ClassicSalesInvoices_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ClassicSalesInvoices_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ClassicSalesInvoices_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ClassicSalesInvoices_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object CreateTransaction_A_Lock = new object();
        private static readonly object CreateTransaction_B_Lock = new object();
        private static readonly object CreateTransaction_C_Lock = new object();
        private static readonly object CreateTransaction_D_Lock = new object();
        private static readonly object CreateTransaction_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> CreateTransaction_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CreateTransaction_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CreateTransaction_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CreateTransaction_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> CreateTransaction_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object ReadTransaction_A_Lock = new object();
        private static readonly object ReadTransaction_B_Lock = new object();
        private static readonly object ReadTransaction_C_Lock = new object();
        private static readonly object ReadTransaction_D_Lock = new object();
        private static readonly object ReadTransaction_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> ReadTransaction_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ReadTransaction_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ReadTransaction_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ReadTransaction_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ReadTransaction_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object UserAccessSettings_A_Lock = new object();
        private static readonly object UserAccessSettings_B_Lock = new object();
        private static readonly object UserAccessSettings_C_Lock = new object();
        private static readonly object UserAccessSettings_D_Lock = new object();
        private static readonly object UserAccessSettings_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> UserAccessSettings_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> UserAccessSettings_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> UserAccessSettings_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> UserAccessSettings_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> UserAccessSettings_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object DocumentManagement_A_Lock = new object();
        private static readonly object DocumentManagement_B_Lock = new object();
        private static readonly object DocumentManagement_C_Lock = new object();
        private static readonly object DocumentManagement_D_Lock = new object();
        private static readonly object DocumentManagement_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> DocumentManagement_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> DocumentManagement_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> DocumentManagement_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> DocumentManagement_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> DocumentManagement_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object PayAndCollectRun_A_Lock = new object();
        private static readonly object PayAndCollectRun_B_Lock = new object();
        private static readonly object PayAndCollectRun_C_Lock = new object();
        private static readonly object PayAndCollectRun_D_Lock = new object();
        private static readonly object PayAndCollectRun_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> PayAndCollectRun_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> PayAndCollectRun_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> PayAndCollectRun_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> PayAndCollectRun_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> PayAndCollectRun_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object ExportCustomers_A_Lock = new object();
        private static readonly object ExportCustomers_B_Lock = new object();
        private static readonly object ExportCustomers_C_Lock = new object();
        private static readonly object ExportCustomers_D_Lock = new object();
        private static readonly object ExportCustomers_E_Lock = new object();
        private static Dictionary<int, SharedThreadData> ExportCustomers_A_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ExportCustomers_B_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ExportCustomers_C_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ExportCustomers_D_UserData = new Dictionary<int, SharedThreadData>();
        private static Dictionary<int, SharedThreadData> ExportCustomers_E_UserData = new Dictionary<int, SharedThreadData>();

        private static readonly object EditInvoice_Lock = new object();
        private static Dictionary<int, SharedThreadData> EditInvoiceUserData_Web = new Dictionary<int, SharedThreadData>(); 
        #endregion

        public void Save_CreateInvoice_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch(DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (CreateInvoice_A_Lock)
                    {
                        if (CreateInvoice_A_UserData.ContainsKey(UserId))
                        {
                            CreateInvoice_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CreateInvoice_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (CreateInvoice_B_Lock)
                    {
                        if (CreateInvoice_B_UserData.ContainsKey(UserId))
                        {
                            CreateInvoice_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CreateInvoice_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (CreateInvoice_C_Lock)
                    {
                        if (CreateInvoice_C_UserData.ContainsKey(UserId))
                        {
                            CreateInvoice_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CreateInvoice_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (CreateInvoice_D_Lock)
                    {
                        if (CreateInvoice_D_UserData.ContainsKey(UserId))
                        {
                            CreateInvoice_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CreateInvoice_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (CreateInvoice_E_Lock)
                    {
                        if (CreateInvoice_E_UserData.ContainsKey(UserId))
                        {
                            CreateInvoice_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CreateInvoice_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }
            
        }
        public SharedThreadData Get_CreateInvoice_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (CreateInvoice_A_Lock)
                    {
                        return CreateInvoice_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (CreateInvoice_B_Lock)
                    {
                        return CreateInvoice_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (CreateInvoice_C_Lock)
                    {
                        return CreateInvoice_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (CreateInvoice_D_Lock)
                    {
                        return CreateInvoice_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (CreateInvoice_E_Lock)
                    {
                        return CreateInvoice_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }


        public void Save_CompanySettings_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (CompanySettings_A_Lock)
                    {
                        if (CompanySettings_A_UserData.ContainsKey(UserId))
                        {
                            CompanySettings_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CompanySettings_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (CompanySettings_B_Lock)
                    {
                        if (CompanySettings_B_UserData.ContainsKey(UserId))
                        {
                            CompanySettings_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CompanySettings_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (CompanySettings_C_Lock)
                    {
                        if (CompanySettings_C_UserData.ContainsKey(UserId))
                        {
                            CompanySettings_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CompanySettings_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (CompanySettings_D_Lock)
                    {
                        if (CompanySettings_D_UserData.ContainsKey(UserId))
                        {
                            CompanySettings_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CompanySettings_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (CompanySettings_E_Lock)
                    {
                        if (CompanySettings_E_UserData.ContainsKey(UserId))
                        {
                            CompanySettings_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CompanySettings_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_CompanySettings_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (CompanySettings_A_Lock)
                    {
                        return CompanySettings_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (CompanySettings_B_Lock)
                    {
                        return CompanySettings_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (CompanySettings_C_Lock)
                    {
                        return CompanySettings_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (CompanySettings_D_Lock)
                    {
                        return CompanySettings_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (CompanySettings_E_Lock)
                    {
                        return CompanySettings_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }


        public void SaveEditInvoiceUserDataWeb(int UserId, SharedThreadData sharedThreadData)
        {
            lock (EditInvoice_Lock)
            {
                if (EditInvoiceUserData_Web.ContainsKey(UserId))
                {
                    EditInvoiceUserData_Web[UserId] = sharedThreadData;
                }
                else
                {
                    EditInvoiceUserData_Web.Add(UserId, sharedThreadData);
                }
            }
        }
        public SharedThreadData GetEditInvoiceUserData_Web(int UserId)
        {
            lock (EditInvoice_Lock)
            {
                return EditInvoiceUserData_Web[UserId];
            }
        }
        

        public void Save_ExtendedTBReport_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (ExtendedTBReport_A_Lock)
                    {
                        if (ExtendedTBReport_A_UserData.ContainsKey(UserId))
                        {
                            ExtendedTBReport_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ExtendedTBReport_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (ExtendedTBReport_B_Lock)
                    {
                        if (ExtendedTBReport_B_UserData.ContainsKey(UserId))
                        {
                            ExtendedTBReport_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ExtendedTBReport_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (ExtendedTBReport_C_Lock)
                    {
                        if (ExtendedTBReport_C_UserData.ContainsKey(UserId))
                        {
                            ExtendedTBReport_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ExtendedTBReport_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (ExtendedTBReport_D_Lock)
                    {
                        if (ExtendedTBReport_D_UserData.ContainsKey(UserId))
                        {
                            ExtendedTBReport_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ExtendedTBReport_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (ExtendedTBReport_E_Lock)
                    {
                        if (ExtendedTBReport_E_UserData.ContainsKey(UserId))
                        {
                            ExtendedTBReport_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ExtendedTBReport_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_ExtendedTBReport_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (ExtendedTBReport_A_Lock)
                    {
                        return ExtendedTBReport_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (ExtendedTBReport_B_Lock)
                    {
                        return ExtendedTBReport_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (ExtendedTBReport_C_Lock)
                    {
                        return ExtendedTBReport_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (ExtendedTBReport_D_Lock)
                    {
                        return ExtendedTBReport_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (ExtendedTBReport_E_Lock)
                    {
                        return ExtendedTBReport_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }
        

        public void Save_NeoFixedAsset_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (NeoFixedAsset_A_Lock)
                    {
                        if (NeoFixedAsset_A_UserData.ContainsKey(UserId))
                        {
                            NeoFixedAsset_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            NeoFixedAsset_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (NeoFixedAsset_B_Lock)
                    {
                        if (NeoFixedAsset_B_UserData.ContainsKey(UserId))
                        {
                            NeoFixedAsset_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            NeoFixedAsset_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (NeoFixedAsset_C_Lock)
                    {
                        if (NeoFixedAsset_C_UserData.ContainsKey(UserId))
                        {
                            NeoFixedAsset_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            NeoFixedAsset_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (NeoFixedAsset_D_Lock)
                    {
                        if (NeoFixedAsset_D_UserData.ContainsKey(UserId))
                        {
                            NeoFixedAsset_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            NeoFixedAsset_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (NeoFixedAsset_E_Lock)
                    {
                        if (NeoFixedAsset_E_UserData.ContainsKey(UserId))
                        {
                            NeoFixedAsset_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            NeoFixedAsset_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_NeoFixedAsset_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (NeoFixedAsset_A_Lock)
                    {
                        return NeoFixedAsset_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (NeoFixedAsset_B_Lock)
                    {
                        return NeoFixedAsset_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (NeoFixedAsset_C_Lock)
                    {
                        return NeoFixedAsset_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (NeoFixedAsset_D_Lock)
                    {
                        return NeoFixedAsset_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (NeoFixedAsset_E_Lock)
                    {
                        return NeoFixedAsset_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }
        

        public void Save_NeoSalesInvoices_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (NeoSalesInvoices_A_Lock)
                    {
                        if (NeoSalesInvoices_A_UserData.ContainsKey(UserId))
                        {
                            NeoSalesInvoices_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            NeoSalesInvoices_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (NeoSalesInvoices_B_Lock)
                    {
                        if (NeoSalesInvoices_B_UserData.ContainsKey(UserId))
                        {
                            NeoSalesInvoices_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            NeoSalesInvoices_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (NeoSalesInvoices_C_Lock)
                    {
                        if (NeoSalesInvoices_C_UserData.ContainsKey(UserId))
                        {
                            NeoSalesInvoices_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            NeoSalesInvoices_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (NeoSalesInvoices_D_Lock)
                    {
                        if (NeoSalesInvoices_D_UserData.ContainsKey(UserId))
                        {
                            NeoSalesInvoices_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            NeoSalesInvoices_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (NeoSalesInvoices_E_Lock)
                    {
                        if (NeoSalesInvoices_E_UserData.ContainsKey(UserId))
                        {
                            NeoSalesInvoices_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            NeoSalesInvoices_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_NeoSalesInvoices_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (NeoSalesInvoices_A_Lock)
                    {
                        return NeoSalesInvoices_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (NeoSalesInvoices_B_Lock)
                    {
                        return NeoSalesInvoices_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (NeoSalesInvoices_C_Lock)
                    {
                        return NeoSalesInvoices_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (NeoSalesInvoices_D_Lock)
                    {
                        return NeoSalesInvoices_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (NeoSalesInvoices_E_Lock)
                    {
                        return NeoSalesInvoices_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }
        

        public void Save_ClassicSalesInvoices_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (ClassicSalesInvoices_A_Lock)
                    {
                        if (ClassicSalesInvoices_A_UserData.ContainsKey(UserId))
                        {
                            ClassicSalesInvoices_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ClassicSalesInvoices_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (ClassicSalesInvoices_B_Lock)
                    {
                        if (ClassicSalesInvoices_B_UserData.ContainsKey(UserId))
                        {
                            ClassicSalesInvoices_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ClassicSalesInvoices_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (ClassicSalesInvoices_C_Lock)
                    {
                        if (ClassicSalesInvoices_C_UserData.ContainsKey(UserId))
                        {
                            ClassicSalesInvoices_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ClassicSalesInvoices_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (ClassicSalesInvoices_D_Lock)
                    {
                        if (ClassicSalesInvoices_D_UserData.ContainsKey(UserId))
                        {
                            ClassicSalesInvoices_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ClassicSalesInvoices_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (ClassicSalesInvoices_E_Lock)
                    {
                        if (ClassicSalesInvoices_E_UserData.ContainsKey(UserId))
                        {
                            ClassicSalesInvoices_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ClassicSalesInvoices_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_ClassicSalesInvoices_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (ClassicSalesInvoices_A_Lock)
                    {
                        return ClassicSalesInvoices_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (ClassicSalesInvoices_B_Lock)
                    {
                        return ClassicSalesInvoices_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (ClassicSalesInvoices_C_Lock)
                    {
                        return ClassicSalesInvoices_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (ClassicSalesInvoices_D_Lock)
                    {
                        return ClassicSalesInvoices_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (ClassicSalesInvoices_E_Lock)
                    {
                        return ClassicSalesInvoices_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }
        

        public void Save_CreateTransaction_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (CreateTransaction_A_Lock)
                    {
                        if (CreateTransaction_A_UserData.ContainsKey(UserId))
                        {
                            CreateTransaction_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CreateTransaction_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (CreateTransaction_B_Lock)
                    {
                        if (CreateTransaction_B_UserData.ContainsKey(UserId))
                        {
                            CreateTransaction_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CreateTransaction_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (CreateTransaction_C_Lock)
                    {
                        if (CreateTransaction_C_UserData.ContainsKey(UserId))
                        {
                            CreateTransaction_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CreateTransaction_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (CreateTransaction_D_Lock)
                    {
                        if (CreateTransaction_D_UserData.ContainsKey(UserId))
                        {
                            CreateTransaction_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CreateTransaction_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (CreateTransaction_E_Lock)
                    {
                        if (CreateTransaction_E_UserData.ContainsKey(UserId))
                        {
                            CreateTransaction_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            CreateTransaction_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_CreateTransaction_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (CreateTransaction_A_Lock)
                    {
                        return CreateTransaction_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (CreateTransaction_B_Lock)
                    {
                        return CreateTransaction_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (CreateTransaction_C_Lock)
                    {
                        return CreateTransaction_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (CreateTransaction_D_Lock)
                    {
                        return CreateTransaction_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (CreateTransaction_E_Lock)
                    {
                        return CreateTransaction_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }
        

        public void Save_ReadTransaction_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (ReadTransaction_A_Lock)
                    {
                        if (ReadTransaction_A_UserData.ContainsKey(UserId))
                        {
                            ReadTransaction_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ReadTransaction_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (ReadTransaction_B_Lock)
                    {
                        if (ReadTransaction_B_UserData.ContainsKey(UserId))
                        {
                            ReadTransaction_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ReadTransaction_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (ReadTransaction_C_Lock)
                    {
                        if (ReadTransaction_C_UserData.ContainsKey(UserId))
                        {
                            ReadTransaction_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ReadTransaction_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (ReadTransaction_D_Lock)
                    {
                        if (ReadTransaction_D_UserData.ContainsKey(UserId))
                        {
                            ReadTransaction_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ReadTransaction_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (ReadTransaction_E_Lock)
                    {
                        if (ReadTransaction_E_UserData.ContainsKey(UserId))
                        {
                            ReadTransaction_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ReadTransaction_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_ReadTransaction_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (ReadTransaction_A_Lock)
                    {
                        return ReadTransaction_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (ReadTransaction_B_Lock)
                    {
                        return ReadTransaction_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (ReadTransaction_C_Lock)
                    {
                        return ReadTransaction_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (ReadTransaction_D_Lock)
                    {
                        return ReadTransaction_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (ReadTransaction_E_Lock)
                    {
                        return ReadTransaction_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }

        
        public void Save_UserAccessSettings_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (UserAccessSettings_A_Lock)
                    {
                        if (UserAccessSettings_A_UserData.ContainsKey(UserId))
                        {
                            UserAccessSettings_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            UserAccessSettings_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (UserAccessSettings_B_Lock)
                    {
                        if (UserAccessSettings_B_UserData.ContainsKey(UserId))
                        {
                            UserAccessSettings_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            UserAccessSettings_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (UserAccessSettings_C_Lock)
                    {
                        if (UserAccessSettings_C_UserData.ContainsKey(UserId))
                        {
                            UserAccessSettings_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            UserAccessSettings_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (UserAccessSettings_D_Lock)
                    {
                        if (UserAccessSettings_D_UserData.ContainsKey(UserId))
                        {
                            UserAccessSettings_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            UserAccessSettings_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (UserAccessSettings_E_Lock)
                    {
                        if (UserAccessSettings_E_UserData.ContainsKey(UserId))
                        {
                            UserAccessSettings_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            UserAccessSettings_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_UserAccessSettings_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (UserAccessSettings_A_Lock)
                    {
                        return UserAccessSettings_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (UserAccessSettings_B_Lock)
                    {
                        return UserAccessSettings_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (UserAccessSettings_C_Lock)
                    {
                        return UserAccessSettings_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (UserAccessSettings_D_Lock)
                    {
                        return UserAccessSettings_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (UserAccessSettings_E_Lock)
                    {
                        return UserAccessSettings_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }
        

        public void Save_DocumentManagement_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (DocumentManagement_A_Lock)
                    {
                        if (DocumentManagement_A_UserData.ContainsKey(UserId))
                        {
                            DocumentManagement_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            DocumentManagement_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (DocumentManagement_B_Lock)
                    {
                        if (DocumentManagement_B_UserData.ContainsKey(UserId))
                        {
                            DocumentManagement_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            DocumentManagement_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (DocumentManagement_C_Lock)
                    {
                        if (DocumentManagement_C_UserData.ContainsKey(UserId))
                        {
                            DocumentManagement_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            DocumentManagement_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (DocumentManagement_D_Lock)
                    {
                        if (DocumentManagement_D_UserData.ContainsKey(UserId))
                        {
                            DocumentManagement_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            DocumentManagement_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (DocumentManagement_E_Lock)
                    {
                        if (DocumentManagement_E_UserData.ContainsKey(UserId))
                        {
                            DocumentManagement_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            DocumentManagement_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_DocumentManagement_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (DocumentManagement_A_Lock)
                    {
                        return DocumentManagement_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (DocumentManagement_B_Lock)
                    {
                        return DocumentManagement_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (DocumentManagement_C_Lock)
                    {
                        return DocumentManagement_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (DocumentManagement_D_Lock)
                    {
                        return DocumentManagement_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (DocumentManagement_E_Lock)
                    {
                        return DocumentManagement_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }
        

        public void Save_PayAndCollectRun_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (PayAndCollectRun_A_Lock)
                    {
                        if (PayAndCollectRun_A_UserData.ContainsKey(UserId))
                        {
                            PayAndCollectRun_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            PayAndCollectRun_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (PayAndCollectRun_B_Lock)
                    {
                        if (PayAndCollectRun_B_UserData.ContainsKey(UserId))
                        {
                            PayAndCollectRun_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            PayAndCollectRun_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (PayAndCollectRun_C_Lock)
                    {
                        if (PayAndCollectRun_C_UserData.ContainsKey(UserId))
                        {
                            PayAndCollectRun_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            PayAndCollectRun_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (PayAndCollectRun_D_Lock)
                    {
                        if (PayAndCollectRun_D_UserData.ContainsKey(UserId))
                        {
                            PayAndCollectRun_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            PayAndCollectRun_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (PayAndCollectRun_E_Lock)
                    {
                        if (PayAndCollectRun_E_UserData.ContainsKey(UserId))
                        {
                            PayAndCollectRun_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            PayAndCollectRun_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_PayAndCollectRun_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (PayAndCollectRun_A_Lock)
                    {
                        return PayAndCollectRun_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (PayAndCollectRun_B_Lock)
                    {
                        return PayAndCollectRun_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (PayAndCollectRun_C_Lock)
                    {
                        return PayAndCollectRun_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (PayAndCollectRun_D_Lock)
                    {
                        return PayAndCollectRun_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (PayAndCollectRun_E_Lock)
                    {
                        return PayAndCollectRun_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }
        

        public void Save_ExportCustomers_UserData(int UserId, SharedThreadData sharedThreadData, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (ExportCustomers_A_Lock)
                    {
                        if (ExportCustomers_A_UserData.ContainsKey(UserId))
                        {
                            ExportCustomers_A_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ExportCustomers_A_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (ExportCustomers_B_Lock)
                    {
                        if (ExportCustomers_B_UserData.ContainsKey(UserId))
                        {
                            ExportCustomers_B_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ExportCustomers_B_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (ExportCustomers_C_Lock)
                    {
                        if (ExportCustomers_C_UserData.ContainsKey(UserId))
                        {
                            ExportCustomers_C_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ExportCustomers_C_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (ExportCustomers_D_Lock)
                    {
                        if (ExportCustomers_D_UserData.ContainsKey(UserId))
                        {
                            ExportCustomers_D_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ExportCustomers_D_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (ExportCustomers_E_Lock)
                    {
                        if (ExportCustomers_E_UserData.ContainsKey(UserId))
                        {
                            ExportCustomers_E_UserData[UserId] = sharedThreadData;
                        }
                        else
                        {
                            ExportCustomers_E_UserData.Add(UserId, sharedThreadData);
                        }
                    }
                    break;

            }

        }
        public SharedThreadData Get_ExportCustomers_UserData(int UserId, TwinfieldDBTenant DBTenant)
        {
            switch (DBTenant)
            {
                case TwinfieldDBTenant.A:
                    lock (ExportCustomers_A_Lock)
                    {
                        return ExportCustomers_A_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.B:
                    lock (ExportCustomers_B_Lock)
                    {
                        return ExportCustomers_B_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.C:
                    lock (ExportCustomers_C_Lock)
                    {
                        return ExportCustomers_C_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.D:
                    lock (ExportCustomers_D_Lock)
                    {
                        return ExportCustomers_D_UserData[UserId];
                    }
                    break;
                case TwinfieldDBTenant.E:
                    lock (ExportCustomers_E_Lock)
                    {
                        return ExportCustomers_E_UserData[UserId];
                    }
                    break;
                default: return null; break;
            }
        }
        
        #endregion ReducedLogin
    }
}
