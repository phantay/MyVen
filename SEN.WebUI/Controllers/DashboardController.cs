using SEN.WebUI.Code;
using System.Web.Mvc;

namespace SEN.WebUI.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Admin/Dashboard/

        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(SessionHelper.GetEmailSession()))
                return RedirectToAction("Index", "Login", null);
            return View();
        }

    }
}
