using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Character Data")]
public class CharacterDataSO : ScriptableObject
{
    public CharacterBrain CharacterPrefab;
    public GameObject CharacterIcon;

    public Character BaseData;
}
