using EasyCashIdentityProject.DataAccessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.DataAccessLayer.Repositories;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EasyCashIdentityProject.DataAccessLayer.EntityFramework
{
    public class EfCustomerAccountProcessDal : GenericRepository<CustomerAccountProcess>, ICustomerAccountProcessDal
    {
        public List<CustomerAccountProcess> MyLastProcess(int id)
        {
            using var context = new Context();
            var values = context.CustomerAccountProceses
                .Include(p => p.SenderCustomer)
                    .ThenInclude(c => c.AppUser)
                .Include(p => p.ReceiverCustomer)
                    .ThenInclude(c => c.AppUser)
                .Where(x => x.ReceiverID == id || x.SenderID == id)
                .ToList();
            return values;
        }
    }
}
