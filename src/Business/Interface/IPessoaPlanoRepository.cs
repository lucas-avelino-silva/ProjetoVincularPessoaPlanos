using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IPessoaPlanoRepository : IBaseRepository<PessoaPlano>
    {
        Task<List<PessoaPlano>> ObterTodosPorIdPessoa(Guid id);
        Task<List<PessoaPlano>> ObterTodosComInclude();

        Task CancelarPlano(PessoaPlano pessoaPlano);
    }
}
