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

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + new Vector3(0f, playerHeight, 0f), playerRadius, moveDir, moveDistance);
        Debug.DrawLine(transform.position + new Vector3(0f, 1f, 0f), transform.position + moveDir + new Vector3(0f, 1f, 0f), Color.green);

        if (!canMove)
        {
            // Attemp moving only on X-axis
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + new Vector3(0f, playerHeight, 0f), playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }

            else
            {
                // Cannot move on the X-axis, Let's try to move in Z-axis

                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z); ;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + new Vector3(0f, playerHeight, 0f), playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }

                else
                {
                    // We can't move anywhere, we are stuck
                }

            }

        }


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



















