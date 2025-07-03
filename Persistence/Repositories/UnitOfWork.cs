using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) { 
            _context = context;
        }
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //Do sth here such as 
            //ConvertDomainEventsToOutboxMessages();
            //UpdateAuditableEntities();


            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
