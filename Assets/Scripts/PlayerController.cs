using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 3f;
    private float speedMultiplier = 1000f;

    private float inputX = 0f;

    private Rigidbody2D _rigidBody2D;
    private Animator _animator;

    void Awake() {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        _rigidBody2D.velocity = new Vector2(inputX * moveSpeed , _rigidBody2D.velocity.y);
    }

    public void HorizontalMove(InputAction.CallbackContext context) {
        inputX = context.ReadValue<float>();
    }
}
