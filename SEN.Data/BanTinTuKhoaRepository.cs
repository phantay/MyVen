using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data
{
    public class BanTinTuKhoaRepository : BaseRepository
    {
        public void Create(BanTinTuKhoa banTinTK)
        {
            Db.BanTinTuKhoas.Add(banTinTK);
        }

        public BanTinTuKhoa Get(int id)
        {
            return Db.BanTinTuKhoas.FirstOrDefault(bttk => bttk.Id==id);
        }

        public List<BanTinTuKhoa> GetList(int banTinId,int tuKhoaId)
        {
            return Db.BanTinTuKhoas.Include("BanTinTuKhoas").Where(bttk => bttk.BanTinId == banTinId && bttk.TuKhoaId == tuKhoaId).ToList();
        }

        public List<BanTinTuKhoa> GetList(int tuKhoaId, DateTime startDate)
        {
            return Db.BanTinTuKhoas.Where(bttk => bttk.TuKhoaId == tuKhoaId).ToList();
        }

    }
}