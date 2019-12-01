using ContrastIntegrationTest.Utils;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace ContrastIntegrationTest.Flows
{
    public static class AttackFlows
    {
        public static Tuple<int, int> GetActiveAttacks()
        {
            dynamic resp = JObject.Parse(Request.ContrastRequest($"/{Config.OrganizationId}/attacks/", "get").Content);
            int numAttacks = 0;
            int numProbes = 0;
            foreach(var attack in resp.attacks)
            {
                if(attack.active = true)
                {
                    numAttacks++;
                    int probes = attack.probes;
                    numProbes = numProbes + probes;
                }
                
            }

            return new Tuple<int, int>(numAttacks, numProbes);
        }

        public static bool WaitForAttackUpdate(Tuple<int, int> prevAttackCount, int timeout = 120000)
        {
            for (int i = 0; i <= timeout; i = i + 500)
            {
                var attacks = GetActiveAttacks();
                if (prevAttackCount.Item1 == attacks.Item1 && prevAttackCount.Item2 == attacks.Item2)
                {
                    Thread.Sleep(500);
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public static void XssInjectionAttack()
        {
            var loggedInClient = webGLogin();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("QTY1", "1");
            param.Add("QTY2", "1");
            param.Add("QTY3", "1");
            param.Add("QTY4", "1");
            param.Add("field1", "%3Cscript%3Ealert(%27my+javascript+here%27)%3C%2Fscript%3E%22");
            param.Add("field2", "111");

            loggedInClient.BaseUrl = new Uri(Config.WebGoatUrl);
            try
            {
                var resp = Request.RestRequest(Config.WebGoatUrl, "/CrossSiteScripting/attack5a", "get", loggedInClient, null, param);
                if(resp.StatusCode == HttpStatusCode.InternalServerError && resp.Content.Contains("AttackBlockedException"))
                {
                    Console.WriteLine("Attack blocked successfully");
                }
                else
                {
                    throw new Exception("Error injecting attack - attack not caught");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error injecting attack {e}");
            }
         }

        public static RestSharp.RestClient webGLogin()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("username", Config.WebGoatUn);
            param.Add("password", Config.WebGoatPw);

            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Accept", "application/x-www-form-urlencoded");
            RestClient resp;
            try
            {
                resp = Request.LoginClient($"{Config.WebGoatUrl}/login", null, header, param);
            } catch (Exception e)
            {
                throw new Exception($"Error loggin into application attack {e}");
            }
            return resp;
        }
    }
}
