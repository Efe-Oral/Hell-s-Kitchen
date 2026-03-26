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

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.6f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (canMove)
        {
            gameObject.transform.position = transform.position + moveDir * moveDistance;
        }
        gameObject.transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

}



















