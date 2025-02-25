using AutoMapper;
using HUODotNet.Data.Entities;

namespace HUODotNet.ViewModels.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProductCreateRequest, Product>();
        CreateMap<Product, ProductViewModel>();
    }
}