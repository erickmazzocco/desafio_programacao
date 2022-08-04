using AutoMapper;
using DesafioProgramacao.Application.Models;
using DesafioProgramacao.CrossCutting.Pagination;
using DesafioProgramacao.Domain.Dtos;
using DesafioProgramacao.Domain.Entities;
using DesafioProgramacao.Domain.Interfaces;
using DesafioProgramacao.Service.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioProgramacao.Application.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IBaseService<Product> _baseService;        
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IBaseService<Product> baseService,            
            ILogger<ProductController> logger)
        {
            _baseService = baseService;            
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 1) return BadRequest();

                var result = await _baseService.GetById<ProductDto>(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
        {
            try
            {
                var result = await _baseService.Get<ProductDto>(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateModel product)
        {
            try
            {
                if (product == null) return BadRequest();

                var result = await _baseService.Add<ProductCreateModel, ProductDto, ProductValidator>(product);                

                return Ok(result);
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return BadRequest();

                var result = await _baseService.Delete(id, soft: true);

                if (!result) return BadRequest();

                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateModel product)
        {
            try
            {
                if (product == null) return BadRequest();

                var result = await _baseService.Update<ProductUpdateModel, ProductDto, ProductValidator>(product);

                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }        
    }
}
