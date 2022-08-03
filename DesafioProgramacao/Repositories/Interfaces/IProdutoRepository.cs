using DesafioProgramacao.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioProgramacao.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> GetAsync(int id);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> CreateAsync(Produto p);
        Task<Produto> UpdateAsync(Produto p);
        Task DeleteAsync(int id);
    }
}
