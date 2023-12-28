using AutoMapper;
using Core.Domain.DTOs.Company;
using Core.Domain.DTOs.Product;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Company Map Configuration
            CreateMap<AddCompanyDTO, Company>().ReverseMap();
            CreateMap<UpdateCompanyDTO, Company>().ReverseMap();
            CreateMap<CompanyDTO, Company>().ReverseMap();

            //Product Map Configuration
            CreateMap<AddProductDTO,Product>().ReverseMap();
            CreateMap<UpdateProductDTO,Product>().ReverseMap();
            CreateMap<ProductDTO,Product>().ReverseMap();

        }
    }
}
