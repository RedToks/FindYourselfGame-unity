using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool canMove = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        TextAnimator.OnCanvasDestroyed += HandleCanvasDestroyed;
    }

    private void Update()
    {
        if (canMove)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");           

            Vector2 movement = new Vector2(horizontalInput, verticalInput);

            movement.Normalize();

            rb.velocity = movement * speed;

            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void HandleCanvasDestroyed()
    {
        TextAnimator.OnCanvasDestroyed -= HandleCanvasDestroyed;

        canMove = true;
    }

    private void OnDestroy()
    {
        TextAnimator.OnCanvasDestroyed -= HandleCanvasDestroyed;
    }
}
