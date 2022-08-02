using AutoMapper;
using DesafioProgramacao.Data;
using DesafioProgramacao.Entities;
using DesafioProgramacao.Repositories.Interfaces;
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

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Fornecedor>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Fornecedor> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Fornecedor f)
        {
            throw new System.NotImplementedException();
        }
    }
}
