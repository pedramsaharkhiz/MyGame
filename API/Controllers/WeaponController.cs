using System.Threading.Tasks;
using API.Dtos.Character;
using API.Dtos.Weapon;
using API.Models;
using API.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            this._weaponService = weaponService;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>>AddWeapon(AddWeaponDto newWeapon)
        {
             return Ok(await _weaponService.AddWeapon(newWeapon));  
        }
    }
}
