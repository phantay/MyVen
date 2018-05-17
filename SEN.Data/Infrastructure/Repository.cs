using SEN.Data.Interfaces;
using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data.Infrastructure
{
    public class Repository<BanTin> : IRepository<BanTin> where BanTin : class
    {
        protected readonly DbContext _db;
        protected readonly DbSet<BanTin> _dbSet;

        public Repository(DbContext dbContext)
        {
            _db = dbContext;
            _dbSet = _db.Set<BanTin>();
        }

        public virtual int Count
        {
            get { return _dbSet.Count(); }
        }

        public virtual IQueryable<BanTin> All()
        {
            return _dbSet.AsQueryable();
        }

        public virtual BanTin GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<BanTin> Get(Expression<Func<BanTin, bool>> filter = null, Func<IQueryable<BanTin>, IOrderedQueryable<BanTin>> orderBy = null, string includeProperties = "")
        {
            IQueryable<BanTin> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }
            else
            {
                return query.AsQueryable();
            }
        }

        public virtual IQueryable<BanTin> Filter(Expression<Func<BanTin, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public virtual IQueryable<BanTin> Filter(Expression<Func<BanTin, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var resetSet = filter != null ? _dbSet.Where(filter).AsQueryable() : _dbSet.AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<BanTin, bool>> predicate)
        {
            return _dbSet.Count(predicate) > 0;
        }

        public virtual BanTin Find(params object[] keys)
        {
            return _dbSet.Find(keys);
        }

        public virtual BanTin Find(Expression<Func<BanTin, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public virtual BanTin Create(BanTin entity)
        {
            var newEntry = _dbSet.Add(entity);
            return newEntry;
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(BanTin entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<BanTin, bool>> predicate)
        {
            var entitiesToDelete = GetEntitiesToDelete(predicate);
            foreach (var entity in entitiesToDelete)
            {
                if (_db.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
        }

        private IQueryable<BanTin> GetEntitiesToDelete(Expression<Func<BanTin, bool>> predicate)
        {
            return Filter(predicate);
        }

        public virtual void Update(BanTin entity)
        {
            var entry = _db.Entry(entity);
            _dbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public virtual IEnumerable<BanTin> GetWithRawSql(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).ToList();
        }

        public IQueryable<BanTin> GetAll()
        {
            throw new NotImplementedException();
        }


    }
}
