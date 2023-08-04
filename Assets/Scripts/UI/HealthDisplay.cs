using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private Material material;
    private UnitHealth unitHealth;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        unitHealth = GetComponentInParent<UnitHealth>();
    }

    public void UpdateDisplay()
    {
        material.SetFloat("_Health", unitHealth.GetPercentage());
    }
}
