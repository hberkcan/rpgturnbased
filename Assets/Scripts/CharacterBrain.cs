using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterBrain : MonoBehaviour, IClickable
{
    private Character character;
    public Action<CharacterBrain> Clicked;

    public void OnClick()
    {
        Clicked?.Invoke(this);
    }

    public void OnLongPress()
    {
    }

    public void Init(ref Character character)
    {
        this.character = character;
    }
}
