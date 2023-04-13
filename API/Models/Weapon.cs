namespace API.Models
{
    public class Weapon
    {
       public int Id { get; set; }    
       public string Name { get; set; }    
       public int Damage { get; set; }    
       public Character Character { get; set; } //one to one relation between weapons and characters Tables that created in DataContext file with DBset
       public int CharacterId { get; set; }   //its a foreign key for this relation because without this we had an error --The dependent side could not be determined for the one-to-one relationship between 'Character.Weapon' and 'Weapon.Character'. To identify the dependent side of the relationship, configure the foreign key property. --
    }
}