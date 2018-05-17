using SEN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<BanTin> GetRepository<BanTin>() where BanTin : class;
        void Save();
    }
}
