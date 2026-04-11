using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;


    public void Interact()
    {
        if (kitchenObject == null)
        {
            GameObject kitchenObjectTransfor = Instantiate(kitchenObjectsSO.prefab, counterTopPoint);
            kitchenObjectTransfor.transform.localPosition = Vector3.zero;

            kitchenObject = kitchenObjectTransfor.GetComponent<KitchenObject>();
            kitchenObject.SetClearCounter(this);
        }

        else
        { // if there is already an object on the counter, log on which counter the object is located
            Debug.Log(kitchenObject.GetClearCounter());
        }



    }
}
