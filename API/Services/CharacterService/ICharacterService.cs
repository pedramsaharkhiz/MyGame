using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Character;
using API.Models;

namespace API.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>>GetAllCharacters(int userId);
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>>AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>>UpdateCharacter(UpdateCharacterDto UpdateCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>>DeleteCharacter(int Id);

         
    }
}