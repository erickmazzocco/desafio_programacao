using DesafioProgramacao.CrossCutting.Pagination;
using DesafioProgramacao.Domain.Entities;
using DesafioProgramacao.Domain.Interfaces;
using DesafioProgramacao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioProgramacao.Infra.Data.Repository
{
    public class BaseRepository<TEntity> :
        IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            _context.Set<TEntity>()
                .Remove(await Get(id));

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TEntity>> Get(PaginationFilter pagination)
        {
            var obj = _context.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrEmpty(pagination.Description))
                obj = obj.Where(o => o.Description.Contains(pagination.Description));

            return await obj
                .Skip(pagination.CalcSkip())
                .Take(pagination.PageSize)
                .ToListAsync();
        }
            

        public async Task<TEntity> Get(int id) =>
            await _context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> Insert(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var obj = await Get(id);

            if (obj == null)
                return false;

            obj.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TEntity> Update(TEntity obj)
        {            
            _context.Set<TEntity>().Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
