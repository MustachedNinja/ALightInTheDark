using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D _rigidBody;
    private float inputX = 0f;

    void Awake() {
        // _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        _rigidBody.velocity = new Vector2(inputX * moveSpeed, _rigidBody.velocity.y);
    }

    public void Move(float input) {
        inputX = input;
    }

    public void Jump() {
        if (IsGrounded()) {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundPoint.position, 0.05f, groundLayer);
    }
}
