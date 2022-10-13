using Business.Interface;
using Business.Model;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PessoaPlanoRepository : BaseRepository<PessoaPlano>, IPessoaPlanoRepository
    {

        public PessoaPlanoRepository(ContextDB con) : base(con)
        {

        }

        public async Task<List<PessoaPlano>> ObterTodosComInclude()
        {
            return await _context.PessoaPlano.Include(x => x.Pessoa).Include(x => x.Plano).ToListAsync();
        }

        public async Task<List<PessoaPlano>> ObterTodosPorIdPessoa(Guid id)
        {
            return  _context.PessoaPlano.Where(p => p.PessoaId == id).ToList();
        }

        public Task CancelarPlano(PessoaPlano pessoaPlano)
        {
            _context.PessoaPlano.Remove(pessoaPlano);
            Salvar();
            return Task.CompletedTask;
        }
    }
}
