using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[Serializable] public class Player1MoveInputEvent : UnityEvent<float> {}
[Serializable] public class Player1JumpEvent : UnityEvent {}

[Serializable] public class Player2MoveInputEvent : UnityEvent<float> {}
[Serializable] public class Player2JumpEvent : UnityEvent {}

public class PlayerInputController : MonoBehaviour
{

    [SerializeField] private InputActions _inputActions;

    public Player1MoveInputEvent player1MoveInputEvent;
    public Player1JumpEvent player1JumpEvent;

    public Player2MoveInputEvent player2MoveInputEvent;
    public Player2JumpEvent player2JumpEvent;

    private void Awake() {
        _inputActions = new InputActions();
    }

    private void OnEnable() {
        EnablePlayer1Actions();
        EnablePlayer2Actions();
    }

    private void EnablePlayer1Actions() {
        _inputActions.Player1.Enable();
        _inputActions.Player1.HorizontalMove.performed += OnPlayerMove(player1MoveInputEvent);
        _inputActions.Player1.HorizontalMove.canceled += OnPlayerMove(player1MoveInputEvent);
        _inputActions.Player1.Jump.performed += OnPlayerJump(player1JumpEvent);
    }

    private void EnablePlayer2Actions() {
        _inputActions.Player2.Enable();
        _inputActions.Player2.HorizontalMove.performed += OnPlayerMove(player2MoveInputEvent);
        _inputActions.Player2.HorizontalMove.canceled += OnPlayerMove(player2MoveInputEvent);
        _inputActions.Player2.Jump.performed += OnPlayerJump(player2JumpEvent);
    }

    private Action<InputAction.CallbackContext> OnPlayerMove(UnityEvent<float> moveEvent) {
        return (context) => { 
            float moveInput = context.ReadValue<float>(); 
            moveEvent.Invoke(moveInput); 
        }; 
    }

    private Action<InputAction.CallbackContext> OnPlayerJump(UnityEvent jumpEvent) {
        return (context) => {
            jumpEvent.Invoke();
        };
    }
}
