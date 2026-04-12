using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    [SerializeField] private Transform counterTopPoint;

    [SerializeField] private ClearCounter newParentCounter;

    private KitchenObject kitchenObject;


    private void Update()
    {
        if (kitchenObject != null && Input.GetKeyDown(KeyCode.T) && newParentCounter != null)
        {
            kitchenObject.SetClearCounter(newParentCounter);

        }

    }

    public void Interact()
    {
        if (kitchenObject == null)
        {
            GameObject kitchenObjectTransfor = Instantiate(kitchenObjectsSO.prefab, counterTopPoint);
            kitchenObjectTransfor.GetComponent<KitchenObject>().SetClearCounter(this);
        }

        else
        { // if there is already an object on the counter, log on which counter the object is located
            Debug.Log(kitchenObject.GetClearCounter());
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
