using Application.Files.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Domain.BaseEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebFramework.Api
{
    public class CrudController<TDto, TSelectDto, TEntity, TKey> : BaseController
        where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, TKey>, new()
        where TEntity : class, IEntity<TKey>, new()
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        public CrudController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        /// <summary>
        /// دریافت تمامی اطلاعات
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult<List<TSelectDto>>> Get(CancellationToken cancellationToken)
        {
            var Repository = UnitOfWork.GetRepository<TEntity>();
            var list = await Repository.GetAsNoTrackingQuery().ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Ok(list);
        }
        /// <summary>
        /// دریافت اطلاعات با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<ApiResult<TSelectDto>> Get(TKey id, CancellationToken cancellationToken)
        {
            var Repository = UnitOfWork.GetRepository<TEntity>();
            var dto = await Repository.GetAsNoTrackingQuery().ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

            if (dto == null)
                return NotFound();

            return dto;
        }
        /// <summary>
        /// دریافت اطلاعات با بجینیشن
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public virtual async Task<ApiResult<GlobalGridResult<TSelectDto>>> GetByPage([FromQuery] GlobalGrid input)
        {
            var Repository = UnitOfWork.GetRepository<TEntity>();
            var skip = (input.PageNumber!.Value - 1) * input.Count!.Value;

            var data = await Repository.GetAsNoTrackingQuery().ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .Skip(skip)
                .Take(input.Count.Value)
                .ToListAsync();

            var totalCount = await Repository.GetQuery().CountAsync();
            return Ok(new GlobalGridResult<TSelectDto>
            {
                TotalCount = totalCount,
                Data = data
            });
        }
        /// <summary>
        /// ایجاد یک انتیتی جدید
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ApiResult<TSelectDto>> Create(TDto dto, CancellationToken cancellationToken)
        {
            var Repository = UnitOfWork.GetRepository<TEntity>();
            var model = dto.ToEntity(Mapper);

            await Repository.AddAsync(model, cancellationToken);

            var resultDto = await Repository.GetAsNoTrackingQuery().ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }

        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<ApiResult<TSelectDto>> Update(TKey id, TDto dto, CancellationToken cancellationToken)
        {
            var Repository = UnitOfWork.GetRepository<TEntity>();
            var model = await Repository.GetByIdAsync(cancellationToken, id);

            model = dto.ToEntity(Mapper, model);

            await Repository.UpdateAsync(model, cancellationToken);

            var resultDto = await Repository.GetAsNoTrackingQuery().ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }

        /// <summary>
        /// حذف
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<ApiResult> Delete(TKey id, CancellationToken cancellationToken)
        {
            var Repository = UnitOfWork.GetRepository<TEntity>();
            var model = await Repository.GetByIdAsync(cancellationToken, id);

            await Repository.SoftDeleteAsync(model, cancellationToken);

            return Ok();
        }
    }

    
}
