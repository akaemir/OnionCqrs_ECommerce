using AutoMapper;
using Core.Application.Pipelines.Caching;
using ECommerce.Application.Services.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetListByImages;

public class GetListProductByProductImagesQuery : IRequest<List<GetListProductByProductImagesResponse>>, ICachableRequest
{
    public string CacheKey => "GetListProductByProductImages";
    public bool ByPassCache => false;
    public string? CacheGroupKey => "ProductByProductImages";
    public TimeSpan? SlidingExpiration { get; }
    
    public sealed class GetListProductByProductImagesHandler : IRequestHandler<GetListProductByProductImagesQuery, List<GetListProductByProductImagesResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;

        public GetListProductByProductImagesHandler(IMapper mapper, IProductRepository productRepository, IProductImageRepository productImageRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
        }

        public async Task<List<GetListProductByProductImagesResponse>> Handle(GetListProductByProductImagesQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetListAsync(enableTracking: false, withDeleted: true,cancellationToken: cancellationToken);
            var images = await _productImageRepository.GetListAsync(cancellationToken:cancellationToken,enableTracking:false);
            products.ForEach(x => x.ProductImages = images.Where(i => i.ProductId == x.Id).ToList());
            var result = _mapper.Map<List<GetListProductByProductImagesResponse>>(products);
            return result;
        }   
    }
}