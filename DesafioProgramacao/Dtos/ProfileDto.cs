using AutoMapper;
using DesafioProgramacao.Entities;

namespace DesafioProgramacao.Dtos
{
    public class ProfileDto : Profile
    {
        public ProfileDto()
        {
            CreateMap<Produto, ProdutoDto>();
            CreateMap<ProdutoCreateDto, Produto>();
            CreateMap<ProdutoUpdateDto, Produto>();

            CreateMap<FornecedorDto, Fornecedor>();
        }
        
    }
}
