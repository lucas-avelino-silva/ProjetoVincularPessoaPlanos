using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IPessoaRepository: IBaseRepository<Pessoa>
    {
        Task<List<Pessoa>> ObterTodosPessoasPlanos();
        Task<Pessoa> ObterPessoaPlanosPorId(Guid id);
    }
}
