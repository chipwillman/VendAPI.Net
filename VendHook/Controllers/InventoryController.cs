namespace VendHook.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using VendAPI.Extentions;
    using VendAPI.Models;

    public class InventoryController : ApiController
    {
        // {"TEST":"WARNING: THIS IS A TEST PAYLOAD ONLY. THESE IDs ARE NOT VALID FOR THIS RETAILER","id":"d36b12d7-2d43-11e2-8057-080027706aa2","product_id":"a0c0df02-2d20-11e2-8057-080027706aa2","outlet_id":"9ae97219-2d20-11e2-8057-080027706aa2","attributed_cost":"1","count":"-3","product":{"id":"a0c0df02-2d20-11e2-8057-080027706aa2","sku":"SODA","handle":"SODA_BLUE","source":"USER","active":"1","name":"Soda (Blue)","description":""},"outlet":{"id":"9ae97219-2d20-11e2-8057-080027706aa2","name":"Our retail store","time_zone":"Pacific\/Auckland"}}
        //

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody]VendHookPost value)
        {
            var inventory = value.payload.FromJson<Inventory>();

            System.Diagnostics.Debug.WriteLine(inventory.Product + " change " + inventory.Count);

            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
