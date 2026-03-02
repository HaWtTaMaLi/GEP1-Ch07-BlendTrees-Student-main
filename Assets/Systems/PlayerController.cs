using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 moveInput;

    private Rigidbody2D playerRb;

    [SerializeField] private Animator playerAnimController;

    private int MoveInputXHash = Animator.StringToHash("MoveInputX");
    private int MoveInputYHash = Animator.StringToHash("MoveInputY");
    private int isMovingHash = Animator.StringToHash("isMoving");
    private int LastMoveXHash = Animator.StringToHash("LastMoveX");
    private int LastMoveYHash = Animator.StringToHash("LastMoveY");

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null) Debug.LogError("Rigidbody2D component not found on the player object.");

        playerAnimController = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandlePlayerAnimations();
        HandlePlayerAnimDirection();
    }

    public void HandlePlayerAnimations()
    {
        if (moveInput != Vector2.zero)
        {
            playerAnimController.SetFloat(MoveInputXHash, moveInput.x);
            playerAnimController.SetFloat(MoveInputYHash, moveInput.y);

            playerAnimController.SetBool(isMovingHash, true);
        }
        else
        {
            playerAnimController.SetBool(isMovingHash, false);
        }
    }

    public void HandlePlayerAnimDirection()
    {
        if (moveInput != Vector2.zero)
        {
            playerAnimController.SetFloat(LastMoveXHash, moveInput.x);
            playerAnimController.SetFloat(LastMoveYHash, moveInput.y);
        }
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void LateUpdate()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void HandlePlayerMovement()
    {
        playerRb.MovePosition(playerRb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

}
















