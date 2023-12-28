using APIStart.Core.DTOs.ProfessionModelDTOs;
using APIStart.Core.Entities;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIStart.Business.Services
{
    public interface IProfessionService
    {
        Task CreateAsync([FromForm] ProfessionCreateDto professionCreateDto);
        Task Delete(int id);
        Task ToggleDelete(int id);
        IQueryable<Profession> GetProfessionTable();
        Task<ProfessionGetDto> GetByIdAsync(int id);
        Task<IEnumerable<ProfessionGetDto>> GetAllAsync();
        Task UpdateAsync( int ? id ,[FromForm] ProfessionUpdateDto professionUpdateDto);
    }
}
