using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[Serializable]
public class Player1JumpInputEvent : UnityEvent<bool> {}

[Serializable]
public class Player1MoveInputEvent : UnityEvent<float> {}
[Serializable]
public class Player2JumpInputEvent : UnityEvent<bool> {}

[Serializable]
public class Player2MoveInputEvent : UnityEvent<float> {}

[Serializable]
public class PauseInputEvent : UnityEvent<bool> {}

public class InputController : MonoBehaviour
{
    Input controls = null;

    public Player1JumpInputEvent player1JumpInputEvent = null;
    public Player1MoveInputEvent player1MoveInputEvent= null;
    public Player2JumpInputEvent player2JumpInputEvent = null;
    public Player2MoveInputEvent player2MoveInputEvent = null;
    public PauseInputEvent pauseInputEvent = null;

    void Awake() {
        controls = new Input();
    }

    private void OnEnable() {
        controls.Player1.Enable();
        controls.Player1.Jump.performed += ctx => OnJumpPlayer(1, ctx);
        controls.Player1.Move.performed += ctx => OnMovePlayer(1, ctx);
        controls.Player1.Move.canceled += ctx => OnMovePlayer(1, ctx);

        controls.Player2.Enable();
        controls.Player2.Jump.performed += ctx => OnJumpPlayer(2, ctx);
        controls.Player2.Move.performed += ctx => OnMovePlayer(2, ctx);
        controls.Player2.Move.canceled += ctx => OnMovePlayer(2, ctx);

        controls.UI.Enable();
        controls.UI.Pause.performed += OnPause;
    }

    private bool IdentifyPlayer(int player) {
        return player == 1;
    }

    private void OnJumpPlayer(int player, InputAction.CallbackContext context){
        if (player == 1) {
            player1JumpInputEvent.Invoke(true);
        } else {
            player2JumpInputEvent.Invoke(true);
        }
    }

    private void OnMovePlayer(int player, InputAction.CallbackContext context) {
        float direction = context.ReadValue<float>();
        if (player == 1) {
            player1MoveInputEvent.Invoke(direction);
        } else {
            player2MoveInputEvent.Invoke(direction);
        }
    }

    private void OnPause(InputAction.CallbackContext context) {
        pauseInputEvent.Invoke(true);
    }

}
