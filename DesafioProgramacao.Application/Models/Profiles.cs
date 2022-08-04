using AutoMapper;
using DesafioProgramacao.Domain.Dtos;
using DesafioProgramacao.Domain.Entities;

namespace DesafioProgramacao.Application.Models
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<ProductCreateModel, Product>();
            CreateMap<ProductUpdateModel, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<ProviderCreateModel, Provider>();
            CreateMap<ProviderUpdateModel, Provider>();
            CreateMap<Provider, ProviderDto>();
        }       
    }
}
