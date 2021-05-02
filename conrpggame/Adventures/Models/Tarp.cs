using conrpggame.Game;

namespace conrpggame.Adventures.Models
{
    public class Tarp
    {
        public TarpType tarpType;
        public Die DamageDie = Die.D32;
        public bool SearchedFor = false;
        public bool TrippedOrDisarmed = false;
    }
    public enum TarpType    //陷阱種類
    {
        Pit,
        Poison,
        spike
    }
}
