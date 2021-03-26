using UnityEngine;
using UnityEngine.Events;

public class LevelComplete : MonoBehaviour
{

    public UnityEvent levelCompleteEvent;

    private bool player1InBounds = false;
    private bool player2InBounds = false;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player1") {
            player1InBounds = true;
        } else if (other.gameObject.name == "Player2") {
            player2InBounds = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "Player1") {
            player1InBounds = false;
        } else if (other.gameObject.name == "Player2") {
            player2InBounds = false;
        }
    }

    void Update() {
        if (player1InBounds && player2InBounds) {
            levelCompleteEvent.Invoke();
        }
    }
}
