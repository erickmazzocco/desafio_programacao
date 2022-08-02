using DesafioProgramacao.Data;
using DesafioProgramacao.Entities;
using DesafioProgramacao.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioProgramacao.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDataContext _context;

        public ProdutoRepository(IDataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Produto p)
        {
            _context.Produtos.Add(p);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);

        }

        public async Task UpdateAsync(Produto p)
        {
            _context.Produtos.Update(p);
            await _context.SaveChangesAsync();
        }
    }
}
