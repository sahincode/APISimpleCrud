using APIStart.Business.Exceptions.FormatExceptions;
using APIStart.Business.InternalHelperServices;
using APIStart.Core.DTOs.EmployeeModelDTOs;
using APIStart.Core.Entities;
using APIStart.Core.Repositories;
using APIStart.Data.DAL;
using AutoMapper;
using FluentValidation.Validators;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIStart.Business.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmployeeProfessionRepository _workerProfessionRepository;
        private readonly IProfessionRepository _professionRepository;

        public EmployeeService(IEmployeeRepository employeeRepository,
                                IMapper mapper,
                                IWebHostEnvironment webHostEnvironment,
                                IEmployeeProfessionRepository workerProfessionRepository,
                               IProfessionRepository professionRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _workerProfessionRepository = workerProfessionRepository;
            this._professionRepository = professionRepository;
        }

        public async Task CreateAsync([FromForm] EmployeeCreateDto employeeCreateDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeCreateDto);
            bool check = false;

            if (employeeCreateDto.ProfessionIds != null)
            {
                foreach (int professionId in  employeeCreateDto.ProfessionIds)
                {
                    if (!_professionRepository.Table.Any(profession => profession.Id == professionId))
                    {
                        check = true;
                        break;
                    }
                }
            }

            if (!check)
            {
                if (employeeCreateDto.ProfessionIds != null)
                {
                    foreach (int professionId in employeeCreateDto.ProfessionIds)
                    {
                        ProfessionEmployee employeeProfession = new ProfessionEmployee
                        {
                            Employee = employee,
                            ProfessionId = professionId,
                        };

                        await _workerProfessionRepository.CreateAsync(employeeProfession);
                    }
                }
            }
            else
            {
                throw new NotFound("professionId is not found");

            }

            if (employeeCreateDto.Image != null)
            {
                if (employeeCreateDto.Image.ContentType != "image/png" && employeeCreateDto.Image.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentTypeOrSize("enter the correct image contenttype!");
                }

                if (employeeCreateDto.Image.Length > 1048576)
                {
                    throw new InvalidImageContentTypeOrSize("image size must be less than 1mb!");
                }
            }
            else
            {
                throw new InvalidImage("Image is required!");
            }

            string folder = "Uploads/workers-images";
            string newImgUrl = await FileHelper.SaveImage(_webHostEnvironment.WebRootPath, folder, employeeCreateDto.Image);


            employee.CreationTime = DateTime.UtcNow.AddHours(4);
            employee.UpdateTime = DateTime.UtcNow.AddHours(4);
            employee.DeletedTime = DateTime.UtcNow.AddHours(4);

            employee.ImageUrl = newImgUrl;
            
            await _employeeRepository.CreateAsync(employee);
            await _employeeRepository.CommitChanges();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeGetDto>> GetAllAsync(string? input, int? professionId, int? orderId)
        {
            IQueryable<Employee> workers = _employeeRepository.Table.Include(worker => worker.Professions).Where(worker => worker.IsDeleted == false).AsQueryable();
            if (workers is not null)
            {
                if (input is not null)
                {
                    workers = workers.Where(worker => worker.FullName.ToLower().Contains(input.ToLower()) || worker.Description.ToLower().Contains(input.ToLower()));
                }

                if (professionId is not null)
                {
                    workers = workers.Where(worker => worker.Professions.Any(worker => worker.ProfessionId == professionId));
                }

                if (orderId is not null)
                {
                    switch (orderId)
                    {
                        case 1:
                            workers = workers.OrderByDescending(worker => worker.CreationTime);
                            break;
                        case 2:
                            workers = workers.OrderBy(worker => worker.Salary);
                            break;
                        case 3:
                            workers = workers.OrderBy(worker => worker.FullName);
                            break;
                        default:
                            throw new NotFound("enter the correct order value!");
                    }
                }
            }
            List<EmployeeGetDto> employeeGetDtos =  new  List<EmployeeGetDto>();
            foreach (var worker in workers)
            {
                
                EmployeeGetDto employeeGetDto = _mapper.Map<EmployeeGetDto>(worker);
                employeeGetDto.ProfessionIds = worker.Professions.Select(p => p.ProfessionId).ToList();
                employeeGetDtos.Add(employeeGetDto);
            }

        

            return employeeGetDtos;
        }

        public async Task<EmployeeGetDto> GetByIdAsync(int id)
        {

            Employee employee = await _employeeRepository.GetByIdAsync(employee => employee.Id == id && employee.IsDeleted == false ,"Professions");

            if (employee == null) throw new NotFound("worker couldn't be null!");
            

            EmployeeGetDto employeeGetDto = _mapper.Map<EmployeeGetDto>(employee);
            employeeGetDto.ProfessionIds=employee.Professions.Select(p => p.ProfessionId).ToList();

            return employeeGetDto;
        }

        public IQueryable<Employee> GetWorkerTable()
        {
            var query = _employeeRepository.Table.AsQueryable();

            return query;
        }

        public async Task ToggleDelete(int id)
        {
            string folder = "Uploads/workers-images";

            Employee employee = await _employeeRepository.GetByIdAsync(worker => worker.Id == id);

            if (employee == null) throw new NotFound("worker couldn't be null!");

            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, folder, employee.ImageUrl);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            employee.IsDeleted = !employee.IsDeleted;
            employee.DeletedTime = DateTime.UtcNow.AddHours(4);

            await _employeeRepository.CommitChanges();
        }

        public async Task UpdateAsync( int id ,[FromForm] EmployeeUpdateDto employeeUpdateDto)
        {
            Employee employee = await _employeeRepository.GetByIdAsync(worker => worker.Id == id && worker.IsDeleted == false, "Professions.Profession");

            if (employee == null) throw new NotFound("worker couldn't be null!");

            employee.Professions.RemoveAll(wp => !employeeUpdateDto.ProfessionIds.Contains(wp.ProfessionId));

            foreach (var professionId in employeeUpdateDto.ProfessionIds.Where(pId => !employee.Professions.Any(wp => wp.ProfessionId == pId)))
            {
                ProfessionEmployee workerProfession = new ProfessionEmployee
                {
                    Employee = employee,
                    ProfessionId = professionId,
                };

                await _workerProfessionRepository.CreateAsync(workerProfession);
            }

            if (employeeUpdateDto.Image != null)
            {
                if (employeeUpdateDto.Image.ContentType != "image/png" && employeeUpdateDto.Image.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentTypeOrSize("enter the correct image contenttype!");
                }

                if (employeeUpdateDto.Image.Length > 1048576)
                {
                    throw new InvalidImageContentTypeOrSize("image size must be less than 1mb!");
                }

                string folder = "Uploads/workers-images";
                string newImgUrl = await FileHelper.SaveImage(_webHostEnvironment.WebRootPath, folder, employeeUpdateDto.Image);

                string oldImgPath = Path.Combine(_webHostEnvironment.WebRootPath, folder, employee.ImageUrl);

                if (System.IO.File.Exists(oldImgPath))
                {
                    System.IO.File.Delete(oldImgPath);
                }

                employee.ImageUrl = newImgUrl;

            }

            employee = _mapper.Map(employeeUpdateDto, employee);
            employee.UpdateTime = DateTime.UtcNow.AddHours(4);

            await _employeeRepository.CommitChanges();
        }

      
    }
}
