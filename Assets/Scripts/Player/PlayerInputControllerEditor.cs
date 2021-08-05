using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerInputController))]
[CanEditMultipleObjects]
public class PlayerInputControllerEditor : Editor
{

    SerializedProperty playerSelect;

    void OnEnable() {
            playerSelect = serializedObject.FindProperty("player");
    }

    public override void OnInspectorGUI()
    {
        // base.OnInspectorGUI();
        serializedObject.Update();
        PlayerInputController script = target as PlayerInputController;

        EditorGUILayout.PropertyField(playerSelect);

        EditorGUILayout.Space();

        if (script.player == PlayerInputController.Player.Player1) {
            // Show Player1Events
            ShowPlayerEvents("player1MoveInputEvent", "player1JumpEvent");
        
        } else if (script.player == PlayerInputController.Player.Player2) {
            // Show Player2Events
            ShowPlayerEvents("player2MoveInputEvent", "player2JumpEvent");
        }

        serializedObject.ApplyModifiedProperties ();

    }

    void ShowPlayerEvents(string moveEvent, string jumpEvent) {
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty(moveEvent), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty(jumpEvent), true);
    }
}
