using SEN.Entities;
using SEN.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace SEN.WebUI.Controllers
{
    public class BanTinController : BaseController
    {
        private readonly BanTinService _banTinService;
        private readonly BinhLuanService _binhLuanService;

        public BanTinController()
        {
            _banTinService = new BanTinService();
            _binhLuanService = new BinhLuanService();
        }

        public ActionResult Index()
        {
            var getSession = Session["user_login"];
            return View();
        }

        public JsonResult ListBanTin(int thanhVienId)
        {
            List<Web.ViewModels.BanTinViewModel> banTins = _banTinService.GetList(thanhVienId);

            return Json(banTins, JsonRequestBehavior.AllowGet);
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
                //TODO: Cần lưu lại lỗi
                throw new Exception(ex.Message);
            }
        }

        public JsonResult GetListTuKhoaById(int tuKhoaId)
        {
            try
            {
                var listTuKhoa = _banTinService.GetTuKhoaChiTiet(tuKhoaId);

                return Json(listTuKhoa, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TODO: Cần lưu lại lỗi
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult DangTin(Entities.BanTin banTin, string TuKhoa)
        {
            try
            {
                var thanhVien = (ThanhVien)Session["user_login"];

                banTin.ThanhVienId = thanhVien.ThanhVienId;

                _banTinService.DangTin(banTin, TuKhoa);

                if (banTin != null && Request.Files != null && Request.Files.Count > 0)
                {
                    var fileIndex = 1;
                    for (var i = 0; i < Request.Files.Keys.Count; i++)
                    {
                        var fileContent = Request.Files[i];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            var inputStream = fileContent.InputStream;
                            var extension = Path.GetExtension(fileContent.FileName);
                            var fileName = $"{banTin.BanTinId}_{i}{extension}";
                            var path = Path.Combine(Server.MapPath("~/Images/BanTin"), fileName);
                            using (var fileStream = System.IO.File.Create(path))
                            {
                                inputStream.CopyTo(fileStream);
                                _banTinService.DangAnh(banTin, fileName);
                            }
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

        public JsonResult SuaTin(Entities.BanTin banTin)
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
                _banTinService.XoaTin(banTinId);

                return Json(true);
            }
            catch (Exception ex)
            {
                //TODO: Cần lưu lại lỗi
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetTuKhoaChiTiet(int tuKhoaId)
        {
            if (tuKhoaId <= 0)
            {
                return Json(new { success = false, error = "từ khóa không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var model = _banTinService.GetTuKhoaChiTiet(tuKhoaId);

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TODO: Cần lưu lại lỗi
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult DangBinhLuan(int banTinId, int thanhVienId, string binhLuan)
        {
            try
            {
                var bantin = _banTinService.Get(banTinId);
                if (bantin == null)
                    throw new Exception("Ban tin khong ton tai");

                var bl = new BinhLuan
                {
                    BanTinId = banTinId,
                    ThanhVienId = thanhVienId,
                    NoiDung = binhLuan,
                };

                _binhLuanService.DangBinhLuan(bl);

                return Json(new { success = true, binhLuanId = bl.BinhLuanId }, JsonRequestBehavior.AllowGet);
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
            int count = 5;

            if (banTinId <= 0)
            {
                return Json(new { success = false, error = "banTinId không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var binhLuans = _binhLuanService.GetTopBinhLuans(banTinId, count);

                return Json(binhLuans, JsonRequestBehavior.AllowGet);
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
                var binhLuan = _binhLuanService.Get(binhLuanId);

                return Json(binhLuan, JsonRequestBehavior.AllowGet);
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
                var binhLuans = _binhLuanService.GetMoreBinhLuan(banTinId, minBinhLuanId);

                return Json(binhLuans, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TODO: Cần lưu lại lỗi
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult GetTopTuKhoa()
        {            
            try
            {
                List<TuKhoa> tuKhoas = _banTinService.GetTopTuKhoa();

                return Json(tuKhoas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TODO: Cần lưu lại lỗi
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetTuKhoaByThanhVienId(int thanhVienId)
        {
            try
            {
                List<TuKhoa> tuKhoabythanhviens = _banTinService.GetTuKhoaByThanhVienId(thanhVienId);

                return Json(tuKhoabythanhviens, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TODO: Cần lưu lại lỗi
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpGet]
        //public JsonResult GetTuKhoaChiTietByThanhVien(int tuKhoaId)
        //{
        //    if (tuKhoaId <= 0)
        //    {
        //        return Json(new { success = false, error = "từ khóa không hợp lệ!" }, JsonRequestBehavior.AllowGet);
        //    }

        //    try
        //    {
        //        var model = _banTinService.GetTuKhoaByThanhVien(tuKhoaId);

        //        return Json(model, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO: Cần lưu lại lỗi
        //        return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}
