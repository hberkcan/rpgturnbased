using UnityEngine;

namespace MyMessagingSystem
{
    public struct DamageDisplayEvent
    {
        public int DamageAmount;
        public Transform DisplayLocation;
        public Color DamageColor;

        public DamageDisplayEvent(int damageAmount, Transform displayLocation, Color damageColor)
        {
            DamageAmount = damageAmount;
            DisplayLocation = displayLocation;
            DamageColor = damageColor;
        }
    }
}