using SEN.Data.Interfaces;
using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _db;
        private bool _disposed;
        public IRepository<BanTin> GetRepository<BanTin>() where BanTin : class
        {
            return new Repository<BanTin>(this._db);
        }
        public UnitOfWork(IDbContextFactory dbContextFactory)
        {
            _db = dbContextFactory.GetDbContext();
        }
        public void Save()
        {
            if (_db.GetValidationErrors().Any())
            {
                throw (new Exception(_db.GetValidationErrors().ToList()[0].ValidationErrors.ToList()[0].ErrorMessage));
            }
            _db.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            _disposed = true;

        }

        IRepository<BanTin> IUnitOfWork.GetRepository<BanTin>()
        {
            throw new NotImplementedException();
        }
    }
}
