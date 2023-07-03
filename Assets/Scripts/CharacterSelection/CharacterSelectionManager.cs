using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour, ISaveable
{
    [SerializeField] private CharacterDataSO[] characterDatas;
    [SerializeField] private CharacterSelect characterUIPrefab;

    private List<CharacterDataSO> selectedCharacters;
    public List<CharacterDataSO> SelectedCharacters => selectedCharacters;

    private List<int> selectedIndexes;
    public List<int> SelectedIndexes => selectedIndexes;

    private int selectedCharacterCount;
    public int SelectedCharacterCount => selectedCharacterCount;

    public readonly int MaxCount = 3;
    
    public event Action<int> OnSelectedCharacterCountChanged;

    private void Awake()
    {
        selectedCharacters = new List<CharacterDataSO>(MaxCount);
        selectedIndexes = new List<int>(MaxCount);
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        for(int i = 0; i < characterDatas.Length; i++)
        {
            var characterSelect = Instantiate(characterUIPrefab, transform);
            characterSelect.Init(this, i, selectedIndexes);
            Instantiate(characterDatas[i].CharacterIcon, characterSelect.transform);
        }
    }

    public void SelectCharacter(int characterIndex)
    {
        selectedIndexes.Add(characterIndex);
        selectedCharacters.Add(characterDatas[characterIndex]);
        selectedCharacterCount++;
        OnSelectedCharacterCountChanged?.Invoke(selectedCharacterCount);
    }

    public void DeselectCharacter(int characterIndex)
    {
        selectedIndexes.Remove(characterIndex);
        selectedCharacters.Remove(characterDatas[characterIndex]);
        selectedCharacterCount--;
        OnSelectedCharacterCountChanged?.Invoke(selectedCharacterCount);
    }

    public JToken CaptureState()
    {
        return JToken.FromObject(selectedIndexes);
    }

    public void RestoreFromState(JToken state)
    {
        selectedIndexes = state.ToObject<List<int>>();
        selectedCharacterCount = selectedIndexes.Count;

        foreach (int i in selectedIndexes)
        {
            selectedCharacters.Add(characterDatas[i]);
        }
    }
}
