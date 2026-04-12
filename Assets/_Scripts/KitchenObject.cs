using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
        // this.clearCounter is the current parent; clearCounter is the new parent counter to be set

        if (this.clearCounter != null) // if there's a kitchenObject on top of the clearCounter, we clean it first
        {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter;

        transform.parent = clearCounter.GetKitchenObjectFollowTransfor();
        transform.localPosition = Vector3.zero; // created kitchen object teleports to its parent counter's top position
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }

}
