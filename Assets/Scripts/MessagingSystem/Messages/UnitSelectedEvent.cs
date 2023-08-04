
namespace MyMessagingSystem
{
    public struct UnitSelectedEvent
    {
        public Unit unit;

        public UnitSelectedEvent(Unit unit)
        {
            this.unit = unit;
        }
    }
}