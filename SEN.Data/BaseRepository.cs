using SEN.Entities;

namespace SEN.Data
{
    public class BaseRepository
    {
        private VenEntities _db;

        protected VenEntities Db
        {
            get
            {
                if (_db != null)
                    return _db;

                _db = new VenEntities();
                _db.Configuration.LazyLoadingEnabled = false;

                return _db;
            }
        }

        public void SaveChanges()
        {
            Db.SaveChanges();
        }
    }
}