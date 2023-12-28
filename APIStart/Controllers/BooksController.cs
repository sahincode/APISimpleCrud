
using APIStart.Core.DTOs.BookModelDTOs;
using APIStart.Core.Entities;
using APIStart.Data.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace APIStart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<BookGetDto> bookGetModels = new List<BookGetDto>();
            foreach (var book in _context.Books)
            {
                BookGetDto bookGetModel = new BookGetDto()
                {
                    Id = book.Id,
                    Name = book.Name,
                    Price = book.Price
                };
                bookGetModels.Add(bookGetModel);
            }
            return Ok(bookGetModels);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            BookGetDto bookGetModelDto = new BookGetDto()
            {
                Id = book.Id,
                Name = book.Name,
                Price = book.Price

            };
            return Ok(bookGetModelDto);
        }
        [HttpPost]
        public IActionResult Create(BookCreateDto bookCreateModel)
        {
           if(! _context.Categories.Any(c => c.Id == bookCreateModel.CategoryId))
            {
                return StatusCode(404, new { message = "Category whisch id is equal to this not found" });
            }
            Book book = new Book()
            {
                CategoryId = bookCreateModel.CategoryId,
                Name = bookCreateModel.Name,
                Price = bookCreateModel.Price,
                IsDeleted = false,
                CreationTime = DateTime.UtcNow.AddHours(4),
                UpdateTime = DateTime.UtcNow.AddHours(4)

            };
            _context.Books.Add(book);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Created" });


        }
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, BookUpdateDto bookUpdateModel)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            book.Name = bookUpdateModel.Name;
            book.Price = bookUpdateModel.Price;
            book.CategoryId = bookUpdateModel.CategoryId;
            book.UpdateTime = DateTime.UtcNow.AddHours(4);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Updated" });


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Deleted" });


        }
        [HttpPut("{id}")]
        public IActionResult SoftDelete(int id)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            book.IsDeleted = true;
            _context.SaveChanges();
            return StatusCode(201, new { message = "Softy deleted" });


        }
    }
}
