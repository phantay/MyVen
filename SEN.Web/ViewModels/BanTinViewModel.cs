using SEN.Entities;
using SEN.Entities.BanTinModels;
using System;
using System.Collections.Generic;

namespace SEN.Web.ViewModels
{
    public class BanTinViewModel
    {
        public BanTinViewModel()
        {
            TuKhoas = new List<TuKhoa>();
        }

        public int BanTinId { get; set; }
        public string NoiDung { get; set; }
        public int ThanhVienId { get; set; }
        public string ThanhVienFullName { get; set; }
        public List<TuKhoa> TuKhoas { get; set; }
        public DateTime ThoiGian { get; set; }
        public List<BanTinAnhModel> BanTinAnhs { get; set; }
    }
}