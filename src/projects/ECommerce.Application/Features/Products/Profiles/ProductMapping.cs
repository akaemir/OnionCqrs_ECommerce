﻿using AutoMapper;
using ECommerce.Application.Features.Products.Commands.Create;
using ECommerce.Application.Features.Products.Queries.GetList;
using ECommerce.Application.Features.Products.Queries.GetListByImages;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Products.Profiles;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<ProductAddCommand, Product>();
        CreateMap<Product, ProductAddResponseDto>();

        CreateMap<Product, GetListProductResponseDto>()
            .ForMember(p => p.CategoryName,
                opt => opt.MapFrom(x => x.SubCategory.Name));
        CreateMap<Product, GetListProductByProductImagesResponse>()
            .ForMember(x => x.Urls, opt
                => opt.MapFrom(x => x.ProductImages.Select(y => y.Url)))
            .ForMember(x => x.CategoryName, opt =>
                opt.MapFrom(x=> x.SubCategory.Name));
    }
}