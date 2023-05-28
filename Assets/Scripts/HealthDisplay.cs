using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private CharacterHealth characterHealth;
    private Material material;

    private void Awake()
    {
        characterHealth = GetComponentInParent<CharacterHealth>();
        material = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        characterHealth.OnTakeDamage += CharacterHealth_OnTakeDamage;
        characterHealth.OnDie += CharacterHealth_OnDie;
    }

    private void CharacterHealth_OnTakeDamage()
    {
        material.SetFloat("_Health", characterHealth.GetPercentage());
    }

    private void CharacterHealth_OnDie()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        characterHealth.OnTakeDamage -= CharacterHealth_OnTakeDamage;
        characterHealth.OnDie -= CharacterHealth_OnDie;
    }
}
