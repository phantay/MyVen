﻿using SEN.Entities;
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
            return Db.BinhLuans.FirstOrDefault(bl => bl.BinhLuanId == id);
        }
        public List<BinhLuan> GetList(int banTinId)
        {
            return Db.BinhLuans.Include("BinhLuan").Where(bl => bl.BanTinId == banTinId).OrderByDescending(bl => bl.ThoiGian).ToList();
        }

        public List<BinhLuan> GetList(int thanhVienId, DateTime startDate, DateTime endDate)
        {
            return Db.BinhLuans.Where(bl => bl.ThanhVienId == thanhVienId && startDate <= bl.ThoiGian && bl.ThoiGian <= endDate).ToList();
        }

    }
}
