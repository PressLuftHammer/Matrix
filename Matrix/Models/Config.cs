using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Matrix.Models
{
    public class Config
    {
        public string DBConnection;
        public Zone[] zones;

        private static Config _config = null;
        public static Config instance
        {
            get
            {
                if (_config == null)
                {
                    _config=JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
                }
                return _config;
            }
        }
    }
}
