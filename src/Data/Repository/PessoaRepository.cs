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
    public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
    {

        public PessoaRepository(ContextDB con) : base(con)
        {

        }

        public async Task<List<Pessoa>> ObterTodosPessoasPlanos()
        {
            return await _context.Pessoa.Include(x => x.Planos).Where(x => x.Deletado != true).ToListAsync();
        }

        public async Task<Pessoa> ObterPessoaPlanosPorId(Guid id)
        {
            return await _context.Pessoa.Include(x => x.Planos).Where(x => x.Deletado != true).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
