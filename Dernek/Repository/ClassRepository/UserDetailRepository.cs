using Dernek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dernek.Repository.ClassRepository
{
    public class UserDetailRepository:Repository<userDetail>,IUserDetail
    {
        public UserDetailRepository(ApplicationDbContext context) : base(context)
        { }
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}