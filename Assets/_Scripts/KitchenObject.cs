using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    private ClearCounter clearCounter;




    public KitchenObjectsSO GetKitchenObjectsSO()
    {
        return kitchenObjectsSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        this.clearCounter = clearCounter;

        transform.parent = clearCounter.GetKitchenObjectFollowTransfor();
        transform.localPosition = Vector3.zero; // created kitchen object teleports to its parent counter's top position
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }

}
