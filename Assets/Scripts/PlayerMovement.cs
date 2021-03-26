using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float jumpForce = 400f;
    [Range(0, 1)] [SerializeField] private float crouchSpeed = 0.36f;
    [Range(0, 0.3f)] [SerializeField] private float movementSmoothing = 0.05f;
    [Range(0, 1)] [SerializeField] private float airMovementSpeed = 1f; // 1 means theres not control reduction in the air, 0 means no movement control in the air
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Collider2D crouchDisableCollider;
    [SerializeField] private float levelLimit = -10f;
    [SerializeField] private LevelManager manager = null;


    const float groundedRadius = 0.1f;
    private bool isGrounded;
    const float ceilingRadius = 0.2f;
    private Rigidbody2D rb2D;
    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;

    private float direction = 0f;
    private bool jump = false;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> {}

    public BoolEvent OnCrouchEvent;
    private bool wasCrouching = false;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null) {
            OnLandEvent = new UnityEvent();
        }

        if (OnCrouchEvent == null) {
            OnCrouchEvent = new BoolEvent();
        }
    }

    private void FixedUpdate() {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                isGrounded = true;
                if (!wasGrounded) {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    private void Update() {
        if (transform.position.y < levelLimit) {
            manager.ReloadLevel();
        }
        Move(direction * movementSpeed * Time.deltaTime, false);
    }

    public void OnMove(float dir) {
        direction = dir;
    }

    public void OnJump() {
        jump = true;
    }

    public void Move(float move, bool crouch) {
        if (!crouch) {
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround)) {
                crouch = true;
            }
        }

        if (isGrounded || airMovementSpeed > 0f) {
            if (crouch) {
                if (!wasCrouching) {
                    wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                move *= crouchSpeed;

                if (crouchDisableCollider != null) {
                    crouchDisableCollider.enabled = false;
                }
            } else if (!isGrounded) {
                move *= airMovementSpeed;
            } else {
                if (crouchDisableCollider != null) {
                    crouchDisableCollider.enabled = true;
                }
                if (wasCrouching) {
                    wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            Vector3 targetVelocity = new Vector2(move * 3000f, rb2D.velocity.y);


            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            if (move > 0 && !facingRight) {
                Flip();
            } else if (move < 0 && facingRight) {
                Flip();
            }
        } if (isGrounded && jump) {
            isGrounded = false;
            rb2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    private void Flip() {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}