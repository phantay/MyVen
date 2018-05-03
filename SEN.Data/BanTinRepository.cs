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
            return Db.BanTins.FirstOrDefault(_ => _.BanTinId == id);
        }

        public List<BanTin> GetList(int thanhVienId)
        {
            return Db.BanTins.Include("ThanhVien").Where(_ => _.ThanhVienId == thanhVienId).OrderByDescending(_ => _.ThoiGian).ToList();
        }

        public List<BanTin> GetList(int thanhVienId, DateTime startDate, DateTime endDate)
        {
            return Db.BanTins.Where(_ => _.ThanhVienId == thanhVienId && startDate <= _.ThoiGian && _.ThoiGian <= endDate).ToList();
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
