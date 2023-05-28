using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Newtonsoft.Json.Linq;

public class CharacterSelect : MonoBehaviour, IClickable
{
    private CharacterSelectionManager characterSelectionManager;
    private CharacterSelectUIBehaviour behaviour;

    private int characterIndex;
    private bool isSelected;

    private void Awake()
    {
        behaviour = GetComponent<CharacterSelectUIBehaviour>();
    }

    public void Init(CharacterSelectionManager characterSelectionManager, int index, List<int> selectedIndexes)
    {
        this.characterSelectionManager = characterSelectionManager;
        characterIndex = index;

        if (selectedIndexes.Contains(characterIndex))
        {
            isSelected = true;
            behaviour.OnSelected();
        }
    }

    public void OnClick()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            if (characterSelectionManager.SelectedCharacterCount >= characterSelectionManager.MaxCount)
            {
                isSelected = !isSelected;
                return;
            }

            characterSelectionManager.SelectCharacter(characterIndex);
            behaviour.OnSelected();
        }
        else
        {
            characterSelectionManager.DeselectCharacter(characterIndex);
            behaviour.OnDeselected();
        }
    }

    public void OnLongPress()
    {
        //show stats
    }
}
