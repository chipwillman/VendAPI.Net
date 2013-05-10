namespace VendHook.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using VendAPI.Extentions;
    using VendAPI.Models;

    public class CustomerController : ApiController
    {
        public HttpResponseMessage Post([FromBody]VendHookPost value)
        {
            var customer = value.payload.FromJson<Customer>();
            System.Diagnostics.Debug.WriteLine(customer.ContactFirstName + " " + customer.ContactLastName + " has changed");
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
