using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour, ISaveable
{
    [SerializeField] CharacterDataSO[] characterDataSOs;
    public static List<Character> CharacterDatas { get; private set; }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        CharacterDatas = new List<Character>(characterDataSOs.Length);

        for (int i = 0; i < characterDataSOs.Length; i++)
        {
            Character character = new Character(characterDataSOs[i].BaseData.Name, characterDataSOs[i].BaseData.Stats, characterDataSOs[i].BaseData.Experience, characterDataSOs[i].BaseData.Level);
            CharacterDatas.Add(character);
        }
    }

    public JToken CaptureState()
    {
        return JToken.FromObject(CharacterDatas);
    }

    public void RestoreFromState(JToken state)
    {
        CharacterDatas = state.ToObject<List<Character>>();
    }
}
