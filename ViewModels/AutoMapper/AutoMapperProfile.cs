using AutoMapper;
using HUODotNet.Data.Entities;

namespace HUODotNet.ViewModels.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProductCreateRequest, Product>()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(x => x.UpdatedAt, opt => opt.MapFrom(_ => DateTime.Now));
        CreateMap<Product, ProductViewModel>();
    }
}