using APIStart.data;
using APIStart.DTOs.BookModelDTOs;
using APIStart.DTOs.CategoryModelDTOs;
using APIStart.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIStart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<CategoryGetDto> categoryGetDtos = new List<CategoryGetDto>();
            foreach (var book in _context.Books)
            {
                CategoryGetDto categoryGet = new CategoryGetDto()
                {
                    Id = book.Id,
                    Name = book.Name,
                    
                };
                categoryGetDtos.Add(categoryGet);
            }
            return Ok(categoryGetDtos);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = _context.Categories.FirstOrDefault(b => b.Id == id);
            if (category == null) return NotFound();
            CategoryGetDto categoryGet= new CategoryGetDto()
            {
                Id = category.Id,
                Name = category.Name,
               
            };
            return Ok(categoryGet);
        }
        [HttpPost]
        public IActionResult Create(CategoryCreateDto categoryCreateDto)
        {
            Category category = new Category()
            {
               
                Name = categoryCreateDto.Name,
                
                IsDeleted = false,
                CreationTime = DateTime.UtcNow.AddHours(4),
                UpdateTime = DateTime.UtcNow.AddHours(4)

            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Created" });


        }
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, CategoryUpdateDto categoryUpdate)
        {
            Category category = _context.Categories.FirstOrDefault(b => b.Id == id);
            if (category == null) return NotFound();
            category.Name = categoryUpdate.Name;
           
            _context.SaveChanges();
            return StatusCode(201, new { message = "Updated" });


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(b => b.Id == id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Deleted" });


        }
        [HttpPut("{id}")]
        public IActionResult SoftDelete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(b => b.Id == id);
            if (category == null) return NotFound();
            category.IsDeleted = true;
            _context.SaveChanges();
            return StatusCode(201, new { message = "Softy deleted" });


        }
    }
}
