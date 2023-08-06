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
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly AppDbContext _db;
        public ClientRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Client obj) 
        {
            _db.Clients.Update(obj);
        }
    }
}
