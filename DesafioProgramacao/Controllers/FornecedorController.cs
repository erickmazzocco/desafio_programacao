using AutoMapper;
using DesafioProgramacao.Dtos;
using DesafioProgramacao.Entities;
using DesafioProgramacao.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DesafioProgramacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FornecedorController> _logger;

        public FornecedorController(
            IFornecedorRepository fornecedorRepository, 
            IMapper mapper,
            ILogger<FornecedorController> logger)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] FornecedorDto fornecedorDto)
        {
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);
            await _fornecedorRepository.CreateAsync(fornecedor);
            return Ok();
        }
    }
}
