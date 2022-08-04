using DesafioProgramacao.CrossCutting.Pagination;
using DesafioProgramacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioProgramacao.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Insert(T obj);

        Task<T> Update(T obj);
        
        Task<bool> Delete(int id);

        Task<bool> SoftDelete(int id);

        Task<IEnumerable<T>> Get(PaginationFilter pagination);

        Task<T> Get(int id);
    }
}
