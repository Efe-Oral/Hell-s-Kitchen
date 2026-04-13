using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; } // other scripts read data from Player.cs but only player class can write internal data

    public event Action<ClearCounter> OnSelectedCounterChanged;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 2f;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private float kitchenObjectPickupLerpSpeed = 8f;

    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking = false;
    private Vector3 lastInteractionDirection;
    private ClearCounter selectedCounter; // currently selected counter
    private KitchenObject kitchenObject;
    private Vector3 kitchenObjectPickupStartWorld;
    private bool kitchenObjectPickupLerping;
    private bool kitchenObjectPickupLerpResetPending;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player script instance!");
        }

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteraction;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void LateUpdate()
    {
        if (!kitchenObjectPickupLerping || kitchenObject == null)
        {
            return;
        }

        if (kitchenObjectPickupLerpResetPending)
        {
            kitchenObject.transform.position = kitchenObjectPickupStartWorld;
            kitchenObjectPickupLerpResetPending = false;
        }

        kitchenObject.transform.position = Vector3.Lerp(
            kitchenObject.transform.position,
            kitchenObjectHoldPoint.position,
            Time.deltaTime * kitchenObjectPickupLerpSpeed);

        if ((kitchenObject.transform.position - kitchenObjectHoldPoint.position).sqrMagnitude < 0.0004f)
        {
            kitchenObject.transform.localPosition = Vector3.zero;
            kitchenObjectPickupLerping = false;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }


    private void HandleMovement()
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
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + new Vector3(0f, playerHeight, 0f), playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }

            else
            {
                // Cannot move on the X-axis, Let's try to move in Z-axis

                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
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

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractionDirection = moveDir;
        }

        float interactionDistance = 2f;
        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, lastInteractionDirection, out raycastHit, interactionDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter); // this triggers the set counter event for the new clearCounter instance!
                }
            }
            else
            {
                SetSelectedCounter(null);
            }


        }

        else
        {
            SetSelectedCounter(null);
        }
    }

    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void SetSelectedCounter(ClearCounter newCounter)
    {
        selectedCounter = newCounter; // newCounter is the incoming counter from the raycast

        if (OnSelectedCounterChanged != null) // shoter version OnSelectedCounterChanged?.Invoke(counter);
        {
            OnSelectedCounterChanged.Invoke(newCounter);
        }
    }




    public Transform GetKitchenObjectFollowTransfor()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject == null)
        {
            kitchenObjectPickupLerping = false;
            return;
        }

        kitchenObjectPickupStartWorld = kitchenObject.transform.position;
        kitchenObjectPickupLerpResetPending = true;
        kitchenObjectPickupLerping = true;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObjectPickupLerping = false;
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}



















