using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.WebTesting;
using System.Threading;
using Microsoft.VisualStudio.TestTools.WebTesting.Rules;
using Newtonsoft.Json;
namespace Twinfield_NewFramework
{
    public class Common
    {
        public static string webserver = "10.152.124.72";
    }

    public static class MyExtensions
    {
        public static void ToWait(this string value)
        {
            try
            {
                System.Threading.Thread.Sleep((int)((SleepTimeConstants)Enum.Parse(typeof(SleepTimeConstants), value)));
                //return (int)((SleepTimeConstants)Enum.Parse(typeof(SleepTimeConstants), value));
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ToWait method inside common - " + ex.Message);
            }
        }

        public static void Validate(this Microsoft.VisualStudio.TestTools.WebTesting.WebTestContext webTestContext, string ContextParameterName, string requestUrl, out string variableToSet)
        {
            try
            {
                if(webTestContext[ContextParameterName] == null)
                {
                    throw new Exception("Required Context Parameter " + ContextParameterName + " Value Not Found in request " + requestUrl  + " its response - ");

                }
                else
                {
                    variableToSet = webTestContext[ContextParameterName].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Context Parameter Not Found, Error Thrown in Validate Common Method : " + ex.Message);
            }
        }

        public static void ExtractText(this WebTestRequest objWebTestRequest, string ContextParameterName, string startWith, string endWith, int index = 0)
        {
            try
            {
                Microsoft.VisualStudio.TestTools.WebTesting.Rules.ExtractText objExtractText = new Microsoft.VisualStudio.TestTools.WebTesting.Rules.ExtractText();
                objExtractText.StartsWith = startWith;
                objExtractText.EndsWith = endWith;
                objExtractText.IgnoreCase = false;
                objExtractText.UseRegularExpression = false;

                objExtractText.Required = false;
                objExtractText.ExtractRandomMatch = false;
                objExtractText.Index = index;
                objExtractText.HtmlDecode = true;
                objExtractText.ContextParameterName = ContextParameterName;
                objWebTestRequest.ExtractValues += new EventHandler<ExtractionEventArgs>(objExtractText.Extract);

            }
            catch (Exception ex)
            {
                throw new Exception("Extraction Rule Failed, Error Thrown in ExtractText Method in common for Request : " + objWebTestRequest.Url + "Error is : " + ex.Message);
            }
        }
    }

    
    public class Helper
    {
        /// <summary>
        /// Decode
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetSessionId
        /// </summary>
        /// <param name="decoded"></param>
        /// <returns></returns>
        public static string GetSessionId(string decoded)
        {
            try
            {
                int location = decoded.IndexOf("SessionId\":\"") + 12;
                string tr2 = "\",\"jti";
                int location2 = decoded.LastIndexOf(tr2);
                return decoded.Substring(location, location2 - location);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Encode
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public struct InputTableRecord
    {
        public string UserName;
        public string Tenant;
        public string Customer;
        public bool hasFullAccess;
    }
    
	public enum SleepTimeConstants
    {
        THOUSAND = 1000,
        TWOTHOUSAND = 2000,
        THREETHOUSAND = 3000,
        FOURTHOUSAND = 4000,
        FIVETHOUSAND = 5000,
    }



}