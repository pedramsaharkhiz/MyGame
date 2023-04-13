using System.Threading.Tasks;
using API.Dtos.Character;
using API.Dtos.Weapon;
using API.Models;

namespace API.Services.WeaponService
{
    public interface IWeaponService
    {
       Task<ServiceResponse<GetCharacterDto>>AddWeapon(AddWeaponDto newWeapon);  
    }
}