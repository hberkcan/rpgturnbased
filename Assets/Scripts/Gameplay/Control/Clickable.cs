using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
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

    //public UnityEvent Clicked;
    //public UnityEvent LongPressed;
    //public UnityEvent LongPressEnded;

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
            //Clicked?.Invoke();
            foreach (IClick i in GetComponents<IClick>())
                i.OnClick();
        }
        else 
        {
            //LongPressEnded?.Invoke();
            foreach (ILongPressEnd i in GetComponents<ILongPressEnd>())
                i.OnLongPressEnd();
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

                //LongPressed?.Invoke();
                foreach (ILongPress i in GetComponents<ILongPress>())
                    i.OnLongPress();

                yield break;
            }

            yield return delay;
        }
    }
}
