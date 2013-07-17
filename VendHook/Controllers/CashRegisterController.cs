using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VendHook.Controllers
{
    using System.Text;

    public class CashRegisterController : ApiController
    {
        // GET api/cashregister
        public IEnumerable<string> Get()
        {
            try
            {
                byte[] data = new byte[] { 27, 112, 0, 25, 250 };
                PrintThroughDriver.SendStringToPrinter("POS58", Encoding.ASCII.GetString(data));
                return new string[] { "value1", "sucess" };
            }
            catch (Exception)
            {
                return new string[] { "value1", "fail" };
            }
        }

        // GET api/cashregister/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/cashregister
        public void Post([FromBody]string value)
        {
        }

        // PUT api/cashregister/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/cashregister/5
        public void Delete(int id)
        {
        }
    }
}
