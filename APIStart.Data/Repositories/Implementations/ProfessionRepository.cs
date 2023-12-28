
using APIStart.Core.Entities;
using APIStart.Core.Repositories;
using APIStart.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIStart.Data.Repositories.Implementations
{

    public class ProfessionRepository : GenericRepository<Profession>, IProfessionRepository
    {
        public ProfessionRepository(AppDbContext context) : base(context)
        {

        }
    }
}
