using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyMessagingSystem;

public class CharacterSelectionMenu : Menu<CharacterSelectionMenu>
{
    [SerializeField] private CharacterSelectionManager characterSelectionManager;
    [SerializeField] private Button startButton;

    protected override void Awake()
    {
        base.Awake();
        InitStartButton();
    }

    private void OnEnable()
    {
        characterSelectionManager.OnSelectedCharacterCountChanged += CharacterSelectionManager_OnSelectedCharacterCountChanged;
    }

    private void OnDisable()
    {
        characterSelectionManager.OnSelectedCharacterCountChanged -= CharacterSelectionManager_OnSelectedCharacterCountChanged;
    }

    private void CharacterSelectionManager_OnSelectedCharacterCountChanged(int count)
    {
        if (count == characterSelectionManager.MaxCount)
            startButton.interactable = true;
        else
            startButton.interactable = false;
    }

    private void StartBattle()
    {
        MessagingSystem.Instance.Dispatch(new StartBattleEvent(characterSelectionManager.SelectedCharacters));
        Close();
    }

    private void InitStartButton()
    {
        startButton.interactable = false;
        startButton.onClick.AddListener(StartBattle);
    }
}
