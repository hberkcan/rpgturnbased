using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu<T> : Menu
{
    protected MenuManager menuManager;

    protected virtual void Awake()
    {
        menuManager = GetComponentInParent<MenuManager>();
    }

    protected void Open()
    {
        menuManager.OpenMenu();
    }

    protected void Close()
    {
        menuManager.CloseMenu();
    }
}

public abstract class Menu : MonoBehaviour
{
}
