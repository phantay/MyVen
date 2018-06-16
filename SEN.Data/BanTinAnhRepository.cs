using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data
{
    public class BanTinAnhRepository :BaseRepository
    {
        public void Create(BanTinAnh banTinAnh)
        {
            Db.BanTinAnhs.Add(banTinAnh);
        }

        public BanTinAnh Get(int id)
        {
            return Db.BanTinAnhs.FirstOrDefault(bta => bta.IdAnh == id);
        }

        public BanTinAnh GetAnhByBanTin(int banTinId)
        {
            return Db.BanTinAnhs.FirstOrDefault(bta => bta.BanTinId == banTinId);
        }
    }
}
