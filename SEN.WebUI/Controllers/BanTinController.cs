using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var banTins = _banTinService.GetList(1);
            var listBanTin = banTins.Select(_=> new BanTinModel {
                BanTinId = _.BanTinId,
                NoiDung = _.NoiDung,
                ThoiGian = _.ThoiGian,               
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
                banTin.ThanhVienId = 1;
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

        [HttpGet]
        public JsonResult GetDanhSachBinhLuan(int banTinId, int pageSize = 0)
        {
            try
            {
                using (VenEntities data = new VenEntities())
                {
                    int pageIndex = 1;
                    int _pageSize = (pageSize != 0) ? pageSize : 5;

                    var ds = data.BinhLuans.Where(_ => _.BanTinId == banTinId)
                        .OrderByDescending(_=>_.ThoiGian)
                        .Skip((pageIndex - 1) * _pageSize)
                        .Take(_pageSize)
                        .ToList()
                        .Select(_=> new BinhLuan {
                            BinhLuanId = _.BinhLuanId,
                            BanTinId = _.BanTinId,
                            NoiDung = _.NoiDung,
                            ThoiGian = _.ThoiGian
                        }).ToList();

                    return Json(ds, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //TODO: Cần lưu lại lỗi

                throw new Exception(ex.Message);
            }
        }
    }
}
