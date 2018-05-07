using System;
using System.Linq;
using System.Web.Mvc;
using SEN.WebUI.Models;
using SEN.Entities;
using System.IO;
using SEN.Service;

namespace SEN.WebUI.Controllers
{
    public class BanTinController : BaseController
    {
        private readonly BanTinService _banTinService;

        public BanTinController()
        {
            _banTinService = new BanTinService();
        }

        public ActionResult Index()
        {
            var getSession = Session["user_login"];
            return View();
        }

        public JsonResult ListBanTin(int thanhVienId) {
            var banTins = _banTinService.GetList(thanhVienId);
            var listBanTin = banTins.Select(b=> new BanTinModel {
                BanTinId = b.BanTinId,
                NoiDung = b.NoiDung,
                ThoiGian = b.ThoiGian,               
            }).ToList();
            return Json(listBanTin, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListBanTin(int thanhVienId)
        {
            try
            {
                var listBanTin = _banTinService.GetList(thanhVienId);

                return Json(listBanTin, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //
                //TODO: Cần lưu lại lỗi
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult DangTin(BanTin banTin)
        {
            try
            {
                var thanhVien = (ThanhVien)Session["user_login"];
                banTin.ThanhVienId = thanhVien.ThanhVienId;
                _banTinService.DangTin(banTin);

                if(banTin != null && Request.Files != null && Request.Files.Count > 0)
                {
                    var fileContent = Request.Files[0];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var inputStream = fileContent.InputStream;
                        var fileName = banTin.BanTinId + Path.GetExtension(fileContent.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images/BanTin"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            inputStream.CopyTo(fileStream);
                        }
                    }
                }
                return Json(banTin);
            }
            catch (Exception ex)
            {
                //
                //TODO: Cần lưu lại lỗi
                throw new Exception(ex.Message);
            }
        }

        public JsonResult SuaTin(BanTin banTin)
        {
            try
            {
                var banTinMoi = _banTinService.SuaTin(banTin);
                return Json(banTinMoi);
            }
            catch (Exception ex)
            {
                //
                //TODO: Cần lưu lại lỗi
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public JsonResult XoaTin(int banTinId)
        {
           try
            {
                using (VenEntities data = new VenEntities())
                {                    
                    var bantin = data.BanTins.Where(x => x.BanTinId == banTinId).FirstOrDefault();
                    if (bantin == null)
                        throw new Exception("Ban tin khong ton tai");
                    data.BanTins.Remove(bantin);
                    data.SaveChanges();
                    return Json(true);
                }
            }
            catch (Exception ex)
            {
                //
                //TODO: Cần lưu lại lỗi
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult DangBinhLuan(int banTinId, string binhLuan)
        {
           try
            {
                using (VenEntities data = new VenEntities())
                {                    
                    var bantin = data.BanTins.Where(x => x.BanTinId == banTinId).FirstOrDefault();
                    if (bantin == null)
                        throw new Exception("Ban tin khong ton tai");
                    var bl = new BinhLuan
                    {
                        BanTinId = banTinId,
                        NoiDung = binhLuan,
                        ThanhVienId = 1,
                        ThoiGian = DateTime.Now,                        
                    };
                    data.BinhLuans.Add(bl);
                    data.SaveChanges();

                    return Json(new { success = true, binhLuanId = bl.BinhLuanId }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //
                //TODO: Cần lưu lại lỗi
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetTopBinhLuanMoiNhat(int banTinId)
        {
            int pageSize = 5;

            if (banTinId <= 0)
            {
                return Json(new { success = false, error = "banTinId không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                using (VenEntities data = new VenEntities())
                {
                    var binhLuans = data.BinhLuans.Where(b => b.BanTinId == banTinId)
                        .OrderByDescending(b => b.ThoiGian)
                        .Take(pageSize)
                        .ToList()
                        .Select(b => new BinhLuan {
                            BinhLuanId = b.BinhLuanId,
                            BanTinId = b.BanTinId,
                            NoiDung = b.NoiDung,
                            ThoiGian = b.ThoiGian
                        }).ToList();

                    return Json(binhLuans, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //TODO: Cần lưu lại lỗi
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetBinhLuanById(int binhLuanId)
        {
            if (binhLuanId <= 0)
            {
                return Json(new { success = false, error = "binhLuanId không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                using (VenEntities data = new VenEntities())
                {
                    var binhLuan = data.BinhLuans.Where(b => b.BinhLuanId == binhLuanId)
                        .ToList()
                        .Select(b => new BinhLuan
                        {
                            BinhLuanId = b.BinhLuanId,
                            BanTinId = b.BanTinId,
                            NoiDung = b.NoiDung,
                            ThoiGian = b.ThoiGian
                        }).FirstOrDefault();

                    return Json(binhLuan, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //TODO: Cần lưu lại lỗi
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetMoreBinhLuan(int banTinId, int minBinhLuanId)
        {
            int pageSize = 5;

            if (banTinId <= 0)
            {
                return Json(new { success = false, error = "banTinId không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }

            if (minBinhLuanId <= 0)
            {
                return Json(new { success = false, error = "minBinhLuanId không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                using (VenEntities data = new VenEntities())
                {
                    var binhLuans = data.BinhLuans.Where(b => b.BanTinId == banTinId && b.BinhLuanId < minBinhLuanId)
                        .OrderByDescending(b => b.ThoiGian)
                        .Take(pageSize)
                        .ToList()
                        .Select(b => new BinhLuan
                        {
                            BinhLuanId = b.BinhLuanId,
                            BanTinId = b.BanTinId,
                            NoiDung = b.NoiDung,
                            ThoiGian = b.ThoiGian
                        }).ToList();

                    return Json(binhLuans, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //TODO: Cần lưu lại lỗi
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
