using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualHighlightCounter;


    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        HideVisual();
    }


    private void Update()
    {

    }

    private void Player_OnSelectedCounterChanged(ClearCounter counter)
    {
        if (counter == clearCounter)
        {
            ShowVisual();
        }

        else
        {
            HideVisual();
        }
    }

    private void ShowVisual()
    {
        visualHighlightCounter.SetActive(true);
    }

    private void HideVisual()
    {
        visualHighlightCounter.SetActive(false);
    }

}
