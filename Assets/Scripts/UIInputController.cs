using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable] public class UITogglePauseGameEvent : UnityEvent {}
public class UIInputController : MonoBehaviour
{
    private InputActions _inputActions;
    public UITogglePauseGameEvent uITogglePauseGameEvent;

    private LevelManager _levelManager;

    void Awake() {
        _inputActions = new InputActions();
        _levelManager = transform.GetComponent<LevelManager>();
    }

    private void OnEnable() {
        _inputActions.UI.Enable();
        _inputActions.UI.Pause.performed += TogglePause;
    }

    private void TogglePause(InputAction.CallbackContext context) {
        uITogglePauseGameEvent.Invoke();
    }
}
