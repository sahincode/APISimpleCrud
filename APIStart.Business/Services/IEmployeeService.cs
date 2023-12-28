
using APIStart.Core.DTOs.EmployeeModelDTOs;
using APIStart.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIStart.Business.Services
{ 
    public interface IEmployeeService
    {
        Task CreateAsync([FromForm]EmployeeCreateDto workerCreateDto);
        Task Delete(int id);
        Task ToggleDelete(int id);
        IQueryable<Employee> GetWorkerTable();
        Task<EmployeeGetDto> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeGetDto>> GetAllAsync(string? input, int? professionId, int? orderId);
        Task UpdateAsync(int id ,[FromForm] EmployeeUpdateDto workerUpdateDto);

    }
}
