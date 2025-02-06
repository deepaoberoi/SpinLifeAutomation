using AlerStallings.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace AlerStallings.Support
{
    class JsonHelper
    {
        public static string GetDataByEnvironment(string parameter)
        {
            string environmentData = ReadJsonFile(@"AlerStallings.Config\EnvironmentConfig.json");
            var inputJsonObject = JsonConvert.DeserializeObject<EnvironmentConfig>(environmentData);

            var envConfig = inputJsonObject.GetDataByEnvironment.FirstOrDefault(x => x.environment.ToLower() == TestConfig.getEnvironment().ToLower());
            foreach (var config in envConfig.environmentData)
            {
                if (config.key == parameter)
                {
                    parameter = config.value;
                    return parameter;
                }
            }
            return parameter;
        }

        public static string ReadJsonFile(string fileName)
        {
            string assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPathofFile = Path.Combine(assemblyPath, @"" + fileName).Replace("Environment", TestConfig.getEnvironment());
            string inputData = File.ReadAllText(fullPathofFile);
            return inputData;
        }
    }
}
