using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data.Interfaces
{
    public interface IDbContextFactory
    {
        DbContext GetDbContext();
    }
    public class DbContextFactory<BanTin> : IDbContextFactory where BanTin : DbContext, new()
    {
        private readonly DbContext _context;
        public DbContextFactory()
        {
            _context = new BanTin();
        }
        public DbContext GetDbContext()
        {
            return _context;
        }
    }

}
