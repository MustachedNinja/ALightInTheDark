using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelEndZone : MonoBehaviour
{

    [SerializeField] public UnityEvent _levelCompleteEvent;

    private int playerInsideCount = 0;

    private bool isLevelComplete = false;

    void Update() {
        if (playerInsideCount == 2 && !isLevelComplete) {
            isLevelComplete = true;

        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            playerInsideCount++;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            playerInsideCount--;
        }
    }
}
