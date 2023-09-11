using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

public class NumberDisplayManager : MonoBehaviour, IMessagingSubscriber<DamageDisplayEvent>
{   
    [Header("Object Pool of Numbers")]
    public ObjectPoolBehaviour objectPool;
    
    [Header("Position Random Offset")]
    public Vector3 positionRandomOffsetRange;

    private void OnEnable()
    {
        MessagingSystem.Instance.Subscribe<DamageDisplayEvent>(this);
    }

    private void OnDisable()
    {
        MessagingSystem.Instance.Unsubscribe<DamageDisplayEvent>(this);
    }

    private void ShowNumber(int numberAmount, Transform numberTransform, Color numberColor)
    {
        GameObject numberObject = objectPool.GetPooledObject();

        Vector3 newPosition = numberTransform.position + RandomOffsetRange(positionRandomOffsetRange);

        numberObject.GetComponent<NumberDisplayBehaviour>().SetupDisplay(numberAmount, newPosition, numberColor);
        numberObject.SetActive(true);
    }

    Vector3 RandomOffsetRange(Vector3 rangeVectors)
    {
        
        return new Vector3( RandomInRange(rangeVectors.x),
                            RandomInRange(rangeVectors.y),
                            RandomInRange(rangeVectors.x)
        );
    }

    float RandomInRange(float rangeValue)
    {
        return Random.Range(-rangeValue, rangeValue);
    }

    public void OnReceiveMessage(DamageDisplayEvent message)
    {
        ShowNumber((int)message.DamageAmount, message.DisplayLocation, message.DamageColor);
    }
}
