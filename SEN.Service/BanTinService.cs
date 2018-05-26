using SEN.Data;
using SEN.Entities;
using SEN.Entities.BanTinModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Service
{
    public class BanTinService
    {
        private BanTinRepository _banTinStore;
        private ThanhVienRepository _thanhVienStore;

        protected BanTinRepository BanTinStore
        {
            get { return _banTinStore ?? (_banTinStore = new BanTinRepository()); }
            set { _banTinStore = value; }
        }

        protected ThanhVienRepository ThanhVienStore
        {
            get { return _thanhVienStore ?? (_thanhVienStore = new ThanhVienRepository()); }
            set { _thanhVienStore = value; }
        }

        public List<BanTin> GetList(int thanhVienId)
        {
            return BanTinStore.GetList(thanhVienId);
        }

        //public List<BanTinTuKhoaModel> GetListTuKhoas(int tuKhoaId)
        //{
        //    var Db = new VenEntities();
        //    var result = from bt in Db.BanTins
        //                  join bttk in Db.BanTinTuKhoas on bt.BanTinId equals bttk.BanTinId
        //                  join tk in Db.TuKhoas on bttk.TuKhoaId equals tk.TuKhoaId
        //                  where tk.TuKhoa == tuKhoaId
        //                  select new { BanTin = bt, TuKhoa = tk};
        //    return result.ToList();
        //}

        public void DangTin(BanTin banTin)
        {
            if (banTin == null)
                throw new ArgumentNullException("banTin", "Bản tin rỗng");

            var thanhVien = ThanhVienStore.Get(banTin.ThanhVienId);
            if (thanhVien == null)
                throw new Exception("Thành viên không tồn tại");

            if (string.IsNullOrWhiteSpace(banTin.NoiDung))
                throw new Exception("Bản tin phải có nội dung");

            banTin.BanTinId = 1;
            banTin.ThoiGian = DateTime.Now;

            try
            {
                BanTinStore.Create(banTin);
                BanTinStore.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi đăng tin", ex);
            }
        }

        public BanTin SuaTin(BanTin banTin)
        {
            if (banTin == null)
                throw new ArgumentNullException("banTin", "Bản tin rỗng");

            var thanhVien = ThanhVienStore.Get(banTin.ThanhVienId);
            if (thanhVien == null)
                throw new Exception("Thành viên không tồn tại");

            if (string.IsNullOrWhiteSpace(banTin.NoiDung))
                throw new Exception("Bản tin phải có nội dung");

            var banTinDb = BanTinStore.Get(banTin.BanTinId);
            if (banTinDb == null)
                throw new Exception("Bản tin không tồn tại");

            if (banTin.ThanhVienId != banTinDb.ThanhVienId)
                throw new Exception("Bạn không có quyền sửa bản tin này");

            // TODO: Cần lưu lại lịch sử sửa bản tin
            try
            {
                BanTinStore.Create(banTin);
                BanTinStore.SaveChanges();

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
                BanTinStore.Remove(banTin);
                BanTinStore.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi đăng tin");
            }
        }
    }
}
