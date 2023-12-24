using APIStart.InternalHelperServices;
using APIStart.data;
using APIStart.DTOs.BookModelDTOs;
using APIStart.DTOs.EmployeeModelDTOs;
using APIStart.Entities;
using AutoMapper;
using APIStart.Exceptions.FormatExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIStart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public string passPath = "employee";
        private readonly IWebHostEnvironment _environment;
        public EmployeesController(AppDbContext context ,IMapper mapper ,IWebHostEnvironment environment)
        {
            this._context = context;
            this._mapper = mapper;
            this._environment = environment;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<EmployeeGetDto> bookGetModels = new List<EmployeeGetDto>();
            foreach (var employee in _context.Employees)
            {
                var bookGetModel = _mapper.Map<EmployeeGetDto>(employee);
                bookGetModels.Add(bookGetModel);
            }
            return Ok(bookGetModels);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Employee employee = _context.Employees.FirstOrDefault(b => b.Id == id);
            if (employee == null) return NotFound();
            var employeeGetModelDto = _mapper.Map<EmployeeGetDto>(employee);


            return Ok(employeeGetModelDto);
        }
        [HttpPost]
        public async Task< IActionResult> Create([FromForm]EmployeeCreateDto employeeCreateModel)
        {
            
            if (!_context.Professions.Any(c => c.Id == employeeCreateModel.ProfessionId))
            {
                return StatusCode(404, new { message = "Category whisch id is equal to this not found" });
            }
            if (employeeCreateModel.Image.ContentType != "image/png" && employeeCreateModel.Image.ContentType != "image/jpeg")
                throw new FileFormatException("Image", "please add png or jpeg file");
            
            string rootPath = _environment.WebRootPath;
          
            
            var employee =_mapper.Map<Employee>(employeeCreateModel);
            employee.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, employeeCreateModel.Image);

            employee.CreationTime = DateTime.UtcNow.AddHours(4);
            employee.UpdateTime = DateTime.UtcNow.AddHours(4);

            _context.Employees.Add(employee);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Created" });


        }
        [HttpPut("update/{id}")]
        public async Task < IActionResult> Update(int id, [FromForm]EmployeeUpdateDto employeeUpdateModel)
        {
            string rootPath = _environment.WebRootPath;
            Employee employee = _context.Employees.FirstOrDefault(b => b.Id == id);
            if (employee == null) return NotFound();
            if (employeeUpdateModel.Image != null)
            {
                System.IO.File.Delete(Path.Combine(rootPath ,passPath ,employee.ImageUrl));
                if (employeeUpdateModel.Image.ContentType != "image/png" && employeeUpdateModel.Image.ContentType != "image/jpeg")
                    throw new FileFormatException("Image", "please add png or jpeg file");
                employee.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, employeeUpdateModel.Image);


            }
            employee.FullName = employeeUpdateModel.FullName;
            employee.LinkEdn = employeeUpdateModel.LinkEdn;
            employee.FaceLink = employeeUpdateModel.FaceLink;
            employee.InstaLink = employeeUpdateModel.InstaLink;
            employee.TwitLink = employeeUpdateModel.TwitLink;
            employee.Description = employeeUpdateModel.Description;



            employee.ProfessionId = employeeUpdateModel.ProfessionId;
            employee.UpdateTime = DateTime.UtcNow.AddHours(4);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Updated" });


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            string rootPath = _environment.WebRootPath;
            Employee employee = _context.Employees.FirstOrDefault(b => b.Id == id);
            if (employee == null) return NotFound();
            System.IO.File.Delete(Path.Combine(rootPath, passPath, employee.ImageUrl));
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Deleted" });


        }
        [HttpPut("{id}")]
        public async Task< IActionResult> SoftDelete(int id)
        {
            Employee employee =  await _context.Employees.FirstOrDefaultAsync(b => b.Id == id);
            if (employee == null) return NotFound();
            employee.IsDeleted = true;
            _context.SaveChanges();
            return StatusCode(201, new { message = "Softy deleted" });


        }
    }
}
