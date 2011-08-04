using System.Configuration;

namespace CampfireTraceListener.Configuration
{
    public class CampfireConfiguration : ConfigurationSection, ICampfireConfiguration
    {
        #region Singleton

        private CampfireConfiguration()
        {

        }

        public static CampfireConfiguration Instance
        {
            get { return Nested.instance; }
        }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly CampfireConfiguration instance =
                (CampfireConfiguration) ConfigurationManager.GetSection("campfireSettings");
        }


        #endregion

        #region Properties

        [ConfigurationProperty("accountName", IsRequired = true)]
        public string AccountName
        {
            get { return (string) base["accountName"]; }
            set { base["accountName"] = value; }
        }

        [ConfigurationProperty("authToken", IsRequired = true)]
        public string AuthToken
        {
            get { return (string) base["authToken"]; }
            set { base["authToken"] = value; }
        }

        [ConfigurationProperty("roomId", IsRequired = true)]
        public int RoomId
        {
            get { return (int) base["roomId"]; }
            set { base["roomId"] = value; }
        }

        [ConfigurationProperty("useHttps", IsRequired = true)]
        public bool UseHttps
        {
            get { return (bool) base["useHttps"]; }
            set { base["useHttps"] = value; }
        }

        #endregion


    }
}
