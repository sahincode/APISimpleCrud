using APIStart.Business.Exceptions.FormatExceptions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIStart.Data.DAL;
using APIStart.Core.DTOs.EmployeeModelDTOs;
using APIStart.Core.Entities;
using APIStart.Business.InternalHelperServices;

using APIStart.Business.Services;

namespace APIStart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOne(int id)
        {

            if (id == null && id <= 0) return NotFound();
            EmployeeGetDto employeeGetDto = null;
            try
            {
                employeeGetDto = await _employeeService.GetByIdAsync(id);

            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }


            return Ok(employeeGetDto);
        }


        [HttpGet("")]
        public async Task<IActionResult> GetAll(string? search, int? professionId, int? orderId)
        {

            IEnumerable<EmployeeGetDto> workerGetDtos = await _employeeService.GetAllAsync(search, professionId, orderId);

            return Ok(workerGetDtos);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] EmployeeCreateDto employeeCreateDto)
        {
            try
            {

                await _employeeService.CreateAsync(employeeCreateDto);
            }
            catch (InvalidImageContentTypeOrSize ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidImage ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }

            return StatusCode(201, new { message = "Object yaradildi" });
        }
        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id ,[FromForm] EmployeeUpdateDto employeeUpdateDto)
        {
            try
            {

                await _employeeService.UpdateAsync(id ,employeeUpdateDto);
            }
            catch (InvalidImageContentTypeOrSize ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("/workers/toggleDelete/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ToggleDelete(int id)
        {

            if (id == null && id <= 0) return NotFound();

            try
            {

                await _employeeService.ToggleDelete(id);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
