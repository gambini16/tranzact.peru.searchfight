using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tranzact.peru.CommonLayer.Utility
{
    public class ConfigKeys
    {
        public static string GoogleSearchUri => GetValueKey<string>("GoogleSearchUri");
        public static string BingSearchUri => GetValueKey<string>("BingSearchUri");
        public static string GoogleSearchKey => GetValueKey<string>("GoogleSearchKey");
        public static string GoogleSearchCEKey => GetValueKey<string>("GoogleSearchCEKey");
        public static string BingSearchKey => GetValueKey<string>("BingSearchKey");

        public static T GetValueKey<T>(string key, T defaultValue = default(T))
        {
            var value = ConfigurationManager.AppSettings[key];

            return string.IsNullOrWhiteSpace(value) ? defaultValue : (T)(object)(value);
        }
    }
}
