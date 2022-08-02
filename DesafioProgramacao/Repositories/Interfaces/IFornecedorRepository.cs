using DesafioProgramacao.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioProgramacao.Repositories.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<Fornecedor> GetAsync(int id);
        Task<IEnumerable<Fornecedor>> GetAllAsync();
        Task CreateAsync(Fornecedor f);
        Task UpdateAsync(Fornecedor f);
        Task DeleteAsync(int id);
    }
}
