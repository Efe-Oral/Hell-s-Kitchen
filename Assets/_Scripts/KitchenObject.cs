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
        // this.clearCounter is the current(previous) parent; clearCounter is the new parent counter to be set

        if (this.clearCounter != null) // if there's a kitchenObject on top of the clearCounter, we clean it first
        {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter; // after clearing the old counter, now we make the "old" counter the new, which is empty for now

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter has already a kitchen object on it!");
        }

        clearCounter.SetKitchenObject(this); // put this kitchen object to the interacted counter

        transform.parent = clearCounter.GetKitchenObjectFollowTransfor();
        transform.localPosition = Vector3.zero; // created kitchen object teleports to its parent counter's top position
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }

}
