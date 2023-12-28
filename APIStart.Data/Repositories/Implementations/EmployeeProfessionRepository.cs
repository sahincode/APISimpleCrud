using APIStart.Core.Entities;
using APIStart.Core.Repositories;
using APIStart.Data.DAL;
using APIStart.Data.Repositories.Implementations;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIStart.Data.Repositories.Implementations
{
    public class EmployeeProfessionRepository : GenericRepository<ProfessionEmployee>, IEmployeeProfessionRepository
    {
        public EmployeeProfessionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
