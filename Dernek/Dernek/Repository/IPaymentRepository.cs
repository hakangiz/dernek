using Dernek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dernek.Repository
{
    public interface IPaymentRepository:IRepository<payment>
    {
        IEnumerable<payment> GetUserPaymentsByUserId(string UserId);
        IEnumerable<payment> GetUserPaymentsByUserName(string UserName);
    }
}