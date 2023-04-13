using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Character;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> Characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Pedram" }
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(
            IMapper mapper,
            DataContext context,
            IHttpContextAccessor httpContextAccessor
        )
        {
            this._httpContextAccessor = httpContextAccessor;
            this._context = context;
            this._mapper = mapper;
        }
        private int GetUserId()=>int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(
            AddCharacterDto newCharacter
        )
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User=await _context.Users.FirstOrDefaultAsync(c=>c.Id==GetUserId());
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters
            .Where(c=>c.User.Id==GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToListAsync();
                
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id&&c.User.Id==GetUserId());
                if(character !=null)
                {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Characters
                .Where(c=>c.User.Id==GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c))
                    .ToListAsync();
                }
                else
                {
                    serviceResponse.Success=false;
                    serviceResponse.Message="Character not found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters
            .Include(c=>c.Weapon)
            .Include(c=>c.Skills)
            .Where(c=>c.User.Id==GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters
            .Include(c=>c.Weapon)
            .Include(c=>c.Skills)
            .FirstOrDefaultAsync(c => c.Id == id&&c.User.Id==GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter); 
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(
            UpdateCharacterDto updateCharacter
        )
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters
                .Include(c=>c.User)
                .FirstOrDefaultAsync(
                    c => c.Id == updateCharacter.Id
                );
                if(character.User.Id==GetUserId()){
                character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defense = updateCharacter.Defense;
                character.Intelligence = updateCharacter.Intelligence;
                character.Class = updateCharacter.Class;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            else
            {
                serviceResponse.Success=false;
                serviceResponse.Message="Character not found";
            }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character=await _context.Characters
                .Include(c=>c.Weapon)
                .Include(c=>c.Skills)
                .FirstOrDefaultAsync(c=>c.Id==newCharacterSkill.CharacterId&&c.User.Id==GetUserId());

                if(character==null)
                {
                    response.Success=false;
                    response.Message="Character not found";
                    return response;
                }
                var skill=await _context.Skills.FirstOrDefaultAsync(c=>c.Id==newCharacterSkill.SkillId);
                if (skill==null)
                {
                    response.Success=false;
                    response.Message="Skill not found";
                    return response;
                }
                character.Skills.Add(skill);
                await _context.SaveChangesAsync();
                response.Data=_mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success=false;
                response.Message=ex.Message;
            }
            return response;
        }

       
    }
}
