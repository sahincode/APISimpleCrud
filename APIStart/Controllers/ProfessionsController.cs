using APIStart.data;
using APIStart.DTOs.CategoryModelDTOs;
using APIStart.DTOs.ProfessionModelDTOs;
using APIStart.Entities;
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
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProfessionsController(AppDbContext context ,IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ProfessionGetDto> professionGetDtos= new List<ProfessionGetDto>();
            foreach (var profession in _context.Professions)
            {
                ProfessionGetDto professionGetDto = _mapper.Map<ProfessionGetDto>(profession);


                professionGetDtos.Add(professionGetDto);
            }
            return Ok(professionGetDtos);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Profession profession= _context.Professions.FirstOrDefault(b => b.Id == id);
            if (profession == null) return NotFound();
            ProfessionGetDto professionGetDto = _mapper.Map<ProfessionGetDto>(profession);
            return Ok(professionGetDto);
        }
        [HttpPost]
        public IActionResult Create(ProfessionCreateDto professionCreateDto)
        {
            if (_context.Professions.Any(p => p.Name == professionCreateDto.Name))
            {
                return StatusCode(409, "This Profession already exist");
            }
            Profession profession = _mapper.Map<Profession>(professionCreateDto);
            profession.CreationTime = DateTime.UtcNow.AddHours(4);
            profession.UpdateTime = DateTime.UtcNow.AddHours(4);

            _context.Professions.Add(profession);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Created" });


        }
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, ProfessionUpdateDto professionUpdateDto)
        {
           
            Profession profession = _context.Professions.FirstOrDefault(b => b.Id == id);
            if (profession == null) return NotFound();
            if (_context.Professions.Any(p => p.Name == professionUpdateDto.Name && p.Id != id))
            {
                return StatusCode(409, "This Profession already exis");
            }
            profession.Name = professionUpdateDto.Name;

            _context.SaveChanges();
            return StatusCode(201, new { message = "Updated" });


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Profession profession = _context.Professions.FirstOrDefault(b => b.Id == id);
            if (profession == null) return NotFound();
            _context.Professions.Remove(profession);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Deleted" });


        }
        [HttpPut("{id}")]
        public IActionResult SoftDelete(int id)
        {
            Profession profession = _context.Professions.FirstOrDefault(b => b.Id == id);
            if (profession == null) return NotFound();
            profession.IsDeleted = true;
            _context.SaveChanges();
            return StatusCode(201, new { message = "Softy deleted" });


        }
    }
}
