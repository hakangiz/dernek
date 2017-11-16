using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Dernek.Models;

namespace Dernek.Repository.ClassRepository
{
    public class MonthlyRepository : Repository<monthlyUserFollowUp>, IMonthlyRepository
    {
        public MonthlyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<monthlyUserFollowUp> GetAllReverseByDate()
        {
            return ApplicationDbContext.monthlyUserFollowUps.OrderBy(m => m.followDate).ToList();
        }

        monthlyUserFollowUp IMonthlyRepository.Get(int? id)
        {
           return ApplicationDbContext.monthlyUserFollowUps.Where(m => m.id == id).FirstOrDefault();
        }

        public IEnumerable<monthlyUserFollowUp> GetUserMounthlyFollowByUserId(string userId)
        {
            return ApplicationDbContext.monthlyUserFollowUps.Where(x => x.ApplicationUser.Id == userId).ToList();
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}