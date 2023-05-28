using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBattleButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private IntVariable selectedCharacterCount;

    private void Awake()
    {
        button = GetComponent<Button>();
        //button.interactable = false;
    }

    private void Start()
    {
        selectedCharacterCount.OnValueChanged += SelectedCharacterCount_OnValueChanged;
    }

    private void SelectedCharacterCount_OnValueChanged(int count)
    {
        if (count == 3)
            button.interactable = true;
        else
            button.interactable = false;
    }
}
