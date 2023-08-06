using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TemplateRepository: Repository<Template>, ITemplateRepository
    {
        private readonly AppDbContext _db;
        public TemplateRepository(AppDbContext db) : base (db)
        {
            _db = db;
        }
        public void Update(Template obj) 
        {
            _db.Templates.Update(obj);
        }
    }
}
