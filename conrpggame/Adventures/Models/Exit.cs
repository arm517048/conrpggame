namespace conrpggame.Adventures.Models
{
    public class Exit
    {
        public bool Locked = false;
        public CompassDirection WallLocation;
        public Riddle Riddle;
        public int LeadToRoomNumber;
    }
    public enum CompassDirection
    {
        North,
        South,
        East,
        West
    }
}

