using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data
{
    public class ThanhVienModel
    {
        private VenEntities context = null;
        public ThanhVienModel()
        {
            context = new VenEntities();
        }

        public bool Login(string Email, string Password)
        {
            object[] sqlParams =
             {
                new SqlParameter("@Email",Email),
                new SqlParameter("@Password",Password),
            };

            var res = context.Sp_ThanhViens_Login(Email, Password);

            var items = new List<bool?>((IEnumerable<bool?>)res);

            return items != null && items.Any() && items.First() != null && items.First().Value;
        }
        public bool ModelState { get; set; }
        public bool IsValid { get; set; }
        public bool result { get; }
    }
}
