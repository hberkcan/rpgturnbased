using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterBrain : MonoBehaviour, IClickable
{
    private Character character;
    public CharacterAttack CharacterAttack { get; private set; }
    public CharacterHealth CharacterHealth { get; private set; }
    public Action<CharacterBrain> Clicked;

    private void Awake()
    {
        CharacterAttack = GetComponent<CharacterAttack>();
        CharacterHealth = GetComponent<CharacterHealth>();
    }

    public void OnClick()
    {
        Clicked?.Invoke(this);
    }

    public void OnLongPress()
    {
        //show stats
    }

    public void Init(ref Character character)
    {
        this.character = character;
    }
}
