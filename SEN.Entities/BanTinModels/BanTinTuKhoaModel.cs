using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Entities.BanTinModels
{
    public class BanTinWithTuKhoa
    {
        public BanTin BanTin { get; set; }
        public List<TuKhoa> TuKhoas { get; set; }
    }
}