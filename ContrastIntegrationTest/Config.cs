using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ContrastIntegrationTest
{
    public static class Config
    {
        public static string ApiKey;
        public static string Auth;
        public static string ContrastUrl;
        public static string WebGoatUrl;
        public static string AppId;
        public static string OrganizationId;
        public static string WebGoatUn;
        public static string WebGoatPw;
        static Config()
        {
            ApiKey = Environment.GetEnvironmentVariable("apiKey");
            Auth = Environment.GetEnvironmentVariable("auth");
            ContrastUrl = Environment.GetEnvironmentVariable("contrastUrl");
            AppId = Environment.GetEnvironmentVariable("appId");
            OrganizationId = Environment.GetEnvironmentVariable("organizationId");
            WebGoatUrl = Environment.GetEnvironmentVariable("webGoatUrl");
            WebGoatPw = Environment.GetEnvironmentVariable("webGoatPw");
            WebGoatUn = Environment.GetEnvironmentVariable("webGoatUn");


        }

        private static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }
    }
}
