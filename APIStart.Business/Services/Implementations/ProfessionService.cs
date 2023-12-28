using APIStart.Business.Exceptions.FormatExceptions;
using APIStart.Core.DTOs.ProfessionModelDTOs;
using APIStart.Core.Entities;
using APIStart.Core.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIStart.Business.Services.Implementations
{
    public class ProfessionService : IProfessionService
    {
        private readonly IProfessionRepository _professionRepository;
        private readonly IMapper _mapper;

        public ProfessionService(IProfessionRepository professionRepository, IMapper mapper)
        {
            _professionRepository = professionRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync([FromForm] ProfessionCreateDto professionCreateDto)
        {
            Profession profession = _mapper.Map<Profession>(professionCreateDto);

            profession.CreationTime = DateTime.UtcNow.AddHours(4);
            profession.UpdateTime= DateTime.UtcNow.AddHours(4);
            profession.DeletedTime = DateTime.UtcNow.AddHours(4);
            profession.IsDeleted = false;

            await _professionRepository.CreateAsync(profession);
            await _professionRepository.CommitChanges();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProfessionGetDto>> GetAllAsync()
        {
            List<Profession> professions = await _professionRepository.GetAllAsync(profession => profession.IsDeleted == false);

            IEnumerable<ProfessionGetDto> professionGetDtos =  professions.Select(profession => new ProfessionGetDto { Id = profession.Id, Name = profession.Name });

            return professionGetDtos;
        }

        public async Task<ProfessionGetDto> GetByIdAsync(int id)
        {

            Profession profession = await _professionRepository.GetByIdAsync(profession => profession.Id == id && profession.IsDeleted == false);

            if (profession == null) throw new NotFound("profession couldn't be null!");

            ProfessionGetDto professionGetDto = _mapper.Map<ProfessionGetDto>(profession);

            return professionGetDto;
        }

        public IQueryable<Profession> GetProfessionTable()
        {
            var query = _professionRepository.Table.AsQueryable();

            return query;
        }

        public async Task ToggleDelete(int id)
        {

            Profession profession = await _professionRepository.GetByIdAsync(profession => profession.Id == id);

            if (profession == null) throw new NotFound("profession couldn't be null!");

            profession.IsDeleted = !profession.IsDeleted;
            profession.DeletedTime = DateTime.UtcNow.AddHours(4);

            await _professionRepository.CommitChanges();
        }

        public async Task UpdateAsync( int ? id , [FromForm] ProfessionUpdateDto professionUpdateDto)
        {

            Profession profession = await _professionRepository.GetByIdAsync(profession => profession.Id ==id);

            if (profession == null) throw new NotFound("profession couldn't be null!");

            profession = _mapper.Map(professionUpdateDto, profession);
            profession.UpdateTime = DateTime.UtcNow.AddHours(4);

            await _professionRepository.CommitChanges();
        }
    }
}
