using UnityEngine;

namespace MyMessagingSystem
{
    public struct DamageDisplayEvent
    {
        public float damageAmount;
        public Transform displayLocation;
        public Color damageColor;

        public DamageDisplayEvent(float damageAmount, Transform displayLocation, Color damageColor)
        {
            this.damageAmount = damageAmount;
            this.displayLocation = displayLocation;
            this.damageColor = damageColor;
        }
    }
}