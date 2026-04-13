using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;


    private IKitchenObjectParent kitchenObjectParent;




    public KitchenObjectsSO GetKitchenObjectsSO()
    {
        return kitchenObjectsSO;
    }

    public void SetkitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        // this.kitchenObjectParent is the current(previous) parent; kitchenObjectParent is the new parent counter to be set

        if (this.kitchenObjectParent != null) // if there's a kitchenObject on top of the kitchenObjectParent, we clean it first
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent; // after clearing the old counter, now we make the "old" counter the new, which is empty for now

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter has already a kitchen object on it!");
        }

        kitchenObjectParent.SetKitchenObject(this); // put this kitchen object to the interacted counter

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransfor();
        transform.localPosition = Vector3.zero; // created kitchen object teleports to its parent counter's top position
    }

    public IKitchenObjectParent GetkitchenObjectParent()
    {
        return kitchenObjectParent;
    }

}
