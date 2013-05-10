namespace VendHook.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using VendAPI.Extentions;

    using VendAPI.Models;

    public class RegisterSalesController : ApiController
    {
        public HttpResponseMessage Post([FromBody]VendHookPost value)
        {
            var registerSale = value.payload.FromJson<RegisterSale>();

            if (registerSale.RegisterSalePayments != null)
            {
                var lastPayment = registerSale.RegisterSalePayments.Last();
                if (lastPayment != null && lastPayment.PaymentTypeId == "1")
                {
                    System.Diagnostics.Debug.WriteLine("Cash sale found. Opening Cash Drawer");
                }
            }

            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
