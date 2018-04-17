using SEN.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEN.WebUI.Controllers
{
    public class ThanhVienController : Controller
    {
        private readonly ThanhVienService _thanhVienService;

        public ThanhVienController()
        {
            _thanhVienService= new ThanhVienService();
        }

        public JsonResult GetThanhVien()
        {
            try
            {
                var thanhVienId = 1; // Session
                var thanhVien = _thanhVienService.GetThanhVien(thanhVienId);

                return Json(thanhVien, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //
                //TODO: Cần lưu lại lỗi

                throw new Exception(ex.Message);
            }
        }


    }
}
