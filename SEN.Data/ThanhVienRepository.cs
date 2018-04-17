﻿using SEN.Entities;
using System.Linq;

namespace SEN.Data
{
    public class ThanhVienRepository : BaseRepository
    {
        public ThanhVien Get(int id)
        {
            return Db.ThanhViens.FirstOrDefault(_ => _.ThanhVienId == id);
        }

        public void Create(ThanhVien thanhVien)
        {
            Db.ThanhViens.Add(thanhVien);
            Db.SaveChanges();
        }
    }
}