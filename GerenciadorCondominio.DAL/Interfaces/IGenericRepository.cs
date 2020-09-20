using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        Task<IEnumerable<TEntity>> FindAll();

        Task<TEntity> FindById(int id);
        Task<TEntity> FindById(string id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task Remove(int id);
        Task Remove(string id);

    }
}
