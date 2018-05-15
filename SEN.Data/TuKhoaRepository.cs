using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data
{
    public class TuKhoaRepository : BaseRepository
    {
        public void Create(TuKhoa tuKhoa)
        {
            Db.TuKhoas.Add(tuKhoa);
        }

        public TuKhoa Get(int id)
        {
            return Db.TuKhoas.FirstOrDefault(tk => tk.TuKhoaId == id);
        }

        //public List<BanTinTuKhoa> GetList(int banTinId)
        //{
        //    return Db.TuKhoas.Include("TuKhoas").Where(tk => tk.BanTinId == banTinId).ToList();
        //}

        public List<TuKhoa> GetList(int thanhVienId, DateTime startDate, DateTime endDate)
        {
            return Db.TuKhoas.Where(tk => tk.ThanhVienId == thanhVienId).ToList();
        }

    }
}