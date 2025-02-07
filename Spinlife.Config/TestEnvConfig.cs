using System;
using Microsoft.Extensions.Configuration;

namespace Spinlife.Config{
     public class TestConfig
     {
        public static string getEnvironment()
        {
            var testEnvironment = Environment.GetEnvironmentVariable("Environment")?.Trim();
            if(testEnvironment == null){
                var testConfig = new ConfigurationBuilder().AddJsonFile("client-secrets.json").Build();
            return  testConfig["Environment"];
            } else
            return  testEnvironment;
        }
     }
}