using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos.Character;
using API.Models;
using API.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("Find")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetOne(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>>UpdateCharacter(UpdateCharacterDto updateCharacter){
            var response=await _characterService.UpdateCharacter(updateCharacter);
            if(response.Data==null)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>>DeleteCharacter(int id){
            var response=await _characterService.DeleteCharacter(id);
            if (response.Data==null)
            {   
                return NotFound(response.Message);
            }
            return Ok(response);
        }
    }
}
