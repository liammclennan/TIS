using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TodayIShall.Core.AppServices
{
    public interface IConfigService
    {
        string RavenUrl();
        string MongoConnectionString { get; }
    }

    public class ConfigService : IConfigService
    {
        public string RavenUrl()
        {
            return ReadSetting("RavenUrl");
        }

        public string MongoConnectionString 
        { 
            get { return ReadSetting("MongoConnectionString"); }
        }
        
        private string ReadSetting(string key)
        {
            CheckForKey(key);
            return ConfigurationManager.AppSettings[key];
        }

        private void CheckForKey(string appSettingKey)
        {
            if (ConfigurationManager.AppSettings[appSettingKey] == null
                || ConfigurationManager.AppSettings[appSettingKey].Equals(string.Empty))
            {
                throw new ConfigurationException("Missing appsetting: " + appSettingKey);
            }
        }
    }
}
