using SEN.Data;
using SEN.Entities;
using SEN.Service;
using SEN.WebUI.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace SEN.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        ThanhVienService thanhVienService;

        public HomeController()
        {
            thanhVienService = new ThanhVienService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult DangNhap(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Thiếu tên đăng nhập hoặc mật khẩu chưa đúng");
                return View(model);
            }

            // Get ThanhVien by email
            var thanhVien = thanhVienService.GetByEmail(model.Email);

            if (thanhVien != null && thanhVien.Password == model.Password)
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
                Session["user_login"] = thanhVien;

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu chưa đúng");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {
                thanhVienService.Create(thanhVien);

                return View(thanhVien);
            }

            return RedirectToAction("DangKy");
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap", "Home");
        }
    }
}