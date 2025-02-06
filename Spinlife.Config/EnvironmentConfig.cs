using System.Collections.Generic;

namespace AlerStallings.Config
{
    public class EnvironmentData
    {
        public string key { get; set; }
        public string value { get; set; }
        public string valueFormat { get; set; }
    }
    public class GetDataByEnvironment
    {
        public string environment { get; set; }
        public IList<EnvironmentData> environmentData { get; set; }

    }
    public class EnvironmentConfig
    {
        public IList<GetDataByEnvironment> GetDataByEnvironment { get; set; }

    }
}
