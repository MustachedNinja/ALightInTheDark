using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    static HashSet<GameEvent> _listenedEvents = new HashSet<GameEvent>();
    private HashSet<GameEventListener> _gameEventListeners = new HashSet<GameEventListener>();

    public void Register(GameEventListener gameEventListener) {
        _gameEventListeners.Add(gameEventListener);
        _listenedEvents.Add(this);
    }

    public void Deregister(GameEventListener gameEventListener) {
        _gameEventListeners.Remove(gameEventListener);
        if (_gameEventListeners.Count == 0) {
            _listenedEvents.Remove(this);
        }
    }


    [ContextMenu("Invoke")]
    public void Invoke() {
        foreach(GameEventListener listener in _gameEventListeners) {
            listener.RaiseEvent();
        }
    }

    public static void RaiseEvent(string eventName) {
        foreach(GameEvent gameEvent in _listenedEvents) {
            if (gameEvent.name == eventName) {
                gameEvent.Invoke();
            }
        }
    }
}