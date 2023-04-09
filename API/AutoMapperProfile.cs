using API.Dtos.Character;
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
        }

    }
}