using UnityEngine;

namespace MyMessagingSystem
{
    public struct StatDisplayEvent
    {
        public Unit UnitData;
        public Vector2 ScreenPos;

        public StatDisplayEvent(Unit unitData, Vector2 screenPos)
        {
            UnitData = unitData;
            ScreenPos = screenPos;
        }
    }
}