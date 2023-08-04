using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject persistentObject;

    private void Awake()
    {
        if (persistentObject != null)
        {
            Destroy(gameObject);
            return;
        }

        persistentObject = this;
        DontDestroyOnLoad(gameObject);
    }
}
