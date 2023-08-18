using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    private Material material;

    private void Awake()
    {
        material = rend.material;
    }

    public void UpdateDisplay(float healthRatio)
    {
        material.SetFloat("_Health", healthRatio);
    }

    public void SetActive(bool value)
    {
        rend.gameObject.SetActive(value);
    }
}
