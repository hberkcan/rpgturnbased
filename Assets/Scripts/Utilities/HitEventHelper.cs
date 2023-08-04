using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEventHelper : MonoBehaviour
{
    [SerializeField] private UnitAttack unitAttack;

    //Animation Event
    public void Hit()
    {
        unitAttack.ApplyDamage();
    }
}
