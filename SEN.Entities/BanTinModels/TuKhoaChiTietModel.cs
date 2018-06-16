using System;
using System.Collections.Generic;

namespace SEN.Entities.BanTinModels
{
    public class TuKhoaChiTietModel
    {
        public TuKhoaChiTietModel()
        {
            TuKhoas = new List<BanTinTuKhoa>();
            BanTinAnhs = new List<BanTinAnh>();
        }

        public TuKhoa TuKhoa { get; set; }
        public int BanTinId { get; set; }
        public string NoiDung { get; set; }
        public int ThanhVienId { get; set; }
        public string ThanhVienFullName { get; set; }
        public ICollection<BanTinTuKhoa> TuKhoas { get; set; }
        public ICollection<BanTinAnh> BanTinAnhs { get; set; }
        public DateTime ThoiGian { get; set; }
    }
}
