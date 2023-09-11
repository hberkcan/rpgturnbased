using System.Collections.Generic;

namespace MyMessagingSystem
{
    public struct BattleEndEvent
    {
        public List<UnitController> AlivePlayerUnits;

        public BattleEndEvent(List<UnitController> alivePlayerUnits)
        {
            AlivePlayerUnits = alivePlayerUnits;
        }
    }
}