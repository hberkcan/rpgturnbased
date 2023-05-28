using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour, ISaveable
{
    [SerializeField] CharacterData[] characterDatas;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < characterDatas.Length; i++)
        {
            Character character = new Character(characterDatas[i].character.Name, characterDatas[i].character.Stats, characterDatas[i].character.Experience, characterDatas[i].character.Level);
            characterDatas[i].runtimeCharacter = character;
        }
    }

    public JToken CaptureState()
    {
        List<Character> characters = new List<Character>(characterDatas.Length);

        for (int i = 0; i < characterDatas.Length; i++)
        {
            characters.Add(characterDatas[i].runtimeCharacter);
        }

        return JToken.FromObject(characters);
    }

    public void RestoreFromState(JToken state)
    {
        List<Character> characters;
        characters = state.ToObject<List<Character>>();
        
        for(int i = 0; i < characters.Count; i++)
        {
            characterDatas[i].runtimeCharacter = characters[i];
        }
    }
}
