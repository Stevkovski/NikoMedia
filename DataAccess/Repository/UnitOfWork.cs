using DataAccess.Data;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;

        public IClientRepository Client { get; private set; }
        public IConfigurationRepository Configuration { get; private set; }
        public ITemplateRepository Template { get; private set; }

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Client = new ClientRepository(_db);
            Configuration = new ConfigurationRepository(_db);
            Template = new TemplateRepository(_db);
        }


        public void Save() 
        {
            _db.SaveChanges();
        }
    }
}
