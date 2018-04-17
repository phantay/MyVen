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
            return View();
        }

        public JsonResult ListBanTin(int thanhVienId) {
            //var listBanTin = new List<BanTinModel> {
              //   new BanTinModel
            //    {
            //        TenNguoiDang = "abc",
            //        AvatarUrl= "",
            //        ThoiGian= new DateTime(2017, 4, 21, 15, 24, 16),
            //        TuKhoa = "#CIT",
            //        NoiDung= "  Chủ tịch Hà Nội mong được sớm đối thoại với dân Đồng Tâm",
            //        HinhAnhUrl= "http://image.24h.com.vn/upload/2-2017/images/2017-04-20/1492690837-doi-thoai--1-.jpg"
            //    },
            //    new BanTinModel
            //    {
            //        TenNguoiDang = "Trong Tran",
            //        AvatarUrl= "",
            //        ThoiGian= new DateTime(2017, 4, 2, 15, 24, 16),
            //        TuKhoa = "#CIT",
            //        NoiDung= "Tàu chở hàng dài hơn 300 m mất tích ở Đại Tây Dương",
            //        HinhAnhUrl= "http://img.f29.vnecdn.net/2017/04/02/ship-2908-1491100211.jpg"
            //    },
            //    new BanTinModel
            //    {
            //        TenNguoiDang= "Tay",
            //        AvatarUrl= "",
            //        ThoiGian= new DateTime(2017, 4, 2, 10, 24, 16),
            //        TuKhoa= "#CIT",
            //        NoiDung= "Sân bay Phượng Hoàng ở Kon Tum thành nơi phơi sắn",
            //        HinhAnhUrl= "http://img.f31.vnecdn.net/2017/03/29/sanbay-5-1490778014_660x0.jpg"
            //    }
            //};

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
        public JsonResult GetDanhSachBinhLuan(int banTinId)
        {
            try
            {
                using (VenEntities data = new VenEntities())
                {
                    var pageSize = 5;
                    var pageIndex = 1;

                    var ds = data.BinhLuans.Where(_ => _.BanTinId == banTinId)
                        .OrderByDescending(_=>_.ThoiGian)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
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
                //
                //TODO: Cần lưu lại lỗi

                throw new Exception(ex.Message);
            }
        }
    }
}
