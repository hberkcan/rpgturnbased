
namespace MyMessagingSystem
{
    public struct UnitDeselectedEvent
    {
        public Unit unit;

        public UnitDeselectedEvent(Unit unit)
        {
            this.unit = unit;
        }
    }
}