using SEN.Data;
using SEN.Entities;

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

        public bool CheckEmailExist(string email)
        {
            return ThanhVienStore.CheckEmailExist(email);
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
