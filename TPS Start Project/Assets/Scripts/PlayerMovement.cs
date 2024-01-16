using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController characterController;
    private PlayerInput playerInput;
    private PlayerShooter playerShooter;
    private Animator animator;

    private Camera followCam;

    public float speed = 6f;
    public float jumpVelocity = 20f;
    [Range(0.01f, 1f)] public float airControlPercent;

    public float speedSmoothTime = 0.1f;
    public float turnSmoothTime = 0.1f;

    private float speedSmoothVelocity;
    private float turnSmoothVelocity;

    private float currentVelocityY;

    public float currentSpeed => new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        followCam = Camera.main;
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if(currentSpeed > 0.2f || playerInput.fire)
        {
            Rotate();
        }
        Move(playerInput.moveInput);

        if(playerInput.jump)
        {
            Jump();
        }
    }

    private void Update()
    {
        UpdateAnimation(playerInput.moveInput);
    }

    public void Move(Vector2 moveInput)
    {
        float targetSpped = speed * moveInput.magnitude;
        Vector3 moveDirection = Vector3.Normalize(transform.forward * moveInput.y + transform.right * moveInput.x);
        float smoothTime = characterController.isGrounded ? speedSmoothTime : speedSmoothTime / airControlPercent;

        targetSpped = Mathf.SmoothDamp(currentSpeed, targetSpped, ref speedSmoothVelocity, smoothTime);
        currentVelocityY += Time.deltaTime * Physics.gravity.y;

        Vector3 velocity = moveDirection * targetSpped + Vector3.up * currentVelocityY;

        characterController.Move(velocity * Time.deltaTime);

        if(characterController.isGrounded)
        {
            currentVelocityY = 0;
        }
    }

    public void Rotate()
    {
        float targetRotation = followCam.transform.eulerAngles.y;

        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
    }

    public void Jump()
    {
        if(!characterController.isGrounded)
        {
            return;
        }

        currentVelocityY = jumpVelocity;
    }

    private void UpdateAnimation(Vector2 moveInput)
    {
        float animationSpeedPercent = currentSpeed / speed;

        animator.SetFloat("Horizontal Move", moveInput.x * animationSpeedPercent, 0.05f, Time.deltaTime);
        animator.SetFloat("Vertical Move", moveInput.y * animationSpeedPercent, 0.05f, Time.deltaTime);
    }

    //private CharacterController characterController;
    //private PlayerInput playerInput;
    //private Animator animator;

    //private Camera followCam;

    //public float speed = 6f;
    //public float jumpVelocity = 20f;
    //[Range(0.01f, 1f)] public float airControlPercent;

    //public float speedSmoothTime = 0.1f;
    //public float turnSmoothTime = 0.1f;

    //private float speedSmoothVelocity;
    //private float turnSmoothVelocity;

    //private float currentVelocityY;

    //public float currentSpeed =>
    //    new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

    //private void Start()
    //{

    //}

    //private void FixedUpdate()
    //{
    //    if (currentSpeed > 0.2f || playerInput.fire) Rotate();

    //    Move(playerInput.moveInput);

    //    if (playerInput.jump) Jump();
    //}

    //private void Update()
    //{

    //}

    //public void Move(Vector2 moveInput)
    //{

    //}

    //public void Rotate()
    //{

    //}

    //public void Jump()
    //{

    //}

    //private void UpdateAnimation(Vector2 moveInput)
    //{

    //}
}