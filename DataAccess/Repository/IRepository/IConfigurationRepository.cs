using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IConfigurationRepository : IRepository<Configuration>
    {
        void Update(Configuration obj);
        public Configuration GetConfigurationWithRelatedDataById(int configurationId);
    }
}
