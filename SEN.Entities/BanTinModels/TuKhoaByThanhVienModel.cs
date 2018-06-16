using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Entities.BanTinModels
{
    public class TuKhoaByThanhVienModel
    {
        public int ThanhVienId { get; set; }
        public int TuKhoaId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
