﻿using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<BanTin> GetList(int thanhVienId)
        {
            return Db.BanTins.Include("ThanhVien").Where(b => b.ThanhVienId == thanhVienId).OrderByDescending(b => b.ThoiGian).ToList();
        }

        public List<BanTin> GetListByTuKhoa(int tuKhoaId)
        {
            return (from bt in Db.BanTins
                    join bttk in Db.BanTinTuKhoas on bt.BanTinId equals bttk.BanTinId
                    where bttk.TuKhoaId == tuKhoaId
                    select bt).ToList();
        }

        //public List<BanTin> GetListTuKhoaByThanhVien(int tuKhoaId)
        //{
        //    return (from bt in Db.BanTins
        //            join bttk in Db.BanTinTuKhoas on bt.BanTinId equals bttk.BanTinId
        //            join tv in Db.ThanhViens on bt.ThanhVienId equals tv.ThanhVienId
        //            where bttk.TuKhoaId == tuKhoaId
        //            select bt).ToList();
        //}

        public List<BanTin> GetList(int thanhVienId, DateTime startDate, DateTime endDate)
        {
            return Db.BanTins.Where(bt => bt.ThanhVienId == thanhVienId && startDate <= bt.ThoiGian && bt.ThoiGian <= endDate).ToList();
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
