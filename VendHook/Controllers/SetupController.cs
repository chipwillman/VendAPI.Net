namespace VendHook.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    public class SetupController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Labels()
        {
            return this.View();
        }
    }
}
