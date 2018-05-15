using SEN.Data;
using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Service
{
    public class TuKhoaService
    {
        private TuKhoaRepository _tuKhoaStore;
        private BanTinRepository _banTinStore;
        private ThanhVienRepository _thanhVienStore;


        protected TuKhoaRepository TuKhoaStore
        {
            get { return _tuKhoaStore ?? (_tuKhoaStore = new TuKhoaRepository()); }
            set { _tuKhoaStore = value; }
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

        //public List<TuKhoa> GetList(int thanhVienId)
        //{
        //    return TuKhoaStore.GetList(thanhVienId);
        //}
        public void TaoTuKhoa(TuKhoa tuKhoa)
        {
            if (tuKhoa == null)
                throw new ArgumentNullException("tuKhoa", "Tu Khoa rỗng");

            var thanhVien = ThanhVienStore.Get(tuKhoa.ThanhVienId);
            if (thanhVien == null)
                throw new Exception("Thành viên không tồn tại");

            if (string.IsNullOrWhiteSpace(tuKhoa.NoiDung))
                throw new Exception("tu khoa phải có nội dung");

            tuKhoa.TuKhoaId = 1;

            try
            {
                TuKhoaStore.Create(tuKhoa);
                TuKhoaStore.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi đăng tin", ex);
            }
        }
        public TuKhoa SuaTuKhoa(TuKhoa tuKhoa)
        {
            if (tuKhoa == null)
                throw new ArgumentNullException("banTin", "Bản tin rỗng");

            var thanhVien = ThanhVienStore.Get(tuKhoa.ThanhVienId);
            if (thanhVien == null)
                throw new Exception("Thành viên không tồn tại");

            if (string.IsNullOrWhiteSpace(tuKhoa.NoiDung))
                throw new Exception("Bình luận phải có nội dung");

            var tuKhoaDb = TuKhoaStore.Get(tuKhoa.TuKhoaId);
            if (tuKhoaDb == null)
                throw new Exception("Từ khóa không tồn tại");

            if (tuKhoa.ThanhVienId != tuKhoaDb.ThanhVienId)
                throw new Exception("Bạn không có quyền sửa bản tin này");

            // TODO: Cần lưu lại lịch sử sửa bản tin
            try
            {
                TuKhoaStore.Create(tuKhoa);
                TuKhoaStore.SaveChanges();

                return tuKhoaDb;

            }
            catch (Exception ex)
            {
                throw new Exception("Chúng tôi đang gặp vấn đề khó về kỹ thuật khi đăng tin", ex);
            }
        }
    }
}
