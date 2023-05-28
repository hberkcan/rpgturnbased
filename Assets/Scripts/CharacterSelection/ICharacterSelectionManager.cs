using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICharacterSelectionManager
{
    //public void SelectCharacter(int characterIndex);

    //public void DeselectCharacter(int characterIndex);

    //public int SelectedCharacterCount { get; }

    //public int MaxCount { get; }

    public event Action<int> OnSelectedCharacterCountChanged;
}
