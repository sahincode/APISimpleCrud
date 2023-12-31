﻿using APIStart.Business.Exceptions.FormatExceptions;
using APIStart.Business.Services;
using APIStart.Core.DTOs.ProfessionModelDTOs;
using APIStart.Core.Entities;
using APIStart.Data.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIStart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly IProfessionService _professionService;
        public ProfessionsController(IProfessionService professionService)
        {
            _professionService = professionService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOne(int id)
        {
            if (id == null && id <= 0) return NotFound();
            ProfessionGetDto professionGetDto = null;

            try
            {
                professionGetDto = await _professionService.GetByIdAsync(id);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }


            return Ok(professionGetDto);
        }


        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {

            IEnumerable<ProfessionGetDto> professionGetDtos = await _professionService.GetAllAsync();

            return Ok(professionGetDtos);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] ProfessionCreateDto professionCreateDto)
        {

            await _professionService.CreateAsync(professionCreateDto);

            return StatusCode(201, new { message = "Object yaradildi" });
        }

        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int ?id ,[FromForm] ProfessionUpdateDto professionUpdateDto)
        {
            try
            {
                await _professionService.UpdateAsync(  id,professionUpdateDto);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("/professions/toggleDelete/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ToggleDelete(int id)
        {

            if (id == null && id <= 0) return NotFound();

            try
            {

                await _professionService.ToggleDelete(id);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
