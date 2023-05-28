using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Menu characterSelectionMenu;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void OpenMenu()
    {
        characterSelectionMenu.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        characterSelectionMenu.gameObject.SetActive(false);
    }
}
