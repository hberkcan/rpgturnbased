using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterSelectUIBehaviour : MonoBehaviour
{
    private Image image;

    [SerializeField] private Color targetColor;
    private Color initialColor;

    private Vector2 initialScale;
    private float scaleFactor = 1.15f;
    private float scaleDuration = 0.1f;

    private void Awake()
    {
        image = GetComponent<Image>();
        initialColor = image.color;
        initialScale = transform.localScale;
    }

    private void SetColor(Color color)
    {
        image.color = color;
    }

    public void OnSelected()
    {
        SetColor(targetColor);
        transform.DOScale(initialScale * scaleFactor, scaleDuration);
    }

    public void OnDeselected()
    {
        SetColor(initialColor);
        transform.DOScale(initialScale, scaleDuration);
    }
}
