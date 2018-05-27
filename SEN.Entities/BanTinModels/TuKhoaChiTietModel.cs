using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Entities.BanTinModels
{
    public class TuKhoaChiTietModel
    {
        public List<BanTin> BanTins { get; set; }
        public TuKhoa TuKhoa { get; set; }
    }
}
