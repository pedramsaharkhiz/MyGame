namespace API.Dtos.Fight
{
    public class AttackResultDto
    {
        public string Attacker { get; set; }   
        public int AttackerHP { get; set; }   
        public string Opponent { get; set; }   
        public int OpponentHP { get; set; }   
        public int Damage { get; set; }   
    }
}