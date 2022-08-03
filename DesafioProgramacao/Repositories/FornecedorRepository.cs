using AutoMapper;
using DesafioProgramacao.Data;
using DesafioProgramacao.Entities;
using DesafioProgramacao.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioProgramacao.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly IDataContext _context;        

        public FornecedorRepository(IDataContext context)
        {
            _context = context;            
        }

        public async Task CreateAsync(Fornecedor f)
        {
            _context.Fornecedores.Add(f);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var f = await _context.Fornecedores.FindAsync(id);
            _context.Fornecedores.Remove(f);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Fornecedor>> GetAllAsync()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        public async Task<Fornecedor> GetAsync(int id)
        {
            return await _context.Fornecedores.FindAsync(id);
        }

        public async Task UpdateAsync(Fornecedor f)
        {
            _context.Fornecedores.Update(f);
            await _context.SaveChangesAsync();
        }
    }
}
