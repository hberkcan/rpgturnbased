using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Range(2f, 4f)]
    [SerializeField] float longPressDuration = 3f;

    private bool isLongPressed;
    private bool isPointerDown;

    private DateTime pressTime;

    private WaitForSeconds delay;
    private Coroutine timerCoroutine;

    private void Awake()
    {
        delay = new WaitForSeconds(0.1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        pressTime = DateTime.Now;
        timerCoroutine = StartCoroutine(Timer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(timerCoroutine);

        if (!isLongPressed)
        {
            foreach(IClickable clickable in GetComponents<IClickable>())
            {
                clickable.OnClick();
            }
        }

        isPointerDown = false;
        isLongPressed = false;
    }

    private IEnumerator Timer()
    {
        while (isPointerDown && !isLongPressed)
        {
            double elapsedSeconds = (DateTime.Now - pressTime).TotalSeconds;

            if (elapsedSeconds >= longPressDuration)
            {
                isLongPressed = true;

                foreach (IClickable clickable in GetComponents<Clickable>())
                {
                    clickable.OnClick();
                }

                Debug.Log("Long Press");
                yield break;
            }

            yield return delay;
        }
    }
}
