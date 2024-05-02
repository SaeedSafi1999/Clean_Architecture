using Application.Cqrs.Queris;
using Core.Application.Common;
using Core.Application.Mediator.Categories.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Mediator.Categories.Query
{
    public class GetllCategoryQuery : IQuery<ServiceRespnse<List<CategoryDTO>>>
    {
        public int PageNumber { get; set; }
        public int Count { get; set; }

        public class GetllCategoryQueryHandler : IQueryHandler<GetllCategoryQuery, ServiceRespnse<List<CategoryDTO>>>
        {
            private readonly IGenericRepository<Category> _categoryEepository;

            public GetllCategoryQueryHandler(IGenericRepository<Category> categoryEepository)
            {
                _categoryEepository = categoryEepository;
            }
            public async Task<ServiceRespnse<List<CategoryDTO>>> Handle(GetllCategoryQuery request, CancellationToken cancellationToken)
            {
                var repo = _categoryEepository.GetAsNoTrackingQuery();
                var cats = await repo.Select(z=> new CategoryDTO
                {
                    Id = z.Id,
                    Name = z.Name,
                    parentId = z.parentId,
                    ParentName = z.Parent.Name
                }).Skip((request.PageNumber - 1) * request.Count).Take(request.Count).ToListAsync();
                var totalCount = await repo.CountAsync();
                return new ServiceRespnse<List<CategoryDTO>>().OK(cats,totalCount);
            }
        }
    }
}
