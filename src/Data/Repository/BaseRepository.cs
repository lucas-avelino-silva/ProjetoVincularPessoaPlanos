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
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseModel
    {
        public readonly ContextDB _context;
        private readonly DbSet<TEntity> _conset;
        public BaseRepository(ContextDB con)
        {
            _context = con;
            _conset = _context.Set<TEntity>();
        }

        public async Task Adicionar(TEntity entity)
        {
            await _conset.AddAsync(entity);
            await Salvar();
        }

        public async Task Atualizar(TEntity entity)
        {
            _conset.Update(entity);
            await Salvar();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await _conset.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await _conset.Where(x => x.Deletado != true).ToListAsync();
        }

        public async Task Remover(TEntity entity)
        {
            entity.Deletado = true;
            _conset.Update(entity);
            await Salvar();
        }

        public Task<int> Salvar()
        {
            return _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
