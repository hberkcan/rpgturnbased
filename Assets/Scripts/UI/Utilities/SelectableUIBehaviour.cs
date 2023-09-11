using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectableUIBehaviour : MonoBehaviour
{
    private Image image;
    private Outline outline;

    private Vector2 initialScale;
    private float scaleFactor = 1.15f;
    private float scaleDuration = 0.1f;

    private void Awake()
    {
        image = GetComponent<Image>();
        outline = GetComponent<Outline>();
        initialScale = transform.localScale;
    }

    public void OnSelect()
    {
        transform.DOScale(initialScale * scaleFactor, scaleDuration);
        outline.enabled = true;
    }

    public void OnDeselect()
    {
        transform.DOScale(initialScale, scaleDuration);
        outline.enabled = false;
    }

    public void Lock()
    {
        var tempColor = image.color;
        tempColor.a = 0.5f;
        image.color = tempColor;

        image.raycastTarget = false;
    }

    public void Unlock()
    {
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;

        image.raycastTarget = true;
    }
}
