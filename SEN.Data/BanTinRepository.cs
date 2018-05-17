using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data
{
    public class BanTinRepository : BaseRepository
    {
        public void Create(BanTin banTin)
        {
            Db.BanTins.Add(banTin);
        }

        public BanTin Get(int id)
        {
            return Db.BanTins.FirstOrDefault(bt => bt.BanTinId == id);
        }

        //public List<BanTin> GetList(int thanhVienId)
        //{
        //    var result = from bt in Db.BanTins
        //                 join bttk in Db.BanTinTuKhoas on bt.BanTinId equals bttk.BanTinId
        //                 join tk in Db.TuKhoas on bttk.TuKhoaId equals tk.TuKhoaId
        //                 where bt.ThanhVienId == thanhVienId
        //                 select new { BanTin = bt, TuKhoa = tk };
        //    return result.ToList();
        //}

        public List<BanTin> GetList(int thanhVienId)
        {
            return Db.BanTins.Include("ThanhVien").Where(b => b.ThanhVienId == thanhVienId).OrderByDescending(b => b.ThoiGian).ToList();
        }

        public List<BanTin> GetList(int thanhVienId, DateTime startDate, DateTime endDate)
        {
            return Db.BanTins.Where(bt => bt.ThanhVienId == thanhVienId && startDate <= bt.ThoiGian && bt.ThoiGian <= endDate).ToList();
        }

        public void CreateTuKhoa(TuKhoa tuKhoa)
        {
            throw new NotImplementedException();
        }

        public void Create(BinhLuan binhLuan)
        {
            throw new NotImplementedException();
        }

        public void Remove(BanTin banTin)
        {
            Db.BanTins.Remove(banTin);
        }
    }
}
