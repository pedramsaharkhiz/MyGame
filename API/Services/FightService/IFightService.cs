using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Fight;
using API.Models;

namespace API.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>>WeaponAttack(WeaponAttackDto request);   
        Task<ServiceResponse<AttackResultDto>>SkillAttack(SkillAttackDto request);   
        Task<ServiceResponse<FightResultDto>>Fight(FightRequestDto request);   
        Task<ServiceResponse<List<HighScoreDto>>>GetHighScore();   
    }
}