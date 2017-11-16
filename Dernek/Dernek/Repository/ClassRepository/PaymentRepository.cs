
using Dernek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dernek.Repository.ClassRepository
{
    public class PaymentRepository:Repository<payment>,IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        { }
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<payment> GetUserPaymentsByUserId(string UserId)
        {
            return ApplicationDbContext.payment.Where(x => x.ApplicationUser.Id == UserId).ToList();
        }

        public IEnumerable<payment> GetUserPaymentsByUserName(string UserName)
        {
            return ApplicationDbContext.payment.Where(x => x.ApplicationUser.UserName == UserName).ToList();
        }
    }
}