using API.Dtos.Character;
using API.Dtos.Skill;
using API.Dtos.Weapon;
using API.Models;
using AutoMapper;

namespace API
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character,GetCharacterDto>();
            CreateMap<AddCharacterDto,Character>();
            CreateMap<Weapon,GetWeaponDto>();
            CreateMap<Skill,GetSkillDto>();
        }

    }
}