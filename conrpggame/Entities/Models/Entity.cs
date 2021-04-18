namespace conrpggame.Entities.Models
{
    public abstract class Entity
    {
        public int Hitpoints = 0;
        public Attack attack;
    }
    public class Attack
    {
        public int DamageDie;
        public int BonusDamage;
    }
}
