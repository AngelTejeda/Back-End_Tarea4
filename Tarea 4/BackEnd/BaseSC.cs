using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_4.DataAccess;

namespace Tarea_4.BackEnd
{
    public class BaseSC
    {
        protected NorthwindContext dbContext = new();
    }
}
