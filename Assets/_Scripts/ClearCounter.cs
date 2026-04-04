using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    [SerializeField] private Transform counterTopPoit;


    public void Interact()
    {
        Debug.Log(transform.name);
        GameObject kitchenObject = Instantiate(kitchenObjectsSO.prefab, counterTopPoit);
        kitchenObject.transform.localPosition = Vector3.zero;
    }
}
