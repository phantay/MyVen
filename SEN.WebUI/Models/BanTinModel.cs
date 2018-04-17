using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEN.WebUI.Models
{
    public class BanTinModel
    {
        public int BanTinId { get; set; }
        public string TenNguoiDang { get; set; }
        public string AvatarUrl { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public string TuKhoa { get; set; }
        public string HinhAnhUrl { get; set; }
    }
}