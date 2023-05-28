using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    public CharacterBrain characterPrefab;
    public GameObject characterIcon;

    public Character character;

    public Character runtimeCharacter = null;
}
