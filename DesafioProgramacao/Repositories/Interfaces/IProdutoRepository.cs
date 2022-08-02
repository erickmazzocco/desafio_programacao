using DesafioProgramacao.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioProgramacao.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> GetAsync(int id);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task CreateAsync(Produto p);
        Task UpdateAsync(Produto p);
        Task DeleteAsync(int id);
    }
}
