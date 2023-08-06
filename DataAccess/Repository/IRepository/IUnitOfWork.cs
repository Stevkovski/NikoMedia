using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IClientRepository Client { get; }
        IConfigurationRepository Configuration { get; }
        ITemplateRepository Template { get; }

        void Save();
    }
}
