using SEN.Data;
using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Service
{
    public class BinhLuanService
    {
        private BinhLuanRepository _binhLuanStore;
        private BanTinRepository _banTinStore;
        private ThanhVienRepository _thanhVienStore;


        protected BinhLuanRepository BinhLuanStore
        {
            get { return _binhLuanStore ?? (_binhLuanStore = new BinhLuanRepository()); }
            set { _binhLuanStore = value; }
        }

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

        public List<BinhLuan> GetList(int thanhVienId)
        {
            return BinhLuanStore.GetList(thanhVienId);
        }
        public void DangBinhLuan(BinhLuan binhLuan)
        {
            if (binhLuan == null)
                throw new ArgumentNullException("binhLuan", "Bình luận rỗng");

            var thanhVien = ThanhVienStore.Get(binhLuan.ThanhVienId);
            if (thanhVien == null)
                throw new Exception("Thành viên không tồn tại");

            if (string.IsNullOrWhiteSpace(binhLuan.NoiDung))
                throw new Exception("Bình luận phải có nội dung");

            binhLuan.ThoiGian = DateTime.Now;

            try
            {
                BinhLuanStore.Create(binhLuan);
                BinhLuanStore.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi đăng tin", ex);
            }
        }
        public BinhLuan SuaBinhLuan(BinhLuan binhLuan)
        {
            if (binhLuan == null)
                throw new ArgumentNullException("banTin", "Bản tin rỗng");

            var thanhVien = ThanhVienStore.Get(binhLuan.ThanhVienId);
            if (thanhVien == null)
                throw new Exception("Thành viên không tồn tại");

            if (string.IsNullOrWhiteSpace(binhLuan.NoiDung))
                throw new Exception("Bình luận phải có nội dung");

            var binhLuanDb = BinhLuanStore.Get(binhLuan.BinhLuanId);
            if (binhLuanDb == null)
                throw new Exception("Bình luận không tồn tại");

            if (binhLuan.ThanhVienId != binhLuanDb.ThanhVienId)
                throw new Exception("Bạn không có quyền sửa bản tin này");

            // TODO: Cần lưu lại lịch sử sửa bản tin
            try
            {
                BanTinStore.Create(binhLuan);
                BanTinStore.SaveChanges();

                return binhLuanDb;

            }
            catch (Exception ex)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi đăng tin", ex);
            }
        }

        public List<BinhLuan> GetTopBinhLuans(int banTinId, int count)
        {
            return BinhLuanStore.GetList(banTinId, count);
        }

        public BinhLuan Get(int binhLuanId)
        {
            return BinhLuanStore.Get(binhLuanId);
        }

        public List<BinhLuan> GetMoreBinhLuan(int banTinId, int minBinhLuanId)
        {
            return BinhLuanStore.GetMoreBinhLuan(banTinId, minBinhLuanId);
        }
    }
}
