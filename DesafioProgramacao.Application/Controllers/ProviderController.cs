using DesafioProgramacao.Application.Models;
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
    public class ProviderController : ControllerBase
    {
        private readonly IBaseService<Provider> _baseService;
        private readonly ILogger<ProviderController> _logger;

        public ProviderController(
            IBaseService<Provider> baseService,
            ILogger<ProviderController> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProviderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 1) return BadRequest();

                var result = await _baseService.GetById<ProviderDto>(id);

                if (result == null)
                    NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProviderDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _baseService.Get<ProviderDto>();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow:hh:mm:ss}: Error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProviderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProviderCreateModel provider)
        {
            try
            {
                if (provider == null) return BadRequest();

                var result = await _baseService.Add<ProviderCreateModel, ProviderDto, ProviderValidator>(provider);

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

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProviderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProviderUpdateModel provider)
        {
            try
            {
                if (provider == null) return BadRequest();

                var result = await _baseService.Update<ProviderUpdateModel, ProviderDto, ProviderValidator>(provider);

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
