using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Player : MonoBehaviour
{




    private void Start()
    {

    }

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        inputVector = inputVector.normalized;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = inputVector.y + 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = inputVector.x - 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = inputVector.y - 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = inputVector.x + 1f;
        }
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        moveDir = moveDir.normalized;

        transform.position = transform.position + moveDir * Time.deltaTime;
    }

}






























