using SEN.Entities;
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
        public List<TuKhoa> TuKhoas { get; set; }
        public DateTime ThoiGian { get; set; }
    }
}