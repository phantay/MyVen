using SEN.Data;
using SEN.Entities;
using SEN.Entities.BanTinModels;
using SEN.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEN.Service
{
    public class BanTinService
    {
        private BanTinRepository _banTinRepository;
        private TuKhoaRepository _tuKhoaRepository;
        private BanTinTuKhoaRepository _banTinTuKhoaRepository;
        private ThanhVienRepository _thanhVienRepository;
        private BanTinAnhRepository _banTinAnhRepository;

        protected BanTinRepository BanTinRepository
        {
            get { return _banTinRepository ?? (_banTinRepository = new BanTinRepository()); }
            set { _banTinRepository = value; }
        }

        protected TuKhoaRepository TuKhoaRepository
        {
            get { return _tuKhoaRepository ?? (_tuKhoaRepository = new TuKhoaRepository()); }
            set { _tuKhoaRepository = value; }
        }

        protected BanTinTuKhoaRepository BanTinTuKhoaRepository
        {
            get { return _banTinTuKhoaRepository ?? (_banTinTuKhoaRepository = new BanTinTuKhoaRepository()); }
            set { _banTinTuKhoaRepository = value; }
        }

        protected ThanhVienRepository ThanhVienRepository
        {
            get { return _thanhVienRepository ?? (_thanhVienRepository = new ThanhVienRepository()); }
            set { _thanhVienRepository = value; }
        }

        protected BanTinAnhRepository BanTinAnhRepository
        {
            get { return _banTinAnhRepository ?? (_banTinAnhRepository = new BanTinAnhRepository()); }
            set { _banTinAnhRepository = value; }
        }

        public List<BanTinViewModel> GetList(int thanhVienId)
        {
            var result = new List<BanTinViewModel>();
            var banTins = BanTinRepository.GetList(thanhVienId);

            foreach (var banTin in banTins)
            {
                var tuKhoas = TuKhoaRepository.GetByBanTinId(banTin.BanTinId);
                var banTinVM = new BanTinViewModel
                {
                    TuKhoas = tuKhoas.ToList(),
                    BanTinId = banTin.BanTinId,
                    NoiDung = banTin.NoiDung,
                    ThanhVienId = banTin.ThanhVienId,
                    ThoiGian = banTin.ThoiGian,
                    BanTinAnhs = new List<BanTinAnhModel>()
                };

                if(banTin.BanTinAnhs != null)
                {
                    foreach (var anh in banTin.BanTinAnhs)
                    {
                        banTinVM.BanTinAnhs.Add(new BanTinAnhModel
                        {
                            BanTinId = anh.BanTinId,
                            IdAnh = anh.IdAnh,
                            FileName = anh.FileName
                        });
                    }
                }

                result.Add(banTinVM);
            }

            return result;
        }

        public BanTin Get(int banTinId)
        {
            return BanTinRepository.Get(banTinId);
        }

        public TuKhoaChiTietModel GetTuKhoaChiTiet(int tuKhoaId)
        {
            var tuKhoa = TuKhoaRepository.Get(tuKhoaId);
            if (tuKhoa == null)
                throw new Exception("");

            var banTins = BanTinRepository.GetListByTuKhoa(tuKhoaId);

            return new TuKhoaChiTietModel { TuKhoa = tuKhoa, BanTins = banTins };
        }

        public void DangTin(BanTin banTin, string noiDungTuKhoa)
        {
            if (banTin == null)
                throw new ArgumentNullException("banTin", "Bản tin rỗng");

            if (string.IsNullOrWhiteSpace(banTin.NoiDung))
                throw new Exception("Bản tin phải có nội dung");

            try
            {
                var thanhVien = ThanhVienRepository.Get(banTin.ThanhVienId);
                if (thanhVien == null)
                    throw new Exception("Thành viên không tồn tại");

                TuKhoa tuKhoa = null;
                if (!string.IsNullOrWhiteSpace(noiDungTuKhoa))
                {
                    noiDungTuKhoa = noiDungTuKhoa.Trim();
                    var tk = TuKhoaRepository.GetTuKhoaByNoiDung(noiDungTuKhoa);
                    if (tk == null)
                    {
                        tuKhoa = new TuKhoa();
                        tuKhoa.NoiDung = noiDungTuKhoa;
                        TuKhoaRepository.Create(tuKhoa);
                        TuKhoaRepository.SaveChanges();
                    }
                    else
                    {
                        tuKhoa = tk;
                    }
                }

                banTin.ThoiGian = DateTime.Now;

                BanTinRepository.Create(banTin);
                BanTinRepository.SaveChanges();

                if (tuKhoa != null && tuKhoa.TuKhoaId > 0)
                {
                    var banTinTuKhoa = new BanTinTuKhoa
                    {
                        BanTinId = banTin.BanTinId,
                        TuKhoaId = tuKhoa.TuKhoaId
                    };
                    BanTinTuKhoaRepository.Create(banTinTuKhoa);
                }

                BanTinTuKhoaRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi đăng tin", ex);
            }
        }

        public void DangAnh(BanTin banTin, string fileName)
        {
            if (banTin == null || banTin.BanTinId <= 0 || string.IsNullOrWhiteSpace(fileName))
                throw new Exception("Bản tin hoạc hình ảnh không tồn tại");

            var hinhAnh = new BanTinAnh { BanTinId = banTin.BanTinId, FileName = fileName };

            BanTinAnhRepository.Create(hinhAnh);
            BanTinAnhRepository.SaveChanges();
        }

        public BanTin SuaTin(BanTin banTin)
        {
            if (banTin == null)
                throw new ArgumentNullException("banTin", "Bản tin rỗng");

            var thanhVien = ThanhVienRepository.Get(banTin.ThanhVienId);
            if (thanhVien == null)
                throw new Exception("Thành viên không tồn tại");

            if (string.IsNullOrWhiteSpace(banTin.NoiDung))
                throw new Exception("Bản tin phải có nội dung");

            var banTinDb = BanTinRepository.Get(banTin.BanTinId);
            if (banTinDb == null)
                throw new Exception("Bản tin không tồn tại");

            if (banTin.ThanhVienId != banTinDb.ThanhVienId)
                throw new Exception("Bạn không có quyền sửa bản tin này");

            // TODO: Cần lưu lại lịch sử sửa bản tin
            try
            {
                BanTinRepository.Create(banTin);
                BanTinRepository.SaveChanges();

                return banTinDb;
            }
            catch (Exception ex)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi đăng tin", ex);
            }
        }

        public void XoaTin(BanTin banTin)
        {
            try
            {
                BanTinRepository.Remove(banTin);
                BanTinRepository.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi xóa tin");
            }
        }

        public void XoaTin(int banTinId)
        {
            try
            {
                var bantin = BanTinRepository.Get(banTinId);
                if (bantin == null)
                    throw new Exception("Bản tin không tồn tại");

                BanTinRepository.Remove(bantin);

                BanTinRepository.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi xóa tin");
            }
        }

        public List<TuKhoa> GetTopTuKhoa()
        {
            return TuKhoaRepository.GetTopTuKhoa();
        }

        public List<TuKhoa> GetTuKhoaByThanhVienId(int thanhVienId)
        {
            return TuKhoaRepository.GetTuKhoaByThanhVienId(thanhVienId);
        }
    }
}
