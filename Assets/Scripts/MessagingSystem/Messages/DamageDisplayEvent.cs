using UnityEngine;

namespace MyMessagingSystem
{
    public struct DamageDisplayEvent
    {
        public int damageAmount;
        public Transform displayLocation;
        public Color damageColor;

        public DamageDisplayEvent(int damageAmount, Transform displayLocation, Color damageColor)
        {
            this.damageAmount = damageAmount;
            this.displayLocation = displayLocation;
            this.damageColor = damageColor;
        }
    }
}