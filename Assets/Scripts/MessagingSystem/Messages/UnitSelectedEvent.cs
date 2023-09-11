
namespace MyMessagingSystem
{
    public struct UnitSelectedEvent
    {
        public Unit Unit;

        public UnitSelectedEvent(Unit unit)
        {
            Unit = unit;
        }
    }
}