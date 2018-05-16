using SEN.Data;
using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Service
{
    public class BanTinTuKhoaService
    {
        private BanTinTuKhoaRepository _banTinTuKhoaStore;
        private BanTinRepository _banTinStore;
        private TuKhoaRepository _tuKhoaStore;

        protected BanTinTuKhoaRepository BanTinTuKhoaStore
        {
            get { return _banTinTuKhoaStore ?? (_banTinTuKhoaStore = new BanTinTuKhoaRepository()); }
            set { _banTinTuKhoaStore = value; }
        }

        protected BanTinRepository BanTinStore
        {
            get { return _banTinStore ?? (_banTinStore = new BanTinRepository()); }
            set { _banTinStore = value; }
        }

        protected TuKhoaRepository TuKhoaStore
        {
            get { return _tuKhoaStore ?? (_tuKhoaStore = new TuKhoaRepository()); }
            set { _tuKhoaStore = value; }
        }

        public List<BanTinTuKhoa> GetList(int banTinId, int tuKhoaId)
        {
            return BanTinTuKhoaStore.GetList(banTinId, tuKhoaId);
        }

        public void DangTuKhoa(BanTinTuKhoa banTinTK,TuKhoa tuKhoa)
        {
            if (banTinTK == null)
                throw new ArgumentNullException("tuKhoa", "Tu Khoa rỗng");

            var banTin = BanTinStore.Get(banTinTK.BanTinId);
            if (banTin == null)
                throw new Exception("Ban tin không tồn tại");

            var bantinTuKhoa = TuKhoaStore.Get(banTinTK.TuKhoaId);
            if (tuKhoa == null)
                throw new Exception("Tu khoa khong ton tai");
            if (string.IsNullOrWhiteSpace(tuKhoa.NoiDung))
                throw new Exception("tu khoa phải có nội dung");

            banTinTK.TuKhoaId = 1;

            try
            {
                BanTinTuKhoaStore.Create(banTinTK);
                BanTinTuKhoaStore.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi đăng tin", ex);
            }
        }
        public BanTinTuKhoa SuaTuKhoa(BanTinTuKhoa banTinTK)
        {
            if (banTinTK == null)
                throw new ArgumentNullException("banTin", "Bản tin rỗng");

            var banTin = BanTinStore.Get(banTinTK.BanTinId);
            if (banTin == null)
                throw new Exception("Ban tin không tồn tại");

            var tuKhoaDb = TuKhoaStore.Get(banTinTK.TuKhoaId);
            if (banTinTK == null)
                throw new Exception("Tu khoa khong ton tai");

            //if (banTinTK.ThanhVienId != tuKhoaDb.ThanhVienId)
            //    throw new Exception("Bạn không có quyền sửa bản tin này");

            // TODO: Cần lưu lại lịch sử sửa bản tin
            try
            {
                BanTinTuKhoaStore.Create(banTinTK);
                BanTinTuKhoaStore.SaveChanges();
                return banTinTK;
            }
            catch (Exception ex)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi đăng tin", ex);
            }
        }
    }
}
