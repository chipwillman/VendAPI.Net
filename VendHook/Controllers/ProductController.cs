
namespace VendHook.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using VendAPI.Extentions;
    using VendAPI.Models;

    public class ProductController : ApiController
    {
        public HttpResponseMessage Post([FromBody]VendHookPost value)
        {
            var product = value.payload.FromJson<Product>();
            System.Diagnostics.Debug.WriteLine(product.Handle + " has been updated");
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
