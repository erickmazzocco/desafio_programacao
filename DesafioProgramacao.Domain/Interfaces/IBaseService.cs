using DesafioProgramacao.CrossCutting.Pagination;
using DesafioProgramacao.Domain.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioProgramacao.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;

        Task<bool> Delete(int id, bool soft = true);        

        Task<IEnumerable<TOutputModel>> Get<TOutputModel>(PaginationFilter pagination) where TOutputModel : class;

        Task<TOutputModel> GetById<TOutputModel>(int id) where TOutputModel : class;        

        Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;       
    }
}
