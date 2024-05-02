using Application.Cqrs.Commands;
using Core.Application.Common;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Application.Mediator.Categories.Command
{
    public class DeleteCategoryCommand:ICommand<ServiceRespnse>
    {
        public long Id { get; set; }

        public  class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, ServiceRespnse>
        {
            private readonly IGenericRepository<Category> _categoryEepository;

            public DeleteCategoryCommandHandler(IGenericRepository<Category> categoryEepository)
            {
                _categoryEepository = categoryEepository;
            }

            public async Task<ServiceRespnse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var cat = await _categoryEepository.GetAsNoTrackingQuery().Where(z => z.Id == request.Id).FirstOrDefaultAsync();
                if (cat is null)
                {
                    Hashtable errors = new();
                    errors.Add("Id", "Not Found");
                    return new ServiceRespnse().Failed(System.Net.HttpStatusCode.NotFound, errors);
                }
                await _categoryEepository.SoftDeleteAsync(cat,cancellationToken);
                return new ServiceRespnse().OK();
            }
        }
    }
}
