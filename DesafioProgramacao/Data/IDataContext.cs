using DesafioProgramacao.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioProgramacao.Data
{
    public interface IDataContext
    {
        DbSet<Produto> Produtos { get; set; }
        DbSet<Fornecedor> Fornecedores { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationTOken = default);
    }
}
