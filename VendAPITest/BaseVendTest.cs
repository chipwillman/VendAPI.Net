namespace VendAPITest
{
    using System.Configuration;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class BaseVendTest
    {
        public string Url
        {
            get
            {
                var result = ConfigurationManager.AppSettings["Url"];
                return result;
            }
        }

        public string Username
        {
            get
            {
                var result = ConfigurationManager.AppSettings["Username"];
                return result;
            }
        }

        public string Password
        {
            get
            {
                var result = ConfigurationManager.AppSettings["Password"];
                return result;
            }
        }
    }
}
