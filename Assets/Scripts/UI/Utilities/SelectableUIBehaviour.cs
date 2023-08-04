using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectableUIBehaviour : MonoBehaviour
{
    private Vector2 initialScale;
    private float scaleFactor = 1.15f;
    private float scaleDuration = 0.1f;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    public void OnSelect()
    {
        transform.DOScale(initialScale * scaleFactor, scaleDuration);
    }

    public void OnDeselect()
    {
        transform.DOScale(initialScale, scaleDuration);
    }
}
