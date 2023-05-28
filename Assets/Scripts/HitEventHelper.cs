using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEventHelper : MonoBehaviour
{
    CharacterAttack characterAttack;

    private void Awake()
    {
        characterAttack = GetComponentInParent<CharacterAttack>();
    }

    //Animation Event
    public void Hit()
    {
        characterAttack.Hit();
    }
}
