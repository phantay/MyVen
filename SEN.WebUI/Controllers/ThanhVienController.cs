using SEN.Entities;
using SEN.Service;
using System;
using System.Web.Mvc;

namespace SEN.WebUI.Controllers
{
    public class ThanhVienController : Controller
    {
        private readonly ThanhVienService _thanhVienService;

        public ThanhVienController()
        {
            _thanhVienService = new ThanhVienService();
        }

        public JsonResult GetThanhVien()
        {
            try
            {
                var thanhVien = (ThanhVien)Session["user_login"];

                if (thanhVien == null)
                {
                    return Json(new { StatusCode = 403, ThanhVien = "" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { StatusCode = 200, ThanhVien = new { thanhVien.ThanhVienId, thanhVien.FirstName, thanhVien.LastName } },
                                  JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { StatusCode = 500, ThanhVien = "",  ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
