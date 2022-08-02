using AutoMapper;
using DesafioProgramacao.Dtos;
using DesafioProgramacao.Entities;
using DesafioProgramacao.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioProgramacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ILogger<ProdutoController> _logger;
        private readonly IMapper _mapper;

        public ProdutoController(
            ILogger<ProdutoController> logger,
            IMapper mapper,
            IProdutoRepository produtoRepository
            )
        {
            _logger = logger;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {produtos.Count()} products");
            return Ok(produtosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _produtoRepository.GetAsync(id);
            if (produto == null)
                return NotFound();

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProdutoCreateDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            await _produtoRepository.CreateAsync(produto);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ProdutoCreateDto produtoDto)
        {

            var produto = _mapper.Map<Produto>(produtoDto);
            await _produtoRepository.UpdateAsync(produto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _produtoRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
