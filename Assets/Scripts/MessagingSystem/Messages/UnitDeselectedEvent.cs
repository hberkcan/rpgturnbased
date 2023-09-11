
namespace MyMessagingSystem
{
    public struct UnitDeselectedEvent
    {
        public Unit Unit;

        public UnitDeselectedEvent(Unit unit)
        {
            Unit = unit;
        }
    }
}