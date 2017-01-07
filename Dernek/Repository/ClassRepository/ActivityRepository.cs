using Dernek.Models;
using Dernek.Models.EnumProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dernek.Repository.ClassRepository
{
    public class ActivityRepository : Repository<activity>,IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext context):base(context)
        { }

        public IEnumerable<activity> GetActivityByStatus(enums.activityStatus status)
        {
            return ApplicationDbContext.activity
                .Where(c => c.status == status)
                .ToList();
        }
        public IEnumerable<activity> GetAllReverseByDate()
        {
            return ApplicationDbContext.activity.OrderBy(m => m.startDate).ToList();
        }
     
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}