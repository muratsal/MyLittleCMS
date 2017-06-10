using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace MyLittleCMS.Core.Constants
{
    public class AppConstants
    {
        private static AppConstants _instance;

        public static AppConstants Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance= new AppConstants();
                    return _instance;
                }

                return _instance;
            }
        }

        public string GetConfig(string Key)
        {
            string value = WebConfigurationManager.AppSettings[Key];
            return value;
        }
        public int DefaultPageSize => Convert.ToInt32(GetConfig("DefaultPageSize"));
    }
}
