using SEN.Entities;
using System.Linq;

namespace SEN.Data
{
    public class ThanhVienRepository : BaseRepository
    {
        public ThanhVien Get(int id)
        {
            return Db.ThanhViens.FirstOrDefault(tv => tv.ThanhVienId == id);
        }

        public bool CheckEmailExist(string email)
        {
            return Db.ThanhViens.Any(acc => acc.Email == email);
        }

        public void Create(ThanhVien thanhVien)
        {
            Db.ThanhViens.Add(thanhVien);
            Db.SaveChanges();
        }

        public ThanhVien GetByEmail(string email)
        {
            return Db.ThanhViens.FirstOrDefault(tv => tv != null && tv.Email.ToLower() == email.ToLower());
        }
    }
}