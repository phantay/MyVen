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

        public List<TuKhoa> GetTopTuKhoa()
        {
            var tuKhoaGroups = Db.BanTinTuKhoas.GroupBy(_ => _.TuKhoaId)
                .Select(_ => new { TuKhoaId = _.Key, Count = _.Count() })
                .OrderByDescending(_ => _.Count)
                .Take(10)
                .ToList();

            var tuKhoaIds = tuKhoaGroups.Select(_ => _.TuKhoaId);

            var tuKhoas = Db.TuKhoas.Where(_ => tuKhoaIds.Contains(_.TuKhoaId)).ToList();

            foreach (var t in tuKhoas)
            {
                t.CountView = tuKhoaGroups.Where(_ => _.TuKhoaId == t.TuKhoaId).Select(_ => _.Count).FirstOrDefault();
            }

            return tuKhoas.OrderByDescending(_ => _.CountView).ToList();
        }

        public List<TuKhoa> GetByBanTinId(int banTinId)
        {
            var tuKhoas = from tk in Db.TuKhoas
                          join bttk in Db.BanTinTuKhoas on tk.TuKhoaId equals bttk.TuKhoaId
                          join bt in Db.BanTins on bttk.BanTinId equals bt.BanTinId
                          where bt.BanTinId == banTinId
                          select tk;

            return tuKhoas.ToList();
        }
    }
}
