using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Twinfield_NewFramework
{
	public class LoadTestRun
	{
		public int loadtestrunID { get; set; }
		public string LoadTestName { get; set; }
		public string Description { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int RunDuration { get; set; }
		public int WarmupTime { get; set; }
		public string RunSettingUsed { get; set; }
		public bool IsLocalRun { get; set; }
		public string ControllerName { get; set; }
		public string Outcome { get; set; }
		public int CooldownTime { get; set; }
	}
	public class LoadtestTestCase
	{
		public int LoadTestRunId { get; set; }
		public int ScenarioId { get; set; }
		public int TestCaseId { get; set; }
		public string TestCaseName { get; set; }
		public string TestElement { get; set; }
		public string TestType { get; set; }
	}
	public class WebLoadTestTransaction
	{
		public WebLoadTestTransaction()
		{ }
		public WebLoadTestTransaction(WebLoadTestTransaction para)
		{
			this.LoadTestRunId = para.LoadTestRunId;
			this.TransactionId = para.TransactionId;
			this.TestCaseId = para.TestCaseId;
			this.TransactionName = para.TransactionName;
			this.Goal = para.Goal;
			this.isAdded = para.isAdded;
		}
		public int LoadTestRunId { get; set; }
		public int TransactionId { get; set; }
		public int TestCaseId { get; set; }
		public string TransactionName { get; set; }
		public float Goal { get; set; }
		public bool isAdded { get; set; }
	}
	public class LoadTestTestDetail
	{
		public LoadTestTestDetail()
		{ }
		public LoadTestTestDetail(LoadTestTestDetail para)
		{
			this.LoadTestRunId = para.LoadTestRunId;
			this.TestDetailId = para.TestDetailId;
			this.TimeStamp = para.TimeStamp;
			this.TestCaseId = para.TestCaseId;
			this.ElapsedTime = para.ElapsedTime;
			this.AgentId = para.AgentId;
			this.BrowserId = para.BrowserId;
			this.NetworkId = para.NetworkId;
			this.Outcome = para.Outcome;
			this.TestLogId = para.TestLogId;
			this.UserId = para.UserId;
			this.EndTime = para.EndTime;
			this.InMeasurementInterval = para.InMeasurementInterval;
		}
		public int LoadTestRunId { get; set; }
		public int TestDetailId { get; set; }
		public DateTime TimeStamp { get; set; }
		public int TestCaseId { get; set; }
		public double ElapsedTime { get; set; }
		public int AgentId { get; set; }
		public int BrowserId { get; set; }
		public int NetworkId { get; set; }
		public int Outcome { get; set; }
		public int TestLogId { get; set; }
		public int UserId { get; set; }
		public DateTime EndTime { get; set; }
		public int InMeasurementInterval { get; set; }
	}
	class LoadTestRunAgent
	{
		int LoadTestRunId { get; set; }
		int AgentId { get; set; }
		string AgentName { get; set; }
	}
	public class LoadTestTransactionDetail
	{
		public LoadTestTransactionDetail(LoadTestTransactionDetail para)
		{
			this.LoadTestRunId = para.LoadTestRunId;
			this.TransactionDetailId = para.TransactionDetailId;
			this.TestDetailId = para.TestDetailId;
			this.TimeStamp = para.TimeStamp;
			this.TransactionId = para.TransactionId;
			this.ElapsedTime = para.ElapsedTime;
			this.EndTime = para.EndTime;
			this.InMeasurementInterval = para.InMeasurementInterval;
			this.ResponseTime = para.ResponseTime;
			this.TransactionName = para.TransactionName;
		}
		public LoadTestTransactionDetail()
		{ }
		public int LoadTestRunId { get; set; }
		public int TransactionDetailId { get; set; }
		public int TestDetailId { get; set; }
		public DateTime TimeStamp { get; set; }
		public int TransactionId { get; set; }
		public double ElapsedTime { get; set; }
		public DateTime EndTime { get; set; }
		public bool InMeasurementInterval { get; set; }
		public double ResponseTime { get; set; }
		public string TransactionName { get; set; }
	}
	class DBConnector
	{
        // Need to update
        string connectiostring = "Server = tcp:ltsql.database.windows.net,1433; Initial Catalog = AzureTestRigSQL; Persist Security Info = False; User ID = jredinger@ltsql; Password = Perfmon#1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //"Data Source=sagarlt.database.windows.net;Initial Catalog=LTTransactionDB_InputDB; User ID=sagard@sagarlt;pwd=admin12!@;MultipleActiveResultSets=True;";
		//string connectiostring = "Data Source=172.18.100.13;Initial Catalog=LTTransactionDB;User ID=sa; Password=LT!@#123;";
		// { get; set; }
		//string connectiostring = "Data Source=10.152.129.175;Initial Catalog=LoadTest2010;Persist Security Info=True;User ID=sa;Password=LT!@#123";
	   //string connectiostring;//= "Server=tcp:sagarlt.database.windows.net,1433;Initial Catalog=LTTransactionDB_InputDB;Persist Security Info=False;User ID=sagard@sagarlt;Password=admin12!@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
	  // string connectiostring = "Server=tcp:jcsufnoq5r.database.windows.net,1433;Initial Catalog=AxcessInputDB;Persist Security Info=False;User ID=loadtest;Password=perfmon#1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
		private SqlConnection _conn;
		private SqlCommand _cmd;
		private SqlDataAdapter _adpptor;

		public void openConnection()
		{
			try
			{
				if (connectiostring != "" && _conn == null)
				{
					_conn = new SqlConnection(connectiostring);
					if (_conn.State == System.Data.ConnectionState.Closed)
						_conn.Open();
				}
				else if (_conn != null)
				{
					if (_conn.State == System.Data.ConnectionState.Closed || _conn.State == System.Data.ConnectionState.Broken)
					{ _conn.Open(); }
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
		public void closeConnection()
		{
			try
			{
				if (_conn != null)
				{
					_conn.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void excuteMyQuery(string query)
		{
			try
			{
				openConnection();
				_cmd = new SqlCommand(query, _conn);
				_cmd.ExecuteNonQuery();
			}
			catch (Exception x)
			{ throw x; }
			finally
			{ closeConnection(); }
		}
		public void InsertTestCse(List<LoadtestTestCase> lstTestCSe)
		{
			try
			{
				openConnection();
				foreach (var item in lstTestCSe)
				{
					using (_cmd = new SqlCommand("Twinfield_Transaction.Prc_TwinfieldInsertWKLoadtestcase", _conn))
					{
						_cmd.CommandType = System.Data.CommandType.StoredProcedure;
						_cmd.Parameters.AddWithValue("@LoadTestRunId", item.LoadTestRunId);
						_cmd.Parameters.AddWithValue("@ScenarioId", item.ScenarioId);
						_cmd.Parameters.AddWithValue("@TestCaseId", item.TestCaseId);
						_cmd.Parameters.AddWithValue("@TestCaseName", item.TestCaseName);
						_cmd.Parameters.AddWithValue("@TestType", item.TestType);
						_cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{

				throw;
			}
			finally
			{ closeConnection(); }
		}
		public void Insertloadtestdetails(List<LoadTestTestDetail> lsttestdetails)
		{
			try
			{
				openConnection();
				foreach (var item in lsttestdetails)
				{
					using (_cmd = new SqlCommand("Twinfield_Transaction.PRC_TwinfieldInsertWKLoadTestTestDetail", _conn))
					{
						_cmd.CommandType = System.Data.CommandType.StoredProcedure;
						_cmd.Parameters.AddWithValue("@LoadTestRunId", item.LoadTestRunId);
						_cmd.Parameters.AddWithValue("@TestDetailId", item.TestDetailId);
						_cmd.Parameters.AddWithValue("@TestCaseId", item.TestCaseId);
						_cmd.Parameters.AddWithValue("@TimeStamp", item.TimeStamp);
						_cmd.Parameters.AddWithValue("@ElapsedTime", item.ElapsedTime);
						_cmd.Parameters.AddWithValue("@AgentId", item.AgentId);
						_cmd.Parameters.AddWithValue("@BrowserId", item.BrowserId);
						_cmd.Parameters.AddWithValue("@NetworkId", item.NetworkId);
						_cmd.Parameters.AddWithValue("@Outcome", item.Outcome);
						_cmd.Parameters.AddWithValue("@TestLogId", item.TestLogId);
						_cmd.Parameters.AddWithValue("@UserId", item.UserId);
						_cmd.Parameters.AddWithValue("@EndTime", item.EndTime);
						_cmd.Parameters.AddWithValue("@InMeasurementInterval", item.InMeasurementInterval);
						_cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{

				throw;
			}
			finally
			{ closeConnection(); }
		}
		public void Insertloadtestdetails(List<LoadTestTestDetail> lsttestdetails, List<LoadTestTransactionDetail> lsttestTransactiondetails, SqlTransaction transaction)
		{
			try
			{
				foreach (var item in lsttestdetails)
				{
					using (_cmd = new SqlCommand("Twinfield_Transaction.PRC_TwinfieldInsertWKLoadTestTestDetail", _conn))
					{
						_cmd.Transaction = transaction;
						SqlParameter outpara = new SqlParameter("@TestDetailId", System.Data.SqlDbType.Int, 10) { Direction = System.Data.ParameterDirection.Output };
						_cmd.CommandType = System.Data.CommandType.StoredProcedure;
						_cmd.Parameters.Add(outpara);
						_cmd.Parameters.AddWithValue("@LoadTestRunId", item.LoadTestRunId);
						//_cmd.Parameters.AddWithValue("@TestDetailId", item.TestDetailId);
						_cmd.Parameters.AddWithValue("@TestCaseId", item.TestCaseId);
						_cmd.Parameters.AddWithValue("@TimeStamp", item.TimeStamp);
						_cmd.Parameters.AddWithValue("@ElapsedTime", item.ElapsedTime);
						_cmd.Parameters.AddWithValue("@AgentId", item.AgentId);
						_cmd.Parameters.AddWithValue("@BrowserId", item.BrowserId);
						_cmd.Parameters.AddWithValue("@NetworkId", item.NetworkId);
						_cmd.Parameters.AddWithValue("@Outcome", item.Outcome);
						_cmd.Parameters.AddWithValue("@TestLogId", item.TestLogId);
						_cmd.Parameters.AddWithValue("@UserId", item.UserId);
						_cmd.Parameters.AddWithValue("@EndTime", item.EndTime);
						_cmd.Parameters.AddWithValue("@InMeasurementInterval", item.InMeasurementInterval);
						_cmd.ExecuteNonQuery();
						_cmd.Parameters.Clear();

						List<LoadTestTransactionDetail> lsttestTransactiondetails01 = (from l in lsttestTransactiondetails
																					   where l.TestDetailId == Convert.ToInt16(outpara.Value)
																					   select l).ToList();
						InserttesttrasactionDetails(lsttestTransactiondetails01, transaction, Convert.ToInt16(outpara.Value), item.TestCaseId);
					}
				}
			}
			catch (Exception ex)
			{

				throw;
			}
			finally
			{ closeConnection(); }
		}

		public void Insertloadtestdetails_Indivisual(List<LoadTestTestDetail> lsttestdetails, List<LoadTestTransactionDetail> lsttestTransactiondetails)
		{
			try
			{
				openConnection();
				foreach (var item in lsttestdetails)
				{
					using (_cmd = new SqlCommand("Twinfield_Transaction.Prc_TwinfieldInsertWKLoadTestTestDetail_Dynamic", _conn))
					{
						_cmd.Parameters.Clear();
						SqlParameter outpara = new SqlParameter("@TestDetailId", System.Data.SqlDbType.Int, 10) { Direction = System.Data.ParameterDirection.Output };
						_cmd.CommandType = System.Data.CommandType.StoredProcedure;
						_cmd.Parameters.Add(outpara);
						_cmd.Parameters.AddWithValue("@LoadTestRunId", item.LoadTestRunId);
						_cmd.Parameters.AddWithValue("@TestCaseId", item.TestCaseId);
						_cmd.Parameters.AddWithValue("@TimeStamp", item.TimeStamp);
						_cmd.Parameters.AddWithValue("@ElapsedTime", item.ElapsedTime);
						_cmd.Parameters.AddWithValue("@AgentId", item.AgentId);
						_cmd.Parameters.AddWithValue("@BrowserId", item.BrowserId);
						_cmd.Parameters.AddWithValue("@NetworkId", item.NetworkId);
						_cmd.Parameters.AddWithValue("@Outcome", item.Outcome);
						_cmd.Parameters.AddWithValue("@TestLogId", item.TestLogId);
						_cmd.Parameters.AddWithValue("@UserId", item.UserId);
						_cmd.Parameters.AddWithValue("@EndTime", item.EndTime);
						_cmd.Parameters.AddWithValue("@InMeasurementInterval", item.InMeasurementInterval);
						_cmd.Parameters.AddWithValue("@testdetailTblName", vsoTransactionContext._testDetailTblName);

						_cmd.ExecuteNonQuery();
						int testdtlID = Convert.ToInt16(_cmd.Parameters["@TestDetailId"].Value);

						List<LoadTestTransactionDetail> lsttestTransactiondetails01 = (from l in lsttestTransactiondetails
																					   where l.TestDetailId == item.TestDetailId // Convert.ToInt16(outpara.Value)
																					   select l).ToList();
						InserttesttrasactionDetailsIndivisual(lsttestTransactiondetails01, testdtlID, item.TestCaseId);
					}
				}
			}
			catch (Exception ex)
			{

				// throw;
			}
			finally
			{ closeConnection(); }
		}


		public void InsertWebtesttrasactions(List<WebLoadTestTransaction> lstwebtestTransaction)
		{
			try
			{
				openConnection();
				foreach (var item in lstwebtestTransaction)
				{
					using (_cmd = new SqlCommand("Twinfield_Transaction.Prc_TwinfieldInsertWKWebLoadTestTransaction", _conn))
					{
						_cmd.CommandType = System.Data.CommandType.StoredProcedure;
						_cmd.Parameters.AddWithValue("@LoadTestRunId", item.LoadTestRunId);
						_cmd.Parameters.AddWithValue("@TransactionId", item.TransactionId);
						_cmd.Parameters.AddWithValue("@TestCaseId", item.TestCaseId);
						_cmd.Parameters.AddWithValue("@TransactionName", item.TransactionName);
						_cmd.Parameters.AddWithValue("@Goal", item.Goal);

						_cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{

				throw;
			}
			finally
			{
				closeConnection();
			}
		}
		public void InserttesttrasactionDetails(List<LoadTestTransactionDetail> lsttestTransactiondetails)
		{
			try
			{
				openConnection();
				foreach (var item in lsttestTransactiondetails)
				{
					//if (item.ElapsedTime > 0)
					//{
					using (_cmd = new SqlCommand("Twinfield_Transaction.Prc_TwinfieldInsertWKLoadTestTransactionDetail", _conn))
					{
						_cmd.CommandType = System.Data.CommandType.StoredProcedure;
						_cmd.Parameters.AddWithValue("@LoadTestRunId", item.LoadTestRunId);
						_cmd.Parameters.AddWithValue("@TransactionDetailId", item.TransactionDetailId);
						_cmd.Parameters.AddWithValue("@TestDetailId", item.TestDetailId);
						_cmd.Parameters.AddWithValue("@TimeStamp", item.TimeStamp);
						_cmd.Parameters.AddWithValue("@TransactionId", item.TransactionId);
						_cmd.Parameters.AddWithValue("@ElapsedTime", item.ElapsedTime);
						_cmd.Parameters.AddWithValue("@EndTime", item.EndTime);
						_cmd.Parameters.AddWithValue("@InMeasurementInterval", item.InMeasurementInterval);
						_cmd.Parameters.AddWithValue("@ResponseTime", item.ResponseTime);
						_cmd.ExecuteNonQuery();
					}
					// }
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				closeConnection();
			}
		}
		public int insertLoadtestRun(LoadTestRun loadtestrun)
		{
			try
			{
				openConnection();
				using (_cmd = new SqlCommand("Twinfield_Transaction.Prc_TwinfieldInsertWKLoadTestRun", _conn))
				{
					_cmd.CommandType = System.Data.CommandType.StoredProcedure;
					SqlParameter outpara = new SqlParameter("@LoadTestRunId", System.Data.SqlDbType.Int, 100) { Direction = System.Data.ParameterDirection.Output };
					_cmd.Parameters.Add(outpara);// AddWithValue("@LoadTestRunId", loadtestrun.loadtestrunID);
					_cmd.Parameters.AddWithValue("@LoadTestName", loadtestrun.LoadTestName);
					_cmd.Parameters.AddWithValue("@Description", loadtestrun.Description);
					_cmd.Parameters.AddWithValue("@StartTime", loadtestrun.StartTime);
					_cmd.Parameters.AddWithValue("@RunDuration", loadtestrun.RunDuration);
					_cmd.Parameters.AddWithValue("@WarmupTime", loadtestrun.WarmupTime);
					_cmd.Parameters.AddWithValue("@RunSettingUsed", loadtestrun.RunSettingUsed);
					_cmd.Parameters.AddWithValue("@IsLocalRun", loadtestrun.IsLocalRun);
					_cmd.Parameters.AddWithValue("@ControllerName", loadtestrun.ControllerName);
					_cmd.Parameters.AddWithValue("@CooldownTime", loadtestrun.CooldownTime);
					_cmd.ExecuteNonQuery();
					return Convert.ToInt16(outpara.Value);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				closeConnection();
			}
		}
		public System.Data.DataTable getDatatable(string query)
		{
			try
			{
				openConnection();
				_adpptor = new SqlDataAdapter(query, _conn);
				System.Data.DataSet DS = new System.Data.DataSet();
				_adpptor.Fill(DS);
				return DS.Tables[0];
			}
			catch (Exception x)
			{ throw x; }
			finally
			{ closeConnection(); }
		}
		public System.Data.DataTable getDatatable(string query, SqlTransaction transaction)
		{
			try
			{
				// openConnection();
				_adpptor = new SqlDataAdapter(query, _conn);
				_adpptor.SelectCommand.Transaction = transaction;
				System.Data.DataSet DS = new System.Data.DataSet();
				_adpptor.Fill(DS);
				return DS.Tables[0];
			}
			catch (Exception x)
			{ throw x; }
			finally
			{ //closeConnection(); 
			}
		}
		public int ExecuteScalar(string query)
		{
			try
			{
				openConnection();
				_cmd = new SqlCommand(query, _conn);
				return Convert.ToInt16(_cmd.ExecuteScalar());
			}
			catch (Exception x)
			{ throw x; }
			finally
			{ closeConnection(); }
		}
		public int ExecuteScalar_1(string query, SqlTransaction transaction)
		{
			try
			{
				_cmd = new SqlCommand(query, _conn);
				_cmd.Transaction = transaction;
				return Convert.ToInt16(_cmd.ExecuteScalar());
			}
			catch (Exception x)
			{ throw x; }
			finally { _cmd.Parameters.Clear(); }
		}

		public void insertData(string loadtestdetails, int loadtestrundiD)
		{
			try
			{
				if (_conn.State == System.Data.ConnectionState.Closed) { _conn.Open(); }
				using (_cmd = new SqlCommand("Twinfield_Transaction.PRC_TwinfieldInsertWkLoadtestdetails", _conn))
				{
					_cmd.CommandType = System.Data.CommandType.StoredProcedure;
					_cmd.Parameters.AddWithValue("@Description", loadtestdetails);
					_cmd.Parameters.AddWithValue("@LoadtestRunID", loadtestrundiD);
					_cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public System.Data.DataTable InsertLoadtestrunNtestCse(LoadTestRun loadtestrun, List<LoadtestTestCase> lstTestCSe, string query)
		{
			openConnection();
			SqlTransaction transaction = _conn.BeginTransaction();
			try
			{
				int loadtestrunID = ExecuteScalar_1(query, transaction);

				//  RTMonitor.Write("loadtestrun od" + loadtestrunID);
				if (loadtestrunID <= 0)
				{
					loadtestrunID = insertLoadtestRun_1(loadtestrun, transaction);
					lstTestCSe.ToList().ForEach(l => l.LoadTestRunId = loadtestrunID);
					InsertTestCse(lstTestCSe, transaction);
				}
				vsoTransactionContext._loadtestRunID = loadtestrunID;
				return getDatatable("select *  from Twinfield.tbl_WkLoadtestcase  where LoadTestRunId =" + loadtestrunID, transaction);
			}
			catch (Exception ex)
			{

				throw;
			}
			finally
			{
				transaction.Commit();
				closeConnection();
			}

		}

		public void insertTestData(List<WebLoadTestTransaction> webLoadTestTransaction, List<LoadTestTransactionDetail> loadTestTransactionDetail, List<LoadTestTestDetail> loadTestDetail)
		{
			openConnection();
			SqlTransaction transaction = _conn.BeginTransaction();
			try
			{

			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				transaction.Commit();
				closeConnection();
			}

		}

		public int insertLoadtestRun_1(LoadTestRun loadtestrun, SqlTransaction transaction)
		{
			try
			{
				// openConnection();
				using (_cmd = new SqlCommand("Twinfield.Prc_InsertWKLoadTestRun", _conn))
				{
					_cmd.CommandType = System.Data.CommandType.StoredProcedure;
					SqlParameter outpara = new SqlParameter("@LoadTestRunId", System.Data.SqlDbType.Int, 100) { Direction = System.Data.ParameterDirection.Output };
					_cmd.Transaction = transaction;
					_cmd.Parameters.Add(outpara);// AddWithValue("@LoadTestRunId", loadtestrun.loadtestrunID);
					_cmd.Parameters.AddWithValue("@LoadTestName", loadtestrun.LoadTestName);
					_cmd.Parameters.AddWithValue("@Description", loadtestrun.Description);
					_cmd.Parameters.AddWithValue("@StartTime", loadtestrun.StartTime);
					_cmd.Parameters.AddWithValue("@RunDuration", loadtestrun.RunDuration);
					_cmd.Parameters.AddWithValue("@WarmupTime", loadtestrun.WarmupTime);
					_cmd.Parameters.AddWithValue("@RunSettingUsed", loadtestrun.RunSettingUsed);
					_cmd.Parameters.AddWithValue("@IsLocalRun", loadtestrun.IsLocalRun);
					_cmd.Parameters.AddWithValue("@ControllerName", loadtestrun.ControllerName);
					_cmd.Parameters.AddWithValue("@CooldownTime", loadtestrun.CooldownTime);
					var query=_cmd.ToString();
					_cmd.ExecuteNonQuery();
					return Convert.ToInt16(outpara.Value);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				_cmd.Parameters.Clear();
				//closeConnection();
			}
		}
		public void InsertTestCse(List<LoadtestTestCase> lstTestCSe, SqlTransaction transaction)
		{
			try
			{
				foreach (var item in lstTestCSe)
				{
					using (_cmd = new SqlCommand("Twinfield.Prc_InsertWKLoadtestcase", _conn))
					{
						_cmd.CommandType = System.Data.CommandType.StoredProcedure;
						_cmd.Transaction = transaction;
						_cmd.Parameters.AddWithValue("@LoadTestRunId", item.LoadTestRunId);
						_cmd.Parameters.AddWithValue("@ScenarioId", item.ScenarioId);
						_cmd.Parameters.AddWithValue("@TestCaseId", item.TestCaseId);
						_cmd.Parameters.AddWithValue("@TestCaseName", item.TestCaseName);
						_cmd.Parameters.AddWithValue("@TestType", item.TestType);
						_cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		public void InserttesttrasactionDetails(List<LoadTestTransactionDetail> lsttestTransactiondetails, SqlTransaction transaction, int testdetailId, int testcaseId)
		{
			try
			{
				// openConnection();
				foreach (var item in lsttestTransactiondetails)
				{
					//if (item.ElapsedTime > 0)
					//{
					using (_cmd = new SqlCommand("Twinfield_Transaction.PRC_TwinfieldInsertWKLoadTestTransactionDetail", _conn))
					{
						_cmd.CommandType = System.Data.CommandType.StoredProcedure;
						_cmd.Transaction = transaction;
						_cmd.Parameters.AddWithValue("@LoadTestRunId", item.LoadTestRunId);
						_cmd.Parameters.AddWithValue("@TransactionDetailId", item.TransactionDetailId);
						_cmd.Parameters.AddWithValue("@TestDetailId", testdetailId);
						_cmd.Parameters.AddWithValue("@TimeStamp", item.TimeStamp);
						_cmd.Parameters.AddWithValue("@TransactionId", item.TransactionId);
						_cmd.Parameters.AddWithValue("@ElapsedTime", item.ElapsedTime);
						_cmd.Parameters.AddWithValue("@EndTime", item.EndTime);
						_cmd.Parameters.AddWithValue("@InMeasurementInterval", item.InMeasurementInterval);
						_cmd.Parameters.AddWithValue("@ResponseTime", item.ResponseTime);
						_cmd.Parameters.AddWithValue("@TransctionName", item.TransactionName);
						_cmd.Parameters.AddWithValue("@testCaseId", testcaseId);
						_cmd.ExecuteNonQuery();
						_cmd.Parameters.Clear();
					}
					// }
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				// closeConnection();
			}
		}
		public void InserttesttrasactionDetailsIndivisual(List<LoadTestTransactionDetail> lsttestTransactiondetails, int testdetailId, int testcaseId)
		{
			try
			{
				// openConnection();
				foreach (var item in lsttestTransactiondetails)
				{
					//if (item.ElapsedTime > 0)
					//{
					using (_cmd = new SqlCommand("Twinfield_Transaction.Prc_TwinfieldInsertWKLoadTestTransactionDetail_Dyanmic", _conn))
					{
						_cmd.Parameters.Clear();
						_cmd.CommandType = System.Data.CommandType.StoredProcedure;
						_cmd.Parameters.AddWithValue("@LoadTestRunId", item.LoadTestRunId);
						_cmd.Parameters.AddWithValue("@TransactionDetailId", item.TransactionDetailId);
						_cmd.Parameters.AddWithValue("@TestDetailId", testdetailId);
						_cmd.Parameters.AddWithValue("@TimeStamp", item.TimeStamp);
						_cmd.Parameters.AddWithValue("@TransactionId", item.TransactionId);
						_cmd.Parameters.AddWithValue("@ElapsedTime", item.ElapsedTime);
						_cmd.Parameters.AddWithValue("@EndTime", item.EndTime);
						_cmd.Parameters.AddWithValue("@InMeasurementInterval", item.InMeasurementInterval);
						_cmd.Parameters.AddWithValue("@ResponseTime", item.ResponseTime);
						_cmd.Parameters.AddWithValue("@TransctionName", item.TransactionName);
						_cmd.Parameters.AddWithValue("@testCaseId", testcaseId);
						_cmd.Parameters.AddWithValue("@WebtransactionTbleName", vsoTransactionContext._webTransactionTbleName);
						_cmd.Parameters.AddWithValue("@TransactionDetailTbleName", vsoTransactionContext._transactionTbleName);
						_cmd.ExecuteNonQuery();
						_cmd.Parameters.Clear();
					}
					// }
				}
			}
			catch (Exception ex)
			{
				// throw;
			}
			finally
			{
				// closeConnection();
			}
		}

		public void CreateTables(string webtransactionTblName, string transactionTblName, string testdetailTblName)
		{
			try
			{
				string query = "" + Environment.NewLine +
					//" DROP TABLE " + webtransactionTblName + " " + Environment.NewLine +
					//" DROP TABLE " + transactionTblName + " " + Environment.NewLine +
					//" DROP TABLE " + testdetailTblName + " " + Environment.NewLine +
					" " + Environment.NewLine +
					" CREATE TABLE[dbo].[" + webtransactionTblName.ToString() + "]( " +
	" [LoadTestRunId]  " +
"        [int] NULL,  " +
"    [TransactionId]  " +
"        [int] NULL,  " +
"    [TestCaseId]  " +
"        [int] NULL,  " +
"    [TransactionName]  " +
"        [nvarchar](max) NULL,  " +
"    [Goal]   " +
"        [float] NULL   " +
") ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]   " + Environment.NewLine +
"                      " + Environment.NewLine +
"  " +
"                      " + Environment.NewLine +
"CREATE TABLE[dbo].[" + testdetailTblName + "](   " +
"	[LoadTestRunId]   " +
"        [int] NULL,   " +
"	[TestDetailId]   " +
"        [int] NULL,   " +
"	[TimeStamp]   " +
"        [datetime]   " +
"        NULL,   " +
"	[TestCaseId]   " +
"        [int] NULL,   " +
"	[ElapsedTime]   " +
"        [float] NULL,   " +
"	[AgentId]   " +
"        [int] NULL,   " +
"	[BrowserId]   " +
"        [int] NULL,   " +
"	[NetworkId]   " +
"        [int] NULL,   " +
"	[Outcome]   " +
"        [int] NULL,   " +
"	[TestLogId]   " +
"        [int] NULL,   " +
"	[UserId]   " +
"        [int] NULL,   " +
"	[EndTime]   " +
"        [datetime]   " +
"        NULL,   " +
"	[InMeasurementInterval]   " +
"        [bit]   " +
"        NULL   " +
") ON[PRIMARY]   " +
"              " + Environment.NewLine +
"  " + Environment.NewLine +
"              " + Environment.NewLine +
"CREATE TABLE[dbo].[" + transactionTblName + "](   " +
"	[LoadTestRunId]   " +
"        [int] NULL,   " +
"	[TransactionDetailId]   " +
"        [int] NULL,   " +
"	[TestDetailId]   " +
"        [int] NULL,   " +
"	[TimeStamp]   " +
"        [datetime]   " +
"        NULL,   " +
"	[TransactionId]   " +
"        [int] NULL,  " +
"	[ElapsedTime]   " +
"        [float] NULL,   " +
"	[EndTime]   " +
"        [datetime]   " +
"        NULL,   " +
"	[InMeasurementInterval]   " +
"        [bit]   " +
"        NULL,   " +
"	[ResponseTime]   " +
"        [float] NULL   " +
") ON[PRIMARY]   " +
"              " + Environment.NewLine +
"  ";
				excuteMyQuery(query);
			}
			catch (Exception ex)
			{

				//throw;
			}
		}

		public void BulkInsertdata(System.Data.DataTable Dt)
		{
			try
			{
				if (_conn.State == System.Data.ConnectionState.Closed) { _conn.Open(); }
				using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(_conn))
				{
					sqlBulkCopy.DestinationTableName = "Twinfield_Transaction.tbl_WkTransactionDetail";
					sqlBulkCopy.WriteToServer(Dt);
				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}
	}
}

