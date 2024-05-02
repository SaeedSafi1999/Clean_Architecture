﻿using Application.Cqrs.Commands;
using Core.Application.Common;
using Core.Application.Mediator.Categories.DTOs;
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
    public class InsertCategoryCommand:ICommand<ServiceRespnse>
    {
        public CategoryDTO dto { get; set; }

        public class InsertCategoryCommandHandler : ICommandHandler<InsertCategoryCommand, ServiceRespnse>
        {
            private readonly IGenericRepository<Category> _categoryEepository;

            public InsertCategoryCommandHandler(IGenericRepository<Category> categoryEepository)
            {
                _categoryEepository = categoryEepository;
            }

            public async Task<ServiceRespnse> Handle(InsertCategoryCommand request, CancellationToken cancellationToken)
            {
                var exist = await _categoryEepository.GetAsNoTrackingQuery().AnyAsync(z => z.Name == request.dto.Name);
                if (exist)
                {
                    Hashtable errors = new();
                    errors.Add("name","duplicate record");
                    return new ServiceRespnse().Failed(System.Net.HttpStatusCode.BadRequest,errors);
                }
                await _categoryEepository.AddAsync(new Category
                {
                    Name = request.dto.Name,
                    parentId = request.dto.parentId
                },cancellationToken);
                return new ServiceRespnse().OK();

            }
        }
    }
}
