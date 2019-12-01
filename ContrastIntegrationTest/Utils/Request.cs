using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ContrastIntegrationTest.Utils
{
    public class Request
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="endpoint"></param>
        /// <param name="verb"></param>
        /// <param name="client"></param>
        /// <param name="headers"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IRestResponse RestRequest(string url, string endpoint, string verb, RestClient client = null, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null, string json = null)
        {
            if (client == null)
            {
                client = new RestClient(url);
            }
            IRestResponse response = null;
            RestRequest request = new RestRequest(endpoint);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    request.AddParameter(param.Key, param.Value);
                }
            }

            if (json != null)
            {
                request.AddParameter("application/json", json, ParameterType.RequestBody);
            }


            try
            {
                if (verb.ToLower().Equals("get"))
                {
                    response = client.Get(request);
                }
                else if (verb.ToLower().Equals("post"))
                {
                    response = client.Post(request);
                }
                else if (verb.ToLower().Equals("patch"))
                {
                    response = client.Patch(request);
                }
                else if (verb.ToLower().Equals("put"))
                {
                    response = client.Put(request);
                }
                else if (verb.ToLower().Equals("delete"))
                {
                    response = client.Delete(request);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with your request: " + e.InnerException);
            }

            return response;
        }

        public static RestClient LoginClient(string endpoint, RestClient client, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null, string jsonBody = null)
        {
            RestRequest request = new RestRequest(endpoint, Method.POST);
            if(client == null)
            {
                client = new RestClient();
            }
            client.CookieContainer = new CookieContainer();

            if (headers == null)
            {
                headers = new Dictionary<string, string>();
                headers.Add("content-type", "application/json");
            }
            else
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (parameters == null && jsonBody != null)
            {
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
            }
            else
            {
                foreach (var param in parameters)
                {
                    request.AddParameter(param.Key, param.Value);
                }
            }

            try
            {
                IRestResponse response = client.Execute(request);

                StatusOkay(response);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with your request: " + e.InnerException);
            }

            return client;
        }


        public static IRestResponse ContrastRequest(string endpoint, string verb, string json = null, Dictionary<string, string> parameters = null)
        {
            return RestRequest(Config.ContrastUrl, endpoint, verb, null, BuildHeaders(), null, json);
        }


        private static Dictionary<string, string> BuildHeaders()
        {
            var headers = new Dictionary<string, string>();
            headers.Add("API-Key", Config.ApiKey);
            headers.Add("Authorization", Config.Auth);

            return headers;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static bool StatusOkay(IRestResponse response)
        {
            bool okay = true;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Request was not completed Status: " + response.StatusCode);
                Console.WriteLine("Issue: " + response.ErrorMessage);
                okay = false;
            }

            return okay;
        }
    }

}
