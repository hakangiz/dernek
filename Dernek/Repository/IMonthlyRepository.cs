using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dernek.Models;

namespace Dernek.Repository
{
    public interface IMonthlyRepository
    {
        monthlyUserFollowUp Get(int? id);

        IEnumerable<monthlyUserFollowUp> GetAllReverseByDate();

        IEnumerable<monthlyUserFollowUp> GetUserMounthlyFollowByUserId(string userId);
    }
}
