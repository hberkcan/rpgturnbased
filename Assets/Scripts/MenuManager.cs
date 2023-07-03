using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Menu characterSelectionMenu;
    [SerializeField] private Menu[] menus;
    private Dictionary<Type, Menu> menuDictionary;

    private void Awake()
    {
        menuDictionary = new Dictionary<Type, Menu>(menus.Length);

        for (int i = 0; i < menus.Length; i++)
        {
            menuDictionary.Add(menus[i].GetType(), menus[i]);
        }
    }

    private void Start()
    {
        OpenMenu();
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
