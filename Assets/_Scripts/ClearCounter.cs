using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    [SerializeField] private Transform counterTopPoint;

    [SerializeField] private ClearCounter newParentCounter;

    private KitchenObject kitchenObject;


    private void Update()
    {
        if (kitchenObject != null && Input.GetKeyDown(KeyCode.T) && newParentCounter != null)
        {
            kitchenObject.SetkitchenObjectParent(newParentCounter);

        }

    }

    public void Interact(Player player)
    {
        if (kitchenObject == null) // meaning there is no kitchen object on the counter
        {
            GameObject kitchenObjectTransfor = Instantiate(kitchenObjectsSO.prefab, counterTopPoint);
            kitchenObjectTransfor.GetComponent<KitchenObject>().SetkitchenObjectParent(this);
        }

        else
        {   // if there is already an object on the counter, give it to player's hand
            kitchenObject.SetkitchenObjectParent(player);
        }
    }

    public Transform GetKitchenObjectFollowTransfor()
    {
        return counterTopPoint;
    }


    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

}
