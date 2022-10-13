using Business.Interface;
using Business.Model;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PlanoRepository : BaseRepository<Plano>, IPlanoRepository
    {

        public PlanoRepository(ContextDB con) : base(con)
        {
        }

        public Task<List<Plano>> ObterTodosPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
