using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(IPlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private Transform groundCheck;

    private CharacterController controller;
    private IPlayerInput input;
    private Vector3 velocity;
    private Vector3 moveDirection;
    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<IPlayerInput>();
    }

    private void Update()
    {
        CalculateMovement();
        ApplyGravityAndJump();
    }

    private void CalculateMovement()
    {
        // DÜZELTİLEN KISIM: input.KosuyorMu -> input.IsRunning
        float activeSpeed = input.IsRunning ? settings.runSpeed : settings.walkSpeed;

        // DÜZELTİLEN KISIM: input.HareketGirdisi -> input.MovementInput
        moveDirection = transform.right * input.MovementInput.x + transform.forward * input.MovementInput.y;

        if (moveDirection.magnitude > 1f) moveDirection.Normalize();

        controller.Move(moveDirection * (activeSpeed * Time.deltaTime));
    }

    private void ApplyGravityAndJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, settings.groundCheckRadius, settings.groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // DÜZELTİLEN KISIM: input.ZipladiMi -> input.Jumped
        if (input.Jumped && isGrounded)
        {
            velocity.y = Mathf.Sqrt(settings.jumpPower * -2f * settings.gravity);
        }

        velocity.y += settings.gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null && settings != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, settings.groundCheckRadius);
        }
    }
}