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
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ILogger<ProdutoController> _logger;
        private readonly IMapper _mapper;

        public ProdutoController(
            ILogger<ProdutoController> logger,
            IMapper mapper,
            IProdutoRepository produtoRepository,
            IFornecedorRepository fornecedorRepository
            )
        {
            _logger = logger;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll()
        {
            try
            {
                var produtos = await _produtoRepository.GetAllAsync();
                var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
                
                return Ok(produtosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Erro: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetAsync(id);
                if (produto == null)
                    return NotFound("Produto não encontrado");

                return Ok(produto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Erro: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<ProdutoDto>> Create([FromBody] ProdutoCreateDto produtoDto)
        {
            try
            {
                if (await _fornecedorRepository.GetAsync(produtoDto.FornecedorId) == null)
                    return BadRequest("Fornecedor não encontrado");

                if (produtoDto.DataFabricacao > produtoDto.DataValidade)
                    return BadRequest("Data de fabricação não pode ser maior que a data de validade");

                var produto = _mapper.Map<Produto>(produtoDto);
                var result = await _produtoRepository.CreateAsync(produto);

                var resultDto = _mapper.Map<ProdutoDto>(result);

                return Created(string.Empty, resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Erro: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult<ProdutoDto>> Update([FromBody] ProdutoUpdateDto produtoUpdateDto)
        {
            try
            {
                if (produtoUpdateDto.DataFabricacao > produtoUpdateDto.DataValidade)
                    return BadRequest("Data de fabricação não pode ser maior que a data de validade");

                if (await _produtoRepository.GetAsync(produtoUpdateDto.Id) == null)
                    return NotFound("Produto não encontrado");

                var produto = _mapper.Map<Produto>(produtoUpdateDto);                

                var newProduto = await _produtoRepository.UpdateAsync(produto);
                var newProdutoDto = _mapper.Map<ProdutoDto>(newProduto);
                return Ok(newProdutoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Erro: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await _produtoRepository.GetAsync(id) == null)
                    return NotFound("Produto não encontrado");

                await _produtoRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Erro: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
