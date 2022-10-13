using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IBaseRepository<TEntity>: IDisposable where TEntity : BaseModel
    {
        Task<List<TEntity>> ObterTodos();

        Task Adicionar(TEntity entity);

        Task Atualizar(TEntity entity);
        Task Remover(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<int> Salvar();
    }
}
