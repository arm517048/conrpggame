namespace conrpggame.Adventures.Models
{
    public class Objective
    {
        public ObjectiveType ObjectiveType;
    }
    public enum ObjectiveType
    {
        MonsterInRoom,
        AllMonster,
        ItemObtained
    }
}
