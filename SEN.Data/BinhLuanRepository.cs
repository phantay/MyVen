using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data
{
    public class BinhLuanRepository : BaseRepository
    {
        public void Create(BinhLuan binhLuan)
        {
            Db.BinhLuans.Add(binhLuan);
        }
        public BinhLuan Get(int id)
        {
            return Db.BinhLuans.FirstOrDefault(_ => _.BinhLuanId == id);
        }
        public List<BinhLuan> GetList(int banTinId)
        {
            return Db.BinhLuans.Include("BinhLuan").Where(_ => _.BanTinId == banTinId).OrderByDescending(_ => _.ThoiGian).ToList();
        }

        public List<BinhLuan> GetList(int thanhVienId, DateTime startDate, DateTime endDate)
        {
            return Db.BinhLuans.Where(_ => _.ThanhVienId == thanhVienId && startDate <= _.ThoiGian && _.ThoiGian <= endDate).ToList();
        }

    }
}
