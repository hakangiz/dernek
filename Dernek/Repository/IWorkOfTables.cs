using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dernek.Repository
{
    public interface IWorkOfTables:IDisposable
    {
        IActivityRepository Activity { get; }
        int Complete();
    }
}
