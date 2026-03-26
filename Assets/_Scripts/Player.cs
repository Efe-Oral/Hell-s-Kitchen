using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 2f;

    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;



    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        inputVector = gameInput.GetMovementVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        moveDir = moveDir.normalized;

        isWalking = moveDir.magnitude > 0;

        gameObject.transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        gameObject.transform.position = transform.position + moveDir * moveSpeed * Time.deltaTime;
    }

    public bool IsWalking()
    {
        return isWalking;
    }

}



















