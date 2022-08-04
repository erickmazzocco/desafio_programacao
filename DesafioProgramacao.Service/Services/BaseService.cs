using AutoMapper;
using DesafioProgramacao.CrossCutting.Pagination;
using DesafioProgramacao.Domain.Entities;
using DesafioProgramacao.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioProgramacao.Service.Services
{
    public class BaseService<TEntity> : 
        IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(
            IBaseRepository<TEntity> baseRepository,
            IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            Validate(entity, Activator.CreateInstance<TValidator>());
            await _baseRepository.Insert(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public async Task<bool> Delete(int id, bool soft = true) => 
            soft ? await _baseRepository.SoftDelete(id) : await _baseRepository.Delete(id);

        public async Task<IEnumerable<TOutputModel>> Get<TOutputModel>(PaginationFilter pagination) where TOutputModel : class
        {
            var entities = await _baseRepository.Get(pagination);

            var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s));

            return outputModels;
        }

        public async Task<TOutputModel> GetById<TOutputModel>(int id) where TOutputModel : class
        { 
            var entity = await _baseRepository.Get(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }   

        public async Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            Validate(entity, Activator.CreateInstance<TValidator>());
            await _baseRepository.Update(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        private static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            validator.ValidateAndThrow(obj);
        }

    }
}
