using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

public class UnitDamageDisplayBehaviour : MonoBehaviour
{

    [Header("Damage Color Tint")]
    public Color damageColorTint = Color.red;

    [Header("Heal Color Tint")]
    public Color healColorTint = Color.green;

    [Header("Damage Location")]
    public Transform damageDisplayTransform;

    //UnityEvent
    public void DisplayDamage(float damageTaken)
    {
        // tint colors for damage or healing
        Color colorTint = (damageTaken < 0) ? damageColorTint : healColorTint;
        MessagingSystem.Instance.Dispatch(new DamageDisplayEvent(damageTaken, damageDisplayTransform, colorTint));
    }
}
