using Dernek.Models;
using Dernek.Models.EnumProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dernek.Repository
{
    public interface IActivityRepository :IRepository<activity>
    {
        IEnumerable<activity> GetActivityByStatus(enums.activityStatus status);
        activity Get(int? id);

        IEnumerable<activity> GetAllReverseByDate();
    }
}
