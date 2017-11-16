using Dernek.Models;
using Dernek.Repository.ClassRepository;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dernek.Repository
{
    public class WorkOfTables : IWorkOfTables
    {
        private readonly ApplicationDbContext _context;
        public WorkOfTables(ApplicationDbContext context)
        {
            _context = context;
            Activity = new ActivityRepository(_context);
            Users = _context.Users;
            Payment = new PaymentRepository(_context);
            UserDetail = new UserDetailRepository(_context);
            MonthlyUser = new MonthlyRepository(_context);
        }
        public IActivityRepository Activity { get; private set; }
        public  IDbSet<ApplicationUser> Users { get; set; }
        public IDbSet<IdentityUserRole> Roles { get; set; }
        public IPaymentRepository Payment { get; set; }
        public IUserDetail UserDetail { get; set; }
        public IMonthlyRepository MonthlyUser { get; set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}