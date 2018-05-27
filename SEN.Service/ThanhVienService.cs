using SEN.Data;
using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Service
{
    public class ThanhVienService
    {
        private ThanhVienRepository _thanhVienStore;

        protected ThanhVienRepository ThanhVienStore
        {
            get { return _thanhVienStore ?? (_thanhVienStore = new ThanhVienRepository()); }
            set { _thanhVienStore = value; }
        }

        public ThanhVien GetThanhVien(int thanhVienId)
        {
            return ThanhVienStore.Get(thanhVienId);
        }

        public ThanhVien GetByEmail(string email)
        {
            return ThanhVienStore.GetByEmail(email);
        }

        public void Create(ThanhVien thanhVien)
        {
            ThanhVienStore.Create(thanhVien);
        }
    }
}
