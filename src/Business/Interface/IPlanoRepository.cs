using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IPlanoRepository: IBaseRepository<Plano>
    {
        Task<List<Plano>> ObterTodosPorId(int id);
    }
}
