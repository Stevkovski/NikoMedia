using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ConfigurationRepository: Repository<Configuration>, IConfigurationRepository
    {
        private readonly AppDbContext _db;
        public ConfigurationRepository(AppDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(Configuration obj) 
        {
            _db.Configurations.Update(obj);
        }

        public Configuration GetConfigurationWithRelatedDataById(int configurationId)
        {
            return _db.Configurations
                .Where(c => c.Id == configurationId)
                .Include(c => c.Client)
                .Include(c => c.Template)
                .FirstOrDefault();
        }

    }
}
